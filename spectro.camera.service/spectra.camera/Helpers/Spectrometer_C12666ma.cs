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
using System.Collections.Generic;

namespace spectra.camera.Helpers
{
    public class SPPSensorBase:IDisposable
    {
        protected DeviceInformation _deviceInfo;
        StreamSocket socket; 
        RfcommDeviceService serviceRfcomm;
        DataReader reader;
        DataWriter writer;
        CancellationTokenSource cancelSource = new CancellationTokenSource();
        CancellationToken cancel;
        List<Task> tasks;

        public class BLEDataClientNotficationEventArgs : EventArgs
        {
            public string Data { get; set; }

            public BLEDataClientNotficationEventArgs(string data)
            {
              
                Data = data;
            }

        }

        public class BLEConnectionStatusEventArgs : EventArgs
        {
            public BluetoothConnectionStatus ConnectionStatus { get; set; }

            public BLEConnectionStatusEventArgs(BluetoothConnectionStatus connectionStatus)
            {
                ConnectionStatus = connectionStatus;
            }

        }

        public delegate void BLEDataClientNotficationEventHandle(object sender, BLEDataClientNotficationEventArgs e);

        public event BLEDataClientNotficationEventHandle DataNotification;


        public delegate void BLEConnectionStatusEventHandle(object sender, BLEConnectionStatusEventArgs e);

        public event BLEConnectionStatusEventHandle BLEConnectionStatusChanged;

        private void OnDataNotification(string data)
        {
            if (DataNotification != null)
               DataNotification(this, new BLEDataClientNotficationEventArgs(data));
        }

        private async Task OnBLEConnectionStatusChanged(BluetoothConnectionStatus connectionStatus)
        {
            if (BLEConnectionStatusChanged != null)
                await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, ()=>BLEConnectionStatusChanged(this, new BLEConnectionStatusEventArgs(connectionStatus)));
        }

        public SPPSensorBase()
        {
            ConnectionStatus = BluetoothConnectionStatus.Disconnected;
            cancel = cancelSource.Token;
            tasks = new List<Task>();
        }

        public virtual async Task ConnectService()
        {
            await Connect();
        }

        public virtual async Task DisconnectService()
        {
            await CloseConnection();
        }
        
        private void device_ConnectionStatusChanged(BluetoothLEDevice sender, object args)
        {
            ConnectionStatus = sender.ConnectionStatus;
            OnBLEConnectionStatusChanged(ConnectionStatus);
        }

        public BluetoothConnectionStatus ConnectionStatus { get; private set; } 

        public string DeviceName { get; protected set; }
        public DeviceInformation DeviceInfo
        {
            get { return _deviceInfo; }
        }
        public Guid CharacteristicDataUUID { get; set; }

        public bool Pause { get; set; }


        protected async Task WriteInt(int value)
        {
            try
            {
                writer.WriteInt32(value);
                await writer.StoreAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                throw ex;
            }
        }

        protected async Task WriteString(string value)
        {
            try
            {
                writer.WriteString(value);
                await writer.StoreAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                throw ex;
            }
        }

        public async Task<bool> Connect()
        {

            try
            {
                Guid uuid = new Guid("00001101-0000-1000-8000-00805f9b34fb");

                string deviceFilter = RfcommDeviceService.GetDeviceSelector(RfcommServiceId.FromUuid(uuid));
                var devices = await DeviceInformation.FindAllAsync(deviceFilter);
                var selectedDevice = devices.FirstOrDefault(d => d.Name.Contains(DeviceName));
                serviceRfcomm = await RfcommDeviceService.FromIdAsync(selectedDevice.Id);
                serviceRfcomm.Device.ConnectionStatusChanged += Device_ConnectionStatusChanged;

                socket = new StreamSocket();
                await socket.ConnectAsync(
                 serviceRfcomm.ConnectionHostName,
                 serviceRfcomm.ConnectionServiceName,
                 SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication); 
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0}\r\n{1}",ex.Message,ex.StackTrace));
                return false;
            }

        }

        bool _Connected = false;
        private async void Device_ConnectionStatusChanged(BluetoothDevice sender, object args)
        {
            if (serviceRfcomm == null) return;
            if (serviceRfcomm.Device != null)
            {
                await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    if (serviceRfcomm == null) return;
                    if (serviceRfcomm.Device == null)
                    {
                        _Connected = false;
                        ConnectionStatus = BluetoothConnectionStatus.Disconnected;
                        OnBLEConnectionStatusChanged(ConnectionStatus);

                    }
                    else
                    {
                        ConnectionStatus = serviceRfcomm.Device.ConnectionStatus;
                        if (ConnectionStatus == BluetoothConnectionStatus.Connected)
                        {
                            _Connected = true;
                            OnConnectionEstablished();
                            OnBLEConnectionStatusChanged(ConnectionStatus);
                        }
                        else
                        {
                            _Connected = false;
                            if (cancel.CanBeCanceled)
                            {
                                cancelSource.Cancel();
                            }
                            CloseConnection();
                            OnBLEConnectionStatusChanged(ConnectionStatus);
                        }
                    }
                });

            }

        }


        public async Task CloseConnection()
        {
            try
            {
                if (reader != null)
                {
                    reader.DetachStream();
                    reader.Dispose();
                    reader = null;
                }
                if (writer != null)
                {
                    writer.DetachStream();
                    writer.Dispose();
                    writer = null;
                }
                if (serviceRfcomm != null)
                {
                    if (serviceRfcomm.Device != null)
                    {
                        await OnBLEConnectionStatusChanged(BluetoothConnectionStatus.Disconnected);
                        serviceRfcomm.Device.ConnectionStatusChanged -= Device_ConnectionStatusChanged;
                        serviceRfcomm.Device.Dispose();
                    }
                    serviceRfcomm.Dispose();
                    serviceRfcomm = null;
                }
                if (socket != null)
                {
                    //await socket.CancelIOAsync();
                    socket.Dispose();
                    socket = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
                //do thing
            }
        }


        private void OnConnectionEstablished()
        {
            try
            {
                if (cancel.IsCancellationRequested)
                {
                    cancelSource = new CancellationTokenSource();
                    cancel = cancelSource.Token;
                }
                if(tasks.All(t=>t.IsCompleted) | tasks.Count == 0)
                    tasks.Add(Task.Run(() => loop(), cancel));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
                throw (ex);
            }
        }

        string data = "";
        Timer dataRate;
        protected virtual async Task loop()
        {

            System.Diagnostics.Debug.WriteLine("Loop started");
            string measurement = "";
            try
            {

                writer = new DataWriter(socket.OutputStream);
                reader = new DataReader(socket.InputStream);
                dataRate = new Timer((o) =>
                {
                    OnDataNotification(data);
                    data = "";
                }, null, 0, 1000);
                reader.InputStreamOptions = InputStreamOptions.Partial;
                while (_Connected)
                {
                    try
                    {
                        if (cancel.IsCancellationRequested) break;
                        if (Pause) continue;

                        if (reader == null) break;

                        var taskLoad = reader.LoadAsync(64);
                        taskLoad.AsTask().Wait();
                        //await reader.LoadAsync(1294);
                        taskLoad.GetResults();

                        if (reader.UnconsumedBufferLength > 0)
                        {

                            string n = reader.ReadString(reader.UnconsumedBufferLength);
                            if (n.Contains("w") | n.Contains("m")) System.Diagnostics.Debug.WriteLine(n);
                            measurement += n;
                            if (measurement.IndexOf('\n') < 0)
                                continue;
                            while (measurement.IndexOf('\n') > -1)
                            {
                                string m = measurement.Substring(0, measurement.IndexOf('\n')).Trim();
                                measurement = measurement.Substring(measurement.IndexOf('\n')).TrimStart();
                                if (!string.IsNullOrWhiteSpace(m))
                                {
                                    if (m.StartsWith("s")) data = m; //send through data at a fixed rate;
                                    else OnDataNotification(m); //send through messages and other notifications immidiatly.
                                }
                            }
                        }

                        await writer.StoreAsync();

                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }


                System.Diagnostics.Debug.WriteLine("Loop finished");
                dataRate.Dispose();
                await DisconnectService();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
                throw ex;
            }
            finally
            {
                dataRate.Dispose();
                await OnBLEConnectionStatusChanged(BluetoothConnectionStatus.Disconnected);
                await DisconnectService();
            }
        }

 
        public async void Dispose()
        {
            await DisconnectService();
        }



    }

    public class c12880ma:SPPSensorBase
    {
        public string SerialNumber { get; private set; }

        public c12880ma()
        {
            _IntegrationTime = 6;
            DeviceName = "RNI-SPP";
            base.BLEConnectionStatusChanged += C12880ma_BLEConnectionStatusChanged;
            base.DataNotification += C12880ma_DataNotification;
        }

        private void C12880ma_DataNotification(object sender, BLEDataClientNotficationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data)) return;
            if (e.Data.StartsWith("w"))
            {
                SerialNumber = e.Data.Substring(1);
                ssTimer.Dispose();
                ssTimer = null;
            }
        }
        System.Threading.Timer ssTimer;
        private void C12880ma_BLEConnectionStatusChanged(object sender, BLEConnectionStatusEventArgs e)
        {
            if (e.ConnectionStatus == BluetoothConnectionStatus.Connected)
            {
                ssTimer = new System.Threading.Timer(async (o) =>
                {
                    if (string.IsNullOrWhiteSpace(SerialNumber))
                    {
                        await getSerialNumber();
                    }
                    else
                    {
                        if (ssTimer != null)
                        {
                            ssTimer.Dispose();
                            ssTimer = null;
                        }
                    }
                } , null, 1000, 1000);
            } else
            {
                if (ssTimer != null) ssTimer.Dispose();
            }
        }


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
                         WriteString(string.Format("i{0}",_IntegrationTime)); 
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
                        throw ex;
                    }
                }
            }
        }

        async Task getSerialNumber()
        {
            await WriteString(string.Format("w"));
        }

    }


    //public class Spectrometer_C12880ma : INotifyPropertyChanged, IDisposable
    //{
    //   // TaskScheduler context;
    //    StreamSocket socket;
    //    DeviceInformation bluetoothDeviceHc06;
    //    RfcommDeviceService serviceRfcomm;
    //    DataReader reader;
    //    CancellationTokenSource cancelSource = new CancellationTokenSource();
    //    CancellationToken cancel;
    //    public Spectrometer_C12880ma()
    //    {
    //        Spectra = new ObservableCollection<Int16>();
    //        //context = TaskScheduler.FromCurrentSynchronizationContext();
    //        Status = BluetoothConnectionStatus.Disconnected;
    //        IntegrationTime = 0;
    //        cancel = cancelSource.Token; 

    //    }


    //    BluetoothConnectionStatus _Status;
    //    public BluetoothConnectionStatus Status
    //    {
    //        get { return _Status; }
    //        set
    //        {
    //            if (value != _Status)
    //            {
    //                _Status = value;
    //                RaisePropertyChanged("Status");
    //            }
    //        }
    //    }

    //    string _StatusMessage;
    //    public string StatusMessage
    //    {
    //        get { return _StatusMessage; }
    //        set
    //        {
    //            if (value != _StatusMessage)
    //            {
    //                _StatusMessage = value;
    //                RaisePropertyChanged("StatusMessage");
    //            }
    //        }
    //    }

    //    public ObservableCollection<Int16> Spectra { get; set; }

    //    int _IntegrationTime;
    //    public int IntegrationTime
    //    {
    //        get { return _IntegrationTime; }
    //        set
    //        {
    //            if (value != _IntegrationTime)
    //            {
    //                try
    //                {
    //                    _IntegrationTime = value;

    //                  //  WriteSetting(_IntegrationTime); 
    //                }
    //                catch(Exception ex)
    //                {
    //                    StatusMessage = ex.Message;
    //                }
    //            }
    //        }
    //    }


    //    async Task WriteSetting(int value)
    //    {
    //        try
    //        {
    //            writer.WriteInt32(value);
    //            await writer.StoreAsync();
    //        }
    //        catch(Exception ex)
    //        {
    //            StatusMessage = ex.Message;
    //        }
    //    }
         
    //    bool _paused;
    //    public bool Pause
    //    {
    //        get { return _paused; }
    //        set
    //        {
    //            if (value != _paused)
    //            {
    //                _paused = value;
    //                //if (!value)
    //                   // Task.Run(() => loop());
    //            }
                
    //        }
    //    } 
        
 

    //    #region Connect bluetooth
    //    public ICommand ConnectCommand
    //    {
    //        get { return new AsyncRelayCommand(Connect); }
    //    }

    //    DataWriter writer;
    //    RfcommServiceProvider _provider;

    //    private RfcommDeviceService _service;

    //    public async Task<bool> Connect()
    //    {

    //        try
    //        {
    //            Guid uuid = new Guid("00001101-0000-1000-8000-00805f9b34fb");

    //            string deviceFilter = RfcommDeviceService.GetDeviceSelector(RfcommServiceId.FromUuid(uuid));
    //            var devices = await DeviceInformation.FindAllAsync(deviceFilter);
    //            var selectedDevice = devices.FirstOrDefault(d => d.Name.Contains("RNI-SPP"));  
    //            serviceRfcomm = await RfcommDeviceService.FromIdAsync(selectedDevice.Id);
    //            serviceRfcomm.Device.ConnectionStatusChanged += Device_ConnectionStatusChanged;

    //            socket = new StreamSocket();
    //            await socket.ConnectAsync(
    //             serviceRfcomm.ConnectionHostName,
    //             serviceRfcomm.ConnectionServiceName,
    //             SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication);

                
    //            StatusMessage = "Initializing sensor...";
    //            RaisePropertyChanged("Connected");
    //            RaisePropertyChanged("");
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            var token = Task.Factory.CancellationToken;
    //            await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => StatusMessage = ex.Message); //, token, TaskCreationOptions.None, context);
    //            return false;
    //        }
            
    //    }

    //    private void Listener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    bool _Connected = false;
    //    private async void Device_ConnectionStatusChanged(BluetoothDevice sender, object args)
    //    {
    //        if (serviceRfcomm.Device != null)
    //        {
    //            await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
    //             {
    //                 Status = serviceRfcomm.Device.ConnectionStatus;
    //                 if (Status == BluetoothConnectionStatus.Connected)
    //                 {
    //                     _Connected = true;
    //                     OnConnectionEstablished();
    //                 }
    //                 else
    //                 {
    //                     _Connected = false;
    //                     if (cancel.CanBeCanceled)
    //                     {
    //                         cancelSource.Cancel();
    //                     }
    //                     CloseConnection();
    //                 }
    //             });

    //        }

    //    }

    //    public async Task CloseConnection()
    //    {
    //        try
    //        {
    //            if (reader != null) reader.Dispose();
    //            reader = null;
    //            if (writer != null) writer.Dispose();
    //            writer = null;
    //           // serviceRfcomm.Device.Dispose();
    //            if (serviceRfcomm != null) serviceRfcomm.Dispose();
    //            serviceRfcomm = null;
    //            if (socket != null)
    //            {
    //                await socket.CancelIOAsync();
    //                socket.Dispose();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //do thing
    //        }
    //    }
      

    //    private void OnConnectionEstablished()
    //    {
    //        try
    //        {
    //            //enable the buttons on the UI thread!  
    //            StatusMessage = "Initializing sensor...";
    //            RaisePropertyChanged("Connected");
    //            RaisePropertyChanged("");
    //            //timer.Start();
    //            Task.Run(()=>loop(),cancel);

    //        }
    //        catch (Exception ex)
    //        {
    //            throw (ex);
    //        }
    //    }

    //    #endregion
         

    //    public class MeasurmentEventArgs:EventArgs
    //    {
    //        public string Spectra { get; set; } 

    //        public MeasurmentEventArgs(string spectra)
    //        {
    //            Spectra = spectra;
    //        }
    //    }

    //    public delegate void MeasurmentChangedEventHandler(object sender, MeasurmentEventArgs e);

    //    public event MeasurmentChangedEventHandler MeasurmentChanged;

    //    void OnMeasurmentChanged(string spectra)
    //    {
    //        if (MeasurmentChanged != null)
    //            MeasurmentChanged(this, new MeasurmentEventArgs(spectra));
    //    }
          
    //    string measurement = ""; 
    //    DispatcherTimer timer = new DispatcherTimer();
         
    //    private async Task loop()
    //    {
    //        string measurement = "";
    //        try
    //        {

    //            writer = new DataWriter(socket.OutputStream);
    //            reader = new DataReader(socket.InputStream);
    //            reader.InputStreamOptions = InputStreamOptions.Partial;
    //            while (_Connected)
    //            {
    //                try
    //                {
    //                    if (cancel.IsCancellationRequested) break;
    //                    if (Pause) return;

    //                    if (reader == null) break;

    //                    var taskLoad = await reader.LoadAsync(64);
    //                   // taskLoad.AsTask().Wait();
    //                    //await reader.LoadAsync(1294);

    //                    if (reader.UnconsumedBufferLength > 0)
    //                    {
    //                        measurement += reader.ReadString(reader.UnconsumedBufferLength);

    //                        if (measurement.IndexOf('\n') < 0)
    //                            continue;

    //                        string m = measurement.Substring(0, measurement.IndexOf('\n'));
    //                        measurement = measurement.Substring(measurement.IndexOf('\n')).Trim();
    //                        if (!string.IsNullOrWhiteSpace(m))
    //                        {
    //                            OnMeasurmentChanged(m);
    //                            //var context = TaskScheduler.FromCurrentSynchronizationContext();
    //                            var token = Task.Factory.CancellationToken;
    //                            await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => processMeasurement(m)); //, token, TaskCreationOptions.None, context);
    //                        }
    //                    }

    //                    writer.WriteString(string.Format("{i0}\n", _IntegrationTime));
    //                    await writer.StoreAsync();

    //                }
    //                catch (Exception ex)
    //                {
                        
    //                    var token = Task.Factory.CancellationToken;
    //                    await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => StatusMessage = ex.Message); //, token); //, TaskCreationOptions.None, context);
    //                    if (ex.Message.StartsWith("Object has been closed")) break;
                        
    //                }
    //            }


    //            //while (true)
    //            //{
    //            //    if (Pause)
    //            //        return;
                     
    //            //    measurement = "";
    //            //    char v = '\n';
    //            //    do
    //            //    {
    //            //        if (bluetooth.available() > 0)
    //            //        {
    //            //            v = (char)bluetooth.read();
    //            //            measurement += v;
    //            //        }
    //            //    } while (v != '\n');


    //            //    if (!string.IsNullOrWhiteSpace(measurement))
    //            //    {
    //            //        OnMeasurmentChanged(measurement);
    //            //        //var context = TaskScheduler.FromCurrentSynchronizationContext();
    //            //        var token = Task.Factory.CancellationToken;
    //            //        await Task.Factory.StartNew(() => processMeasurement(measurement), token, TaskCreationOptions.None, context);
    //            //    }
    //            //}

    //        }
    //        catch (Exception ex)
    //        {
    //            var token = Task.Factory.CancellationToken;
    //            await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => StatusMessage = ex.Message); //, token, TaskCreationOptions.None, context);
               
    //        }
    //    }




    //    public int intTime { get; private set; }
    //    public bool gain { get; private set; }
    //    private void processMeasurement(string measurement)
    //    {
    //        try
    //        {
    //            var data = measurement.Substring(1).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

    //            if (data.First().StartsWith("Settings"))
    //                return;
    //            intTime = int.Parse(data.Last());
                

    //            if(_IntegrationTime == 0)
    //            { 
    //                _IntegrationTime = intTime;
    //            }
                 

    //            if (data.Length == 2)
    //            {
    //                if (intTime == 6)
    //                    StatusMessage = "Sensor is Saturated";
    //                else
    //                    StatusMessage = "Calibrating sensor...";
    //            }
    //            else
    //            {
    //                if (data.Length != 289)
    //                    throw new Exception(string.Format("Spectral data does not contains 288 bands, {0} bands found",data.Length));
    //                StatusMessage = "Processing Measurement..."; 
    //                Spectra = new ObservableCollection<Int16>(data.Skip(2).Select(d => Int16.Parse(d)));
    //                //error skip(1) instead of (2)
    //                RaisePropertyChanged("Spectra");
    //            }
                
    //            RaisePropertyChanged("IntegrationTime");
    //            RaisePropertyChanged("intTime");
    //            //RaisePropertyChanged("Status");
    //            // timer.Start();

    //        }
    //        catch (Exception ex)
    //        {
    //            var token = Task.Factory.CancellationToken;
    //            App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => StatusMessage = ex.Message); //, token, TaskCreationOptions.None, context);
               
    //        }
    //    }

         
    //    public void Reset()
    //    {
    //        //TODO
    //        //bluetooth.Dispose();
    //        //Status = "Disconnected";
    //        //RaisePropertyChanged("");
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    private void RaisePropertyChanged(string propertyName)
    //    {
    //        if (PropertyChanged != null)
    //            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    //    }

    //    public void Dispose()
    //    {
    //        CloseConnection();
    //        cancelSource.Dispose();
    //    }
    //}
}
