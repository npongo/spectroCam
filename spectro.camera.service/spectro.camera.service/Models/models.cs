using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace spectro.camera.service.Models
{
    [Table("projects")]
    public class project
    {

        #region EntityData Properties 

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Index]
        [TableColumn(TableColumnType.CreatedAt)]
        public DateTimeOffset? CreatedAt { get; set; }

        [TableColumn(TableColumnType.Deleted)]
        public bool Deleted { get; set; }

        [Index]
        [TableColumn(TableColumnType.Id)]
        [MaxLength(36)]
        public string Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [TableColumn(TableColumnType.UpdatedAt)]
        public DateTimeOffset? UpdatedAt { get; set; }

        [TableColumn(TableColumnType.Version)]
        [Timestamp]
        public byte[] Version { get; set; }

        #endregion

        #region Properties

        [Required()]
        [Key]

        [Column(Order = 1)]
        public Int32 ProjectId { get; set; }

        [MaxLength(256)]

        [Column(Order = 2)]
        public string ProjectProtocols { get; set; }

        [MaxLength(256)]

        [Column(Order = 3)]
        public string ProjectTags { get; set; }

        [MaxLength(1024)]

        [Column(Order = 4)]
        public string SpectralIndexes { get; set; }

        [MaxLength(128)]

        [Column(Order = 5)]
        public string ExternalIdentifier { get; set; }

        [MaxLength(128)]

        [Column(Order = 6)]
        public string Name { get; set; }

        [MaxLength(128)]

        [Column(Order = 7)]
        public string Description { get; set; }


        [Column(Order = 8)]
        public double? Latitude { get; set; }


        [Column(Order = 9)]
        public double? Longitude { get; set; }

        [Required()]

        [Column(Order = 10)]
        public DateTime Timestamp { get; set; }

        #endregion

        #region Foreign Key Properties



        public virtual ICollection<spectra> spectra { get; set; }


        #endregion
    }


    [Table("spectra")]
    public class spectra
    {

        #region EntityData Properties 

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Index]
        [TableColumn(TableColumnType.CreatedAt)]
        public DateTimeOffset? CreatedAt { get; set; }

        [TableColumn(TableColumnType.Deleted)]
        public bool Deleted { get; set; }

        [Index]
        [TableColumn(TableColumnType.Id)]
        [MaxLength(36)]
        public string Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [TableColumn(TableColumnType.UpdatedAt)]
        public DateTimeOffset? UpdatedAt { get; set; }

        [TableColumn(TableColumnType.Version)]
        [Timestamp]
        public byte[] Version { get; set; }

        #endregion

        #region Properties

        [Required()]
        [Key]

        [Column(Order = 1)]
        public Int32 SpectraId { get; set; }

        [MaxLength(16)]
        [Required()]

        [Column(Order = 2)]
        public string SpectrometerSerialNumber { get; set; }

        [MaxLength(32)]

        [Column(Order = 3)]
        public string Protocol { get; set; }

        [MaxLength(32)]

        [Column(Order = 4)]
        public string filter { get; set; }


        [Column(Order = 5)]
        public Int32? IntegrationTime { get; set; }

        [Required()]

        [Column(Order = 6)]
        public Int32 ProjectId { get; set; }


        [Column(Order = 7)]
        public Int16? Value0 { get; set; }


        [Column(Order = 8)]
        public Int16? Value1 { get; set; }


        [Column(Order = 9)]
        public Int16? Value2 { get; set; }


        [Column(Order = 10)]
        public Int16? Value3 { get; set; }


        [Column(Order = 11)]
        public Int16? Value4 { get; set; }


        [Column(Order = 12)]
        public Int16? Value5 { get; set; }


        [Column(Order = 13)]
        public Int16? Value6 { get; set; }


        [Column(Order = 14)]
        public Int16? Value7 { get; set; }


        [Column(Order = 15)]
        public Int16? Value8 { get; set; }


        [Column(Order = 16)]
        public Int16? Value9 { get; set; }


        [Column(Order = 17)]
        public Int16? Value10 { get; set; }


        [Column(Order = 18)]
        public Int16? Value11 { get; set; }


        [Column(Order = 19)]
        public Int16? Value12 { get; set; }


        [Column(Order = 20)]
        public Int16? Value13 { get; set; }


        [Column(Order = 21)]
        public Int16? Value14 { get; set; }


        [Column(Order = 22)]
        public Int16? Value15 { get; set; }


        [Column(Order = 23)]
        public Int16? Value16 { get; set; }


        [Column(Order = 24)]
        public Int16? Value17 { get; set; }


        [Column(Order = 25)]
        public Int16? Value18 { get; set; }


        [Column(Order = 26)]
        public Int16? Value19 { get; set; }


        [Column(Order = 27)]
        public Int16? Value20 { get; set; }


        [Column(Order = 28)]
        public Int16? Value21 { get; set; }


        [Column(Order = 29)]
        public Int16? Value22 { get; set; }


        [Column(Order = 30)]
        public Int16? Value23 { get; set; }


        [Column(Order = 31)]
        public Int16? Value24 { get; set; }


        [Column(Order = 32)]
        public Int16? Value25 { get; set; }


        [Column(Order = 33)]
        public Int16? Value26 { get; set; }


        [Column(Order = 34)]
        public Int16? Value27 { get; set; }


        [Column(Order = 35)]
        public Int16? Value28 { get; set; }


        [Column(Order = 36)]
        public Int16? Value29 { get; set; }


        [Column(Order = 37)]
        public Int16? Value30 { get; set; }


        [Column(Order = 38)]
        public Int16? Value31 { get; set; }


        [Column(Order = 39)]
        public Int16? Value32 { get; set; }


        [Column(Order = 40)]
        public Int16? Value33 { get; set; }


        [Column(Order = 41)]
        public Int16? Value34 { get; set; }


        [Column(Order = 42)]
        public Int16? Value35 { get; set; }


        [Column(Order = 43)]
        public Int16? Value36 { get; set; }


        [Column(Order = 44)]
        public Int16? Value37 { get; set; }


        [Column(Order = 45)]
        public Int16? Value38 { get; set; }


        [Column(Order = 46)]
        public Int16? Value39 { get; set; }


        [Column(Order = 47)]
        public Int16? Value40 { get; set; }


        [Column(Order = 48)]
        public Int16? Value41 { get; set; }


        [Column(Order = 49)]
        public Int16? Value42 { get; set; }


        [Column(Order = 50)]
        public Int16? Value43 { get; set; }


        [Column(Order = 51)]
        public Int16? Value44 { get; set; }


        [Column(Order = 52)]
        public Int16? Value45 { get; set; }


        [Column(Order = 53)]
        public Int16? Value46 { get; set; }


        [Column(Order = 54)]
        public Int16? Value47 { get; set; }


        [Column(Order = 55)]
        public Int16? Value48 { get; set; }


        [Column(Order = 56)]
        public Int16? Value49 { get; set; }


        [Column(Order = 57)]
        public Int16? Value50 { get; set; }


        [Column(Order = 58)]
        public Int16? Value51 { get; set; }


        [Column(Order = 59)]
        public Int16? Value52 { get; set; }


        [Column(Order = 60)]
        public Int16? Value53 { get; set; }


        [Column(Order = 61)]
        public Int16? Value54 { get; set; }


        [Column(Order = 62)]
        public Int16? Value55 { get; set; }


        [Column(Order = 63)]
        public Int16? Value56 { get; set; }


        [Column(Order = 64)]
        public Int16? Value57 { get; set; }


        [Column(Order = 65)]
        public Int16? Value58 { get; set; }


        [Column(Order = 66)]
        public Int16? Value59 { get; set; }


        [Column(Order = 67)]
        public Int16? Value60 { get; set; }


        [Column(Order = 68)]
        public Int16? Value61 { get; set; }


        [Column(Order = 69)]
        public Int16? Value62 { get; set; }


        [Column(Order = 70)]
        public Int16? Value63 { get; set; }


        [Column(Order = 71)]
        public Int16? Value64 { get; set; }


        [Column(Order = 72)]
        public Int16? Value65 { get; set; }


        [Column(Order = 73)]
        public Int16? Value66 { get; set; }


        [Column(Order = 74)]
        public Int16? Value67 { get; set; }


        [Column(Order = 75)]
        public Int16? Value68 { get; set; }


        [Column(Order = 76)]
        public Int16? Value69 { get; set; }


        [Column(Order = 77)]
        public Int16? Value70 { get; set; }


        [Column(Order = 78)]
        public Int16? Value71 { get; set; }


        [Column(Order = 79)]
        public Int16? Value72 { get; set; }


        [Column(Order = 80)]
        public Int16? Value73 { get; set; }


        [Column(Order = 81)]
        public Int16? Value74 { get; set; }


        [Column(Order = 82)]
        public Int16? Value75 { get; set; }


        [Column(Order = 83)]
        public Int16? Value76 { get; set; }


        [Column(Order = 84)]
        public Int16? Value77 { get; set; }


        [Column(Order = 85)]
        public Int16? Value78 { get; set; }


        [Column(Order = 86)]
        public Int16? Value79 { get; set; }


        [Column(Order = 87)]
        public Int16? Value80 { get; set; }


        [Column(Order = 88)]
        public Int16? Value81 { get; set; }


        [Column(Order = 89)]
        public Int16? Value82 { get; set; }


        [Column(Order = 90)]
        public Int16? Value83 { get; set; }


        [Column(Order = 91)]
        public Int16? Value84 { get; set; }


        [Column(Order = 92)]
        public Int16? Value85 { get; set; }


        [Column(Order = 93)]
        public Int16? Value86 { get; set; }


        [Column(Order = 94)]
        public Int16? Value87 { get; set; }


        [Column(Order = 95)]
        public Int16? Value88 { get; set; }


        [Column(Order = 96)]
        public Int16? Value89 { get; set; }


        [Column(Order = 97)]
        public Int16? Value90 { get; set; }


        [Column(Order = 98)]
        public Int16? Value91 { get; set; }


        [Column(Order = 99)]
        public Int16? Value92 { get; set; }


        [Column(Order = 100)]
        public Int16? Value93 { get; set; }


        [Column(Order = 101)]
        public Int16? Value94 { get; set; }


        [Column(Order = 102)]
        public Int16? Value95 { get; set; }


        [Column(Order = 103)]
        public Int16? Value96 { get; set; }


        [Column(Order = 104)]
        public Int16? Value97 { get; set; }


        [Column(Order = 105)]
        public Int16? Value98 { get; set; }


        [Column(Order = 106)]
        public Int16? Value99 { get; set; }


        [Column(Order = 107)]
        public Int16? Value100 { get; set; }


        [Column(Order = 108)]
        public Int16? Value101 { get; set; }


        [Column(Order = 109)]
        public Int16? Value102 { get; set; }


        [Column(Order = 110)]
        public Int16? Value103 { get; set; }


        [Column(Order = 111)]
        public Int16? Value104 { get; set; }


        [Column(Order = 112)]
        public Int16? Value105 { get; set; }


        [Column(Order = 113)]
        public Int16? Value106 { get; set; }


        [Column(Order = 114)]
        public Int16? Value107 { get; set; }


        [Column(Order = 115)]
        public Int16? Value108 { get; set; }


        [Column(Order = 116)]
        public Int16? Value109 { get; set; }


        [Column(Order = 117)]
        public Int16? Value110 { get; set; }


        [Column(Order = 118)]
        public Int16? Value111 { get; set; }


        [Column(Order = 119)]
        public Int16? Value112 { get; set; }


        [Column(Order = 120)]
        public Int16? Value113 { get; set; }


        [Column(Order = 121)]
        public Int16? Value114 { get; set; }


        [Column(Order = 122)]
        public Int16? Value115 { get; set; }


        [Column(Order = 123)]
        public Int16? Value116 { get; set; }


        [Column(Order = 124)]
        public Int16? Value117 { get; set; }


        [Column(Order = 125)]
        public Int16? Value118 { get; set; }


        [Column(Order = 126)]
        public Int16? Value119 { get; set; }


        [Column(Order = 127)]
        public Int16? Value120 { get; set; }


        [Column(Order = 128)]
        public Int16? Value121 { get; set; }


        [Column(Order = 129)]
        public Int16? Value122 { get; set; }


        [Column(Order = 130)]
        public Int16? Value123 { get; set; }


        [Column(Order = 131)]
        public Int16? Value124 { get; set; }


        [Column(Order = 132)]
        public Int16? Value125 { get; set; }


        [Column(Order = 133)]
        public Int16? Value126 { get; set; }


        [Column(Order = 134)]
        public Int16? Value127 { get; set; }


        [Column(Order = 135)]
        public Int16? Value128 { get; set; }


        [Column(Order = 136)]
        public Int16? Value129 { get; set; }


        [Column(Order = 137)]
        public Int16? Value130 { get; set; }


        [Column(Order = 138)]
        public Int16? Value131 { get; set; }


        [Column(Order = 139)]
        public Int16? Value132 { get; set; }


        [Column(Order = 140)]
        public Int16? Value133 { get; set; }


        [Column(Order = 141)]
        public Int16? Value134 { get; set; }


        [Column(Order = 142)]
        public Int16? Value135 { get; set; }


        [Column(Order = 143)]
        public Int16? Value136 { get; set; }


        [Column(Order = 144)]
        public Int16? Value137 { get; set; }


        [Column(Order = 145)]
        public Int16? Value138 { get; set; }


        [Column(Order = 146)]
        public Int16? Value139 { get; set; }


        [Column(Order = 147)]
        public Int16? Value140 { get; set; }


        [Column(Order = 148)]
        public Int16? Value141 { get; set; }


        [Column(Order = 149)]
        public Int16? Value142 { get; set; }


        [Column(Order = 150)]
        public Int16? Value143 { get; set; }


        [Column(Order = 151)]
        public Int16? Value144 { get; set; }


        [Column(Order = 152)]
        public Int16? Value145 { get; set; }


        [Column(Order = 153)]
        public Int16? Value146 { get; set; }


        [Column(Order = 154)]
        public Int16? Value147 { get; set; }


        [Column(Order = 155)]
        public Int16? Value148 { get; set; }


        [Column(Order = 156)]
        public Int16? Value149 { get; set; }


        [Column(Order = 157)]
        public Int16? Value150 { get; set; }


        [Column(Order = 158)]
        public Int16? Value151 { get; set; }


        [Column(Order = 159)]
        public Int16? Value152 { get; set; }


        [Column(Order = 160)]
        public Int16? Value153 { get; set; }


        [Column(Order = 161)]
        public Int16? Value154 { get; set; }


        [Column(Order = 162)]
        public Int16? Value155 { get; set; }


        [Column(Order = 163)]
        public Int16? Value156 { get; set; }


        [Column(Order = 164)]
        public Int16? Value157 { get; set; }


        [Column(Order = 165)]
        public Int16? Value158 { get; set; }


        [Column(Order = 166)]
        public Int16? Value159 { get; set; }


        [Column(Order = 167)]
        public Int16? Value160 { get; set; }


        [Column(Order = 168)]
        public Int16? Value161 { get; set; }


        [Column(Order = 169)]
        public Int16? Value162 { get; set; }


        [Column(Order = 170)]
        public Int16? Value163 { get; set; }


        [Column(Order = 171)]
        public Int16? Value164 { get; set; }


        [Column(Order = 172)]
        public Int16? Value165 { get; set; }


        [Column(Order = 173)]
        public Int16? Value166 { get; set; }


        [Column(Order = 174)]
        public Int16? Value167 { get; set; }


        [Column(Order = 175)]
        public Int16? Value168 { get; set; }


        [Column(Order = 176)]
        public Int16? Value169 { get; set; }


        [Column(Order = 177)]
        public Int16? Value170 { get; set; }


        [Column(Order = 178)]
        public Int16? Value171 { get; set; }


        [Column(Order = 179)]
        public Int16? Value172 { get; set; }


        [Column(Order = 180)]
        public Int16? Value173 { get; set; }


        [Column(Order = 181)]
        public Int16? Value174 { get; set; }


        [Column(Order = 182)]
        public Int16? Value175 { get; set; }


        [Column(Order = 183)]
        public Int16? Value176 { get; set; }


        [Column(Order = 184)]
        public Int16? Value177 { get; set; }


        [Column(Order = 185)]
        public Int16? Value178 { get; set; }


        [Column(Order = 186)]
        public Int16? Value179 { get; set; }


        [Column(Order = 187)]
        public Int16? Value180 { get; set; }


        [Column(Order = 188)]
        public Int16? Value181 { get; set; }


        [Column(Order = 189)]
        public Int16? Value182 { get; set; }


        [Column(Order = 190)]
        public Int16? Value183 { get; set; }


        [Column(Order = 191)]
        public Int16? Value184 { get; set; }


        [Column(Order = 192)]
        public Int16? Value185 { get; set; }


        [Column(Order = 193)]
        public Int16? Value186 { get; set; }


        [Column(Order = 194)]
        public Int16? Value187 { get; set; }


        [Column(Order = 195)]
        public Int16? Value188 { get; set; }


        [Column(Order = 196)]
        public Int16? Value189 { get; set; }


        [Column(Order = 197)]
        public Int16? Value190 { get; set; }


        [Column(Order = 198)]
        public Int16? Value191 { get; set; }


        [Column(Order = 199)]
        public Int16? Value192 { get; set; }


        [Column(Order = 200)]
        public Int16? Value193 { get; set; }


        [Column(Order = 201)]
        public Int16? Value194 { get; set; }


        [Column(Order = 202)]
        public Int16? Value195 { get; set; }


        [Column(Order = 203)]
        public Int16? Value196 { get; set; }


        [Column(Order = 204)]
        public Int16? Value197 { get; set; }


        [Column(Order = 205)]
        public Int16? Value198 { get; set; }


        [Column(Order = 206)]
        public Int16? Value199 { get; set; }


        [Column(Order = 207)]
        public Int16? Value200 { get; set; }


        [Column(Order = 208)]
        public Int16? Value201 { get; set; }


        [Column(Order = 209)]
        public Int16? Value202 { get; set; }


        [Column(Order = 210)]
        public Int16? Value203 { get; set; }


        [Column(Order = 211)]
        public Int16? Value204 { get; set; }


        [Column(Order = 212)]
        public Int16? Value205 { get; set; }


        [Column(Order = 213)]
        public Int16? Value206 { get; set; }


        [Column(Order = 214)]
        public Int16? Value207 { get; set; }


        [Column(Order = 215)]
        public Int16? Value208 { get; set; }


        [Column(Order = 216)]
        public Int16? Value209 { get; set; }


        [Column(Order = 217)]
        public Int16? Value210 { get; set; }


        [Column(Order = 218)]
        public Int16? Value211 { get; set; }


        [Column(Order = 219)]
        public Int16? Value212 { get; set; }


        [Column(Order = 220)]
        public Int16? Value213 { get; set; }


        [Column(Order = 221)]
        public Int16? Value214 { get; set; }


        [Column(Order = 222)]
        public Int16? Value215 { get; set; }


        [Column(Order = 223)]
        public Int16? Value216 { get; set; }


        [Column(Order = 224)]
        public Int16? Value217 { get; set; }


        [Column(Order = 225)]
        public Int16? Value218 { get; set; }


        [Column(Order = 226)]
        public Int16? Value219 { get; set; }


        [Column(Order = 227)]
        public Int16? Value220 { get; set; }


        [Column(Order = 228)]
        public Int16? Value221 { get; set; }


        [Column(Order = 229)]
        public Int16? Value222 { get; set; }


        [Column(Order = 230)]
        public Int16? Value223 { get; set; }


        [Column(Order = 231)]
        public Int16? Value224 { get; set; }


        [Column(Order = 232)]
        public Int16? Value225 { get; set; }


        [Column(Order = 233)]
        public Int16? Value226 { get; set; }


        [Column(Order = 234)]
        public Int16? Value227 { get; set; }


        [Column(Order = 235)]
        public Int16? Value228 { get; set; }


        [Column(Order = 236)]
        public Int16? Value229 { get; set; }


        [Column(Order = 237)]
        public Int16? Value230 { get; set; }


        [Column(Order = 238)]
        public Int16? Value231 { get; set; }


        [Column(Order = 239)]
        public Int16? Value232 { get; set; }


        [Column(Order = 240)]
        public Int16? Value233 { get; set; }


        [Column(Order = 241)]
        public Int16? Value234 { get; set; }


        [Column(Order = 242)]
        public Int16? Value235 { get; set; }


        [Column(Order = 243)]
        public Int16? Value236 { get; set; }


        [Column(Order = 244)]
        public Int16? Value237 { get; set; }


        [Column(Order = 245)]
        public Int16? Value238 { get; set; }


        [Column(Order = 246)]
        public Int16? Value239 { get; set; }


        [Column(Order = 247)]
        public Int16? Value240 { get; set; }


        [Column(Order = 248)]
        public Int16? Value241 { get; set; }


        [Column(Order = 249)]
        public Int16? Value242 { get; set; }


        [Column(Order = 250)]
        public Int16? Value243 { get; set; }


        [Column(Order = 251)]
        public Int16? Value244 { get; set; }


        [Column(Order = 252)]
        public Int16? Value245 { get; set; }


        [Column(Order = 253)]
        public Int16? Value246 { get; set; }


        [Column(Order = 254)]
        public Int16? Value247 { get; set; }


        [Column(Order = 255)]
        public Int16? Value248 { get; set; }


        [Column(Order = 256)]
        public Int16? Value249 { get; set; }


        [Column(Order = 257)]
        public Int16? Value250 { get; set; }


        [Column(Order = 258)]
        public Int16? Value251 { get; set; }


        [Column(Order = 259)]
        public Int16? Value252 { get; set; }


        [Column(Order = 260)]
        public Int16? Value253 { get; set; }


        [Column(Order = 261)]
        public Int16? Value254 { get; set; }


        [Column(Order = 262)]
        public Int16? Value255 { get; set; }


        [Column(Order = 263)]
        public Int16? Value256 { get; set; }


        [Column(Order = 264)]
        public Int16? Value257 { get; set; }


        [Column(Order = 265)]
        public Int16? Value258 { get; set; }


        [Column(Order = 266)]
        public Int16? Value259 { get; set; }


        [Column(Order = 267)]
        public Int16? Value260 { get; set; }


        [Column(Order = 268)]
        public Int16? Value261 { get; set; }


        [Column(Order = 269)]
        public Int16? Value262 { get; set; }


        [Column(Order = 270)]
        public Int16? Value263 { get; set; }


        [Column(Order = 271)]
        public Int16? Value264 { get; set; }


        [Column(Order = 272)]
        public Int16? Value265 { get; set; }


        [Column(Order = 273)]
        public Int16? Value266 { get; set; }


        [Column(Order = 274)]
        public Int16? Value267 { get; set; }


        [Column(Order = 275)]
        public Int16? Value268 { get; set; }


        [Column(Order = 276)]
        public Int16? Value269 { get; set; }


        [Column(Order = 277)]
        public Int16? Value270 { get; set; }


        [Column(Order = 278)]
        public Int16? Value271 { get; set; }


        [Column(Order = 279)]
        public Int16? Value272 { get; set; }


        [Column(Order = 280)]
        public Int16? Value273 { get; set; }


        [Column(Order = 281)]
        public Int16? Value274 { get; set; }


        [Column(Order = 282)]
        public Int16? Value275 { get; set; }


        [Column(Order = 283)]
        public Int16? Value276 { get; set; }


        [Column(Order = 284)]
        public Int16? Value277 { get; set; }


        [Column(Order = 285)]
        public Int16? Value278 { get; set; }


        [Column(Order = 286)]
        public Int16? Value279 { get; set; }


        [Column(Order = 287)]
        public Int16? Value280 { get; set; }


        [Column(Order = 288)]
        public Int16? Value281 { get; set; }


        [Column(Order = 289)]
        public Int16? Value282 { get; set; }


        [Column(Order = 290)]
        public Int16? Value283 { get; set; }


        [Column(Order = 291)]
        public Int16? Value284 { get; set; }


        [Column(Order = 292)]
        public Int16? Value285 { get; set; }


        [Column(Order = 293)]
        public Int16? Value286 { get; set; }


        [Column(Order = 294)]
        public Int16? Value287 { get; set; }

        [MaxLength(36)]

        [Column(Order = 295)]
        public string Tag { get; set; }

        [MaxLength(128)]

        [Column(Order = 296)]
        public string ExternalIdentifier { get; set; }

        [MaxLength(128)]

        [Column(Order = 297)]
        public string Name { get; set; }

        [MaxLength(128)]

        [Column(Order = 298)]
        public string Description { get; set; }


        [Column(Order = 299)]
        public double? Latitude { get; set; }


        [Column(Order = 300)]
        public double? Longitude { get; set; }

        [Required()]

        [Column(Order = 301)]
        public DateTime Timestamp { get; set; }

        #endregion

        #region Foreign Key Properties


        public virtual project Project { get; set; }



        #endregion
    }



}