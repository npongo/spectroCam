using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace spectro.camera.service.DataObjects
{

    public class projectDTO : EntityData
    {
        public DateTime Timestamp { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public Int32 ProjectId { get; set; }

        public string Description { get; set; }

        public string ExternalIdentifier { get; set; }

        public string Name { get; set; }

        public string ProjectProtocols { get; set; }

        public string ProjectTags { get; set; }

        public string SpectralIndexes { get; set; }


    }


    public class spectraDTO : EntityData
    {
        [NotMapped] public Int32 ProjectId { get; set; }
        public string ProjectIdDTO { get; set; }
        public DateTime Timestamp { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public Int16? Value0 { get; set; }

        public Int16? Value1 { get; set; }

        public Int16? Value10 { get; set; }

        public Int16? Value100 { get; set; }

        public Int16? Value101 { get; set; }

        public Int16? Value102 { get; set; }

        public Int16? Value103 { get; set; }

        public Int16? Value104 { get; set; }

        public Int16? Value105 { get; set; }

        public Int16? Value106 { get; set; }

        public Int16? Value107 { get; set; }

        public Int16? Value108 { get; set; }

        public Int16? Value109 { get; set; }

        public Int16? Value11 { get; set; }

        public Int16? Value110 { get; set; }

        public Int16? Value111 { get; set; }

        public Int16? Value112 { get; set; }

        public Int16? Value113 { get; set; }

        public Int16? Value114 { get; set; }

        public Int16? Value115 { get; set; }

        public Int16? Value116 { get; set; }

        public Int16? Value117 { get; set; }

        public Int16? Value118 { get; set; }

        public Int16? Value119 { get; set; }

        public Int16? Value12 { get; set; }

        public Int16? Value120 { get; set; }

        public Int16? Value121 { get; set; }

        public Int16? Value122 { get; set; }

        public Int16? Value123 { get; set; }

        public Int16? Value124 { get; set; }

        public Int16? Value125 { get; set; }

        public Int16? Value126 { get; set; }

        public Int16? Value127 { get; set; }

        public Int16? Value128 { get; set; }

        public Int16? Value129 { get; set; }

        public Int16? Value13 { get; set; }

        public Int16? Value130 { get; set; }

        public Int16? Value131 { get; set; }

        public Int16? Value132 { get; set; }

        public Int16? Value133 { get; set; }

        public Int16? Value134 { get; set; }

        public Int16? Value135 { get; set; }

        public Int16? Value136 { get; set; }

        public Int16? Value137 { get; set; }

        public Int16? Value138 { get; set; }

        public Int16? Value139 { get; set; }

        public Int16? Value14 { get; set; }

        public Int16? Value140 { get; set; }

        public Int16? Value141 { get; set; }

        public Int16? Value142 { get; set; }

        public Int16? Value143 { get; set; }

        public Int16? Value144 { get; set; }

        public Int16? Value145 { get; set; }

        public Int16? Value146 { get; set; }

        public Int16? Value147 { get; set; }

        public Int16? Value148 { get; set; }

        public Int16? Value149 { get; set; }

        public Int16? Value15 { get; set; }

        public Int16? Value150 { get; set; }

        public Int16? Value151 { get; set; }

        public Int16? Value152 { get; set; }

        public Int16? Value153 { get; set; }

        public Int16? Value154 { get; set; }

        public Int16? Value155 { get; set; }

        public Int16? Value156 { get; set; }

        public Int16? Value157 { get; set; }

        public Int16? Value158 { get; set; }

        public Int16? Value159 { get; set; }

        public Int16? Value16 { get; set; }

        public Int16? Value160 { get; set; }

        public Int16? Value161 { get; set; }

        public Int16? Value162 { get; set; }

        public Int16? Value163 { get; set; }

        public Int16? Value164 { get; set; }

        public Int16? Value165 { get; set; }

        public Int16? Value166 { get; set; }

        public Int16? Value167 { get; set; }

        public Int16? Value168 { get; set; }

        public Int16? Value169 { get; set; }

        public Int16? Value17 { get; set; }

        public Int16? Value170 { get; set; }

        public Int16? Value171 { get; set; }

        public Int16? Value172 { get; set; }

        public Int16? Value173 { get; set; }

        public Int16? Value174 { get; set; }

        public Int16? Value175 { get; set; }

        public Int16? Value176 { get; set; }

        public Int16? Value177 { get; set; }

        public Int16? Value178 { get; set; }

        public Int16? Value179 { get; set; }

        public Int16? Value18 { get; set; }

        public Int16? Value180 { get; set; }

        public Int16? Value181 { get; set; }

        public Int16? Value182 { get; set; }

        public Int16? Value183 { get; set; }

        public Int16? Value184 { get; set; }

        public Int16? Value185 { get; set; }

        public Int16? Value186 { get; set; }

        public Int16? Value187 { get; set; }

        public Int16? Value188 { get; set; }

        public Int16? Value189 { get; set; }

        public Int16? Value19 { get; set; }

        public Int16? Value190 { get; set; }

        public Int16? Value191 { get; set; }

        public Int16? Value192 { get; set; }

        public Int16? Value193 { get; set; }

        public Int16? Value194 { get; set; }

        public Int16? Value195 { get; set; }

        public Int16? Value196 { get; set; }

        public Int16? Value197 { get; set; }

        public Int16? Value198 { get; set; }

        public Int16? Value199 { get; set; }

        public Int16? Value2 { get; set; }

        public Int16? Value20 { get; set; }

        public Int16? Value200 { get; set; }

        public Int16? Value201 { get; set; }

        public Int16? Value202 { get; set; }

        public Int16? Value203 { get; set; }

        public Int16? Value204 { get; set; }

        public Int16? Value205 { get; set; }

        public Int16? Value206 { get; set; }

        public Int16? Value207 { get; set; }

        public Int16? Value208 { get; set; }

        public Int16? Value209 { get; set; }

        public Int16? Value21 { get; set; }

        public Int16? Value210 { get; set; }

        public Int16? Value211 { get; set; }

        public Int16? Value212 { get; set; }

        public Int16? Value213 { get; set; }

        public Int16? Value214 { get; set; }

        public Int16? Value215 { get; set; }

        public Int16? Value216 { get; set; }

        public Int16? Value217 { get; set; }

        public Int16? Value218 { get; set; }

        public Int16? Value219 { get; set; }

        public Int16? Value22 { get; set; }

        public Int16? Value220 { get; set; }

        public Int16? Value221 { get; set; }

        public Int16? Value222 { get; set; }

        public Int16? Value223 { get; set; }

        public Int16? Value224 { get; set; }

        public Int16? Value225 { get; set; }

        public Int16? Value226 { get; set; }

        public Int16? Value227 { get; set; }

        public Int16? Value228 { get; set; }

        public Int16? Value229 { get; set; }

        public Int16? Value23 { get; set; }

        public Int16? Value230 { get; set; }

        public Int16? Value231 { get; set; }

        public Int16? Value232 { get; set; }

        public Int16? Value233 { get; set; }

        public Int16? Value234 { get; set; }

        public Int16? Value235 { get; set; }

        public Int16? Value236 { get; set; }

        public Int16? Value237 { get; set; }

        public Int16? Value238 { get; set; }

        public Int16? Value239 { get; set; }

        public Int16? Value24 { get; set; }

        public Int16? Value240 { get; set; }

        public Int16? Value241 { get; set; }

        public Int16? Value242 { get; set; }

        public Int16? Value243 { get; set; }

        public Int16? Value244 { get; set; }

        public Int16? Value245 { get; set; }

        public Int16? Value246 { get; set; }

        public Int16? Value247 { get; set; }

        public Int16? Value248 { get; set; }

        public Int16? Value249 { get; set; }

        public Int16? Value25 { get; set; }

        public Int16? Value250 { get; set; }

        public Int16? Value251 { get; set; }

        public Int16? Value252 { get; set; }

        public Int16? Value253 { get; set; }

        public Int16? Value254 { get; set; }

        public Int16? Value255 { get; set; }

        public Int16? Value256 { get; set; }

        public Int16? Value257 { get; set; }

        public Int16? Value258 { get; set; }

        public Int16? Value259 { get; set; }

        public Int16? Value26 { get; set; }

        public Int16? Value260 { get; set; }

        public Int16? Value261 { get; set; }

        public Int16? Value262 { get; set; }

        public Int16? Value263 { get; set; }

        public Int16? Value264 { get; set; }

        public Int16? Value265 { get; set; }

        public Int16? Value266 { get; set; }

        public Int16? Value267 { get; set; }

        public Int16? Value268 { get; set; }

        public Int16? Value269 { get; set; }

        public Int16? Value27 { get; set; }

        public Int16? Value270 { get; set; }

        public Int16? Value271 { get; set; }

        public Int16? Value272 { get; set; }

        public Int16? Value273 { get; set; }

        public Int16? Value274 { get; set; }

        public Int16? Value275 { get; set; }

        public Int16? Value276 { get; set; }

        public Int16? Value277 { get; set; }

        public Int16? Value278 { get; set; }

        public Int16? Value279 { get; set; }

        public Int16? Value28 { get; set; }

        public Int16? Value280 { get; set; }

        public Int16? Value281 { get; set; }

        public Int16? Value282 { get; set; }

        public Int16? Value283 { get; set; }

        public Int16? Value284 { get; set; }

        public Int16? Value285 { get; set; }

        public Int16? Value286 { get; set; }

        public Int16? Value287 { get; set; }

        public Int16? Value29 { get; set; }

        public Int16? Value3 { get; set; }

        public Int16? Value30 { get; set; }

        public Int16? Value31 { get; set; }

        public Int16? Value32 { get; set; }

        public Int16? Value33 { get; set; }

        public Int16? Value34 { get; set; }

        public Int16? Value35 { get; set; }

        public Int16? Value36 { get; set; }

        public Int16? Value37 { get; set; }

        public Int16? Value38 { get; set; }

        public Int16? Value39 { get; set; }

        public Int16? Value4 { get; set; }

        public Int16? Value40 { get; set; }

        public Int16? Value41 { get; set; }

        public Int16? Value42 { get; set; }

        public Int16? Value43 { get; set; }

        public Int16? Value44 { get; set; }

        public Int16? Value45 { get; set; }

        public Int16? Value46 { get; set; }

        public Int16? Value47 { get; set; }

        public Int16? Value48 { get; set; }

        public Int16? Value49 { get; set; }

        public Int16? Value5 { get; set; }

        public Int16? Value50 { get; set; }

        public Int16? Value51 { get; set; }

        public Int16? Value52 { get; set; }

        public Int16? Value53 { get; set; }

        public Int16? Value54 { get; set; }

        public Int16? Value55 { get; set; }

        public Int16? Value56 { get; set; }

        public Int16? Value57 { get; set; }

        public Int16? Value58 { get; set; }

        public Int16? Value59 { get; set; }

        public Int16? Value6 { get; set; }

        public Int16? Value60 { get; set; }

        public Int16? Value61 { get; set; }

        public Int16? Value62 { get; set; }

        public Int16? Value63 { get; set; }

        public Int16? Value64 { get; set; }

        public Int16? Value65 { get; set; }

        public Int16? Value66 { get; set; }

        public Int16? Value67 { get; set; }

        public Int16? Value68 { get; set; }

        public Int16? Value69 { get; set; }

        public Int16? Value7 { get; set; }

        public Int16? Value70 { get; set; }

        public Int16? Value71 { get; set; }

        public Int16? Value72 { get; set; }

        public Int16? Value73 { get; set; }

        public Int16? Value74 { get; set; }

        public Int16? Value75 { get; set; }

        public Int16? Value76 { get; set; }

        public Int16? Value77 { get; set; }

        public Int16? Value78 { get; set; }

        public Int16? Value79 { get; set; }

        public Int16? Value8 { get; set; }

        public Int16? Value80 { get; set; }

        public Int16? Value81 { get; set; }

        public Int16? Value82 { get; set; }

        public Int16? Value83 { get; set; }

        public Int16? Value84 { get; set; }

        public Int16? Value85 { get; set; }

        public Int16? Value86 { get; set; }

        public Int16? Value87 { get; set; }

        public Int16? Value88 { get; set; }

        public Int16? Value89 { get; set; }

        public Int16? Value9 { get; set; }

        public Int16? Value90 { get; set; }

        public Int16? Value91 { get; set; }

        public Int16? Value92 { get; set; }

        public Int16? Value93 { get; set; }

        public Int16? Value94 { get; set; }

        public Int16? Value95 { get; set; }

        public Int16? Value96 { get; set; }

        public Int16? Value97 { get; set; }

        public Int16? Value98 { get; set; }

        public Int16? Value99 { get; set; }

        public Int32 SpectraId { get; set; }

        public Int32? IntegrationTime { get; set; }

        public string Description { get; set; }

        public string ExternalIdentifier { get; set; }

        public string filter { get; set; }

        public string Name { get; set; }

        public string Protocol { get; set; }

        public string SpectrometerSerialNumber { get; set; }

        public string Tag { get; set; }


    }


}