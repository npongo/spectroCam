
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Prism.Windows.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;

namespace spectra.camera.Models
{
    public interface ITimestamp
    {
        DateTimeOffset Timestamp { get; set; }
    }

    public interface ILocated
    {
        double? Latitude { get; set; }
        double? Longitude { get; set; }
    }
    public interface IModel : INotifyPropertyChanged 
    {
        [Key]
        [MaxLength(36)]
        string Id { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
        bool Deleted { get; set; }
        void RebindAll();
    }

    public interface ISelfValidate
    {
        IList<string> Validate();
        ObservableCollection<string> AllErrors { get; }
        bool HasErrors { get; }
    }

    public abstract class SelfValidatableBindable : ValidatableBindableBase, ISelfValidate
    {

        public abstract IList<string> Validate();

        [JsonIgnoreAttribute()]
        public ObservableCollection<string> AllErrors
        {
            get { return new ObservableCollection<string>(Validate()); }
        }


        [JsonIgnoreAttribute()]
        public bool HasErrors
        {
            get { return Validate().Count > 0; }
        }

        public void RebindAll()
        {
            RaisePropertyChanged("");
        }
    }

    public abstract class ModelBase : SelfValidatableBindable, IModel
    {  

        string _Id;
        [Key]
        [Required()] 
        public virtual string Id
        {
            get { return _Id; }
            set
            {
                if (value != _Id & value != null)
                {  
                    _Id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }

        DateTimeOffset _CreatedAt;
        [CreatedAt]
        public virtual DateTimeOffset CreatedAt  
        {
            get { return _CreatedAt; }
            set
            {
                if (value != _CreatedAt)
                { 
                    _CreatedAt = value;
                    RaisePropertyChanged("_CreatedAt");
                }
            }
        }

        DateTimeOffset _UpdatedAt;
        [UpdatedAt]
        public virtual DateTimeOffset UpdatedAt
        {
            get { return _UpdatedAt; }
            set
            {
                if (value != _UpdatedAt)
                { 
                    _UpdatedAt = value;
                    RaisePropertyChanged("_UpdatedAt");
                }
            }
        }

        bool _Deleted;
        [Deleted]
        public virtual bool Deleted
        {
            get { return _Deleted; }
            set
            {
                if (value != _Deleted)
                { 
                    _Deleted = value;
                    RaisePropertyChanged("Deleted");
                }
            }
        }

        [Version]
        public virtual string Version { get; set; }

        public ModelBase()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTimeOffset.Now;
            UpdatedAt = DateTimeOffset.Now;
            Deleted = false;
        }

        


    }

    public abstract class ExternalIdentifiedBase:ModelBase,ILocated,ITimestamp
    {
        public ExternalIdentifiedBase() : base()
        {
            Timestamp = DateTimeOffset.Now;
        }

        string _ExternalIdentifier;
        [Display(Description = "ExternalIdentifier")]
        [MaxLength(128)]
        [Column(Order = 2)]
        public string ExternalIdentifier
        {
            get { return _ExternalIdentifier; }
            set
            {
                if (value != _ExternalIdentifier)
                {
                    _ExternalIdentifier = value;
                    RaisePropertyChanged("ExternalIdentifier");
                }
            }
        }

        string _Name;
        [Display(Description = "Name")]
        [MaxLength(128)]
        [Column(Order = 3)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        string _Description;
        [Display(Description = "Description")]
        [MaxLength(128)]
        [Column(Order = 4)]
        public string Description
        {
            get { return _Description; }
            set
            {
                if (value != _Description)
                {
                    _Description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }

      

        double? _Latitude;
        [Column(Order = 6)]
        [Display(Description = "Latitude")]
        public double? Latitude
        {
            get { return _Latitude; }
            set
            {
                if (value != _Latitude)
                {
                    _Latitude = value;
                    RaisePropertyChanged("Latitude");
                }
            }
        }

        double? _Longitude;
        [Column(Order = 7)]
        [Display(Description = "Longitude")]
        public double? Longitude
        {
            get { return _Longitude; }
            set
            {
                if (value != _Longitude)
                {
                    _Longitude = value;
                    RaisePropertyChanged("Longitude");
                }
            }
        }

        DateTimeOffset _Timestamp;
        [Column(Order = 8)]
        [Display(Description = "Timestamp")]
        [Required]
        public DateTimeOffset Timestamp
        {
            get { return _Timestamp; }
            set
            {
                if (value != _Timestamp)
                {
                    _Timestamp = value;
                    RaisePropertyChanged("Timestamp");
                }
            }
        }



    }


}
