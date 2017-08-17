using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using spectra.camera.Helpers;
using spectra.camera.Models;
using Windows.Devices.Geolocation;
using Prism.Windows.Validation;
using Windows.UI.Popups;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Reflection;
using Windows.UI;

namespace spectra.camera.ViewModels
{
    public class SpectrometerViewModel : DataViewModelBase<Models.spectra>
    {
        c12880ma _spectrometer;

        string _Filter;
        public string Filter
        {
            get { return _Filter; }
            set
            {
                if(value != _Filter)
                {
                    _Filter = value;
                    RaisePropertyChanged("Filter");
                }
            }
        }

        projectSummary _Project;
        public projectSummary Project
        {
            get { return _Project; }
            set
            {
                if (value != _Project)
                {
                    _Project = value;
                    if (_Project != null)
                    {
                        parseProtocolsAndTags();
                    }
                }
            }
        }

        string _message;
        System.Threading.Timer msgTimer;
        public string Message
        {
            get { return _message; }
            private set
            {
                _message = value;
                System.Diagnostics.Debug.WriteLine(_message);
                msgTimer = new System.Threading.Timer((o) => {
                    msgTimer.Dispose();
                    _message = "";
                    App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => RaisePropertyChanged("Message"));
                }, null, 3000, System.Threading.Timeout.Infinite);
                App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => RaisePropertyChanged("Message"));
            }
        }

        string _tags;
        string _protocol;
        private void parseProtocolsAndTags()
        {
            Tags.Clear();
            Protocols.Clear();
            if (!string.IsNullOrWhiteSpace(_Project.Project.ProjectTags))
                Tags = new List<CheckBox>(_Project.Project.ProjectTags.Split(new char[] { '\r' }).Select(s => new CheckBox() { IsChecked = false, Content = s }));
            foreach (var item in Tags)
            {
                item.Checked += Tag_Checked;
                item.Unchecked += Tag_Unchecked;
            }

            if (!string.IsNullOrWhiteSpace(_Project.Project.ProjectProtocols))
                Protocols = new List<RadioButton>(_Project.Project.ProjectProtocols.Split(new char[] { '\r' }).Select(s => new RadioButton() { Content = s, GroupName = _Project.Name }));
            Protocols.Add(new RadioButton() { Content = "Other", GroupName = _Project.Name });
            foreach (var item in Protocols)
            {
                item.Checked += Protocol_Checked;
            }

        }

        private void Protocol_Checked(object sender, RoutedEventArgs e)
        {
            _protocol = ((RadioButton)sender).Content.ToString();
        }

        private void Tag_Unchecked(object sender, RoutedEventArgs e)
        {
            _tags.Replace("," + ((CheckBox)sender).Content.ToString(), "");
        }

        private void Tag_Checked(object sender, RoutedEventArgs e)
        {
            _tags += "," + ((CheckBox)sender).Content.ToString();
        }

        System.Threading.Timer integrationTimer;
        int _integrationTime;
        public class TimerDelay
        {
            public int Time { get; set; }
            public TimerDelay(int time)
            {
                Time = time;
            }
        }
        public int IntegrationTime
        {
            get { return _spectrometer.IntegrationTime; }
            set
            {
                _integrationTime = value;
                if (integrationTimer == null)
                {
                    integrationTimer = new System.Threading.Timer((o) =>
                    {
                        try
                        {
                            int x = ((TimerDelay)o).Time;
                            System.Diagnostics.Debug.WriteLine(string.Format("Start Int time: {0}", x));
                            if (x == _integrationTime & x != _spectrometer.IntegrationTime)
                            {
                                System.Diagnostics.Debug.WriteLine("Set Integration Time");
                                integrationTimer.Dispose();
                                integrationTimer = null;
                                _spectrometer.IntegrationTime = x;
                                App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => RaisePropertyChanged("IntegrationTime"));
                            }
                            else
                            {
                                ((TimerDelay)o).Time = _integrationTime;
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }
                    }, new TimerDelay(_integrationTime), 100, 250);
                }
            }
        }
        public List<CheckBox> Tags { get; set; }
        public List<RadioButton> Protocols { get; set; }
        Int16[] _spectra;


        public SpectrometerViewModel(SpectraContext context, Geolocator geolocator, Action<string> startWaitAction, Action stopWaitAction, c12880ma spectrometer) :
            base(context, geolocator, startWaitAction, stopWaitAction)
        {
            IsDarkCurrentCalibrated = false;
            CalibrationMessage = "Not Calibrated!";
            CalibrationColor = Colors.Red;
            Data = new Models.spectra();

            Tags = new List<CheckBox>();
            Protocols = new List<RadioButton>();
            //initialize dark current calibration data
            for (int i = 0; i < 288; i++)
            {
                _darkCurrentCalibrationData[i] = 0;
            }
            _spectrometer = spectrometer;
            _spectrometer.DataNotification += _spectrometer_DataNotification;
            _spectrometer.BLEConnectionStatusChanged += _spectrometer_BLEConnectionStatusChanged;
            //TODO create a connection changed event and took it up to toggle IsConnected. 
            initializeSpectralPlotModel();
            RaisePropertyChanged("");
        }

        private void _spectrometer_BLEConnectionStatusChanged(object sender, SPPSensorBase.BLEConnectionStatusEventArgs e)
        {
            IsConnected = e.ConnectionStatus == Windows.Devices.Bluetooth.BluetoothConnectionStatus.Connected;
            App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => RaisePropertyChanged("IsConnected"));
        }

        System.Threading.Timer t;
        private void _spectrometer_DataNotification(object sender, SPPSensorBase.BLEDataClientNotficationEventArgs e)
        {
            if (e.Data == null) return;
            if (e.Data.StartsWith("m"))
            {
                System.Diagnostics.Debug.WriteLine(e.Data);
                Message = e.Data.Substring(1);
            } else if (e.Data.StartsWith("s"))
            {
               
                Int16[] spectra = parseSpectra(e.Data);
                _spectra = spectra;
                if (spectra != null)
                {
                    t = new System.Threading.Timer((o) =>
                    {
                        clearGraph();
                    }, null, 3000, System.Threading.Timeout.Infinite);
                    updateSpectralModel(spectra);
                    if (_logging)
                    {
                        _logging = false;
                        saveLog(spectra);
                    }

                    if (_isDarkCurrentCalibration)
                    {
                        darkCurrentCalibration(spectra);
                    }
                }


            }


        }

        Int16[] parseSpectra(string data)
        {
            try
            {
                string[] vs = data.Substring(1).Split(new char[] { ',' });
                if (vs.Length != 289)
                {
                    Message = "Incorrect number of spectral bands!";
                    return null;
                }

                App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => Data.IntegrationTime = int.Parse(vs.First()));

                return vs.Skip(1).Select(i => Int16.Parse(i)).ToArray();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(data);

                return null;
            }
        }

        #region Black current calibration

        bool _isDarkCurrentCalibration = false;
        int[] _darkCurrentCalibrationData = new int[288];
        int _darkCurrentCalibrationIterations = 0;

        public string CalibrationMessage { get; private set; }
        public Color CalibrationColor { get; private set; }

        public Visibility ShowDarkCurrentCalibrationDialog { get; set; }
        public ICommand OpenDarkCurrentCalibrationCommand
        {
            get { return new AsyncRelayCommand(OpenDarkCurrentCalibrationExecute); }
        }


        /// <summary>
        /// average 10 black current readings from the spectrometer and store for 
        /// later correction of readings. Show the wait dialog.
        /// Close the dialog when finished
        /// </summary>
        async Task OpenDarkCurrentCalibrationExecute()
        {
            try
            {
                if (!IsConnected)
                {
                    var msg = new MessageDialog("Connect to spectrometer before trying to calibrate!");
                    await msg.ShowAsync();
                    return;
                }
                var start = new MessageDialog("Place the len cap on the spectrometer and click start.", "Dark Current Calibration");
                start.Commands.Add(new UICommand("Start", (s) =>
                {
                    _darkCurrentCalibrationData = new int[288];
                    IsDarkCurrentCalibrated = false;
                    _darkCurrentCalibrationIterations = 0;
                    _isDarkCurrentCalibration = true;
                    StartWaitAction("Calibrating...");
                }));
                start.Commands.Add(new UICommand("Cancel", (s) =>
                {
                    return;
                }));

                await start.ShowAsync();
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        public bool IsDarkCurrentCalibrated { get; set; }

        //todo test how dark current changes with integration!
        void darkCurrentCalibration(Int16[] data) //TODO
        {
            try
            {

                for (int i = 0; i < 288; i++)
                {
                    _darkCurrentCalibrationData[i] += data[i];
                }

                _darkCurrentCalibrationIterations++;

                if (_darkCurrentCalibrationIterations >= 10)
                {
                    for (int i = 0; i < 288; i++)
                    {
                        _darkCurrentCalibrationData[i] /= _darkCurrentCalibrationIterations;
                    }



                    _isDarkCurrentCalibration = false;
                    IsDarkCurrentCalibrated = true;
                    CalibrationColor = Colors.Black;

                    App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        StopWaitAction();
                        CalibrationMessage = "Calibrated";
                        RaisePropertyChanged("CalibrationMessage");
                        RaisePropertyChanged("CalibrationColor");
                        RaisePropertyChanged("RecordCommand");
                        RaisePropertyChanged("IsDarkCurrentCalibrated");
                    });

                }
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                _isDarkCurrentCalibration = false;
                IsDarkCurrentCalibrated = true;
                CalibrationColor = Colors.Black;
                App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    CalibrationMessage = "Calibrated";
                    RaisePropertyChanged("CalibrationMessage");
                    RaisePropertyChanged("CalibrationColor");
                    RaisePropertyChanged("RecordCommand");
                });
            }
        }

        #endregion

        #region connect to spectroCam

        public bool IsConnected { get; set; }

        public ICommand ConnectCommand
        {
            get { return new AsyncRelayCommand(ConnectExicute); }
        }
        /// <summary>
        /// close the dard current dialog
        /// </summary>
        async Task ConnectExicute()
        {
            try
            {
                bool result = false; //TODO await _spectrometer.ConnectService();
                if (!result)
                {
                    MessageDialog msg = new MessageDialog("SpectaCam not within range or is not paired with the device!\nUnable to connect.");
                    await msg.ShowAsync();
                }

            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }


        #endregion

        #region SpectralModel

        public PlotModel SpectralModel { get; private set; }

        void initializeSpectralPlotModel()
        {
            var model = new PlotModel { Title = "Spectra" };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = 380, Maximum = 850, Title = "Wavelength (nm)", IsZoomEnabled = false, IsPanEnabled = false });
            model.Axes.Add(new AnchoredZoomLinearAxes { Position = AxisPosition.Left, Title = "Intensity (DN)", Maximum = 980, Minimum = 0, IsZoomEnabled = true, ZoomAnchor = 0, IsPanEnabled = false });

            var spec = new RectangleBarSeries() { Title = "Spectra" };
            spec.StrokeThickness = 0;
            spec.StrokeColor = OxyColors.Transparent;
            spec.Background = OxyColors.LightGray;
            spec.RenderInLegend = false;
            double j = (850.0 - 380.0) / 288.0;
            for (double i = 380; i < 850; i += j)
            {
                spec.Items.Add(getSpectraBar(i, 75));

            }

            model.Series.Add(spec);
            this.SpectralModel = model;
        }

        void updateSpectralModel(Int16[] data)
        {
            lock (this.SpectralModel.SyncRoot)
            {
                var s = (RectangleBarSeries)SpectralModel.Series[0];
                for (int i = 0; i < 288; i++)
                {
                    s.Items[i].Y1 = (double)data[i] - _darkCurrentCalibrationData[i];
                }
            }

            this.SpectralModel.InvalidatePlot(true);
        }

        void clearGraph()
        {
            Int16[] data = new Int16[288];
            updateSpectralModel(data);
        }

        private RectangleBarItem getSpectraBar(double spectra, int value)
        {
            double j = (850.0 - 380.0) / 288.0 / 2;
            RectangleBarItem bar = new RectangleBarItem();
            bar.X0 = spectra - j;
            bar.X1 = spectra + j;
            bar.Y0 = 0;
            bar.Y1 = value;
            bar.Color = spectralColor(spectra);

            return bar;
        }

        OxyColor spectralColor(double l) // RGB <0,1> <- lambda l <400,700> [nm]
        {
            double t = 0.0, r = 0.0, g = 0.0, b = 0.0;

            if (l <= 400.0)
            {
                r = +(0.33 * .1) - (0.20 * .1 * .1);
            }
            else if ((l >= 400.0) && (l < 410.0))
            {
                t = (l - 400.0) / (410.0 - 400.0);
                r = +(0.33 * t) - (0.20 * t * t);
            }
            else if ((l >= 410.0) && (l < 475.0))
            {
                t = (l - 410.0) / (475.0 - 410.0);
                r = 0.14 - (0.13 * t * t);
            }
            else if ((l >= 545.0) && (l < 595.0))
            {
                t = (l - 545.0) / (595.0 - 545.0);
                r = +(1.98 * t) - (t * t);
            }
            else if ((l >= 595.0) && (l < 650.0))
            {
                t = (l - 595.0) / (650.0 - 595.0);
                r = 0.98 + (0.06 * t) - (0.40 * t * t);
            }
            else if ((l >= 650.0) && (l < 700.0))
            {
                t = (l - 650.0) / (700.0 - 650.0);
                r = 0.65 - (0.84 * t) + (0.20 * t * t);
            }
            else
            {
                r = 0.1;
            }

            if ((l > 415.0) && (l < 475.0))
            {
                t = (l - 415.0) / (475.0 - 415.0);
                g = +(0.80 * t * t);
            }
            else if ((l >= 475.0) && (l < 590.0))
            {
                t = (l - 475.0) / (590.0 - 475.0);
                g = 0.8 + (0.76 * t) - (0.80 * t * t);
            }
            else if ((l >= 585.0) && (l < 639.0))
            {
                t = (l - 585.0) / (639.0 - 585.0);
                g = 0.84 - (0.84 * t);
            }

            if ((l >= 400.0) && (l < 475.0))
            {
                t = (l - 400.0) / (475.0 - 400.0)
               ; b = +(2.20 * t) - (1.50 * t * t);
            }
            else if ((l >= 475.0) && (l < 560.0))
            {
                t = (l - 475.0) / (560.0 - 475.0)
                ; b = 0.7 - (t) + (0.30 * t * t);
            }

            byte red = (byte)(255 * r);
            byte green = (byte)(255 * g);
            byte blue = (byte)(255 * b);

            return OxyColor.FromArgb(255, red, green, blue);
        }

        #endregion


        #region commands

        public override async Task NewExecute()
        {
            try
            {
                if (Data != null && !Saved) await SaveIfNotSaved();
                foreach (var item in Tags.Where(t => t.IsChecked == true)) item.IsChecked = false;
                foreach (var item in Protocols.Where(t => t.IsChecked == true)) item.IsChecked = false;
                await base.NewExecute(); 
                Data.ProjectIdDTO = Project.Id;

            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }

        }

        protected override async Task SaveExecute()
        {
            try
            {

                if (!Protocols.Any(p => p.IsChecked == true))
                {
                    throw new Exception("Protocol was not selected!");
                }
                Data.SpectrometerSerialNumber = _spectrometer.SerialNumber;
                Data.filter = this.Filter;
                Data.Protocol = Protocols.First(p => p.IsChecked.Value).Content.ToString();
                Data.Tag += Tags.Where(t => t.IsChecked == true).Select(t => t.Content.ToString());
                _spectrometer.Pause = true;
                //set the spectra data
                for (int i = 0; i < 288; i++)
                {
                    var prop = Data.GetType().GetProperty(string.Format("Value{0}", i));
                    if (prop != null) prop.SetValue(Data, _spectra[i]);
                }
                await base.SaveExecute();
                _spectrometer.Pause = false;
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        protected override async Task NextExecute()
        {
            try
            {
                if (Data != null && !Saved) await SaveIfNotSaved();
                var p = _context.spectras.Where(d => ((ITimestamp)d).Timestamp > ((ITimestamp)Data).Timestamp).OrderBy(d => d.Timestamp).FirstOrDefault();
                if (p != null)
                {
                    Data = p;
                    last = false;
                    first = false;
                    RaisePropertyChanged("");
                }
                else
                {
                    last = true;
                    RaisePropertyChanged("NextCommand");
                }
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }
        protected override async Task PreviousExecute()
        {
            try
            {
                if (Data != null && !Saved) await SaveIfNotSaved();
                var p = _context.spectras.Where(d => d.Timestamp < Data.Timestamp).OrderByDescending(d => d.Timestamp).FirstOrDefault();
                if (p != null)
                {
                    Data = p;
                    first = false;
                    last = false;
                    RaisePropertyChanged("");

                }
                else
                {
                    first = true;
                    RaisePropertyChanged("PreviousCommand");
                }
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        #endregion

        #region logging
        bool _logging = false;
        int j = 0;
        System.Threading.Timer _timer;

        bool _IsLogging = false;
        public bool IsLogging
        {
            get { return _IsLogging; }
            set
            {
                if (value != _IsLogging)
                {
                    _IsLogging = value;
                    RaisePropertyChanged("IsLogging");
                }
            }
        }
        public bool InvertedIsLogging{get{return !_IsLogging;} }

        public int LoggingInterval { get; set; }
        bool _LoggingEnabled;
        
        public bool LoggingEnabled
        {
            get { return _LoggingEnabled; }
            set
            {
                if(value != _LoggingEnabled)
                {
                    
                    _LoggingEnabled = value;
                    if (!_LoggingEnabled) StopLoggingExecute();
        
                    RaisePropertyChanged("LoggingEnabled");
                    RaisePropertyChanged("RecordCommand");
                }
            }
        }
        public ICommand RecordCommand
        {
            get { return new AsyncRelayCommand(RecordExecute, RecordCanExecute); }
        }

        public async Task RecordExecute()
        {
            try
            {
                if (IsLogging) await StopLoggingExecute();
                else await StartLoggingExecute();
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool RecordCanExecute()
        {
            return IsDarkCurrentCalibrated;
        }


        public ICommand StartLoggingCommand
        {
            get { return new AsyncRelayCommand(StartLoggingExecute, StartLoggingCanExecute); }
        }
        
        public async Task StartLoggingExecute()
        {
            try
            {
                IsLogging = true;
                await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => RaisePropertyChanged("IsLogging"));
                _timer = new System.Threading.Timer((o) => _logging = true, null, 0, LoggingInterval * 1000);
                RaisePropertyChanged("StartLoggingCommand");
                RaisePropertyChanged("StopLoggingCommand");
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool StartLoggingCanExecute()
        {
            return !IsLogging & IsDarkCurrentCalibrated;
        }


        public ICommand StopLoggingCommand
        {
            get { return new AsyncRelayCommand(StopLoggingExecute, StopLoggingCanExecute); }
        }

        public async Task StopLoggingExecute()
        {
            try
            {
                IsLogging = false;
                await App.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,()=>RaisePropertyChanged("IsLogging"));
                _timer.Dispose();
                RaisePropertyChanged("StartLoggingCommand");
                RaisePropertyChanged("StopLoggingCommand");
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool StopLoggingCanExecute()
        {
            return IsLogging;
        }

      

        async Task saveLog(Int16[] data)
        {
            try
            {

                spectra.camera.Models.spectra s = new Models.spectra();
                if (_geoStatus == GeolocationAccessStatus.Allowed) setGPSPoint(s);
                s.Name = Data.Name;
                s.filter = this.Filter;
                s.IntegrationTime = Data.IntegrationTime;
                s.SpectrometerSerialNumber = _spectrometer.SerialNumber;
                s.Description = Data.Description;
                s.ExternalIdentifier = string.Format("{0} ({1})",Data.ExternalIdentifier,j.ToString());
                s.Protocol = _protocol; // Protocols.First(p => p.IsChecked.Value).Content.ToString();
                s.Tag += _tags; // Tags.Where(t => t.IsChecked == true).Select(t => t.Content.ToString());
                
                for (int i = 0; i < 288; i++)
                {
                    var prop = s.GetType().GetProperty(string.Format("Value{0}", i));
                    prop.SetValue(s, data[i]);
                }

                await _context.AddSpectra(s);

                j++;
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        

        async Task setGPSPoint(spectra.camera.Models.spectra data)
        {
            try
            {
                if (_geoStatus == GeolocationAccessStatus.Allowed)
                {
                    // Create Geolocator and define perodic-based tracking (2 second interval)
                    var _geolocator = new Geolocator { DesiredAccuracyInMeters = 25 };

                    var position = _geolocator.GetGeopositionAsync(new TimeSpan(0), new TimeSpan(0, 5, 0)).GetResults();

                    data.Latitude =  position.Coordinate.Point.Position.Latitude;
                    data.Longitude = position.Coordinate.Point.Position.Longitude;
                }
            }
            catch (Exception ex)
            {
                await ShowException(ex);
                _geoStatus = GeolocationAccessStatus.Unspecified;
            }
        }


        #endregion

    }
}
