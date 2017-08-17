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
using System.Reflection;

namespace spectra.camera.ViewModels
{
    public class ProjectGalleryViewModel:ViewModelBase
    {
        public ProjectGalleryViewModel(SpectraContext context, Geolocator geolocator) : base(context, geolocator)
        {

        }

        projectSummary _Project;
        public projectSummary Project
        {
            get { return _Project; }
            set
            {
                if(value != _Project)
                {
                    _Project = value;
                    loadSpectra();
                }
            }
        }

        public List<SpectraGalleryViewModel> SpectraGallery { get; set; }

        private void loadSpectra()
        {
            SpectraGallery = _context.spectras.Where(s => s.ProjectIdDTO == _Project.Id).Select(s => new SpectraGalleryViewModel(s)).ToList();
        }

        #region commands

        public ICommand SelectAllCommand
        {
            get { return new AsyncRelayCommand(SelectAllExecute, SelectAllCanExecute); }
        }
        async Task SelectAllExecute()
        {
            try
            {

                foreach (var item in SpectraGallery) item.Selected = true;
            }
            catch(Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool SelectAllCanExecute()
        {
            return true;
        }



        public ICommand UnselectAllCommand
        {
            get { return new AsyncRelayCommand(UnselectAllExecute, UnselectAllCanExecute); }
        }
        async Task UnselectAllExecute()
        {
            try
            {
                foreach (var item in SpectraGallery) item.Selected = false;
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool UnselectAllCanExecute()
        {
            return true;
        }

        /// <summary>
        /// export a either/both a csv file of selected spectra and graphs of specified size.
        /// </summary>
        public ICommand ExportDropboxCommand
        {
            get { return new AsyncRelayCommand(ExportDropboxExecute, ExportDropboxCanExecute); }
        }
        async Task ExportDropboxExecute()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool ExportDropboxCanExecute()
        {
            return SpectraGallery.Any(s => s.Selected);
        }

        public ICommand ExportDownloadFolderCommand
        {
            get { return new AsyncRelayCommand(ExportDownloadFolderExecute, ExportDownloadFolderCanExecute); }
        }
        async Task ExportDownloadFolderExecute()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool ExportDownloadFolderCanExecute()
        {
            return SpectraGallery.Any(s=>s.Selected);
        }

        public ICommand ExportEmailCommand
        {
            get { return new AsyncRelayCommand(ExportEmailExecute, ExportEmailCanExecute); }
        }
        async Task ExportEmailExecute()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool ExportEmailCanExecute()
        {
            return SpectraGallery.Count(s=>s.Selected) == 1;
        }


        public ICommand CloseCommand
        {
            get { return new AsyncRelayCommand(CloseExecute, CloseCanExecute); }
        }
        async Task CloseExecute()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool CloseCanExecute()
        {
            return SpectraGallery.Count(s => s.Selected) == 1;
        }


        #endregion

        #region export methods

        /// <summary>
        /// export graph image width
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// export graph image height
        /// </summary>
        public double Height { get; set; }

        public enum exportType
        {
            DROPBOX = 1,
            DOWNLOAD_FOLDER = 2,
            EMAIL = 3
        }

        public exportType ExportType { get; set; }

        private void exportDropbox()
        {

        }

        /// <summary>
        /// This methods generates the csv file and optionally graphs into a tempory folder
        /// </summary>
        /// <param name="csv"></param>
        /// <param name="graphs"></param>
        private void generateExportFiles(bool csv, bool graphs)
        {
            //TODO create temp folder to hold csv and graphs
            string contents = getHeader();

            foreach (var item in SpectraGallery.Where(s => s.Selected))
            {
                if (csv)
                {
                    contents += getValues(item);
                }

                if (graphs)
                {
                    generateGraph(item);
                }
            }

            //TODO save csv file 

            //TODO zip folder

            switch (ExportType)
            {
                case exportType.DROPBOX:
                    //TODO upload zip folder to dropbox
                    uploadToDropbox();
                    break;
                case exportType.DOWNLOAD_FOLDER:
                    //TODO copy foler to downlaod folder
                    copyToDownloadFolder();
                    break;
                case exportType.EMAIL:
                    //TODO attach folder to email.
                    sendEmail();
                    break;
            }

            //TODO cleanup delete tempory folder and zip archive

            //TODO report to user result;
        }

        /// <summary>
        /// uploads the temporary zip folder to dropox
        /// </summary>
        private void uploadToDropbox()
        {
            throw new NotImplementedException();
            
        }


        /// <summary>
        /// move zip archive to download folder
        /// </summary>
        private void copyToDownloadFolder()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// create email task and attach zip archive
        /// </summary>
        private void sendEmail()
        {
            throw new NotImplementedException();
        }
        private void generateGraph(SpectraGalleryViewModel item)
        {
            throw new NotImplementedException();
        }

        private string getValues(SpectraGalleryViewModel item)
        {
            string values = "";
            foreach (var v in typeof(Models.spectra).GetProperties())
            {
                values += string.Format(",{0}", v.GetValue(item));
            }
            return values.Substring(1);
        }

        private string getHeader()
        {
            string header = "";
            foreach(var item in typeof(Models.spectra).GetProperties())
            {
                header += string.Format(",{0}", item.Name);
            }
            return header.Substring(1);
        }

        #endregion
    }

    /// <summary>
    /// View model to display a spectra in the gallery. The view is imutable and displays the spectral graph 
    /// or a thumb of an exported image of it, the name and description and external id and if it is selected for export.
    /// </summary>
    public class SpectraGalleryViewModel
    {
        public Models.spectra Spectra { get; set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public bool Selected { get; set; }

        public SpectraGalleryViewModel(Models.spectra spectra):base()
        {

        }

    }
}
