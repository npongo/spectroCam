
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.UI.Popups;

namespace spectra.camera.Models
{
    public class SpectraContext
    {
        public SpectraContext(string ServiceUrl)
        {
            MobileService = new MobileServiceClient(ServiceUrl);
            projectTable = MobileService.GetSyncTable<project>();
            spectraTable = MobileService.GetSyncTable<spectra>();
        }

        public SpectraContext():this("http://spectraCamservice.azurewebsites.net")
        { 
             
        }

        private IMobileServiceSyncTable<project> projectTable;
        private MobileServiceCollection<project, project> _projects;
        public MobileServiceCollection<project, project> projects
        {
            get
            {
                return _projects;
            }
        }

        private IMobileServiceSyncTable<spectra> spectraTable;
        private MobileServiceCollection<spectra, spectra> _spectras;
        public MobileServiceCollection<spectra, spectra> spectras
        {
            get
            {
                return _spectras;
            }
        }

        bool _initialized = false;

        public async Task Initialize()
        {
            if (_initialized) return;

            await this.InitLocalStoreAsync();
            await this.LoadData();

            _initialized = true;
        }



        MobileServiceUser _user;
        public MobileServiceUser User { get { return _user; } }

        //TODO
        public static MobileServiceClient MobileService;

        private async System.Threading.Tasks.Task<bool> AuthenticateAsync()
        {
            string message;
            bool success = false;

            // This sample uses the Facebook provider.
            var provider = "Facebook";

            // Use the PasswordVault to securely store and access credentials.
            PasswordVault vault = new PasswordVault();
            PasswordCredential credential = null;

            try
            {
                // Try to get an existing credential from the vault.
                credential = vault.FindAllByResource(provider).FirstOrDefault();
            }
            catch (Exception)
            {
                // When there is no matching resource an error occurs, which we ignore.
            }

            if (credential != null)
            {
                // Create a _user from the stored credentials.
                _user = new MobileServiceUser(credential.UserName);
                credential.RetrievePassword();
                _user.MobileServiceAuthenticationToken = credential.Password;

                // Set the _user from the stored credentials.
                MobileService.CurrentUser = _user;

                // Consider adding a check to determine if the token is 
                // expired, as shown in this post: http://aka.ms/jww5vp.

                success = true;
                message = string.Format("Cached credentials for User - {0}", _user.UserId);
            }
            else
            {
                try
                {
                    // Login with the identity provider.

                    _user = await MobileService.LoginAsync(MobileServiceAuthenticationProvider.Facebook, true);

                    // Create and store the _user credentials.
                    credential = new PasswordCredential(provider,
                        _user.UserId, _user.MobileServiceAuthenticationToken);
                    vault.Add(credential);

                    success = true;
                }
                catch (MobileServiceInvalidOperationException)
                {
                    message = "You must log in. Login Required";
                }
            }
            message = string.Format("You are now logged in - {0}", _user.UserId);
            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand("OK"));
            await dialog.ShowAsync();

            return success;
        }



        public static IModel newEntity(string name)
        {

            switch (name)
            {
                case "Ordered Observable Unit":
                    return new project();

                case "Measurement Protocol":
                    return new spectra();
            }

            return null;
        }

        public async Task AddEntity(IModel data)
        {

            if (data is project)
            {
                await AddProject((project)data);
                return;
            }

            if (data is spectra)
            {
                await AddSpectra((spectra)data);
                return;
            }
        }

        public async Task SaveChanges(IModel data)
        {

            if (data is project)
            {
                await UpdateProject((project)data);
                return;
            }

            if (data is spectra)
            {
                await UpdateSpectra((spectra)data);
                return;
            }
            
            //await MobileService.SyncContext.PushAsync();
        }

        public async Task DeleteEntity(IModel data)
        {

            if (data is project)
            {
                await RemoveProject((project)data);
                return;
            }


            if (data is spectra)
            {
                await RemoveSpectra((spectra)data);
                return;
            }

           // await MobileService.SyncContext.PushAsync();

        }
        
        public async Task AddProject(project project)
        {
            await projectTable.InsertAsync(project);
            projects.Add(project);
        }
        public async Task RemoveProject(project project)
        {
            await projectTable.DeleteAsync(project);
            if (projects.Contains(project))
                projects.Remove(project);
        }
        public async Task UpdateProject(project project)
        {
            await projectTable.UpdateAsync(project);
        }


        public async Task AddSpectra(spectra spectra)
        {
            await spectraTable.InsertAsync(spectra);
            spectras.Add(spectra);
        }
        public async Task RemoveSpectra(spectra spectra)
        {
            await spectraTable.DeleteAsync(spectra);
            if (spectras.Contains(spectra))
                spectras.Remove(spectra);
        }
        public async Task UpdateSpectra(spectra spectra)
        {
            await spectraTable.UpdateAsync(spectra);
        }
        
        public async Task LoadData()
        {
            try
            {
                //if (HasConnection())
                //    await MobileService.SyncContext.PushAsync();
                
                _spectras = await spectraTable.ToCollectionAsync();

                _projects = await projectTable.ToCollectionAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task InitLocalStoreAsync()
        {
            await InitLocalStoreAsync("localstore.db");
        }

        public async Task InitLocalStoreAsync(string dbFile)
        {
            if (!MobileService.SyncContext.IsInitialized)
            {
                var store = new MobileServiceSQLiteStore(dbFile);
                

                store.DefineTable<spectra>();

                store.DefineTable<project>();
                
                await MobileService.SyncContext.InitializeAsync(store);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SyncAsync()
        {
            try
            {
                if (!HasConnection())
                    return false;

                await MobileService.SyncContext.PushAsync();

                await projectTable.PullAsync("project", projectTable.CreateQuery());

                await spectraTable.PullAsync("spectra", spectraTable.CreateQuery());

               
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> PushChanges()
        {
            try
            {
                if (!HasConnection())
                    return false;

                await MobileService.SyncContext.PushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool HasConnection()
        {
            try
            {
                var connection = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
                if (connection == null)
                    return false;
                return true; // (connection.IsWlanConnectionProfile | connection.IsWwanConnectionProfile);
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            MobileService.Dispose();
        }

    }
}
