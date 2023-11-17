using Bc_prace.Controls.MyGraphControl.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bc_prace.Controls.MyGraphControl.Entities
{
    public class DSolidPath : DGroup, IDrawable
    {

        public override void Draw(Graphics e)
        {
            if (Visible)
            {
                SolidBrush solidBrush = new SolidBrush(Color);
                GraphicsPath path = new GraphicsPath();
                foreach (DEntity entity in Items)
                {
                    if (entity is DCurve)
                    {
                        DCurve dCurve = (DCurve)entity;
                        for (int i = 0; i < dCurve.Points.Count; i++)
                        {
                            if (i > 0)
                            {
                                float x1 = dCurve.Points[i - 1].Position.X;
                                float y1 = dCurve.Points[i - 1].Position.Y;
                                float x2 = dCurve.Points[i].Position.X;
                                float y2 = dCurve.Points[i].Position.Y;
                                path.AddLine(x1, -y1, x2, -y2);
                            }
                        }
                    }
                    if (entity is DArc)
                    {
                        DArc dArc = (DArc)entity;
                        path.AddArc(dArc.Center.X - dArc.Width / 2,
                            -dArc.Center.Y - dArc.Height / 2,
                            dArc.Width, dArc.Height,
                            -dArc.StartAngle,
                            -dArc.SweepAngle);

                    }
                }
                e.FillPath(solidBrush, path);
                solidBrush.Dispose();
            }
        }
    }
}
