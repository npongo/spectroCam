using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Dropbox.Api;
using System.IO;
using System.Threading.Tasks;
using Dropbox.Api.Files;
using Microsoft.WindowsAzure.MobileServices;
using Windows.Storage;

namespace spectra.camera.Helpers
{
    //public static class ModelToCSV
    //{
    //    static StorageFolder dataExport = Windows.Storage.KnownFolders.RemovableDevices;

    //    public static Models.SorghumTrailContext Context{get; set;}

    //    public static async Task<bool> ExportDatabase(string deviceId)
    //    {

    //        StorageFolder sdCard = (await dataExport.GetFoldersAsync()).FirstOrDefault();
    //        if (sdCard != null)
    //        {
    //            return false;
    //        }

    //        var folder = await sdCard.TryGetItemAsync("DataExport") as StorageFolder;

    //        if (folder == null)
    //        {
    //            await sdCard.CreateFolderAsync("DataExport");
    //            folder = await sdCard.GetFolderAsync("DataExport") as StorageFolder;
    //        }

    //        string file = "localstore.db";
    //        DateTime now = DateTime.Now;
    //        string fileName = string.Format("{0}{1}{2}{3}{4}{5}.db", deviceId, now.Year, now.Month, now.Day, now.Hour, now.Minute);
    //        Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
    //        Windows.Storage.StorageFile dbFile = await storageFolder.GetFileAsync(file);
          
    //        await dbFile.CopyAsync(sdCard, fileName, NameCollisionOption.ReplaceExisting);



    //        bool result = true;
    //        string fileDateSuffix = DateTime.Now.ToString("yyyyMMddhhmm") + ".csv";
          
    //        string content = csvModdels<Models.chlorophyll>(Context.chlorophylls.Select(c=> c));
    //        result &= await SaveCSV("chlorophylls" + fileDateSuffix, content);
             
    //        content = csvModdels<Models.soilMoisture>(Context.soilMoistures.Select(c => c));
    //        result &= await SaveCSV("soilMoistures" + fileDateSuffix, content);

    //        content = csvModdels<Models.spectrometer>(Context.spectrometers.Select(c => c));
    //        result &= await SaveCSV("spectrometers" + fileDateSuffix, content);
             

    //        content = csvModdels<Models.plant>(Context.plants.Select(c => c));
    //        result &= await SaveCSV("plants" + fileDateSuffix, content);

    //        content = csvModdels<Models.pointLocation>(Context.pointLocations.Select(c => c));
    //        result &= await SaveCSV("pointLocations" + fileDateSuffix, content);

             

    //        content = csvModdels<Models.plot>(Context.plots.Select(c => c));
    //        result &= await SaveCSV("plots" + fileDateSuffix, content);
             

    //        return result;
    //    }


    //    public static async Task<bool> SaveCSV(string fileName, string content)
    //    {
    //        // Get the first child folder, which represents the SD card.
    //        StorageFolder sdCard = (await dataExport.GetFoldersAsync()).FirstOrDefault();

            

    //        if (sdCard != null)
    //        {
    //            return false;
    //        }
          
    //        var folder = await sdCard.TryGetItemAsync("DataExport") as StorageFolder;

    //        if (folder == null)
    //        {
    //            await sdCard.CreateFolderAsync("DataExport");
    //            folder = await sdCard.GetFolderAsync("DataExport")  as StorageFolder; 
    //        }

    //        var file = await folder.CreateFileAsync(fileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);

    //        await Windows.Storage.FileIO.WriteTextAsync(file, content);

    //        return true;
    //    }

    //    public static string csvModdels<T>(IEnumerable<T> entities) 
    //    {
    //        string csv = "";
    //        foreach(var item in entities)
    //        { 
                
    //            csv += recordCSV(item as Models.ModelBase) + "\r\n";
    //        }

    //        return csv;
    //    }
        
    //    public static string recordCSV(Models.ModelBase model)
    //    {
    //        string record = ""; 
    //        var props = model.GetType().GetProperties();
    //        foreach(var prop in props.OrderBy(p=>p.Name))
    //        {
    //            if (prop.Name == "Errors") continue; 
    //            record += (prop.GetValue(model)??"NULL").ToString() + ",";
    //        }
    //        return record.Substring(0, record.Length - 1);
    //    }

    //}

    public class DropBoxBackup
    {
        DropboxClient dbx;

        public DropBoxBackup()
        {
            //TODO change!!!
            dbx = new DropboxClient("tfH_mBN-W2EAAAAAAAAsI6ZeRdsRDyTvQHEhjPPiUrSWAECALf1YNM-ZhmONQx39");  

        }

        public async Task<Exception> UploadDatabase(string deviceId,string prefixName)
        {
            try
            {
                DateTime now = DateTime.Now;
                string file = "localstore.db";
                string fileName = string.Format("{0}{1}{2}{3}{4}{5}{6}.db", prefixName, deviceId, now.Year, now.Month, now.Day, now.Hour, now.Minute);
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile dbFile = await storageFolder.GetFileAsync(file);
                await dbFile.CopyAsync(storageFolder, fileName,NameCollisionOption.ReplaceExisting);

                dbFile = await storageFolder.GetFileAsync(fileName);
                var f = await dbFile.OpenAsync(FileAccessMode.Read); 

                using (var stream = f.AsStreamForRead() )
                {
                    //var commitInfo = new CommitInfo("/" + fileName);
                    //var result =  await dbx.Files.UploadAsync(commitInfo, stream);

                    var updated = await dbx.Files.UploadAsync(
                        "/" + fileName,
                        WriteMode.Overwrite.Instance,
                        body: stream);

                   
                     
                }

                return null;
            }
            catch(Exception ex)
            {
                return ex;
            }
        }
    }
}
