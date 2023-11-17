using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bc_prace.Controls.MyGraphControl.Entities
{
    public class DArc : DEntity, IDrawable
    {
        public DArc()
        {
            Color = Color.Black;
            Visible = true;
        }

        public PointF Center { get; set; }
        public float StartAngle { get; set; }
        public float SweepAngle { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public override GeometricExtension GeometricExtents { get => Extents; }

        public GeometricExtension Extents { get; set; }

        public override void Draw(Graphics e)
        {
            if (Visible)
            {
                Pen pen = new Pen(this.Color);
                pen.Brush = new SolidBrush(Color);
                pen.Width = this.PenWidth;
                if (this.Selected)
                    pen.Color = this.SelectedColor;
                e.DrawArc(pen, Center.X - Width / 2, -Center.Y - Height / 2, Width, Height, -StartAngle, -SweepAngle);
                pen.Dispose();
            }
        }
    }
}
