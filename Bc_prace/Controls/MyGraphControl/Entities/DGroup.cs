using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bc_prace.Controls.MyGraphControl.Entities
{
    public class DGroup : DEntity, IDrawable
    {
        public DGroup()
        {
            Items = new List<DEntity>();
            Visible = true;
        }

        public List<DEntity> Items { get; set; }

        public void SetColor(Color c)
        {
            foreach (var item in Items)
            {
                item.Color = c;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public override GeometricExtension GeometricExtents
        {
            get
            {
                float xmin = float.MaxValue, xmax = float.MinValue;
                float ymin = float.MaxValue, ymax = float.MinValue;
                foreach (DEntity item in Items)
                {
                    GeometricExtension g = item.GeometricExtents;
                    if (g.Minimum.X < xmin)
                        xmin = g.Minimum.X;
                    if (g.Maximum.X > xmax)
                        xmax = g.Maximum.X;
                    if (g.Minimum.Y < ymin)
                        ymin = g.Minimum.Y;
                    if (g.Maximum.Y > ymax)
                        ymax = g.Maximum.Y;
                }
                return new GeometricExtension(xmin, ymin, xmax, ymax);
            }
        }

        public override void Draw(Graphics e)
        {
            if (Visible)
            {
                foreach (IDrawable item in Items)
                {
                    item.Draw(e);
                }
            }
        }
    }
}
