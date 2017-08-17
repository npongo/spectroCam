using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth;
using System.Text;
using Windows.Foundation;
using spectra.camera.Helpers;
using System.Threading;

namespace spectra.camera.Helpers
{
    public class rtk_gps : INotifyPropertyChanged, IDisposable
    {
       // TaskScheduler context;
        StreamSocket socket;
        DeviceInformation bluetoothDeviceHc06;
        RfcommDeviceService serviceRfcomm;
        DataReader reader;
        CancellationTokenSource cancelSource = new CancellationTokenSource();
        CancellationToken cancel;
        public rtk_gps()
        {
            //context = TaskScheduler.FromCurrentSynchronizationContext();
            Status = BluetoothConnectionStatus.Disconnected;
            IntegrationTime = 0;
            cancel = cancelSource.Token; 

        }


        BluetoothConnectionStatus _Status;
        public BluetoothConnectionStatus Status
        {
            get { return _Status; }
            set
            {
                if (value != _Status)
                {
                    _Status = value;
                    RaisePropertyChanged("Status");
                }
            }
        }

        string _StatusMessage;
        public string StatusMessage
        {
            get { return _StatusMessage; }
            set
            {
                if (value != _StatusMessage)
                {
                    _StatusMessage = value;
                    RaisePropertyChanged("StatusMessage");
                }
            }
        }

        public ObservableCollection<Int16> Spectra { get; set; }

        int _IntegrationTime;
        public int IntegrationTime
        {
            get { return _IntegrationTime; }
            set
            {
                if (value != _IntegrationTime)
                {
                    try
                    {
                        _IntegrationTime = value;

                      //  WriteSetting(_IntegrationTime); 
                    }
                    catch(Exception ex)
                    {
                        StatusMessage = ex.Message;
                    }
                }
            }
        }


        async Task WriteSetting(int value)
        {
            try
            {
                writer.WriteInt32(value);
                await writer.StoreAsync();
            }
            catch(Exception ex)
            {
                StatusMessage = ex.Message;
            }
        }
         
        bool _paused;
        public bool Pause
        {
            get { return _paused; }
            set
            {
                if (value != _paused)
                {
                    _paused = value;
                    if (!value)
                        Task.Run(() => loop());
                }
                
            }
        } 
        
 

        #region Connect bluetooth
        public ICommand ConnectCommand
        {
            get { return new AsyncRelayCommand(Connect); }
        }

        DataWriter writer;
        RfcommServiceProvider _provider;

        private RfcommDeviceService _service;

        public async Task<bool> Connect()
        {

            try
            {
                try
                {
                    var devices =
                          await DeviceInformation.FindAllAsync(
                            RfcommDeviceService.GetDeviceSelector(
                              RfcommServiceId.SerialPort));

                    var device = devices.Single(x => x.Name.Contains("A20B15"));

                    _service = await RfcommDeviceService.FromIdAsync(
                                                            device.Id);

                    socket = new StreamSocket();

                    await socket.ConnectAsync(
                          _service.ConnectionHostName,
                          _service.ConnectionServiceName,
                          SocketProtectionLevel.
                          BluetoothEncryptionAllowNullAuthentication);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                _provider = await RfcommServiceProvider.CreateAsync(RfcommServiceId.SerialPort);

                // Create a listener for this service and start listening
                StreamSocketListener listener = new StreamSocketListener();
                listener.ConnectionReceived += Listener_ConnectionReceived; ;
                await listener.BindServiceNameAsync(
                    _provider.ServiceId.AsString(),
                    SocketProtectionLevel
                        .BluetoothEncryptionAllowNullAuthentication);
                 

                //var devices = await BluetoothSerial.listAvailableDevicesAsync();
                //var sm = devices.FirstOrDefault(d => d.Name == "c12666ma" || d.Name == "RNBT - E12B" || d.Name == "RNI-SPP" || d.Name == "CC41-A" || d.Name.StartsWith("RNBT"));

                await CloseConnection();
                var bluetoothDevicesSpp = await DeviceInformation.FindAllAsync(); // RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));
                var sim800 = bluetoothDevicesSpp.Where(d => d.Name.Contains("SIM800"));
                foreach (var item in sim800)
                {
                    System.Diagnostics.Debug.WriteLine("{0}: {1}", new object[] { item.Name, item.Id });
                }
                bluetoothDeviceHc06 = bluetoothDevicesSpp.LastOrDefault();
                serviceRfcomm = await RfcommDeviceService.FromIdAsync(bluetoothDeviceHc06.Id);
                serviceRfcomm.Device.ConnectionStatusChanged += Device_ConnectionStatusChanged;
               
               socket = new StreamSocket();
                await socket.ConnectAsync(serviceRfcomm.ConnectionHostName, serviceRfcomm.ConnectionServiceName, SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication);
                

                StatusMessage = "Initializing sensor...";
                RaisePropertyChanged("Connected");
                RaisePropertyChanged("");
                return true;
                //timer.Start();
               // Task.Run(() => loop());
            }
            catch(Exception ex)
            { 
                var token = Task.Factory.CancellationToken;
                await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => StatusMessage = ex.Message); //, token, TaskCreationOptions.None, context);
             return false;
            }
            
        }

        private void Listener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            throw new NotImplementedException();
        }

        private async void Device_ConnectionStatusChanged(BluetoothDevice sender, object args)
        {
            if (serviceRfcomm.Device != null)
            {
                await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                 {
                     Status = serviceRfcomm.Device.ConnectionStatus;
                     if (Status == BluetoothConnectionStatus.Connected)
                     {
                         OnConnectionEstablished();
                     }
                     else
                     {
                         if (cancel.CanBeCanceled)
                         {
                             cancelSource.Cancel();
                         }
                         CloseConnection();
                     }
                 });

            }

        }

        public async Task CloseConnection()
        {
            try
            {
                if (reader != null) reader.Dispose();
                reader = null;
                if (writer != null) writer.Dispose();
                writer = null;
               // serviceRfcomm.Device.Dispose();
                if (serviceRfcomm != null) serviceRfcomm.Dispose();
                serviceRfcomm = null;
                if (socket != null)
                {
                    await socket.CancelIOAsync();
                    socket.Dispose();
                }
            }
            catch (Exception ex)
            {
                //do thing
            }
        }
      

        private void OnConnectionEstablished()
        {
            try
            {
                //enable the buttons on the UI thread!  
                StatusMessage = "Initializing sensor...";
                RaisePropertyChanged("Connected");
                RaisePropertyChanged("");
                //timer.Start();
                Task.Run(()=>loop(),cancel);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion
         

        public class MeasurmentEventArgs:EventArgs
        {
            public string Spectra { get; set; } 

            public MeasurmentEventArgs(string spectra)
            {
                Spectra = spectra;
            }
        }

        public delegate void MeasurmentChangedEventHandler(object sender, MeasurmentEventArgs e);

        public event MeasurmentChangedEventHandler MeasurmentChanged;

        void OnMeasurmentChanged(string spectra)
        {
            if (MeasurmentChanged != null)
                MeasurmentChanged(this, new MeasurmentEventArgs(spectra));
        }
          
        string measurement = ""; 
        DispatcherTimer timer = new DispatcherTimer();
         
        private async Task loop()
        {
            string measurement = "";
            try
            {

                writer = new DataWriter(socket.OutputStream);
                reader = new DataReader(socket.InputStream);
                reader.InputStreamOptions = InputStreamOptions.Partial;
                while (true)
                {
                    try
                    {
                        if (cancel.IsCancellationRequested) break;
                        if (Pause) return;

                        if (reader == null) break;

                        IAsyncOperation<uint> taskLoad = reader.LoadAsync(1294);
                        taskLoad.AsTask().Wait();
                        var bytesRead = taskLoad.GetResults();

                        //await reader.LoadAsync(1294);

                        if (reader.UnconsumedBufferLength > 0)
                        {
                            measurement += reader.ReadString(reader.UnconsumedBufferLength);

                            if (measurement.IndexOf('\n') < 0)
                                continue;

                            string m = measurement.Substring(0, measurement.IndexOf('\n'));
                            measurement = measurement.Substring(measurement.IndexOf('\n')).Trim();
                            if (!string.IsNullOrWhiteSpace(m))
                            {
                                OnMeasurmentChanged(m);
                                //var context = TaskScheduler.FromCurrentSynchronizationContext();
                                var token = Task.Factory.CancellationToken;
                                await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => processMeasurement(m)); //, token, TaskCreationOptions.None, context);
                            }
                        }

                        writer.WriteString(string.Format("{i0}\n", _IntegrationTime));
                        await writer.StoreAsync();

                    }
                    catch (Exception ex)
                    {
                        
                        var token = Task.Factory.CancellationToken;
                        await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => StatusMessage = ex.Message); //, token); //, TaskCreationOptions.None, context);
                        if (ex.Message.StartsWith("Object has been closed")) break;
                        
                    }
                }


                //while (true)
                //{
                //    if (Pause)
                //        return;
                     
                //    measurement = "";
                //    char v = '\n';
                //    do
                //    {
                //        if (bluetooth.available() > 0)
                //        {
                //            v = (char)bluetooth.read();
                //            measurement += v;
                //        }
                //    } while (v != '\n');


                //    if (!string.IsNullOrWhiteSpace(measurement))
                //    {
                //        OnMeasurmentChanged(measurement);
                //        //var context = TaskScheduler.FromCurrentSynchronizationContext();
                //        var token = Task.Factory.CancellationToken;
                //        await Task.Factory.StartNew(() => processMeasurement(measurement), token, TaskCreationOptions.None, context);
                //    }
                //}

            }
            catch (Exception ex)
            {
                var token = Task.Factory.CancellationToken;
                await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => StatusMessage = ex.Message); //, token, TaskCreationOptions.None, context);
               
            }
        }




        public int intTime { get; private set; }
        public bool gain { get; private set; }
        private void processMeasurement(string measurement)
        {
            try
            {
                var data = measurement.Substring(1).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (data.First().StartsWith("Settings"))
                    return;
                intTime = int.Parse(data.Last());
                

                if(_IntegrationTime == 0)
                { 
                    _IntegrationTime = intTime;
                }
                 

                if (data.Length == 2)
                {
                    if (intTime == 6)
                        StatusMessage = "Sensor is Saturated";
                    else
                        StatusMessage = "Calibrating sensor...";
                }
                else
                {
                    if (data.Length != 289)
                        throw new Exception(string.Format("Spectral data does not contains 288 bands, {0} bands found",data.Length));
                    StatusMessage = "Processing Measurement..."; 
                    Spectra = new ObservableCollection<Int16>(data.Skip(2).Select(d => Int16.Parse(d)));
                    //error skip(1) instead of (2)
                    RaisePropertyChanged("Spectra");
                }
                
                RaisePropertyChanged("IntegrationTime");
                RaisePropertyChanged("intTime");
                //RaisePropertyChanged("Status");
                // timer.Start();

            }
            catch (Exception ex)
            {
                var token = Task.Factory.CancellationToken;
                App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => StatusMessage = ex.Message); //, token, TaskCreationOptions.None, context);
               
            }
        }

         
        public void Reset()
        {
            //TODO
            //bluetooth.Dispose();
            //Status = "Disconnected";
            //RaisePropertyChanged("");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            CloseConnection();
            cancelSource.Dispose();
        }
    }
}
