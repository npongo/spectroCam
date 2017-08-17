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
    
    public class spectra : ExternalIdentifiedBase
    {
        public spectra() : base()
        {

        }
        public override string ToString()
        {
            return "Spectrometer Measurement";
        }

        #region ISelfValidate
        public override IList<string> Validate()
        {
            List<string> errors = new List<string>();

            //define more error here and add to list of errors.

            return errors.Union(base.Errors.Errors.SelectMany(s => s.Value)).ToList();
        }
        #endregion

        #region Properties

        Int32 _SpectraId;
        [Column(Order = 1)]
        [Display(Description = "Specta Id")]
        [Required()]
        public Int32 SpectraId
        {
            get { return _SpectraId; }
            set
            {
                if (value != _SpectraId)
                {
                    _SpectraId = value;
                    RaisePropertyChanged("SpectraId");
                }
            }
        }

        string _SpectrometerSerialNumber;
        [Column(Order = 2)]
        [Display(Description = "Spectometer Serial Number")]
        [Required()]
        [MaxLength(16)]
        public String SpectrometerSerialNumber
        {
            get { return _SpectrometerSerialNumber; }
            set
            {
                if (value != _SpectrometerSerialNumber)
                {
                    _SpectrometerSerialNumber = value;
                    RaisePropertyChanged("SpectrometerSerialNumber");
                }
            }
        }


        string _protocol;
        [Display(Description = "protocol")]
        [MaxLength(32)]
        public string Protocol
        {
            get { return _protocol; }
            set
            {
                if (value != _protocol)
                {
                    _protocol = value;
                    RaisePropertyChanged("protocol");
                }
            }
        }


        string _filter;
        [Display(Description = "filter")]
        [MaxLength(32)]
        public string filter
        {
            get { return _filter; }
            set
            {
                if (value != _filter)
                {
                    _filter = value;
                    RaisePropertyChanged("filter");
                }
            }
        }


        Int32? _IntegrationTime;
        [Display(Description = "Integration Time")]
        public Int32? IntegrationTime
        {
            get { return _IntegrationTime; }
            set
            {
                if (value != _IntegrationTime)
                {
                    _IntegrationTime = value;
                    RaisePropertyChanged("IntegrationTime");
                }
            }
        }

        string _ProjectIdDTO;
        [Display(Description = "ProjectIdDTO")]
        [MaxLength(32)]
        public string ProjectIdDTO
        {
            get { return _ProjectIdDTO; }
            set
            {
                if (value != _ProjectIdDTO)
                {
                    _ProjectIdDTO = value;
                    RaisePropertyChanged("ProjectIdDTO");
                }
            }
        }

        Int16? _Value0;
        [Display(Description = "Value 0")]
        public Int16? Value0
        {
            get { return _Value0; }
            set
            {
                if (value != _Value0)
                {
                    _Value0 = value;
                    RaisePropertyChanged("Value0");
                }
            }
        }

        Int16? _Value1;
        [Display(Description = "Value 1")]
        public Int16? Value1
        {
            get { return _Value1; }
            set
            {
                if (value != _Value1)
                {
                    _Value1 = value;
                    RaisePropertyChanged("Value1");
                }
            }
        }

        Int16? _Value2;
        [Display(Description = "Value 2")]
        public Int16? Value2
        {
            get { return _Value2; }
            set
            {
                if (value != _Value2)
                {
                    _Value2 = value;
                    RaisePropertyChanged("Value2");
                }
            }
        }

        Int16? _Value3;
        [Display(Description = "Value 3")]
        public Int16? Value3
        {
            get { return _Value3; }
            set
            {
                if (value != _Value3)
                {
                    _Value3 = value;
                    RaisePropertyChanged("Value3");
                }
            }
        }

        Int16? _Value4;
        [Display(Description = "Value 4")]
        public Int16? Value4
        {
            get { return _Value4; }
            set
            {
                if (value != _Value4)
                {
                    _Value4 = value;
                    RaisePropertyChanged("Value4");
                }
            }
        }

        Int16? _Value5;
        [Display(Description = "Value 5")]
        public Int16? Value5
        {
            get { return _Value5; }
            set
            {
                if (value != _Value5)
                {
                    _Value5 = value;
                    RaisePropertyChanged("Value5");
                }
            }
        }

        Int16? _Value6;
        [Display(Description = "Value 6")]
        public Int16? Value6
        {
            get { return _Value6; }
            set
            {
                if (value != _Value6)
                {
                    _Value6 = value;
                    RaisePropertyChanged("Value6");
                }
            }
        }

        Int16? _Value7;
        [Display(Description = "Value 7")]
        public Int16? Value7
        {
            get { return _Value7; }
            set
            {
                if (value != _Value7)
                {
                    _Value7 = value;
                    RaisePropertyChanged("Value7");
                }
            }
        }

        Int16? _Value8;
        [Display(Description = "Value 8")]
        public Int16? Value8
        {
            get { return _Value8; }
            set
            {
                if (value != _Value8)
                {
                    _Value8 = value;
                    RaisePropertyChanged("Value8");
                }
            }
        }

        Int16? _Value9;
        [Display(Description = "Value 9")]
        public Int16? Value9
        {
            get { return _Value9; }
            set
            {
                if (value != _Value9)
                {
                    _Value9 = value;
                    RaisePropertyChanged("Value9");
                }
            }
        }

        Int16? _Value10;
        [Display(Description = "Value 1 0")]
        public Int16? Value10
        {
            get { return _Value10; }
            set
            {
                if (value != _Value10)
                {
                    _Value10 = value;
                    RaisePropertyChanged("Value10");
                }
            }
        }

        Int16? _Value11;
        [Display(Description = "Value 1 1")]
        public Int16? Value11
        {
            get { return _Value11; }
            set
            {
                if (value != _Value11)
                {
                    _Value11 = value;
                    RaisePropertyChanged("Value11");
                }
            }
        }

        Int16? _Value12;
        [Display(Description = "Value 1 2")]
        public Int16? Value12
        {
            get { return _Value12; }
            set
            {
                if (value != _Value12)
                {
                    _Value12 = value;
                    RaisePropertyChanged("Value12");
                }
            }
        }

        Int16? _Value13;
        [Display(Description = "Value 1 3")]
        public Int16? Value13
        {
            get { return _Value13; }
            set
            {
                if (value != _Value13)
                {
                    _Value13 = value;
                    RaisePropertyChanged("Value13");
                }
            }
        }

        Int16? _Value14;
        [Display(Description = "Value 1 4")]
        public Int16? Value14
        {
            get { return _Value14; }
            set
            {
                if (value != _Value14)
                {
                    _Value14 = value;
                    RaisePropertyChanged("Value14");
                }
            }
        }

        Int16? _Value15;
        [Display(Description = "Value 1 5")]
        public Int16? Value15
        {
            get { return _Value15; }
            set
            {
                if (value != _Value15)
                {
                    _Value15 = value;
                    RaisePropertyChanged("Value15");
                }
            }
        }

        Int16? _Value16;
        [Display(Description = "Value 1 6")]
        public Int16? Value16
        {
            get { return _Value16; }
            set
            {
                if (value != _Value16)
                {
                    _Value16 = value;
                    RaisePropertyChanged("Value16");
                }
            }
        }

        Int16? _Value17;
        [Display(Description = "Value 1 7")]
        public Int16? Value17
        {
            get { return _Value17; }
            set
            {
                if (value != _Value17)
                {
                    _Value17 = value;
                    RaisePropertyChanged("Value17");
                }
            }
        }

        Int16? _Value18;
        [Display(Description = "Value 1 8")]
        public Int16? Value18
        {
            get { return _Value18; }
            set
            {
                if (value != _Value18)
                {
                    _Value18 = value;
                    RaisePropertyChanged("Value18");
                }
            }
        }

        Int16? _Value19;
        [Display(Description = "Value 1 9")]
        public Int16? Value19
        {
            get { return _Value19; }
            set
            {
                if (value != _Value19)
                {
                    _Value19 = value;
                    RaisePropertyChanged("Value19");
                }
            }
        }

        Int16? _Value20;
        [Display(Description = "Value 2 0")]
        public Int16? Value20
        {
            get { return _Value20; }
            set
            {
                if (value != _Value20)
                {
                    _Value20 = value;
                    RaisePropertyChanged("Value20");
                }
            }
        }

        Int16? _Value21;
        [Display(Description = "Value 2 1")]
        public Int16? Value21
        {
            get { return _Value21; }
            set
            {
                if (value != _Value21)
                {
                    _Value21 = value;
                    RaisePropertyChanged("Value21");
                }
            }
        }

        Int16? _Value22;
        [Display(Description = "Value 2 2")]
        public Int16? Value22
        {
            get { return _Value22; }
            set
            {
                if (value != _Value22)
                {
                    _Value22 = value;
                    RaisePropertyChanged("Value22");
                }
            }
        }

        Int16? _Value23;
        [Display(Description = "Value 2 3")]
        public Int16? Value23
        {
            get { return _Value23; }
            set
            {
                if (value != _Value23)
                {
                    _Value23 = value;
                    RaisePropertyChanged("Value23");
                }
            }
        }

        Int16? _Value24;
        [Display(Description = "Value 2 4")]
        public Int16? Value24
        {
            get { return _Value24; }
            set
            {
                if (value != _Value24)
                {
                    _Value24 = value;
                    RaisePropertyChanged("Value24");
                }
            }
        }

        Int16? _Value25;
        [Display(Description = "Value 2 5")]
        public Int16? Value25
        {
            get { return _Value25; }
            set
            {
                if (value != _Value25)
                {
                    _Value25 = value;
                    RaisePropertyChanged("Value25");
                }
            }
        }

        Int16? _Value26;
        [Display(Description = "Value 2 6")]
        public Int16? Value26
        {
            get { return _Value26; }
            set
            {
                if (value != _Value26)
                {
                    _Value26 = value;
                    RaisePropertyChanged("Value26");
                }
            }
        }

        Int16? _Value27;
        [Display(Description = "Value 2 7")]
        public Int16? Value27
        {
            get { return _Value27; }
            set
            {
                if (value != _Value27)
                {
                    _Value27 = value;
                    RaisePropertyChanged("Value27");
                }
            }
        }

        Int16? _Value28;
        [Display(Description = "Value 2 8")]
        public Int16? Value28
        {
            get { return _Value28; }
            set
            {
                if (value != _Value28)
                {
                    _Value28 = value;
                    RaisePropertyChanged("Value28");
                }
            }
        }

        Int16? _Value29;
        [Display(Description = "Value 2 9")]
        public Int16? Value29
        {
            get { return _Value29; }
            set
            {
                if (value != _Value29)
                {
                    _Value29 = value;
                    RaisePropertyChanged("Value29");
                }
            }
        }

        Int16? _Value30;
        [Display(Description = "Value 3 0")]
        public Int16? Value30
        {
            get { return _Value30; }
            set
            {
                if (value != _Value30)
                {
                    _Value30 = value;
                    RaisePropertyChanged("Value30");
                }
            }
        }

        Int16? _Value31;
        [Display(Description = "Value 3 1")]
        public Int16? Value31
        {
            get { return _Value31; }
            set
            {
                if (value != _Value31)
                {
                    _Value31 = value;
                    RaisePropertyChanged("Value31");
                }
            }
        }

        Int16? _Value32;
        [Display(Description = "Value 3 2")]
        public Int16? Value32
        {
            get { return _Value32; }
            set
            {
                if (value != _Value32)
                {
                    _Value32 = value;
                    RaisePropertyChanged("Value32");
                }
            }
        }

        Int16? _Value33;
        [Display(Description = "Value 3 3")]
        public Int16? Value33
        {
            get { return _Value33; }
            set
            {
                if (value != _Value33)
                {
                    _Value33 = value;
                    RaisePropertyChanged("Value33");
                }
            }
        }

        Int16? _Value34;
        [Display(Description = "Value 3 4")]
        public Int16? Value34
        {
            get { return _Value34; }
            set
            {
                if (value != _Value34)
                {
                    _Value34 = value;
                    RaisePropertyChanged("Value34");
                }
            }
        }

        Int16? _Value35;
        [Display(Description = "Value 3 5")]
        public Int16? Value35
        {
            get { return _Value35; }
            set
            {
                if (value != _Value35)
                {
                    _Value35 = value;
                    RaisePropertyChanged("Value35");
                }
            }
        }

        Int16? _Value36;
        [Display(Description = "Value 3 6")]
        public Int16? Value36
        {
            get { return _Value36; }
            set
            {
                if (value != _Value36)
                {
                    _Value36 = value;
                    RaisePropertyChanged("Value36");
                }
            }
        }

        Int16? _Value37;
        [Display(Description = "Value 3 7")]
        public Int16? Value37
        {
            get { return _Value37; }
            set
            {
                if (value != _Value37)
                {
                    _Value37 = value;
                    RaisePropertyChanged("Value37");
                }
            }
        }

        Int16? _Value38;
        [Display(Description = "Value 3 8")]
        public Int16? Value38
        {
            get { return _Value38; }
            set
            {
                if (value != _Value38)
                {
                    _Value38 = value;
                    RaisePropertyChanged("Value38");
                }
            }
        }

        Int16? _Value39;
        [Display(Description = "Value 3 9")]
        public Int16? Value39
        {
            get { return _Value39; }
            set
            {
                if (value != _Value39)
                {
                    _Value39 = value;
                    RaisePropertyChanged("Value39");
                }
            }
        }

        Int16? _Value40;
        [Display(Description = "Value 4 0")]
        public Int16? Value40
        {
            get { return _Value40; }
            set
            {
                if (value != _Value40)
                {
                    _Value40 = value;
                    RaisePropertyChanged("Value40");
                }
            }
        }

        Int16? _Value41;
        [Display(Description = "Value 4 1")]
        public Int16? Value41
        {
            get { return _Value41; }
            set
            {
                if (value != _Value41)
                {
                    _Value41 = value;
                    RaisePropertyChanged("Value41");
                }
            }
        }

        Int16? _Value42;
        [Display(Description = "Value 4 2")]
        public Int16? Value42
        {
            get { return _Value42; }
            set
            {
                if (value != _Value42)
                {
                    _Value42 = value;
                    RaisePropertyChanged("Value42");
                }
            }
        }

        Int16? _Value43;
        [Display(Description = "Value 4 3")]
        public Int16? Value43
        {
            get { return _Value43; }
            set
            {
                if (value != _Value43)
                {
                    _Value43 = value;
                    RaisePropertyChanged("Value43");
                }
            }
        }

        Int16? _Value44;
        [Display(Description = "Value 4 4")]
        public Int16? Value44
        {
            get { return _Value44; }
            set
            {
                if (value != _Value44)
                {
                    _Value44 = value;
                    RaisePropertyChanged("Value44");
                }
            }
        }

        Int16? _Value45;
        [Display(Description = "Value 4 5")]
        public Int16? Value45
        {
            get { return _Value45; }
            set
            {
                if (value != _Value45)
                {
                    _Value45 = value;
                    RaisePropertyChanged("Value45");
                }
            }
        }

        Int16? _Value46;
        [Display(Description = "Value 4 6")]
        public Int16? Value46
        {
            get { return _Value46; }
            set
            {
                if (value != _Value46)
                {
                    _Value46 = value;
                    RaisePropertyChanged("Value46");
                }
            }
        }

        Int16? _Value47;
        [Display(Description = "Value 4 7")]
        public Int16? Value47
        {
            get { return _Value47; }
            set
            {
                if (value != _Value47)
                {
                    _Value47 = value;
                    RaisePropertyChanged("Value47");
                }
            }
        }

        Int16? _Value48;
        [Display(Description = "Value 4 8")]
        public Int16? Value48
        {
            get { return _Value48; }
            set
            {
                if (value != _Value48)
                {
                    _Value48 = value;
                    RaisePropertyChanged("Value48");
                }
            }
        }

        Int16? _Value49;
        [Display(Description = "Value 4 9")]
        public Int16? Value49
        {
            get { return _Value49; }
            set
            {
                if (value != _Value49)
                {
                    _Value49 = value;
                    RaisePropertyChanged("Value49");
                }
            }
        }

        Int16? _Value50;
        [Display(Description = "Value 5 0")]
        public Int16? Value50
        {
            get { return _Value50; }
            set
            {
                if (value != _Value50)
                {
                    _Value50 = value;
                    RaisePropertyChanged("Value50");
                }
            }
        }

        Int16? _Value51;
        [Display(Description = "Value 5 1")]
        public Int16? Value51
        {
            get { return _Value51; }
            set
            {
                if (value != _Value51)
                {
                    _Value51 = value;
                    RaisePropertyChanged("Value51");
                }
            }
        }

        Int16? _Value52;
        [Display(Description = "Value 5 2")]
        public Int16? Value52
        {
            get { return _Value52; }
            set
            {
                if (value != _Value52)
                {
                    _Value52 = value;
                    RaisePropertyChanged("Value52");
                }
            }
        }

        Int16? _Value53;
        [Display(Description = "Value 5 3")]
        public Int16? Value53
        {
            get { return _Value53; }
            set
            {
                if (value != _Value53)
                {
                    _Value53 = value;
                    RaisePropertyChanged("Value53");
                }
            }
        }

        Int16? _Value54;
        [Display(Description = "Value 5 4")]
        public Int16? Value54
        {
            get { return _Value54; }
            set
            {
                if (value != _Value54)
                {
                    _Value54 = value;
                    RaisePropertyChanged("Value54");
                }
            }
        }

        Int16? _Value55;
        [Display(Description = "Value 5 5")]
        public Int16? Value55
        {
            get { return _Value55; }
            set
            {
                if (value != _Value55)
                {
                    _Value55 = value;
                    RaisePropertyChanged("Value55");
                }
            }
        }

        Int16? _Value56;
        [Display(Description = "Value 5 6")]
        public Int16? Value56
        {
            get { return _Value56; }
            set
            {
                if (value != _Value56)
                {
                    _Value56 = value;
                    RaisePropertyChanged("Value56");
                }
            }
        }

        Int16? _Value57;
        [Display(Description = "Value 5 7")]
        public Int16? Value57
        {
            get { return _Value57; }
            set
            {
                if (value != _Value57)
                {
                    _Value57 = value;
                    RaisePropertyChanged("Value57");
                }
            }
        }

        Int16? _Value58;
        [Display(Description = "Value 5 8")]
        public Int16? Value58
        {
            get { return _Value58; }
            set
            {
                if (value != _Value58)
                {
                    _Value58 = value;
                    RaisePropertyChanged("Value58");
                }
            }
        }

        Int16? _Value59;
        [Display(Description = "Value 5 9")]
        public Int16? Value59
        {
            get { return _Value59; }
            set
            {
                if (value != _Value59)
                {
                    _Value59 = value;
                    RaisePropertyChanged("Value59");
                }
            }
        }

        Int16? _Value60;
        [Display(Description = "Value 6 0")]
        public Int16? Value60
        {
            get { return _Value60; }
            set
            {
                if (value != _Value60)
                {
                    _Value60 = value;
                    RaisePropertyChanged("Value60");
                }
            }
        }

        Int16? _Value61;
        [Display(Description = "Value 6 1")]
        public Int16? Value61
        {
            get { return _Value61; }
            set
            {
                if (value != _Value61)
                {
                    _Value61 = value;
                    RaisePropertyChanged("Value61");
                }
            }
        }

        Int16? _Value62;
        [Display(Description = "Value 6 2")]
        public Int16? Value62
        {
            get { return _Value62; }
            set
            {
                if (value != _Value62)
                {
                    _Value62 = value;
                    RaisePropertyChanged("Value62");
                }
            }
        }

        Int16? _Value63;
        [Display(Description = "Value 6 3")]
        public Int16? Value63
        {
            get { return _Value63; }
            set
            {
                if (value != _Value63)
                {
                    _Value63 = value;
                    RaisePropertyChanged("Value63");
                }
            }
        }

        Int16? _Value64;
        [Display(Description = "Value 6 4")]
        public Int16? Value64
        {
            get { return _Value64; }
            set
            {
                if (value != _Value64)
                {
                    _Value64 = value;
                    RaisePropertyChanged("Value64");
                }
            }
        }

        Int16? _Value65;
        [Display(Description = "Value 6 5")]
        public Int16? Value65
        {
            get { return _Value65; }
            set
            {
                if (value != _Value65)
                {
                    _Value65 = value;
                    RaisePropertyChanged("Value65");
                }
            }
        }

        Int16? _Value66;
        [Display(Description = "Value 6 6")]
        public Int16? Value66
        {
            get { return _Value66; }
            set
            {
                if (value != _Value66)
                {
                    _Value66 = value;
                    RaisePropertyChanged("Value66");
                }
            }
        }

        Int16? _Value67;
        [Display(Description = "Value 6 7")]
        public Int16? Value67
        {
            get { return _Value67; }
            set
            {
                if (value != _Value67)
                {
                    _Value67 = value;
                    RaisePropertyChanged("Value67");
                }
            }
        }

        Int16? _Value68;
        [Display(Description = "Value 6 8")]
        public Int16? Value68
        {
            get { return _Value68; }
            set
            {
                if (value != _Value68)
                {
                    _Value68 = value;
                    RaisePropertyChanged("Value68");
                }
            }
        }

        Int16? _Value69;
        [Display(Description = "Value 6 9")]
        public Int16? Value69
        {
            get { return _Value69; }
            set
            {
                if (value != _Value69)
                {
                    _Value69 = value;
                    RaisePropertyChanged("Value69");
                }
            }
        }

        Int16? _Value70;
        [Display(Description = "Value 7 0")]
        public Int16? Value70
        {
            get { return _Value70; }
            set
            {
                if (value != _Value70)
                {
                    _Value70 = value;
                    RaisePropertyChanged("Value70");
                }
            }
        }

        Int16? _Value71;
        [Display(Description = "Value 7 1")]
        public Int16? Value71
        {
            get { return _Value71; }
            set
            {
                if (value != _Value71)
                {
                    _Value71 = value;
                    RaisePropertyChanged("Value71");
                }
            }
        }

        Int16? _Value72;
        [Display(Description = "Value 7 2")]
        public Int16? Value72
        {
            get { return _Value72; }
            set
            {
                if (value != _Value72)
                {
                    _Value72 = value;
                    RaisePropertyChanged("Value72");
                }
            }
        }

        Int16? _Value73;
        [Display(Description = "Value 7 3")]
        public Int16? Value73
        {
            get { return _Value73; }
            set
            {
                if (value != _Value73)
                {
                    _Value73 = value;
                    RaisePropertyChanged("Value73");
                }
            }
        }

        Int16? _Value74;
        [Display(Description = "Value 7 4")]
        public Int16? Value74
        {
            get { return _Value74; }
            set
            {
                if (value != _Value74)
                {
                    _Value74 = value;
                    RaisePropertyChanged("Value74");
                }
            }
        }

        Int16? _Value75;
        [Display(Description = "Value 7 5")]
        public Int16? Value75
        {
            get { return _Value75; }
            set
            {
                if (value != _Value75)
                {
                    _Value75 = value;
                    RaisePropertyChanged("Value75");
                }
            }
        }

        Int16? _Value76;
        [Display(Description = "Value 7 6")]
        public Int16? Value76
        {
            get { return _Value76; }
            set
            {
                if (value != _Value76)
                {
                    _Value76 = value;
                    RaisePropertyChanged("Value76");
                }
            }
        }

        Int16? _Value77;
        [Display(Description = "Value 7 7")]
        public Int16? Value77
        {
            get { return _Value77; }
            set
            {
                if (value != _Value77)
                {
                    _Value77 = value;
                    RaisePropertyChanged("Value77");
                }
            }
        }

        Int16? _Value78;
        [Display(Description = "Value 7 8")]
        public Int16? Value78
        {
            get { return _Value78; }
            set
            {
                if (value != _Value78)
                {
                    _Value78 = value;
                    RaisePropertyChanged("Value78");
                }
            }
        }

        Int16? _Value79;
        [Display(Description = "Value 7 9")]
        public Int16? Value79
        {
            get { return _Value79; }
            set
            {
                if (value != _Value79)
                {
                    _Value79 = value;
                    RaisePropertyChanged("Value79");
                }
            }
        }

        Int16? _Value80;
        [Display(Description = "Value 8 0")]
        public Int16? Value80
        {
            get { return _Value80; }
            set
            {
                if (value != _Value80)
                {
                    _Value80 = value;
                    RaisePropertyChanged("Value80");
                }
            }
        }

        Int16? _Value81;
        [Display(Description = "Value 8 1")]
        public Int16? Value81
        {
            get { return _Value81; }
            set
            {
                if (value != _Value81)
                {
                    _Value81 = value;
                    RaisePropertyChanged("Value81");
                }
            }
        }

        Int16? _Value82;
        [Display(Description = "Value 8 2")]
        public Int16? Value82
        {
            get { return _Value82; }
            set
            {
                if (value != _Value82)
                {
                    _Value82 = value;
                    RaisePropertyChanged("Value82");
                }
            }
        }

        Int16? _Value83;
        [Display(Description = "Value 8 3")]
        public Int16? Value83
        {
            get { return _Value83; }
            set
            {
                if (value != _Value83)
                {
                    _Value83 = value;
                    RaisePropertyChanged("Value83");
                }
            }
        }

        Int16? _Value84;
        [Display(Description = "Value 8 4")]
        public Int16? Value84
        {
            get { return _Value84; }
            set
            {
                if (value != _Value84)
                {
                    _Value84 = value;
                    RaisePropertyChanged("Value84");
                }
            }
        }

        Int16? _Value85;
        [Display(Description = "Value 8 5")]
        public Int16? Value85
        {
            get { return _Value85; }
            set
            {
                if (value != _Value85)
                {
                    _Value85 = value;
                    RaisePropertyChanged("Value85");
                }
            }
        }

        Int16? _Value86;
        [Display(Description = "Value 8 6")]
        public Int16? Value86
        {
            get { return _Value86; }
            set
            {
                if (value != _Value86)
                {
                    _Value86 = value;
                    RaisePropertyChanged("Value86");
                }
            }
        }

        Int16? _Value87;
        [Display(Description = "Value 8 7")]
        public Int16? Value87
        {
            get { return _Value87; }
            set
            {
                if (value != _Value87)
                {
                    _Value87 = value;
                    RaisePropertyChanged("Value87");
                }
            }
        }

        Int16? _Value88;
        [Display(Description = "Value 8 8")]
        public Int16? Value88
        {
            get { return _Value88; }
            set
            {
                if (value != _Value88)
                {
                    _Value88 = value;
                    RaisePropertyChanged("Value88");
                }
            }
        }

        Int16? _Value89;
        [Display(Description = "Value 8 9")]
        public Int16? Value89
        {
            get { return _Value89; }
            set
            {
                if (value != _Value89)
                {
                    _Value89 = value;
                    RaisePropertyChanged("Value89");
                }
            }
        }

        Int16? _Value90;
        [Display(Description = "Value 9 0")]
        public Int16? Value90
        {
            get { return _Value90; }
            set
            {
                if (value != _Value90)
                {
                    _Value90 = value;
                    RaisePropertyChanged("Value90");
                }
            }
        }

        Int16? _Value91;
        [Display(Description = "Value 9 1")]
        public Int16? Value91
        {
            get { return _Value91; }
            set
            {
                if (value != _Value91)
                {
                    _Value91 = value;
                    RaisePropertyChanged("Value91");
                }
            }
        }

        Int16? _Value92;
        [Display(Description = "Value 9 2")]
        public Int16? Value92
        {
            get { return _Value92; }
            set
            {
                if (value != _Value92)
                {
                    _Value92 = value;
                    RaisePropertyChanged("Value92");
                }
            }
        }

        Int16? _Value93;
        [Display(Description = "Value 9 3")]
        public Int16? Value93
        {
            get { return _Value93; }
            set
            {
                if (value != _Value93)
                {
                    _Value93 = value;
                    RaisePropertyChanged("Value93");
                }
            }
        }

        Int16? _Value94;
        [Display(Description = "Value 9 4")]
        public Int16? Value94
        {
            get { return _Value94; }
            set
            {
                if (value != _Value94)
                {
                    _Value94 = value;
                    RaisePropertyChanged("Value94");
                }
            }
        }

        Int16? _Value95;
        [Display(Description = "Value 9 5")]
        public Int16? Value95
        {
            get { return _Value95; }
            set
            {
                if (value != _Value95)
                {
                    _Value95 = value;
                    RaisePropertyChanged("Value95");
                }
            }
        }

        Int16? _Value96;
        [Display(Description = "Value 9 6")]
        public Int16? Value96
        {
            get { return _Value96; }
            set
            {
                if (value != _Value96)
                {
                    _Value96 = value;
                    RaisePropertyChanged("Value96");
                }
            }
        }

        Int16? _Value97;
        [Display(Description = "Value 9 7")]
        public Int16? Value97
        {
            get { return _Value97; }
            set
            {
                if (value != _Value97)
                {
                    _Value97 = value;
                    RaisePropertyChanged("Value97");
                }
            }
        }

        Int16? _Value98;
        [Display(Description = "Value 9 8")]
        public Int16? Value98
        {
            get { return _Value98; }
            set
            {
                if (value != _Value98)
                {
                    _Value98 = value;
                    RaisePropertyChanged("Value98");
                }
            }
        }

        Int16? _Value99;
        [Display(Description = "Value 9 9")]
        public Int16? Value99
        {
            get { return _Value99; }
            set
            {
                if (value != _Value99)
                {
                    _Value99 = value;
                    RaisePropertyChanged("Value99");
                }
            }
        }

        Int16? _Value100;
        [Display(Description = "Value 1 0 0")]
        public Int16? Value100
        {
            get { return _Value100; }
            set
            {
                if (value != _Value100)
                {
                    _Value100 = value;
                    RaisePropertyChanged("Value100");
                }
            }
        }

        Int16? _Value101;
        [Display(Description = "Value 1 0 1")]
        public Int16? Value101
        {
            get { return _Value101; }
            set
            {
                if (value != _Value101)
                {
                    _Value101 = value;
                    RaisePropertyChanged("Value101");
                }
            }
        }

        Int16? _Value102;
        [Display(Description = "Value 1 0 2")]
        public Int16? Value102
        {
            get { return _Value102; }
            set
            {
                if (value != _Value102)
                {
                    _Value102 = value;
                    RaisePropertyChanged("Value102");
                }
            }
        }

        Int16? _Value103;
        [Display(Description = "Value 1 0 3")]
        public Int16? Value103
        {
            get { return _Value103; }
            set
            {
                if (value != _Value103)
                {
                    _Value103 = value;
                    RaisePropertyChanged("Value103");
                }
            }
        }

        Int16? _Value104;
        [Display(Description = "Value 1 0 4")]
        public Int16? Value104
        {
            get { return _Value104; }
            set
            {
                if (value != _Value104)
                {
                    _Value104 = value;
                    RaisePropertyChanged("Value104");
                }
            }
        }

        Int16? _Value105;
        [Display(Description = "Value 1 0 5")]
        public Int16? Value105
        {
            get { return _Value105; }
            set
            {
                if (value != _Value105)
                {
                    _Value105 = value;
                    RaisePropertyChanged("Value105");
                }
            }
        }

        Int16? _Value106;
        [Display(Description = "Value 1 0 6")]
        public Int16? Value106
        {
            get { return _Value106; }
            set
            {
                if (value != _Value106)
                {
                    _Value106 = value;
                    RaisePropertyChanged("Value106");
                }
            }
        }

        Int16? _Value107;
        [Display(Description = "Value 1 0 7")]
        public Int16? Value107
        {
            get { return _Value107; }
            set
            {
                if (value != _Value107)
                {
                    _Value107 = value;
                    RaisePropertyChanged("Value107");
                }
            }
        }

        Int16? _Value108;
        [Display(Description = "Value 1 0 8")]
        public Int16? Value108
        {
            get { return _Value108; }
            set
            {
                if (value != _Value108)
                {
                    _Value108 = value;
                    RaisePropertyChanged("Value108");
                }
            }
        }

        Int16? _Value109;
        [Display(Description = "Value 1 0 9")]
        public Int16? Value109
        {
            get { return _Value109; }
            set
            {
                if (value != _Value109)
                {
                    _Value109 = value;
                    RaisePropertyChanged("Value109");
                }
            }
        }

        Int16? _Value110;
        [Display(Description = "Value 1 1 0")]
        public Int16? Value110
        {
            get { return _Value110; }
            set
            {
                if (value != _Value110)
                {
                    _Value110 = value;
                    RaisePropertyChanged("Value110");
                }
            }
        }

        Int16? _Value111;
        [Display(Description = "Value 1 1 1")]
        public Int16? Value111
        {
            get { return _Value111; }
            set
            {
                if (value != _Value111)
                {
                    _Value111 = value;
                    RaisePropertyChanged("Value111");
                }
            }
        }

        Int16? _Value112;
        [Display(Description = "Value 1 1 2")]
        public Int16? Value112
        {
            get { return _Value112; }
            set
            {
                if (value != _Value112)
                {
                    _Value112 = value;
                    RaisePropertyChanged("Value112");
                }
            }
        }

        Int16? _Value113;
        [Display(Description = "Value 1 1 3")]
        public Int16? Value113
        {
            get { return _Value113; }
            set
            {
                if (value != _Value113)
                {
                    _Value113 = value;
                    RaisePropertyChanged("Value113");
                }
            }
        }

        Int16? _Value114;
        [Display(Description = "Value 1 1 4")]
        public Int16? Value114
        {
            get { return _Value114; }
            set
            {
                if (value != _Value114)
                {
                    _Value114 = value;
                    RaisePropertyChanged("Value114");
                }
            }
        }

        Int16? _Value115;
        [Display(Description = "Value 1 1 5")]
        public Int16? Value115
        {
            get { return _Value115; }
            set
            {
                if (value != _Value115)
                {
                    _Value115 = value;
                    RaisePropertyChanged("Value115");
                }
            }
        }

        Int16? _Value116;
        [Display(Description = "Value 1 1 6")]
        public Int16? Value116
        {
            get { return _Value116; }
            set
            {
                if (value != _Value116)
                {
                    _Value116 = value;
                    RaisePropertyChanged("Value116");
                }
            }
        }

        Int16? _Value117;
        [Display(Description = "Value 1 1 7")]
        public Int16? Value117
        {
            get { return _Value117; }
            set
            {
                if (value != _Value117)
                {
                    _Value117 = value;
                    RaisePropertyChanged("Value117");
                }
            }
        }

        Int16? _Value118;
        [Display(Description = "Value 1 1 8")]
        public Int16? Value118
        {
            get { return _Value118; }
            set
            {
                if (value != _Value118)
                {
                    _Value118 = value;
                    RaisePropertyChanged("Value118");
                }
            }
        }

        Int16? _Value119;
        [Display(Description = "Value 1 1 9")]
        public Int16? Value119
        {
            get { return _Value119; }
            set
            {
                if (value != _Value119)
                {
                    _Value119 = value;
                    RaisePropertyChanged("Value119");
                }
            }
        }

        Int16? _Value120;
        [Display(Description = "Value 1 2 0")]
        public Int16? Value120
        {
            get { return _Value120; }
            set
            {
                if (value != _Value120)
                {
                    _Value120 = value;
                    RaisePropertyChanged("Value120");
                }
            }
        }

        Int16? _Value121;
        [Display(Description = "Value 1 2 1")]
        public Int16? Value121
        {
            get { return _Value121; }
            set
            {
                if (value != _Value121)
                {
                    _Value121 = value;
                    RaisePropertyChanged("Value121");
                }
            }
        }

        Int16? _Value122;
        [Display(Description = "Value 1 2 2")]
        public Int16? Value122
        {
            get { return _Value122; }
            set
            {
                if (value != _Value122)
                {
                    _Value122 = value;
                    RaisePropertyChanged("Value122");
                }
            }
        }

        Int16? _Value123;
        [Display(Description = "Value 1 2 3")]
        public Int16? Value123
        {
            get { return _Value123; }
            set
            {
                if (value != _Value123)
                {
                    _Value123 = value;
                    RaisePropertyChanged("Value123");
                }
            }
        }

        Int16? _Value124;
        [Display(Description = "Value 1 2 4")]
        public Int16? Value124
        {
            get { return _Value124; }
            set
            {
                if (value != _Value124)
                {
                    _Value124 = value;
                    RaisePropertyChanged("Value124");
                }
            }
        }

        Int16? _Value125;
        [Display(Description = "Value 1 2 5")]
        public Int16? Value125
        {
            get { return _Value125; }
            set
            {
                if (value != _Value125)
                {
                    _Value125 = value;
                    RaisePropertyChanged("Value125");
                }
            }
        }

        Int16? _Value126;
        [Display(Description = "Value 1 2 6")]
        public Int16? Value126
        {
            get { return _Value126; }
            set
            {
                if (value != _Value126)
                {
                    _Value126 = value;
                    RaisePropertyChanged("Value126");
                }
            }
        }

        Int16? _Value127;
        [Display(Description = "Value 1 2 7")]
        public Int16? Value127
        {
            get { return _Value127; }
            set
            {
                if (value != _Value127)
                {
                    _Value127 = value;
                    RaisePropertyChanged("Value127");
                }
            }
        }

        Int16? _Value128;
        [Display(Description = "Value 1 2 8")]
        public Int16? Value128
        {
            get { return _Value128; }
            set
            {
                if (value != _Value128)
                {
                    _Value128 = value;
                    RaisePropertyChanged("Value128");
                }
            }
        }

        Int16? _Value129;
        [Display(Description = "Value 1 2 9")]
        public Int16? Value129
        {
            get { return _Value129; }
            set
            {
                if (value != _Value129)
                {
                    _Value129 = value;
                    RaisePropertyChanged("Value129");
                }
            }
        }

        Int16? _Value130;
        [Display(Description = "Value 1 3 0")]
        public Int16? Value130
        {
            get { return _Value130; }
            set
            {
                if (value != _Value130)
                {
                    _Value130 = value;
                    RaisePropertyChanged("Value130");
                }
            }
        }

        Int16? _Value131;
        [Display(Description = "Value 1 3 1")]
        public Int16? Value131
        {
            get { return _Value131; }
            set
            {
                if (value != _Value131)
                {
                    _Value131 = value;
                    RaisePropertyChanged("Value131");
                }
            }
        }

        Int16? _Value132;
        [Display(Description = "Value 1 3 2")]
        public Int16? Value132
        {
            get { return _Value132; }
            set
            {
                if (value != _Value132)
                {
                    _Value132 = value;
                    RaisePropertyChanged("Value132");
                }
            }
        }

        Int16? _Value133;
        [Display(Description = "Value 1 3 3")]
        public Int16? Value133
        {
            get { return _Value133; }
            set
            {
                if (value != _Value133)
                {
                    _Value133 = value;
                    RaisePropertyChanged("Value133");
                }
            }
        }

        Int16? _Value134;
        [Display(Description = "Value 1 3 4")]
        public Int16? Value134
        {
            get { return _Value134; }
            set
            {
                if (value != _Value134)
                {
                    _Value134 = value;
                    RaisePropertyChanged("Value134");
                }
            }
        }

        Int16? _Value135;
        [Display(Description = "Value 1 3 5")]
        public Int16? Value135
        {
            get { return _Value135; }
            set
            {
                if (value != _Value135)
                {
                    _Value135 = value;
                    RaisePropertyChanged("Value135");
                }
            }
        }

        Int16? _Value136;
        [Display(Description = "Value 1 3 6")]
        public Int16? Value136
        {
            get { return _Value136; }
            set
            {
                if (value != _Value136)
                {
                    _Value136 = value;
                    RaisePropertyChanged("Value136");
                }
            }
        }

        Int16? _Value137;
        [Display(Description = "Value 1 3 7")]
        public Int16? Value137
        {
            get { return _Value137; }
            set
            {
                if (value != _Value137)
                {
                    _Value137 = value;
                    RaisePropertyChanged("Value137");
                }
            }
        }

        Int16? _Value138;
        [Display(Description = "Value 1 3 8")]
        public Int16? Value138
        {
            get { return _Value138; }
            set
            {
                if (value != _Value138)
                {
                    _Value138 = value;
                    RaisePropertyChanged("Value138");
                }
            }
        }

        Int16? _Value139;
        [Display(Description = "Value 1 3 9")]
        public Int16? Value139
        {
            get { return _Value139; }
            set
            {
                if (value != _Value139)
                {
                    _Value139 = value;
                    RaisePropertyChanged("Value139");
                }
            }
        }

        Int16? _Value140;
        [Display(Description = "Value 1 4 0")]
        public Int16? Value140
        {
            get { return _Value140; }
            set
            {
                if (value != _Value140)
                {
                    _Value140 = value;
                    RaisePropertyChanged("Value140");
                }
            }
        }

        Int16? _Value141;
        [Display(Description = "Value 1 4 1")]
        public Int16? Value141
        {
            get { return _Value141; }
            set
            {
                if (value != _Value141)
                {
                    _Value141 = value;
                    RaisePropertyChanged("Value141");
                }
            }
        }

        Int16? _Value142;
        [Display(Description = "Value 1 4 2")]
        public Int16? Value142
        {
            get { return _Value142; }
            set
            {
                if (value != _Value142)
                {
                    _Value142 = value;
                    RaisePropertyChanged("Value142");
                }
            }
        }

        Int16? _Value143;
        [Display(Description = "Value 1 4 3")]
        public Int16? Value143
        {
            get { return _Value143; }
            set
            {
                if (value != _Value143)
                {
                    _Value143 = value;
                    RaisePropertyChanged("Value143");
                }
            }
        }

        Int16? _Value144;
        [Display(Description = "Value 1 4 4")]
        public Int16? Value144
        {
            get { return _Value144; }
            set
            {
                if (value != _Value144)
                {
                    _Value144 = value;
                    RaisePropertyChanged("Value144");
                }
            }
        }

        Int16? _Value145;
        [Display(Description = "Value 1 4 5")]
        public Int16? Value145
        {
            get { return _Value145; }
            set
            {
                if (value != _Value145)
                {
                    _Value145 = value;
                    RaisePropertyChanged("Value145");
                }
            }
        }

        Int16? _Value146;
        [Display(Description = "Value 1 4 6")]
        public Int16? Value146
        {
            get { return _Value146; }
            set
            {
                if (value != _Value146)
                {
                    _Value146 = value;
                    RaisePropertyChanged("Value146");
                }
            }
        }

        Int16? _Value147;
        [Display(Description = "Value 1 4 7")]
        public Int16? Value147
        {
            get { return _Value147; }
            set
            {
                if (value != _Value147)
                {
                    _Value147 = value;
                    RaisePropertyChanged("Value147");
                }
            }
        }

        Int16? _Value148;
        [Display(Description = "Value 1 4 8")]
        public Int16? Value148
        {
            get { return _Value148; }
            set
            {
                if (value != _Value148)
                {
                    _Value148 = value;
                    RaisePropertyChanged("Value148");
                }
            }
        }

        Int16? _Value149;
        [Display(Description = "Value 1 4 9")]
        public Int16? Value149
        {
            get { return _Value149; }
            set
            {
                if (value != _Value149)
                {
                    _Value149 = value;
                    RaisePropertyChanged("Value149");
                }
            }
        }

        Int16? _Value150;
        [Display(Description = "Value 1 5 0")]
        public Int16? Value150
        {
            get { return _Value150; }
            set
            {
                if (value != _Value150)
                {
                    _Value150 = value;
                    RaisePropertyChanged("Value150");
                }
            }
        }

        Int16? _Value151;
        [Display(Description = "Value 1 5 1")]
        public Int16? Value151
        {
            get { return _Value151; }
            set
            {
                if (value != _Value151)
                {
                    _Value151 = value;
                    RaisePropertyChanged("Value151");
                }
            }
        }

        Int16? _Value152;
        [Display(Description = "Value 1 5 2")]
        public Int16? Value152
        {
            get { return _Value152; }
            set
            {
                if (value != _Value152)
                {
                    _Value152 = value;
                    RaisePropertyChanged("Value152");
                }
            }
        }

        Int16? _Value153;
        [Display(Description = "Value 1 5 3")]
        public Int16? Value153
        {
            get { return _Value153; }
            set
            {
                if (value != _Value153)
                {
                    _Value153 = value;
                    RaisePropertyChanged("Value153");
                }
            }
        }

        Int16? _Value154;
        [Display(Description = "Value 1 5 4")]
        public Int16? Value154
        {
            get { return _Value154; }
            set
            {
                if (value != _Value154)
                {
                    _Value154 = value;
                    RaisePropertyChanged("Value154");
                }
            }
        }

        Int16? _Value155;
        [Display(Description = "Value 1 5 5")]
        public Int16? Value155
        {
            get { return _Value155; }
            set
            {
                if (value != _Value155)
                {
                    _Value155 = value;
                    RaisePropertyChanged("Value155");
                }
            }
        }

        Int16? _Value156;
        [Display(Description = "Value 1 5 6")]
        public Int16? Value156
        {
            get { return _Value156; }
            set
            {
                if (value != _Value156)
                {
                    _Value156 = value;
                    RaisePropertyChanged("Value156");
                }
            }
        }

        Int16? _Value157;
        [Display(Description = "Value 1 5 7")]
        public Int16? Value157
        {
            get { return _Value157; }
            set
            {
                if (value != _Value157)
                {
                    _Value157 = value;
                    RaisePropertyChanged("Value157");
                }
            }
        }

        Int16? _Value158;
        [Display(Description = "Value 1 5 8")]
        public Int16? Value158
        {
            get { return _Value158; }
            set
            {
                if (value != _Value158)
                {
                    _Value158 = value;
                    RaisePropertyChanged("Value158");
                }
            }
        }

        Int16? _Value159;
        [Display(Description = "Value 1 5 9")]
        public Int16? Value159
        {
            get { return _Value159; }
            set
            {
                if (value != _Value159)
                {
                    _Value159 = value;
                    RaisePropertyChanged("Value159");
                }
            }
        }

        Int16? _Value160;
        [Display(Description = "Value 1 6 0")]
        public Int16? Value160
        {
            get { return _Value160; }
            set
            {
                if (value != _Value160)
                {
                    _Value160 = value;
                    RaisePropertyChanged("Value160");
                }
            }
        }

        Int16? _Value161;
        [Display(Description = "Value 1 6 1")]
        public Int16? Value161
        {
            get { return _Value161; }
            set
            {
                if (value != _Value161)
                {
                    _Value161 = value;
                    RaisePropertyChanged("Value161");
                }
            }
        }

        Int16? _Value162;
        [Display(Description = "Value 1 6 2")]
        public Int16? Value162
        {
            get { return _Value162; }
            set
            {
                if (value != _Value162)
                {
                    _Value162 = value;
                    RaisePropertyChanged("Value162");
                }
            }
        }

        Int16? _Value163;
        [Display(Description = "Value 1 6 3")]
        public Int16? Value163
        {
            get { return _Value163; }
            set
            {
                if (value != _Value163)
                {
                    _Value163 = value;
                    RaisePropertyChanged("Value163");
                }
            }
        }

        Int16? _Value164;
        [Display(Description = "Value 1 6 4")]
        public Int16? Value164
        {
            get { return _Value164; }
            set
            {
                if (value != _Value164)
                {
                    _Value164 = value;
                    RaisePropertyChanged("Value164");
                }
            }
        }

        Int16? _Value165;
        [Display(Description = "Value 1 6 5")]
        public Int16? Value165
        {
            get { return _Value165; }
            set
            {
                if (value != _Value165)
                {
                    _Value165 = value;
                    RaisePropertyChanged("Value165");
                }
            }
        }

        Int16? _Value166;
        [Display(Description = "Value 1 6 6")]
        public Int16? Value166
        {
            get { return _Value166; }
            set
            {
                if (value != _Value166)
                {
                    _Value166 = value;
                    RaisePropertyChanged("Value166");
                }
            }
        }

        Int16? _Value167;
        [Display(Description = "Value 1 6 7")]
        public Int16? Value167
        {
            get { return _Value167; }
            set
            {
                if (value != _Value167)
                {
                    _Value167 = value;
                    RaisePropertyChanged("Value167");
                }
            }
        }

        Int16? _Value168;
        [Display(Description = "Value 1 6 8")]
        public Int16? Value168
        {
            get { return _Value168; }
            set
            {
                if (value != _Value168)
                {
                    _Value168 = value;
                    RaisePropertyChanged("Value168");
                }
            }
        }

        Int16? _Value169;
        [Display(Description = "Value 1 6 9")]
        public Int16? Value169
        {
            get { return _Value169; }
            set
            {
                if (value != _Value169)
                {
                    _Value169 = value;
                    RaisePropertyChanged("Value169");
                }
            }
        }

        Int16? _Value170;
        [Display(Description = "Value 1 7 0")]
        public Int16? Value170
        {
            get { return _Value170; }
            set
            {
                if (value != _Value170)
                {
                    _Value170 = value;
                    RaisePropertyChanged("Value170");
                }
            }
        }

        Int16? _Value171;
        [Display(Description = "Value 1 7 1")]
        public Int16? Value171
        {
            get { return _Value171; }
            set
            {
                if (value != _Value171)
                {
                    _Value171 = value;
                    RaisePropertyChanged("Value171");
                }
            }
        }

        Int16? _Value172;
        [Display(Description = "Value 1 7 2")]
        public Int16? Value172
        {
            get { return _Value172; }
            set
            {
                if (value != _Value172)
                {
                    _Value172 = value;
                    RaisePropertyChanged("Value172");
                }
            }
        }

        Int16? _Value173;
        [Display(Description = "Value 1 7 3")]
        public Int16? Value173
        {
            get { return _Value173; }
            set
            {
                if (value != _Value173)
                {
                    _Value173 = value;
                    RaisePropertyChanged("Value173");
                }
            }
        }

        Int16? _Value174;
        [Display(Description = "Value 1 7 4")]
        public Int16? Value174
        {
            get { return _Value174; }
            set
            {
                if (value != _Value174)
                {
                    _Value174 = value;
                    RaisePropertyChanged("Value174");
                }
            }
        }

        Int16? _Value175;
        [Display(Description = "Value 1 7 5")]
        public Int16? Value175
        {
            get { return _Value175; }
            set
            {
                if (value != _Value175)
                {
                    _Value175 = value;
                    RaisePropertyChanged("Value175");
                }
            }
        }

        Int16? _Value176;
        [Display(Description = "Value 1 7 6")]
        public Int16? Value176
        {
            get { return _Value176; }
            set
            {
                if (value != _Value176)
                {
                    _Value176 = value;
                    RaisePropertyChanged("Value176");
                }
            }
        }

        Int16? _Value177;
        [Display(Description = "Value 1 7 7")]
        public Int16? Value177
        {
            get { return _Value177; }
            set
            {
                if (value != _Value177)
                {
                    _Value177 = value;
                    RaisePropertyChanged("Value177");
                }
            }
        }

        Int16? _Value178;
        [Display(Description = "Value 1 7 8")]
        public Int16? Value178
        {
            get { return _Value178; }
            set
            {
                if (value != _Value178)
                {
                    _Value178 = value;
                    RaisePropertyChanged("Value178");
                }
            }
        }

        Int16? _Value179;
        [Display(Description = "Value 1 7 9")]
        public Int16? Value179
        {
            get { return _Value179; }
            set
            {
                if (value != _Value179)
                {
                    _Value179 = value;
                    RaisePropertyChanged("Value179");
                }
            }
        }

        Int16? _Value180;
        [Display(Description = "Value 1 8 0")]
        public Int16? Value180
        {
            get { return _Value180; }
            set
            {
                if (value != _Value180)
                {
                    _Value180 = value;
                    RaisePropertyChanged("Value180");
                }
            }
        }

        Int16? _Value181;
        [Display(Description = "Value 1 8 1")]
        public Int16? Value181
        {
            get { return _Value181; }
            set
            {
                if (value != _Value181)
                {
                    _Value181 = value;
                    RaisePropertyChanged("Value181");
                }
            }
        }

        Int16? _Value182;
        [Display(Description = "Value 1 8 2")]
        public Int16? Value182
        {
            get { return _Value182; }
            set
            {
                if (value != _Value182)
                {
                    _Value182 = value;
                    RaisePropertyChanged("Value182");
                }
            }
        }

        Int16? _Value183;
        [Display(Description = "Value 1 8 3")]
        public Int16? Value183
        {
            get { return _Value183; }
            set
            {
                if (value != _Value183)
                {
                    _Value183 = value;
                    RaisePropertyChanged("Value183");
                }
            }
        }

        Int16? _Value184;
        [Display(Description = "Value 1 8 4")]
        public Int16? Value184
        {
            get { return _Value184; }
            set
            {
                if (value != _Value184)
                {
                    _Value184 = value;
                    RaisePropertyChanged("Value184");
                }
            }
        }

        Int16? _Value185;
        [Display(Description = "Value 1 8 5")]
        public Int16? Value185
        {
            get { return _Value185; }
            set
            {
                if (value != _Value185)
                {
                    _Value185 = value;
                    RaisePropertyChanged("Value185");
                }
            }
        }

        Int16? _Value186;
        [Display(Description = "Value 1 8 6")]
        public Int16? Value186
        {
            get { return _Value186; }
            set
            {
                if (value != _Value186)
                {
                    _Value186 = value;
                    RaisePropertyChanged("Value186");
                }
            }
        }

        Int16? _Value187;
        [Display(Description = "Value 1 8 7")]
        public Int16? Value187
        {
            get { return _Value187; }
            set
            {
                if (value != _Value187)
                {
                    _Value187 = value;
                    RaisePropertyChanged("Value187");
                }
            }
        }

        Int16? _Value188;
        [Display(Description = "Value 1 8 8")]
        public Int16? Value188
        {
            get { return _Value188; }
            set
            {
                if (value != _Value188)
                {
                    _Value188 = value;
                    RaisePropertyChanged("Value188");
                }
            }
        }

        Int16? _Value189;
        [Display(Description = "Value 1 8 9")]
        public Int16? Value189
        {
            get { return _Value189; }
            set
            {
                if (value != _Value189)
                {
                    _Value189 = value;
                    RaisePropertyChanged("Value189");
                }
            }
        }

        Int16? _Value190;
        [Display(Description = "Value 1 9 0")]
        public Int16? Value190
        {
            get { return _Value190; }
            set
            {
                if (value != _Value190)
                {
                    _Value190 = value;
                    RaisePropertyChanged("Value190");
                }
            }
        }

        Int16? _Value191;
        [Display(Description = "Value 1 9 1")]
        public Int16? Value191
        {
            get { return _Value191; }
            set
            {
                if (value != _Value191)
                {
                    _Value191 = value;
                    RaisePropertyChanged("Value191");
                }
            }
        }

        Int16? _Value192;
        [Display(Description = "Value 1 9 2")]
        public Int16? Value192
        {
            get { return _Value192; }
            set
            {
                if (value != _Value192)
                {
                    _Value192 = value;
                    RaisePropertyChanged("Value192");
                }
            }
        }

        Int16? _Value193;
        [Display(Description = "Value 1 9 3")]
        public Int16? Value193
        {
            get { return _Value193; }
            set
            {
                if (value != _Value193)
                {
                    _Value193 = value;
                    RaisePropertyChanged("Value193");
                }
            }
        }

        Int16? _Value194;
        [Display(Description = "Value 1 9 4")]
        public Int16? Value194
        {
            get { return _Value194; }
            set
            {
                if (value != _Value194)
                {
                    _Value194 = value;
                    RaisePropertyChanged("Value194");
                }
            }
        }

        Int16? _Value195;
        [Display(Description = "Value 1 9 5")]
        public Int16? Value195
        {
            get { return _Value195; }
            set
            {
                if (value != _Value195)
                {
                    _Value195 = value;
                    RaisePropertyChanged("Value195");
                }
            }
        }

        Int16? _Value196;
        [Display(Description = "Value 1 9 6")]
        public Int16? Value196
        {
            get { return _Value196; }
            set
            {
                if (value != _Value196)
                {
                    _Value196 = value;
                    RaisePropertyChanged("Value196");
                }
            }
        }

        Int16? _Value197;
        [Display(Description = "Value 1 9 7")]
        public Int16? Value197
        {
            get { return _Value197; }
            set
            {
                if (value != _Value197)
                {
                    _Value197 = value;
                    RaisePropertyChanged("Value197");
                }
            }
        }

        Int16? _Value198;
        [Display(Description = "Value 1 9 8")]
        public Int16? Value198
        {
            get { return _Value198; }
            set
            {
                if (value != _Value198)
                {
                    _Value198 = value;
                    RaisePropertyChanged("Value198");
                }
            }
        }

        Int16? _Value199;
        [Display(Description = "Value 1 9 9")]
        public Int16? Value199
        {
            get { return _Value199; }
            set
            {
                if (value != _Value199)
                {
                    _Value199 = value;
                    RaisePropertyChanged("Value199");
                }
            }
        }

        Int16? _Value200;
        [Display(Description = "Value 2 0 0")]
        public Int16? Value200
        {
            get { return _Value200; }
            set
            {
                if (value != _Value200)
                {
                    _Value200 = value;
                    RaisePropertyChanged("Value200");
                }
            }
        }

        Int16? _Value201;
        [Display(Description = "Value 2 0 1")]
        public Int16? Value201
        {
            get { return _Value201; }
            set
            {
                if (value != _Value201)
                {
                    _Value201 = value;
                    RaisePropertyChanged("Value201");
                }
            }
        }

        Int16? _Value202;
        [Display(Description = "Value 2 0 2")]
        public Int16? Value202
        {
            get { return _Value202; }
            set
            {
                if (value != _Value202)
                {
                    _Value202 = value;
                    RaisePropertyChanged("Value202");
                }
            }
        }

        Int16? _Value203;
        [Display(Description = "Value 2 0 3")]
        public Int16? Value203
        {
            get { return _Value203; }
            set
            {
                if (value != _Value203)
                {
                    _Value203 = value;
                    RaisePropertyChanged("Value203");
                }
            }
        }

        Int16? _Value204;
        [Display(Description = "Value 2 0 4")]
        public Int16? Value204
        {
            get { return _Value204; }
            set
            {
                if (value != _Value204)
                {
                    _Value204 = value;
                    RaisePropertyChanged("Value204");
                }
            }
        }

        Int16? _Value205;
        [Display(Description = "Value 2 0 5")]
        public Int16? Value205
        {
            get { return _Value205; }
            set
            {
                if (value != _Value205)
                {
                    _Value205 = value;
                    RaisePropertyChanged("Value205");
                }
            }
        }

        Int16? _Value206;
        [Display(Description = "Value 2 0 6")]
        public Int16? Value206
        {
            get { return _Value206; }
            set
            {
                if (value != _Value206)
                {
                    _Value206 = value;
                    RaisePropertyChanged("Value206");
                }
            }
        }

        Int16? _Value207;
        [Display(Description = "Value 2 0 7")]
        public Int16? Value207
        {
            get { return _Value207; }
            set
            {
                if (value != _Value207)
                {
                    _Value207 = value;
                    RaisePropertyChanged("Value207");
                }
            }
        }

        Int16? _Value208;
        [Display(Description = "Value 2 0 8")]
        public Int16? Value208
        {
            get { return _Value208; }
            set
            {
                if (value != _Value208)
                {
                    _Value208 = value;
                    RaisePropertyChanged("Value208");
                }
            }
        }

        Int16? _Value209;
        [Display(Description = "Value 2 0 9")]
        public Int16? Value209
        {
            get { return _Value209; }
            set
            {
                if (value != _Value209)
                {
                    _Value209 = value;
                    RaisePropertyChanged("Value209");
                }
            }
        }

        Int16? _Value210;
        [Display(Description = "Value 2 1 0")]
        public Int16? Value210
        {
            get { return _Value210; }
            set
            {
                if (value != _Value210)
                {
                    _Value210 = value;
                    RaisePropertyChanged("Value210");
                }
            }
        }

        Int16? _Value211;
        [Display(Description = "Value 2 1 1")]
        public Int16? Value211
        {
            get { return _Value211; }
            set
            {
                if (value != _Value211)
                {
                    _Value211 = value;
                    RaisePropertyChanged("Value211");
                }
            }
        }

        Int16? _Value212;
        [Display(Description = "Value 2 1 2")]
        public Int16? Value212
        {
            get { return _Value212; }
            set
            {
                if (value != _Value212)
                {
                    _Value212 = value;
                    RaisePropertyChanged("Value212");
                }
            }
        }

        Int16? _Value213;
        [Display(Description = "Value 2 1 3")]
        public Int16? Value213
        {
            get { return _Value213; }
            set
            {
                if (value != _Value213)
                {
                    _Value213 = value;
                    RaisePropertyChanged("Value213");
                }
            }
        }

        Int16? _Value214;
        [Display(Description = "Value 2 1 4")]
        public Int16? Value214
        {
            get { return _Value214; }
            set
            {
                if (value != _Value214)
                {
                    _Value214 = value;
                    RaisePropertyChanged("Value214");
                }
            }
        }

        Int16? _Value215;
        [Display(Description = "Value 2 1 5")]
        public Int16? Value215
        {
            get { return _Value215; }
            set
            {
                if (value != _Value215)
                {
                    _Value215 = value;
                    RaisePropertyChanged("Value215");
                }
            }
        }

        Int16? _Value216;
        [Display(Description = "Value 2 1 6")]
        public Int16? Value216
        {
            get { return _Value216; }
            set
            {
                if (value != _Value216)
                {
                    _Value216 = value;
                    RaisePropertyChanged("Value216");
                }
            }
        }

        Int16? _Value217;
        [Display(Description = "Value 2 1 7")]
        public Int16? Value217
        {
            get { return _Value217; }
            set
            {
                if (value != _Value217)
                {
                    _Value217 = value;
                    RaisePropertyChanged("Value217");
                }
            }
        }

        Int16? _Value218;
        [Display(Description = "Value 2 1 8")]
        public Int16? Value218
        {
            get { return _Value218; }
            set
            {
                if (value != _Value218)
                {
                    _Value218 = value;
                    RaisePropertyChanged("Value218");
                }
            }
        }

        Int16? _Value219;
        [Display(Description = "Value 2 1 9")]
        public Int16? Value219
        {
            get { return _Value219; }
            set
            {
                if (value != _Value219)
                {
                    _Value219 = value;
                    RaisePropertyChanged("Value219");
                }
            }
        }

        Int16? _Value220;
        [Display(Description = "Value 2 2 0")]
        public Int16? Value220
        {
            get { return _Value220; }
            set
            {
                if (value != _Value220)
                {
                    _Value220 = value;
                    RaisePropertyChanged("Value220");
                }
            }
        }

        Int16? _Value221;
        [Display(Description = "Value 2 2 1")]
        public Int16? Value221
        {
            get { return _Value221; }
            set
            {
                if (value != _Value221)
                {
                    _Value221 = value;
                    RaisePropertyChanged("Value221");
                }
            }
        }

        Int16? _Value222;
        [Display(Description = "Value 2 2 2")]
        public Int16? Value222
        {
            get { return _Value222; }
            set
            {
                if (value != _Value222)
                {
                    _Value222 = value;
                    RaisePropertyChanged("Value222");
                }
            }
        }

        Int16? _Value223;
        [Display(Description = "Value 2 2 3")]
        public Int16? Value223
        {
            get { return _Value223; }
            set
            {
                if (value != _Value223)
                {
                    _Value223 = value;
                    RaisePropertyChanged("Value223");
                }
            }
        }

        Int16? _Value224;
        [Display(Description = "Value 2 2 4")]
        public Int16? Value224
        {
            get { return _Value224; }
            set
            {
                if (value != _Value224)
                {
                    _Value224 = value;
                    RaisePropertyChanged("Value224");
                }
            }
        }

        Int16? _Value225;
        [Display(Description = "Value 2 2 5")]
        public Int16? Value225
        {
            get { return _Value225; }
            set
            {
                if (value != _Value225)
                {
                    _Value225 = value;
                    RaisePropertyChanged("Value225");
                }
            }
        }

        Int16? _Value226;
        [Display(Description = "Value 2 2 6")]
        public Int16? Value226
        {
            get { return _Value226; }
            set
            {
                if (value != _Value226)
                {
                    _Value226 = value;
                    RaisePropertyChanged("Value226");
                }
            }
        }

        Int16? _Value227;
        [Display(Description = "Value 2 2 7")]
        public Int16? Value227
        {
            get { return _Value227; }
            set
            {
                if (value != _Value227)
                {
                    _Value227 = value;
                    RaisePropertyChanged("Value227");
                }
            }
        }

        Int16? _Value228;
        [Display(Description = "Value 2 2 8")]
        public Int16? Value228
        {
            get { return _Value228; }
            set
            {
                if (value != _Value228)
                {
                    _Value228 = value;
                    RaisePropertyChanged("Value228");
                }
            }
        }

        Int16? _Value229;
        [Display(Description = "Value 2 2 9")]
        public Int16? Value229
        {
            get { return _Value229; }
            set
            {
                if (value != _Value229)
                {
                    _Value229 = value;
                    RaisePropertyChanged("Value229");
                }
            }
        }

        Int16? _Value230;
        [Display(Description = "Value 2 3 0")]
        public Int16? Value230
        {
            get { return _Value230; }
            set
            {
                if (value != _Value230)
                {
                    _Value230 = value;
                    RaisePropertyChanged("Value230");
                }
            }
        }

        Int16? _Value231;
        [Display(Description = "Value 2 3 1")]
        public Int16? Value231
        {
            get { return _Value231; }
            set
            {
                if (value != _Value231)
                {
                    _Value231 = value;
                    RaisePropertyChanged("Value231");
                }
            }
        }

        Int16? _Value232;
        [Display(Description = "Value 2 3 2")]
        public Int16? Value232
        {
            get { return _Value232; }
            set
            {
                if (value != _Value232)
                {
                    _Value232 = value;
                    RaisePropertyChanged("Value232");
                }
            }
        }

        Int16? _Value233;
        [Display(Description = "Value 2 3 3")]
        public Int16? Value233
        {
            get { return _Value233; }
            set
            {
                if (value != _Value233)
                {
                    _Value233 = value;
                    RaisePropertyChanged("Value233");
                }
            }
        }

        Int16? _Value234;
        [Display(Description = "Value 2 3 4")]
        public Int16? Value234
        {
            get { return _Value234; }
            set
            {
                if (value != _Value234)
                {
                    _Value234 = value;
                    RaisePropertyChanged("Value234");
                }
            }
        }

        Int16? _Value235;
        [Display(Description = "Value 2 3 5")]
        public Int16? Value235
        {
            get { return _Value235; }
            set
            {
                if (value != _Value235)
                {
                    _Value235 = value;
                    RaisePropertyChanged("Value235");
                }
            }
        }

        Int16? _Value236;
        [Display(Description = "Value 2 3 6")]
        public Int16? Value236
        {
            get { return _Value236; }
            set
            {
                if (value != _Value236)
                {
                    _Value236 = value;
                    RaisePropertyChanged("Value236");
                }
            }
        }

        Int16? _Value237;
        [Display(Description = "Value 2 3 7")]
        public Int16? Value237
        {
            get { return _Value237; }
            set
            {
                if (value != _Value237)
                {
                    _Value237 = value;
                    RaisePropertyChanged("Value237");
                }
            }
        }

        Int16? _Value238;
        [Display(Description = "Value 2 3 8")]
        public Int16? Value238
        {
            get { return _Value238; }
            set
            {
                if (value != _Value238)
                {
                    _Value238 = value;
                    RaisePropertyChanged("Value238");
                }
            }
        }

        Int16? _Value239;
        [Display(Description = "Value 2 3 9")]
        public Int16? Value239
        {
            get { return _Value239; }
            set
            {
                if (value != _Value239)
                {
                    _Value239 = value;
                    RaisePropertyChanged("Value239");
                }
            }
        }

        Int16? _Value240;
        [Display(Description = "Value 2 4 0")]
        public Int16? Value240
        {
            get { return _Value240; }
            set
            {
                if (value != _Value240)
                {
                    _Value240 = value;
                    RaisePropertyChanged("Value240");
                }
            }
        }

        Int16? _Value241;
        [Display(Description = "Value 2 4 1")]
        public Int16? Value241
        {
            get { return _Value241; }
            set
            {
                if (value != _Value241)
                {
                    _Value241 = value;
                    RaisePropertyChanged("Value241");
                }
            }
        }

        Int16? _Value242;
        [Display(Description = "Value 2 4 2")]
        public Int16? Value242
        {
            get { return _Value242; }
            set
            {
                if (value != _Value242)
                {
                    _Value242 = value;
                    RaisePropertyChanged("Value242");
                }
            }
        }

        Int16? _Value243;
        [Display(Description = "Value 2 4 3")]
        public Int16? Value243
        {
            get { return _Value243; }
            set
            {
                if (value != _Value243)
                {
                    _Value243 = value;
                    RaisePropertyChanged("Value243");
                }
            }
        }

        Int16? _Value244;
        [Display(Description = "Value 2 4 4")]
        public Int16? Value244
        {
            get { return _Value244; }
            set
            {
                if (value != _Value244)
                {
                    _Value244 = value;
                    RaisePropertyChanged("Value244");
                }
            }
        }

        Int16? _Value245;
        [Display(Description = "Value 2 4 5")]
        public Int16? Value245
        {
            get { return _Value245; }
            set
            {
                if (value != _Value245)
                {
                    _Value245 = value;
                    RaisePropertyChanged("Value245");
                }
            }
        }

        Int16? _Value246;
        [Display(Description = "Value 246")]
        public Int16? Value246
        {
            get { return _Value246; }
            set
            {
                if (value != _Value246)
                {
                    _Value246 = value;
                    RaisePropertyChanged("Value246");
                }
            }
        }

        Int16? _Value247;
        [Display(Description = "Value 2 4 7")]
        public Int16? Value247
        {
            get { return _Value247; }
            set
            {
                if (value != _Value247)
                {
                    _Value247 = value;
                    RaisePropertyChanged("Value247");
                }
            }
        }

        Int16? _Value248;
        [Display(Description = "Value 2 4 8")]
        public Int16? Value248
        {
            get { return _Value248; }
            set
            {
                if (value != _Value248)
                {
                    _Value248 = value;
                    RaisePropertyChanged("Value248");
                }
            }
        }

        Int16? _Value249;
        [Display(Description = "Value 2 4 9")]
        public Int16? Value249
        {
            get { return _Value249; }
            set
            {
                if (value != _Value249)
                {
                    _Value249 = value;
                    RaisePropertyChanged("Value249");
                }
            }
        }

        Int16? _Value250;
        [Display(Description = "Value 2 5 0")]
        public Int16? Value250
        {
            get { return _Value250; }
            set
            {
                if (value != _Value250)
                {
                    _Value250 = value;
                    RaisePropertyChanged("Value250");
                }
            }
        }

        Int16? _Value251;
        [Display(Description = "Value 2 5 1")]
        public Int16? Value251
        {
            get { return _Value251; }
            set
            {
                if (value != _Value251)
                {
                    _Value251 = value;
                    RaisePropertyChanged("Value251");
                }
            }
        }

        Int16? _Value252;
        [Display(Description = "Value 2 5 2")]
        public Int16? Value252
        {
            get { return _Value252; }
            set
            {
                if (value != _Value252)
                {
                    _Value252 = value;
                    RaisePropertyChanged("Value252");
                }
            }
        }

        Int16? _Value253;
        [Display(Description = "Value 2 5 3")]
        public Int16? Value253
        {
            get { return _Value253; }
            set
            {
                if (value != _Value253)
                {
                    _Value253 = value;
                    RaisePropertyChanged("Value253");
                }
            }
        }

        Int16? _Value254;
        [Display(Description = "Value 2 5 4")]
        public Int16? Value254
        {
            get { return _Value254; }
            set
            {
                if (value != _Value254)
                {
                    _Value254 = value;
                    RaisePropertyChanged("Value254");
                }
            }
        }

        Int16? _Value255;
        [Display(Description = "Value 2 5 5")]
        public Int16? Value255
        {
            get { return _Value255; }
            set
            {
                if (value != _Value255)
                {
                    _Value255 = value;
                    RaisePropertyChanged("Value255");
                }
            }
        }

        Int16? _Value256;
        [Display(Description = "Value 2 5 6")]
        public Int16? Value256
        {
            get { return _Value256; }
            set
            {
                if (value != _Value256)
                {
                    _Value256 = value;
                    RaisePropertyChanged("Value256");
                }
            }
        }

        Int16? _Value257;
        [Display(Description = "Value 2 5 7")]
        public Int16? Value257
        {
            get { return _Value257; }
            set
            {
                if (value != _Value257)
                {
                    _Value257 = value;
                    RaisePropertyChanged("Value257");
                }
            }
        }

        Int16? _Value258;
        [Display(Description = "Value 2 5 8")]
        public Int16? Value258
        {
            get { return _Value258; }
            set
            {
                if (value != _Value258)
                {
                    _Value258 = value;
                    RaisePropertyChanged("Value258");
                }
            }
        }

        Int16? _Value259;
        [Display(Description = "Value 2 5 9")]
        public Int16? Value259
        {
            get { return _Value259; }
            set
            {
                if (value != _Value259)
                {
                    _Value259 = value;
                    RaisePropertyChanged("Value259");
                }
            }
        }

        Int16? _Value260;
        [Display(Description = "Value 2 6 0")]
        public Int16? Value260
        {
            get { return _Value260; }
            set
            {
                if (value != _Value260)
                {
                    _Value260 = value;
                    RaisePropertyChanged("Value260");
                }
            }
        }

        Int16? _Value261;
        [Display(Description = "Value 2 6 1")]
        public Int16? Value261
        {
            get { return _Value261; }
            set
            {
                if (value != _Value261)
                {
                    _Value261 = value;
                    RaisePropertyChanged("Value261");
                }
            }
        }

        Int16? _Value262;
        [Display(Description = "Value 2 6 2")]
        public Int16? Value262
        {
            get { return _Value262; }
            set
            {
                if (value != _Value262)
                {
                    _Value262 = value;
                    RaisePropertyChanged("Value262");
                }
            }
        }

        Int16? _Value263;
        [Display(Description = "Value 2 6 3")]
        public Int16? Value263
        {
            get { return _Value263; }
            set
            {
                if (value != _Value263)
                {
                    _Value263 = value;
                    RaisePropertyChanged("Value263");
                }
            }
        }

        Int16? _Value264;
        [Display(Description = "Value 2 6 4")]
        public Int16? Value264
        {
            get { return _Value264; }
            set
            {
                if (value != _Value264)
                {
                    _Value264 = value;
                    RaisePropertyChanged("Value264");
                }
            }
        }

        Int16? _Value265;
        [Display(Description = "Value 2 6 5")]
        public Int16? Value265
        {
            get { return _Value265; }
            set
            {
                if (value != _Value265)
                {
                    _Value265 = value;
                    RaisePropertyChanged("Value265");
                }
            }
        }

        Int16? _Value266;
        [Display(Description = "Value 2 6 6")]
        public Int16? Value266
        {
            get { return _Value266; }
            set
            {
                if (value != _Value266)
                {
                    _Value266 = value;
                    RaisePropertyChanged("Value266");
                }
            }
        }

        Int16? _Value267;
        [Display(Description = "Value 2 6 7")]
        public Int16? Value267
        {
            get { return _Value267; }
            set
            {
                if (value != _Value267)
                {
                    _Value267 = value;
                    RaisePropertyChanged("Value267");
                }
            }
        }

        Int16? _Value268;
        [Display(Description = "Value 2 6 8")]
        public Int16? Value268
        {
            get { return _Value268; }
            set
            {
                if (value != _Value268)
                {
                    _Value268 = value;
                    RaisePropertyChanged("Value268");
                }
            }
        }

        Int16? _Value269;
        [Display(Description = "Value 2 6 9")]
        public Int16? Value269
        {
            get { return _Value269; }
            set
            {
                if (value != _Value269)
                {
                    _Value269 = value;
                    RaisePropertyChanged("Value269");
                }
            }
        }

        Int16? _Value270;
        [Display(Description = "Value 2 7 0")]
        public Int16? Value270
        {
            get { return _Value270; }
            set
            {
                if (value != _Value270)
                {
                    _Value270 = value;
                    RaisePropertyChanged("Value270");
                }
            }
        }

        Int16? _Value271;
        [Display(Description = "Value 2 7 1")]
        public Int16? Value271
        {
            get { return _Value271; }
            set
            {
                if (value != _Value271)
                {
                    _Value271 = value;
                    RaisePropertyChanged("Value271");
                }
            }
        }

        Int16? _Value272;
        [Display(Description = "Value 2 7 2")]
        public Int16? Value272
        {
            get { return _Value272; }
            set
            {
                if (value != _Value272)
                {
                    _Value272 = value;
                    RaisePropertyChanged("Value272");
                }
            }
        }

        Int16? _Value273;
        [Display(Description = "Value 2 7 3")]
        public Int16? Value273
        {
            get { return _Value273; }
            set
            {
                if (value != _Value273)
                {
                    _Value273 = value;
                    RaisePropertyChanged("Value273");
                }
            }
        }

        Int16? _Value274;
        [Display(Description = "Value 2 7 4")]
        public Int16? Value274
        {
            get { return _Value274; }
            set
            {
                if (value != _Value274)
                {
                    _Value274 = value;
                    RaisePropertyChanged("Value274");
                }
            }
        }

        Int16? _Value275;
        [Display(Description = "Value 2 7 5")]
        public Int16? Value275
        {
            get { return _Value275; }
            set
            {
                if (value != _Value275)
                {
                    _Value275 = value;
                    RaisePropertyChanged("Value275");
                }
            }
        }

        Int16? _Value276;
        [Display(Description = "Value 2 7 6")]
        public Int16? Value276
        {
            get { return _Value276; }
            set
            {
                if (value != _Value276)
                {
                    _Value276 = value;
                    RaisePropertyChanged("Value276");
                }
            }
        }

        Int16? _Value277;
        [Display(Description = "Value 2 7 7")]
        public Int16? Value277
        {
            get { return _Value277; }
            set
            {
                if (value != _Value277)
                {
                    _Value277 = value;
                    RaisePropertyChanged("Value277");
                }
            }
        }

        Int16? _Value278;
        [Display(Description = "Value 2 7 8")]
        public Int16? Value278
        {
            get { return _Value278; }
            set
            {
                if (value != _Value278)
                {
                    _Value278 = value;
                    RaisePropertyChanged("Value278");
                }
            }
        }

        Int16? _Value279;
        [Display(Description = "Value 2 7 9")]
        public Int16? Value279
        {
            get { return _Value279; }
            set
            {
                if (value != _Value279)
                {
                    _Value279 = value;
                    RaisePropertyChanged("Value279");
                }
            }
        }

        Int16? _Value280;
        [Display(Description = "Value 2 8 0")]
        public Int16? Value280
        {
            get { return _Value280; }
            set
            {
                if (value != _Value280)
                {
                    _Value280 = value;
                    RaisePropertyChanged("Value280");
                }
            }
        }

        Int16? _Value281;
        [Display(Description = "Value 2 8 1")]
        public Int16? Value281
        {
            get { return _Value281; }
            set
            {
                if (value != _Value281)
                {
                    _Value281 = value;
                    RaisePropertyChanged("Value281");
                }
            }
        }

        Int16? _Value282;
        [Display(Description = "Value 2 8 2")]
        public Int16? Value282
        {
            get { return _Value282; }
            set
            {
                if (value != _Value282)
                {
                    _Value282 = value;
                    RaisePropertyChanged("Value282");
                }
            }
        }

        Int16? _Value283;
        [Display(Description = "Value 2 8 3")]
        public Int16? Value283
        {
            get { return _Value283; }
            set
            {
                if (value != _Value283)
                {
                    _Value283 = value;
                    RaisePropertyChanged("Value283");
                }
            }
        }

        Int16? _Value284;
        [Display(Description = "Value 2 8 4")]
        public Int16? Value284
        {
            get { return _Value284; }
            set
            {
                if (value != _Value284)
                {
                    _Value284 = value;
                    RaisePropertyChanged("Value284");
                }
            }
        }

        Int16? _Value285;
        [Display(Description = "Value 2 8 5")]
        public Int16? Value285
        {
            get { return _Value285; }
            set
            {
                if (value != _Value285)
                {
                    _Value285 = value;
                    RaisePropertyChanged("Value285");
                }
            }
        }

        Int16? _Value286;
        [Display(Description = "Value 2 8 6")]
        public Int16? Value286
        {
            get { return _Value286; }
            set
            {
                if (value != _Value286)
                {
                    _Value286 = value;
                    RaisePropertyChanged("Value286");
                }
            }
        }

        Int16? _Value287;
        [Display(Description = "Value 2 8 7")]
        public Int16? Value287
        {
            get { return _Value287; }
            set
            {
                if (value != _Value287)
                {
                    _Value287 = value;
                    RaisePropertyChanged("Value287");
                }
            }
        }
        
        string _Tag;
        [Display(Description = "Tag")]
        [MaxLength(36)]
        public string Tag
        {
            get { return _Tag; }
            set
            {
                if (value != _Tag)
                {
                    _Tag = value;
                    RaisePropertyChanged("Tag");
                }
            }
        }
         

        

        #endregion

        #region Foreign Key Properties


        //project _Project;
        //public virtual project Project
        //{
        //    get { return _Project; }
        //    set
        //    {
        //        if (value != _Project)
        //        {
        //            _Project = value;
        //            RaisePropertyChanged("Project");
        //        }
        //    }
        //}


        #endregion
    }

}
