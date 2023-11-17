using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bc_prace.Controls.MyGraphControl.Entities
{
    public class DImage : DEntity, IDrawable
    {
        public override GeometricExtension GeometricExtents
        {
            get
            {
                return new GeometricExtension();
            }
        }

        public PointF Location { get; set; }
        public Image Image { get; set; }

        public float ImageZoom { get; set; }

        public bool EnableScaling { get; set; }

        public override void Draw(Graphics e)
        {

            Matrix transform = e.Transform;
            float x = Location.X;
            float y = Location.Y;
            if (EnableScaling == false)
            {
                e.ScaleTransform(1 / transform.Elements[0], 1 / transform.Elements[3]);
                x = x * transform.Elements[0];
                y = y * transform.Elements[3];
            }
            if (ImageZoom == 0)
                ImageZoom = 1;
            e.DrawImage(Image, x, -y, Image.Width * ImageZoom, Image.Height * ImageZoom);
            e.Transform = transform;
        }
    }
}
