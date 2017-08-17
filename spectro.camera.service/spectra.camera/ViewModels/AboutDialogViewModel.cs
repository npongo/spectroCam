using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel;

namespace spectra.camera.ViewModels
{
    public class AboutDialogViewModel
    {
        public string AppVersion
        {
            get
            {
                Package package = Package.Current;
                PackageId packageId = package.Id;
                PackageVersion version = packageId.Version;

                return string.Format("Version: {0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            }
        }

        public string DeviceId
        {
            get { return (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DeviceId"]; }
        }

        public AboutDialogViewModel()
        {
        }
    }
}
