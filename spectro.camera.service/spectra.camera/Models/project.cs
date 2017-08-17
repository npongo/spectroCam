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

namespace spectra.camera.Models
{
    public class project: ExternalIdentifiedBase
    {
        public project() : base()
        {

        }

        public override string ToString()
        {
            return "Project";
        }

        #region ISelfValidate
        public override IList<string> Validate()
        {
            List<string> errors = new List<string>();

            //define more error here and add to list of errors.

            return errors.Union(base.Errors.Errors.SelectMany(s => s.Value)).ToList();
        }
        #endregion


        Int32 _ProjectId;
        [Column(Order = 1)]
        [Display(Description = "Specta Id")]
        [Required()]
        public Int32 ProjectId
        {
            get { return _ProjectId; }
            set
            {
                if (value != _ProjectId)
                {
                    _ProjectId = value;
                    RaisePropertyChanged("ProjectId");
                }
            }
        }

        string _ProjectProtocols;
        
        [Display(Description = "Project protocols")]
        [MaxLength(256)]
        public string ProjectProtocols
        {
            get { return _ProjectProtocols; }
            set
            {
                if (value != _ProjectProtocols)
                {
                    _ProjectProtocols = value;
                    RaisePropertyChanged("ProjectProtocols");
                }
            }
        }

        string _ProjectTags;

        [Display(Description = "Project Tags")]
        [MaxLength(256)]
        public string ProjectTags
        {
            get { return _ProjectTags; }
            set
            {
                if (value != _ProjectTags)
                {
                    _ProjectTags = value;
                    RaisePropertyChanged("ProjectTags");
                }
            }
        }

        string _SpectralIndexes;

        [Display(Description = "Spectral Indexes")]
        [MaxLength(1024)]
        public string SpectralIndexes
        {
            get { return _SpectralIndexes; }
            set
            {
                if (value != _SpectralIndexes)
                {
                    _SpectralIndexes = value;
                    RaisePropertyChanged("SpectralIndexes");
                }
            }
        }
    }
}
