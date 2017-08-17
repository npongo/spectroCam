
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using spectra.camera.Models;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace spectral.camera.tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                string path = @"C:\Users\npongo\Downloads\ModelTables.sql";
                object[] models = new object[] { new project(), new spectra.camera.Models.spectra() };
                string[] exemptList = new string[] { "AllErrors", "HasErrors", "IsValidationEnabled", "Errors" };

                string sqlScript = "";

                foreach (var obj in models)
                {
                    string tableSqlDef = string.Format("CREATE TABLE {0}(\r\n", obj.GetType().Name);
                    foreach (var prop in obj.GetType().GetProperties().Where(x => !exemptList.Contains(x.Name)))
                    {
                        tableSqlDef += string.Format("{0},\r\n", mapClrTypeToSqlType(prop));
                    }
                    tableSqlDef += ")";
                    sqlScript += tableSqlDef;
                }

                Debug.Write(sqlScript);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async System.Threading.Tasks.Task SaveScript(string sqlScript)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.DefaultFileExtension = ".sql";

            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.Downloads;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("sql script", new List<string>() { ".sql" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = string.Format("ModelTables.sql");

            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until
                // we finish making changes and call CompleteUpdatesAsync.
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                // write to file
                await Windows.Storage.FileIO.WriteTextAsync(file, sqlScript);
                // Let Windows know that we're finished changing the file so
                // the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                Windows.Storage.Provider.FileUpdateStatus status =
                    await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);


                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    Debug.WriteLine("File " + file.Name + " was saved.");
                }
                else
                {
                    Debug.WriteLine("File " + file.Name + " couldn't be saved.");
                }
            }
            else
            {
                Debug.WriteLine("File " + file.Name + " couldn't be saved.");
            }
        }

        private string mapClrTypeToSqlType(PropertyInfo prop)
        {
            var required = prop.GetCustomAttribute(typeof(RequiredAttribute));
            switch(prop.PropertyType.Name)
            {
                case "double":
                    return string.Format("[{1}] float {0}",(required!=null?"NOT NULL":"NULL"),prop.Name);
                case "int":
                    return string.Format("[{1}] int {0}", (required != null ? "NOT NULL" : "NULL"), prop.Name);
                case "Int16":
                    return string.Format("[{1}] smallint {0}", (required != null ? "NOT NULL" : "NULL"), prop.Name);
                case "Int32":
                    return string.Format("[{1}] int {0}", (required != null ? "NOT NULL" : "NULL"), prop.Name);
                case "Int64":
                    return string.Format("[{1}] bigint {0}", (required != null ? "NOT NULL" : "NULL"), prop.Name);
                case "byte":
                    return string.Format("[{1}] tinyint {0}", (required != null ? "NOT NULL" : "NULL"), prop.Name);
                case "Boolean":
                    return string.Format("[{1}] bit {0}", (required != null ? "NOT NULL" : "NULL"), prop.Name);
                case "bool":
                    return string.Format("[{1}] bit {0}", (required != null ? "NOT NULL" : "NULL"), prop.Name);
                case "DateTime":
                    return string.Format("[{1}] datetime {0}", (required != null ? "NOT NULL" : "NULL"), prop.Name);
                case "DateTimeOffset":
                    return string.Format("[{1}] datetimeoffset(7) {0}", (required != null ? "NOT NULL" : "NULL"), prop.Name);
                case "String":
                    if(prop.Name == "Version") return "[Version] timestamp NOT NULL";

                    if (prop.Name == "Id") return "[Id] nvarchar(36) NOT NULL";

                    var ml1 = prop.GetCustomAttribute(typeof(MaxLengthAttribute));
                    string l1 = ml1 == null ? "< missing maxlength attribute >" : ((MaxLengthAttribute)ml1).Length.ToString();
                    return string.Format("[{2}] nvarchar({0}) {1}", l1, required, prop.Name);
                case "string":
                    var ml = prop.GetCustomAttribute(typeof(MaxLengthAttribute)); 
                    string l = ml == null ? "< missing maxlength attribute >" : ((MaxLengthAttribute)ml).Length.ToString();
                    return string.Format("[{2}] nvarchar({0}) {1}",l,required,prop.Name);
                case "Nullable`1":
                    switch (prop.PropertyType.ToString())
                    {
                        
                        case "System.Nullable`1[System.Double]":
                            return string.Format("{0} float NULL", prop.Name);
                        case "System.Nullable`1[System.Int16]":
                            return string.Format("{0} smallint NULL", prop.Name);
                        case "System.Nullable`1[System.Int32]":
                            return string.Format("{0} int NULL", prop.Name);
                        case "System.Nullable`1[System.Int64]":
                            return string.Format("{0} bigint NULL", prop.Name);
                        case "System.Nullable`1[System.Byte]":
                            return string.Format("{0} tinyint NULL", prop.Name);
                        case "System.Nullable`1[System.Boolean]":
                            return string.Format("{0} bit NULL", prop.Name);
                        case "System.Nullable`1[System.DateTimeOffset]":
                            return string.Format("{0} DateTimeOffset(7)", prop.Name);
                        case "System.Nullable`1[System.DateTime]":
                            return string.Format("{0} DateTime", prop.Name);
                        default: 
                            Debug.WriteLine(prop.PropertyType.ToString());
                            break;
                             
                    }
                    break;

            }

            return string.Format("Type not defined({0})", prop.PropertyType.Name);
        }
    }
}
