using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bc_prace.Controls.MyGraphControl.Entities
{
    public class DCircle : DEntity, IDrawable
    {

        public DCircle()
        {
            Color = Color.Blue;
            Visible = true;
        }

        public DPoint Center { get; set; }

        public float Radius { get; set; }

        public bool Solid { get; set; }
        public Color SolidColor { get; set; }

        public float Diameter
        {
            get
            {
                return Radius * 2;
            }
            set
            {
                Radius = value / 2;
            }
        }

        public override GeometricExtension GeometricExtents
        {
            get
            {
                return new GeometricExtension(Center.Position.X - Radius,
                    Center.Position.Y - Radius,
                    Center.Position.X + Radius,
                    Center.Position.Y + Radius);
            }
        }

        public override void Draw(Graphics e)
        {
            if (Visible)
            {
                Pen pen = new Pen(this.Color);
                pen.Brush = new SolidBrush(Color);
                pen.Width = this.PenWidth;
                if (this.Selected)
                    pen.Color = this.SelectedColor;
                if (Solid)
                {
                    var solid = new SolidBrush(SolidColor);
                    e.FillEllipse(solid, Center.Position.X - Radius, -Center.Position.Y - Radius, Diameter, Diameter);
                    solid.Dispose();
                }
                else
                {
                    e.DrawEllipse(pen, Center.Position.X - Radius, -Center.Position.Y - Radius, Diameter, Diameter);
                }
                pen.Dispose();
            }
        }
    }
}
