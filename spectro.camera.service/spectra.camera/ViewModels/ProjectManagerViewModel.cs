

using Microsoft.WindowsAzure.MobileServices;
using Prism.Windows.Validation;
using spectra.camera.Helpers;
using spectra.camera.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;

namespace spectra.camera.ViewModels
{
    public abstract class ViewModelBase: ValidatableBindableBase
    {
        protected SpectraContext _context;
        protected Geolocator _geolocator;

        public ViewModelBase(SpectraContext context, Geolocator geolocator)
        {
            _context = context;
            _geolocator = geolocator;

        }

        #region Show exceptions

        protected async Task ShowException(Exception ex)
        {
            string message = string.Format(ex.Message);
            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand("OK"));
            await dialog.ShowAsync();
        }


        #endregion


        #region GPS


        protected Geopoint _CurrentPosition;

        private async void Geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            try
            {

                _CurrentPosition = new Geopoint(new BasicGeoposition()
                {
                    Latitude = args.Position.Coordinate.Point.Position.Latitude,
                    Longitude = args.Position.Coordinate.Point.Position.Longitude
                });

            }
            catch (Exception ex)
            {

                await ShowException(ex);
            }
        }


        protected GeolocationAccessStatus _geoStatus = GeolocationAccessStatus.Unspecified;

        public async Task SetGPSStatus()
        {
            try
            {
                _geoStatus = await Geolocator.RequestAccessAsync();
                if(_geoStatus == GeolocationAccessStatus.Allowed)
                    _geolocator.PositionChanged += Geolocator_PositionChanged;
               
            }
            catch (Exception ex)
            {
                await ShowException(ex);
                _geoStatus = GeolocationAccessStatus.Unspecified;
            }
            finally
            {
                RaisePropertyChanged("EnbleCurrentLocation");
                RaisePropertyChanged("GPSCanExecute");
            }
        }

        public bool EnbleCurrentLocation
        {
            get { return _geoStatus == GeolocationAccessStatus.Allowed; }
        }

        #endregion



    }

    public abstract class DataViewModelBase<T>: ViewModelBase where T: ModelBase, new()
    {
        protected string _deleteMessage;
        protected string _saveMessage;

        public DataViewModelBase(SpectraContext context, Geolocator geolocator,Action<string> startWaitAction, Action stopWaitAction) : base(context, geolocator)
        {
            StartWaitAction = startWaitAction;
            StopWaitAction = stopWaitAction;
            _deleteMessage = "Save Changes?";
            _saveMessage = "Delete record?";
            Saved = true;
        }

        protected Action<string> StartWaitAction;
        protected Action StopWaitAction;
        public bool Saved { get; set; }

        protected void OnSavablePropertyChanged(string propteryName)
        {
            Saved = false;
            RaisePropertyChanged("Saved");
            RaisePropertyChanged("SaveCommand");
            this.RaisePropertyChanged(propteryName);
        }

        T _Data;
        public T Data
        {
            get { return _Data; }
            set
            {
                if(value != _Data)
                {

                    if (_Data != null) _Data.PropertyChanged -= Data_PropertyChanged;
                    _Data = value;
                    if(_Data != null) _Data.PropertyChanged += Data_PropertyChanged;

                }
            }
        }

        private void Data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnSavablePropertyChanged(e.PropertyName);   
        }


        #region CRUD commands

        protected bool last = false;
        protected bool first = false;

        protected abstract Task PreviousExecute();
        protected abstract Task NextExecute();

        public ICommand NextCommand
        {
            get { return new AsyncRelayCommand(NextExecute, NextCanExecute); }
        }

        protected virtual bool NextCanExecute()
        {
            return !last;
        }
        
        public ICommand PreviousCommand
        {
            get { return new AsyncRelayCommand(PreviousExecute, PreviousCanExecute); }
        }

        protected virtual bool PreviousCanExecute()
        {
            return !first;
        }
        
        public ICommand SaveCommand
        {
            get { return new Helpers.AsyncRelayCommand(SaveExecute, SaveCanExecute); }
        }
        protected virtual async Task SaveExecute()
        {
            try
            {
                await _context.SaveChanges(Data);
                Saved = true;
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }
        bool SaveCanExecute()
        {
            return !Saved;
        }

        public async Task SaveIfNotSaved()
        {
            try
            {
                MessageDialog msg = new MessageDialog(_saveMessage, "SAVE");
                msg.Commands.Add(new UICommand("Save", async (s) =>
                {
                    await _context.SaveChanges(Data);
                    Saved = true;
                }));
                msg.Commands.Add(new UICommand("Cancel", (s) =>
                {
                    return;
                }));

                await msg.ShowAsync();

            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        public ICommand DeleteCommand
        {
            get { return new Helpers.AsyncRelayCommand(DeleteExecute, DeleteCanExecute); }
        }
        protected virtual async Task DeleteExecute()
        {
            try
            {
                MessageDialog msg = new MessageDialog(_deleteMessage, "DELETE");
                msg.Commands.Add(new UICommand("Delete", async (s) =>
                {
                    await _context.DeleteEntity(Data);
                    Saved = true;
                    await PreviousExecute();
                }));
                msg.Commands.Add(new UICommand("Cancel", (s) =>
                {
                    return;
                }));

                await msg.ShowAsync();
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }
        protected virtual bool DeleteCanExecute()
        {
            return Data != null;
        }

        public ICommand NewCommand
        {
            get { return new Helpers.AsyncRelayCommand(NewExecute, NewCanExecute); }
        }
        public virtual async Task NewExecute()
        {
            try
            {
                if (Data != null && !Saved) await SaveIfNotSaved();

                var entity = new T();
                Data = entity;
                await setGPSPoint();
                await _context.AddEntity(entity);
                last = true;
                RaisePropertyChanged("");

            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }
        protected virtual bool NewCanExecute()
        {
            return true;
        }

        #endregion

        #region gps

        public ICommand GPSUpdateCommand
        {
            get { return new AsyncRelayCommand<string>(GPSUpdateExecute, GPSUpdateCanExecute); }
        }

        async Task GPSUpdateExecute(string coord)
        {

            try
            {
                await avgGPSPoint(10);
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        public int AvgPts { get; private set; }

        protected async Task avgGPSPoint(int points)
        {
            AvgPts = 0;
            RaisePropertyChanged("AvgPts");
            uint interval = base._geolocator.ReportInterval;
            base._geolocator.ReportInterval = 500;

            List<Geoposition> pts = new List<Geoposition>();
            try
            {
                if (base._geoStatus == GeolocationAccessStatus.Allowed & Data is ILocated)
                {
                    while (pts.Count < points)
                    {
                        var position = _geolocator.GetGeopositionAsync(new TimeSpan(0), new TimeSpan(0, 5, 0));

                        var pos = await position;
                        pts.Add(pos);

                    }

                    AvgPts++;

                    ((ILocated)Data).Latitude = pts.Average(p => p.Coordinate.Point.Position.Latitude);
                    ((ILocated)Data).Longitude = pts.Average(p => p.Coordinate.Point.Position.Longitude);

                    RaisePropertyChanged("Latitude");
                    RaisePropertyChanged("Longitude");
                    RaisePropertyChanged("AvgPts");

                }

                _geolocator.ReportInterval = interval;
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }


        bool GPSUpdateCanExecute(string coord)
        {
            return true;
        }
        #endregion


        #region GPS stamp

        public int NumPtsToAvg { get; set; }

        public ICommand GPSTimeStampCommand
        {
            get { return new AsyncRelayCommand(GPSTimeStampExecute, GPSTimeStampCanExecute); }
        }

        public async Task GPSTimeStampExecute()
        {
            try
            {
                if (Data is ITimestamp)
                    ((ITimestamp)Data).Timestamp = DateTimeOffset.Now;

                if (Data is ILocated)
                    await avgGPSPoint(NumPtsToAvg); // setGPSPoint();
            }
            catch (Exception ex)
            {
                await base.ShowException(ex);
            }
        }

        bool GPSTimeStampCanExecute()
        {
            return true;
        }


        protected async Task setGPSPoint()
        {
            try
            {
                if (_geoStatus == GeolocationAccessStatus.Allowed & Data is ILocated)
                {
                    var position = _geolocator.GetGeopositionAsync(new TimeSpan(0), new TimeSpan(0, 5, 0));

                    var pos = await position;
                    ((ILocated)Data).Latitude = pos.Coordinate.Point.Position.Latitude;
                    ((ILocated)Data).Longitude = pos.Coordinate.Point.Position.Longitude;
                }
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        #endregion

    }


    public class projectSummary:INotifyPropertyChanged
    {
        public projectSummary(project pj, int specCnt)
        {
            Project = pj;
            SpectraCount = specCnt;
            pj.PropertyChanged += Pj_PropertyChanged;
        }

        private void Pj_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) PropertyChanged(this, e);
        }

        public project Project { get; set; }
        public string Id
        {
            get { return Project.Id; }
        }
        public string Name
        {
            get { return Project.Name; }
        }
        public string Description
        {
            get { return Project.Description; }
        }
        public int SpectraCount { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
    }
    public class ProjectManagerViewModel : DataViewModelBase<project>
    {

        public MobileServiceCollection<project,project> Projects
        {
            get { return _context.projects; }
        }

        public ObservableCollection<projectSummary> ProjectSummaries { get; private set; }

        public projectSummary SelectedProject { get; set; }

        public ProjectManagerViewModel(SpectraContext context, Geolocator geolocator, Action<string> startWaitAction, Action stopWaitAction) : base(context, geolocator, startWaitAction, stopWaitAction)
        {
            ProjectSummaries = new ObservableCollection<projectSummary>();
            IsEdit = false;
            base._deleteMessage = "Delete the project and all its associated spectra?";
        }


        public async Task initalizeProjectManager()
        {
            foreach (var p in _context.projects)
            {
                int scnt = _context.spectras.Count(s => s.ProjectIdDTO == p.Id);
                var ps =  ProjectSummaries.FirstOrDefault(i => i.Id == p.Id);
                if (ps == null)
                {
                    var pj = new projectSummary(p, scnt);
                    ProjectSummaries.Add(pj);
                }
                else
                {
                    ps.SpectraCount = scnt;
                }
            }

            if (ProjectSummaries.Count == 0)
                await NewExecute();
        }

        #region

        protected override async Task NextExecute()
        {
            try
            {
                if (Data != null && !Saved) await SaveIfNotSaved();
                var p = _context.projects.Where(d => ((ITimestamp)d).Timestamp > ((ITimestamp)Data).Timestamp).OrderBy(d => d.Timestamp).FirstOrDefault(); ;
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
                var p = _context.projects.Where(d => ((ITimestamp)d).Timestamp < ((ITimestamp)Data).Timestamp).OrderByDescending(d => d.Timestamp).FirstOrDefault();
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

        
       
        public ICommand SearchCommand
        {
            get { return new AsyncRelayCommand<Windows.UI.Xaml.Controls.SearchBoxQueryChangedEventArgs>(SearchExecute, SearchCanExecute); }
        }

        public async Task SearchExecute(Windows.UI.Xaml.Controls.SearchBoxQueryChangedEventArgs searchTerm)
        {
            try
            {
                ProjectSummaries.Sort(s => levensteinSort(s.Name, searchTerm.QueryText));// = new ObservableCollection<projectSummary>(ProjectSummaries.OrderBy(p => levensteinSort(p.Name, searchTerm.QueryText))); 
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        private double levensteinSort(string n1, string n2)
        {
            if (string.IsNullOrWhiteSpace(n1) | string.IsNullOrWhiteSpace(n2)) return 1;
            double result =  1-npongo.wpf.simMetrics.StringMetricFunctions.Levenstein(n1.ToUpper(), n2.ToUpper());
            System.Diagnostics.Debug.WriteLine("{0},{1},{2}", n1, n2, result);
            return result;
        }

        bool SearchCanExecute(Windows.UI.Xaml.Controls.SearchBoxQueryChangedEventArgs searchTerm)
        {
            return true;
        }

        public ICommand StartSpectraCollection
        {
            get { return new AsyncRelayCommand<projectSummary>(StartSpectraCollectionExcute, StartSpectraCollectionCanExecute); }
        }


        async Task StartSpectraCollectionExcute(projectSummary project)
        {
            try
            {
                await App.VM.StartSpectralCollation(project);
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool StartSpectraCollectionCanExecute(projectSummary project)
        {
            return true;
        }

        public ICommand StartSpectraGallery
        {
            get { return new AsyncRelayCommand<projectSummary>(StartSpectraGalleryExcute, StartSpectraGalleryCanExecute); }
        }


        async Task StartSpectraGalleryExcute(projectSummary project)
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

        bool StartSpectraGalleryCanExecute(projectSummary project)
        {
            return true;
        }


        public bool IsEdit { get; set; }

        public ICommand EditProjectCommand
        {
            get { return new AsyncRelayCommand<projectSummary>(EditProjectExcecute, EditProjectCanExecute); }
        }

        async Task EditProjectExcecute(projectSummary project)
        {
            try
            {
                var pj = _context.projects.FirstOrDefault(p => p.Id == project.Id);
                if (pj == null) throw new Exception("project does not exist");
                Data = pj;
                IsEdit = true;
                RaisePropertyChanged("Data");
                RaisePropertyChanged("IsEdit");
                RaisePropertyChanged("PreviousCommand");
                RaisePropertyChanged("NextCommand");
            }
            catch(Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool EditProjectCanExecute(projectSummary project)
        {
            return true;
        }

        public ICommand CloseEditProjectCommand
        {
            get { return new AsyncRelayCommand(CloseEditProjectExcecute, CloseEditProjectCanExecute); }
        }

        async Task CloseEditProjectExcecute()
        {
            try
            {
                if (!Saved) await SaveIfNotSaved();

                IsEdit = false;
                RaisePropertyChanged("Data");
                RaisePropertyChanged("IsEdit");
                RaisePropertyChanged("PreviousCommand");
                RaisePropertyChanged("NextCommand");
            }
            catch (Exception ex)
            {
                await ShowException(ex);
            }
        }

        bool CloseEditProjectCanExecute()
        {
            return true;
        }

        public override async Task NewExecute()
        {
            await base.NewExecute();
            projectSummary ps = new projectSummary(Data, 0);
            ProjectSummaries.Add(ps);
            IsEdit = true;
            RaisePropertyChanged("Data");
            RaisePropertyChanged("IsEdit");
            RaisePropertyChanged("PreviousCommand");
            RaisePropertyChanged("NextCommand");
        }

        protected override async Task DeleteExecute()
        {
            try
            {
                var item = ProjectSummaries.FirstOrDefault(t => t.Project == Data);
                if (item != null) ProjectSummaries.Remove(item);
                foreach (var spectra in _context.spectras.Where(s => s.ProjectIdDTO == Data.Id))
                    await _context.RemoveSpectra(spectra);
                await base.DeleteExecute();
            }
            catch(Exception ex)
            {
                await ShowException(ex);
            }
        }

        protected override bool NextCanExecute()
        {
            return IsEdit & base.NextCanExecute();
        }

        protected override bool PreviousCanExecute()
        {
            return IsEdit & base.PreviousCanExecute();
        }


        //TODO add command to open up the project galloryr 
        #endregion
    }
}
