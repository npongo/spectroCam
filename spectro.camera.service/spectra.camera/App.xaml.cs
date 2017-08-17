
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using Windows.UI.Core;
using spectra.camera.ViewModels;
using spectra.camera.Models;
using spectra.camera.Views;
using Dropbox.Api;
using Windows.Storage;

namespace spectra.camera
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// The dropbox app key.
        /// </summary>
        /// <remarks>
        /// This can be found in the
        /// <a href="https://www.dropbox.com/developers/apps">App Console</a>.
        /// </remarks>
        public const string AppKey = "4hkdkf2a9mels4t";

        /// <summary>
        /// The redirect URI
        /// </summary>
        public static readonly Uri RedirectUri = new Uri("http://localhost:5000/admin/auth");

        /// <summary>
        /// The dropbox client
        /// </summary>
        private static DropboxClient dropboxClient;

        static Geolocator _geolocator;
        static CoreDispatcher _dispatcher;
        static SpectraContext _context;
        static MainViewModel _VM;

        public static SpectraContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public static MainViewModel VM
        {
            get { return _VM; }
        }


        public static Geolocator Geolocator
        {
            get { return _geolocator; }
        }



        public static CoreDispatcher Dispatcher
        {
            get { return _dispatcher; }
            set { _dispatcher = value; }
        }


        /// <summary>
        /// Occurs when the dropbox client is changed.
        /// </summary>
        /// <remarks>
        /// This is typically if the user connects to, or disconnects from,
        /// dropbox.
        /// </remarks>
        public static event EventHandler DropboxClientChanged;

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public static string AccessToken
        {
            get
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                object accessToken;
                if (localSettings.Values.TryGetValue("DropboxAccessToken", out accessToken))
                {
                    return accessToken.ToString();
                }

                return string.Empty;
            }

            set
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                if (string.IsNullOrEmpty(value))
                {
                    localSettings.Values.Remove("DropboxAccessToken");
                    DropboxClient = null;
                }
                else
                {
                    localSettings.Values["DropboxAccessToken"] = value;
                    SetNewDropboxClient();
                }
            }
        }

        /// <summary>
        /// Gets the dropbox client.
        /// </summary>
        public static DropboxClient DropboxClient
        {
            get
            {
                return dropboxClient;
            }

            private set
            {
                using (var old = dropboxClient)
                {
                    dropboxClient = value;
                }

                var handler = DropboxClientChanged;
                if (handler != null)
                {
                    handler(App.Current, EventArgs.Empty);
                }
            }
        }
        

        /// <summary>
        /// Sets the new dropbox client.
        /// </summary>
        private static void SetNewDropboxClient()
        {
            DropboxClient = new DropboxClient(AccessToken, new DropboxClientConfig("SpectroCam"));
        }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            _geolocator = new Geolocator { DesiredAccuracyInMeters = 25, ReportInterval = 1000 };

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (!localSettings.Values.Keys.Contains("ServiceUrl"))
                localSettings.Values["ServiceUrl"] = "http://spectraCamservice.azurewebsites.net";


            if (!localSettings.Values.Keys.Contains("DeviceId"))
                localSettings.Values["DeviceId"] = Guid.NewGuid().ToString();

            if (!string.IsNullOrEmpty(AccessToken))
            {
                SetNewDropboxClient();
            }

            _context = new SpectraContext((string)localSettings.Values["ServiceUrl"]);
            _VM = new MainViewModel(_context, _geolocator);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: SaveCommand application state and stop any background activity
            deferral.Complete();
        }
    }
}
