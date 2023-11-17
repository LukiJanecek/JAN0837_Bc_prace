using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bc_prace.Controls.MyGraphControl.Entities
{
    public class DSpline : DCurve, IDrawable
    {

        public override void Draw(Graphics e)
        {
            if (Visible)
            {
                List<PointF> points = new List<PointF>();
                for (int i = 0; i < Points.Count; i++)
                {
                    Points[i].Draw(e);
                    float x1 = Points[i].Position.X;
                    float y1 = Points[i].Position.Y;
                    points.Add(new PointF(x1, -y1));
                }
                if (Closed)
                {
                    float x1 = Points[0].Position.X;
                    float y1 = Points[0].Position.Y;
                    points.Add(new PointF(x1, -y1));
                }


                Pen pen = new Pen(this.Color);
                pen.Width = this.PenWidth;
                if (this.Selected)
                    pen.Color = this.SelectedColor;
                e.DrawCurve(pen, points.ToArray());
                pen.Dispose();
            }
        }
    }
}
