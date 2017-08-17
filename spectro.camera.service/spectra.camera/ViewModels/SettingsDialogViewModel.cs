using spectra.camera.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;

namespace spectra.camera.ViewModels
{
    public class SettingsDialogViewModel : ViewModelBase
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        string _DeviceId;
        public string DeviceId
        {

            get { return _DeviceId; }

            set
            {
                if(value != _DeviceId)
                {
                    _DeviceId = value;
                    RaisePropertyChanged("DeviceId");
                }
            }
        }

        string _ServiceUrl;
        public string ServiceUrl
        {

            get { return _ServiceUrl; }

            set
            {
                if (value != _ServiceUrl)
                {
                    _ServiceUrl = value;
                    RaisePropertyChanged("ServiceUrl");
                }
            }
        }



        public SettingsDialogViewModel():base(null,null)
        {
            _DeviceId = (string)localSettings.Values["DeviceId"];
            _ServiceUrl = (string)localSettings.Values["ServiceUrl"];
            Connecting = true;
            RaisePropertyChanged("");
            var task = this.CheckConnection();
        }

        /// <summary>
        /// update settings in app settings
        /// </summary>
        public bool SaveSettings()
        {
            bool result = true;
            localSettings.Values["DeviceId"] = _DeviceId;

            Uri sUrl;
            if (isUrl(_ServiceUrl, out sUrl))
            {
                localSettings.Values["ServiceUrl"] = sUrl.ToString();
            }
            else
            {
                result = false;
            }

            return result;
        }

        private bool isUrl(string serviceUrl, out Uri serviceUri)
        {
            return Uri.TryCreate(serviceUrl, UriKind.Absolute, out serviceUri);
        }

        public ICommand NewDeviceIdCommand
        {
            get { return new RelayCommand(NewDeviceIdExecute, NewDeviceIdCanExecute); }
        }


        void NewDeviceIdExecute()
        {
            try
            {
                DeviceId = Guid.NewGuid().ToString();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        bool NewDeviceIdCanExecute()
        {
            return true;
        }


        #region dropbox connection

        public bool IsDropboxConnected { get; set; }

        public string DropboxDisplayName { get; private set; }

        public bool Connecting { get; private set; }

        /// <summary>
        /// Checks the if the app is currently connected and sets the UI to reflect
        /// the connection state.
        /// </summary>
        /// <returns>An asynchronous task.</returns>
        private async Task CheckConnection()
        {
            if (string.IsNullOrEmpty(App.AccessToken))
            {
                this.SetDisconnected();
            }
            else
            {
                await this.SetConnected();
                Connecting = false;
                RaisePropertyChanged("Connecting");
            }
        }

        private void SetDisconnected()
        {
            IsDropboxConnected = false;
            RaisePropertyChanged("IsDropboxConnected");
            DropboxDisplayName = "<Not Connected>";
            RaisePropertyChanged("DropboxDisplayName");
        }

        /// <summary>
        /// Sets the UI to reflect that the app is currently.
        /// </summary>
        /// <returns>An asynchronous task.</returns>
        private async Task SetConnected()
        {
            IsDropboxConnected = true;
           
            var client = App.DropboxClient;
            var account = await client.Users.GetCurrentAccountAsync();
            DropboxDisplayName = account.Name.DisplayName;
            RaisePropertyChanged("DropboxDisplayName");
        }

        public ICommand DropboxConnectCommand
        {
            get { return new AsyncRelayCommand(DropboxConnectExecute, DropboxConnectCanExecute); }
        }

        async Task DropboxConnectExecute()
        {
            try
            { 
                if (string.IsNullOrEmpty(App.AccessToken))
                {
                    //DropboxOAuth.AuthorizeAndContinue();
                    await DropboxOAuth.Authorize();
                    await SetConnected();
                }
                else
                {
                    App.AccessToken = string.Empty;
                    SetDisconnected();
                }
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool DropboxConnectCanExecute()
        {
            return true;
        }

        #endregion

    }
}
