using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot.Axes;

namespace spectra.camera.Helpers
{
    public class AnchoredZoomLinearAxes:LinearAxis
    {
        public double? ZoomAnchor { get; set; }
        public override void Zoom(double x0, double x1)
        {
            if (ZoomAnchor != null) base.Zoom(ZoomAnchor.Value, x1);
            else base.Zoom(x0, x1);
        }

        public override void ZoomAt(double factor, double x)
        {
            if (ZoomAnchor != null) base.ZoomAt(factor, ZoomAnchor.Value);
            else base.ZoomAt(factor, x);
        }


    }
}
