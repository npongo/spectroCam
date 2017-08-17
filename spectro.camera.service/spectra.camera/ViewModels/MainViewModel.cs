using Prism.Windows.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using spectra.camera.Models;
using spectra.camera.Helpers;
using Dropbox.Api;
using Windows.Storage;
using Windows.Networking.Connectivity;

namespace spectra.camera.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Helpers.c12880ma _spectrometer;

        GeolocationAccessStatus _geoStatus;

        public string ServiceUrl
        {
            get { return (string)localSettings.Values["ServiceUrl"]; }
        }

        public bool ShowMainMenu { get; set; }

        public bool EnableMainView { get; set; }

        public Visibility ShowMainView { get; set; }

        public Visibility ShowWaitDialog { get; set; }
        public string WaitDialogMessage { get; set; }


        private string deviceId;
        
        public bool EnableForm { get; set; }

        public ProjectManagerViewModel ProjectMxVM { get; set; }
        public SpectrometerViewModel SpectraVM { get; set; }
        

        public MainViewModel(SpectraContext context, Geolocator geoLocator):base(context,geoLocator)
        {

            ShowWaitDialog = Visibility.Collapsed;
            ShowMainView = Visibility.Visible;
            ShowSpectrometer = false;
            EnableMainView = true;
            EnableForm = false;

            startWaitAction = new Action<string>((s) =>
            {
                WaitDialogMessage = s;
                ShowWaitDialog = Visibility.Visible;
                RaisePropertyChanged("ShowWaitDialog");
                RaisePropertyChanged("WaitDialogMessage");

            });
            stopWaitAction = new Action(() =>

            {

                ShowWaitDialog = Visibility.Collapsed;
                RaisePropertyChanged("ShowWaitDialog");
            });

              

            deviceId = (string)localSettings.Values["DeviceId"];


            _spectrometer = new c12880ma();
            _spectrometer.BLEConnectionStatusChanged += _spectrometer_BLEConnectionStatusChanged;
            ProjectMxVM = new ProjectManagerViewModel(context, geoLocator, startWaitAction, stopWaitAction);
            SpectraVM = new SpectrometerViewModel(context, geoLocator, startWaitAction, stopWaitAction, _spectrometer);
            ProjectMxVM.PropertyChanged += ProjectMxVM_PropertyChanged;
            SpectraVM.PropertyChanged += SpectraVM_PropertyChanged;

            //todo connection changed!
        }

        private void _spectrometer_BLEConnectionStatusChanged(object sender, SPPSensorBase.BLEConnectionStatusEventArgs e)
        {
            IsConnected = e.ConnectionStatus == Windows.Devices.Bluetooth.BluetoothConnectionStatus.Connected;
            RaisePropertyChanged("IsConnected");
        }

        private void SpectraVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }

        private void ProjectMxVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Project") SpectraVM.Project = ProjectMxVM.SelectedProject;

            RaisePropertyChanged(e.PropertyName);
        }

        public Action<string> startWaitAction;

        public Action stopWaitAction;

        public async Task InitializeContext()
        {
            try
            {
                await _context.Initialize();
                await base.SetGPSStatus();
                await ProjectMxVM.SetGPSStatus();
                await ProjectMxVM.initalizeProjectManager();
                await SpectraVM.SetGPSStatus();

                EnableForm = true;
                RaisePropertyChanged("");
            }
            catch(Exception ex)
            {
                ShowException(ex);
            }
        }

        #region features

        #region start spectral collection

        public bool ShowSpectrometer { get; set; }

        public async Task StartSpectralCollation(projectSummary project)
        {
            try
            {
                //TODO made sure saved indicates any unsaved data in the VM!
                if (!ProjectMxVM.Saved) await ProjectMxVM.SaveIfNotSaved();
                SpectraVM.Project = project;
                await SpectraVM.NewExecute();
                ShowSpectrometer = true;
                RaisePropertyChanged("ShowSpectrometer");
                RaisePropertyChanged("ShowProjectManagerCommand");
                RaisePropertyChanged("ShowSpectrometer");
                RaisePropertyChanged("SaveCommand");
                RaisePropertyChanged("DeleteCommand");
                RaisePropertyChanged("NewCommand");
                RaisePropertyChanged("PreviousCommand");
                RaisePropertyChanged("NextCommand");
            }
            catch(Exception ex)
            {
                await ShowException(ex);
            }
        }

        #endregion

        #region Show Project Manager
        
        public ICommand ShowProjectManagerCommand
        {
            get { return new AsyncRelayCommand(ShowProjectManagerExecute, ShowProjectManagerCanExecute); }
        }

        async Task ShowProjectManagerExecute()
        {
            try
            {
                if (!SpectraVM.Saved) await SpectraVM.SaveIfNotSaved();
                if (SpectraVM.IsLogging) await SpectraVM.StopLoggingExecute();

                await ProjectMxVM.initalizeProjectManager();
                ShowSpectrometer = false;
                RaisePropertyChanged("ShowSpectrometer");
                RaisePropertyChanged("SaveCommand");
                RaisePropertyChanged("DeleteCommand");
                RaisePropertyChanged("NewCommand");
                RaisePropertyChanged("PreviousCommand");
                RaisePropertyChanged("NextCommand");
            }
            catch(Exception ex)
            {
                await ShowException(ex);
            }
        }

        
        bool ShowProjectManagerCanExecute()
        {
            return ShowSpectrometer;
        }

        #endregion

        #region bottom app commands

        public ICommand SaveCommand
        {
            get
            {
                if (ShowSpectrometer) return SpectraVM.SaveCommand;
                else return ProjectMxVM.SaveCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (ShowSpectrometer) return SpectraVM.DeleteCommand;
                else return ProjectMxVM.DeleteCommand;
            }
        }

        public ICommand NewCommand
        {
            get
            {
                if (ShowSpectrometer) return SpectraVM.NewCommand;
                else return ProjectMxVM.NewCommand;
            }
        }

        public ICommand PreviousCommand
        {
            get
            {
                if (ShowSpectrometer) return SpectraVM.PreviousCommand;
                else return ProjectMxVM.PreviousCommand;
            }
        }
        public ICommand NextCommand
        {
            get
            {
                if (ShowSpectrometer) return SpectraVM.NextCommand;
                else return ProjectMxVM.NextCommand;
            }
        }



        #endregion

        #region Main Menu

        public ICommand OpenCloseMainMenuCommand
        {
            get { return new AsyncRelayCommand(OpenCloseMainMenuExecute, OpenCloseMainMenuCanExecute); }
        }

        async Task OpenCloseMainMenuExecute()
        {
            try
            {
                ShowMainMenu = !ShowMainMenu;
                RaisePropertyChanged("ShowMainMenu");

            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }

        }

        bool OpenCloseMainMenuCanExecute()
        {
            return EnableForm;
        }

        #endregion


        #region set filter command
        public ICommand SetFilterCommand
        {
            get { return new AsyncRelayCommand(SetFilterExecute, SetFilterCanExecute); }
        }
        /// <summary>
        /// open the dard current dialog
        /// </summary>
        async Task SetFilterExecute()
        {
            try
            {
                if (!IsConnected)
                {
                    var emsg = new MessageDialog("Connect to spectrometer before trying to calibrate!");
                    await emsg.ShowAsync();
                    return;
                }

                var msg = new ContentDialog();
                msg.ContentTemplate = App.Current.Resources["InputDialogTemplate"] as DataTemplate;
                var vm = new InputDialogViewModel("Filter:", SpectraVM.Filter);
                msg.DataContext = vm;
                msg.PrimaryButtonCommand = new RelayCommand(() =>
                {
                    string filter = vm.Input;
                    SpectraVM.Filter = filter;
                });
                msg.PrimaryButtonText = "Set";
                msg.SecondaryButtonText = "Cancel";
                await msg.ShowAsync();
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool SetFilterCanExecute()
        {
            return true;
        }
        #endregion


        #region About Dialog
        public ICommand AboutOpenCommand
        {
            get { return new AsyncRelayCommand(AboutOpenExecute, AboutOpenCanExecute); }
        }
        /// <summary>
        /// open the dard current dialog
        /// </summary>
        async Task AboutOpenExecute()
        {
            try
            { 
                var msg = new ContentDialog();
                msg.ContentTemplate = App.Current.Resources["AboutTemplate"] as DataTemplate;
                var vm = new AboutDialogViewModel();
                msg.DataContext = vm; 
                msg.PrimaryButtonText = "Close"; ;
                await msg.ShowAsync();
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool AboutOpenCanExecute()
        {
            return true;
        }
        #endregion


        #region Settings Dialog
        public ICommand SettingsOpenCommand
        {
            get { return new AsyncRelayCommand(SettingsOpenExecute, SettingsOpenCanExecute); }
        }
        /// <summary>
        /// open the dard current dialog
        /// </summary>
        async Task SettingsOpenExecute()
        {
            try
            {
                var msg = new ContentDialog();
                msg.ContentTemplate = App.Current.Resources["SettingsTemplate"] as DataTemplate;
                var vm = new SettingsDialogViewModel();
                msg.DataContext = vm;
                msg.Closing += Msg_Closing;
                msg.PrimaryButtonCommand = new AsyncRelayCommand(async () =>
                {
                    //update settings
                    
                    if (!vm.SaveSettings()) await msg.ShowAsync();

                    deviceId = vm.DeviceId;
                    _context = new SpectraContext(vm.ServiceUrl);
                });

                 
                msg.PrimaryButtonText = "Close";
                msg.SecondaryButtonText = "Cancel";
                await msg.ShowAsync();
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        private void Msg_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (!((SettingsDialogViewModel)sender.DataContext).SaveSettings()) args.Cancel = true;
            else
            {
                deviceId = ((SettingsDialogViewModel)sender.DataContext).DeviceId;
                App.Context = new SpectraContext(((SettingsDialogViewModel)sender.DataContext).ServiceUrl);
            }
        }

        bool SettingsOpenCanExecute()
        {
            return true;
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
                bool result = await _spectrometer.Connect();
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

        //TODO 
        //private void _droneSensors_BLEConnectionStatusChanged(object sender, DroneSensors.BLEConnectionStatusEventArgs e)
        //{
        //    IsConnected = e.ConnectionStatus == Windows.Devices.Bluetooth.BluetoothConnectionStatus.Connected;
        //    RaisePropertyChanged("IsConnected");
        //}

        #endregion

        #region Time sycn
        public ICommand TimeSyncCommand
        {
            get { return new AsyncRelayCommand(TimeSyncExecute); }
        }
        /// <summary>
        /// Send a time sync command over bluetooth
        /// </summary>
        async Task TimeSyncExecute()
        {
            try
            {
                if (!IsConnected)
                {
                    var msg = new MessageDialog("Connect to SpectaCam before trying sync time!");
                    await msg.ShowAsync();
                    return;
                }
                //await _spectrometer.WriteTimeSync(); // TODO

            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        #endregion


        #region cloud sync database


        #endregion


        public ICommand SyncDatabaseCommand
        {
            get { return new AsyncRelayCommand(SyncDatabaseExecute); }
        }

        async Task SyncDatabaseExecute()
        {
            try
            {

                if (!IsInternet())
                {
                    MessageDialog msg = new MessageDialog("No internet connection, try again once connected to the internet.");
                    await msg.ShowAsync();
                    return;
                }

                TaskMessage = "Syncing data...";
                TaskRunning = true;
                RaisePropertyChanged("TaskMessage");
                RaisePropertyChanged("TaskRunning");

                bool result = await _context.SyncAsync();

                if (result)
                {
                    MessageDialog msg = new MessageDialog("Sync data successful!");
                    await msg.ShowAsync();
                }
                else
                {
                    MessageDialog msg = new MessageDialog("Sync data failed!\r\n");
                    await msg.ShowAsync();
                }

            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
            finally
            {
                TaskRunning = false;
                TaskMessage = "";
                RaisePropertyChanged("TaskMessage");
                RaisePropertyChanged("TaskRunning");
            }


        }



        #region Backup database

        public ICommand DropboxDatabaseCommand
        {
            get { return new AsyncRelayCommand(DropboxDatabaseExecute); }
        }

        async Task DropboxDatabaseExecute()
        {
            try
            {
                if(string.IsNullOrEmpty(App.AccessToken))
                {
                    MessageDialog msg = new MessageDialog("Connect to your Dropbox account in settings before attemping to export the data to Dropbox.");
                    await msg.ShowAsync();
                    return;
                }


                if (!IsInternet())
                {
                    MessageDialog msg = new MessageDialog("No internet connection, try again once connected to the internet.");
                    await msg.ShowAsync();
                    return;
                }

                TaskMessage = "Exporting data to Dropbox...";
                TaskRunning = true;
                RaisePropertyChanged("TaskMessage");
                RaisePropertyChanged("TaskRunning");

                DropBoxBackup backup = new DropBoxBackup();

                //todo get device guid
                var result = await backup.UploadDatabase(deviceId, "SpectraCam");


                if (result == null)
                {
                    MessageDialog msg = new MessageDialog("Export to Dropbox successful!");
                    await msg.ShowAsync();
                }
                else
                {
                    MessageDialog msg = new MessageDialog("Export to Dropbox failed!\r\n" + result.Message);
                    await msg.ShowAsync();
                }

            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
            finally
            {
                TaskRunning = false;
                TaskMessage = "";
                RaisePropertyChanged("TaskMessage");
                RaisePropertyChanged("TaskRunning");
            }


        }



        //public ICommand ExportDatabaseCommand
        //{
        //    get { return new AsyncRelayCommand(ExportDatabaseExecute); }
        //}

        //async Task ExportDatabaseExecute()
        //{
        //    try
        //    {
        //        ModelToCSV.Context = context;
        //        bool result = await ModelToCSV.ExportDatabase(deviceId);

        //        if(!result)
        //        {
        //            MessageDialog msg = new MessageDialog("Export database failed!");
        //            await msg.ShowAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowException(ex);
        //    }

        //}


        #endregion

        #region save database local

        public ICommand BackupDatabaseCommand
        {
            get { return new AsyncRelayCommand(BackupDatabaseExecute); }
        }

        async Task BackupDatabaseExecute()
        {
            try
            {
                TaskRunning = true;
                TaskMessage = "Backing up data...";
                RaisePropertyChanged("TaskMessage");
                RaisePropertyChanged("TaskRunning");


                MessageDialog msg = new MessageDialog("", "Backup Data");

                try
                {
                    // Get the logical root folder for all external storage devices.


                    //copy db
                    string file = "localstore.db";
                    Windows.Storage.StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                    Windows.Storage.StorageFile dbFile = await local.GetFileAsync(file);
                    DateTime now = DateTime.Now;


                    var savePicker = new Windows.Storage.Pickers.FileSavePicker();
                    savePicker.DefaultFileExtension = ".db";

                    savePicker.SuggestedStartLocation =
                        Windows.Storage.Pickers.PickerLocationId.Downloads;
                    // Dropdown of file types the user can save the file as
                    savePicker.FileTypeChoices.Add("sqlite db", new List<string>() { ".db" });
                    // Default file name if the user does not type one in or select a file to replace
                    savePicker.SuggestedFileName = string.Format("spectra_{0}_{1}{2}{3}{4}{5}.db", deviceId, now.Year, now.Month, now.Day, now.Hour, now.Minute);

                    Windows.Storage.StorageFile exportFile = await savePicker.PickSaveFileAsync();
                    if (exportFile != null)
                    {
                        // Prevent updates to the remote version of the file until
                        // we finish making changes and call CompleteUpdatesAsync.
                        //Windows.Storage.CachedFileManager.DeferUpdates(exportFile);
                        Windows.Storage.CachedFileManager.DeferUpdates(dbFile);
                        // write to file
                        await dbFile.CopyAndReplaceAsync(exportFile);
                        //var outFolder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(exportFile.Path);
                        //await dbFile.CopyAsync(outFolder, exportFile.Name, NameCollisionOption.ReplaceExisting);
                        

                        Windows.Storage.Provider.FileUpdateStatus status =
                            await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(dbFile);

                        if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                        {
                            msg.Content = "File " + exportFile.Name + " was saved.";
                        }
                        else
                        {
                            msg.Content = "File " + exportFile.Name + " couldn't be saved.";

                        }
                    }
                    else
                    {
                        msg.Content = "Operation cancelled.";
                    }

                }
                catch (Exception ex)
                {
                    msg.Content = string.Format("An error occured while backing up the data:\n\r{0}", ex.Message);
                }
                finally
                {
                    await msg.ShowAsync();
                }

            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
            finally
            {
                TaskRunning = false;
                TaskMessage = "";
                RaisePropertyChanged("TaskMessage");
                RaisePropertyChanged("TaskRunning");
            }


        }

#endregion

        #region Run Task 
        public string TaskMessage { get; set; }

        public bool TaskRunning { get; set; }

        #endregion

        #endregion


        public bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }
    }
}
