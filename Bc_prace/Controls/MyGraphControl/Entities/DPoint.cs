using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bc_prace.Controls.MyGraphControl.Entities
{
    public enum PointStyle
    {
        None,
        Circle,
        Square,
        XLine,
        YLine,
        FillCircle

    }
    public class DPoint : DEntity, IDrawable
    {
        public DPoint()
        {
        }

        public DPoint(PointF position)
        {
            Position = position;
        }

        public DPoint(double x, double y)
        {
            Position = new PointF((float)x, (float)y);
        }

        public PointF Position { get; set; }

        public PointStyle PointStyle { get; set; }

        public float PointSize { get; set; }

        public bool EnableScale { get; set; }

        #region IDrawable Members


        public override GeometricExtension GeometricExtents
        {
            get
            {
                return new GeometricExtension(Position, Position);
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

                float pointSizeX = PointSize;
                float pointSizeY = PointSize;
                if (EnableScale == false)
                {
                    pointSizeX = PointSize / e.Transform.Elements[0];
                    pointSizeY = PointSize / e.Transform.Elements[3];
                }
                switch (PointStyle)
                {
                    case PointStyle.None:
                        break;
                    case PointStyle.Circle:
                        e.DrawEllipse(pen, Position.X - pointSizeX / 2, -Position.Y - pointSizeY / 2, pointSizeX, pointSizeY);
                        break;
                    case PointStyle.YLine:
                        e.DrawLine(pen, Position.X, -Position.Y + pointSizeY / 2, Position.X, -Position.Y - pointSizeY / 2);
                        break;
                    case PointStyle.XLine:
                        e.DrawLine(pen, Position.X - pointSizeX / 2, -Position.Y, Position.X + pointSizeX / 2, -Position.Y);
                        break;
                    case PointStyle.Square:
                        PointF p1 = new PointF(Position.X - pointSizeX / 2, -Position.Y + pointSizeY / 2);
                        PointF p2 = new PointF(Position.X - pointSizeX / 2, -Position.Y - pointSizeY / 2);
                        PointF p3 = new PointF(Position.X + pointSizeX / 2, -Position.Y - pointSizeY / 2);
                        PointF p4 = new PointF(Position.X + pointSizeX / 2, -Position.Y + pointSizeY / 2);
                        e.DrawPolygon(pen, new PointF[] { p1, p2, p3, p4 });
                        break;
                    case PointStyle.FillCircle:
                        e.FillEllipse(new SolidBrush(Color), Position.X - pointSizeX / 2, -Position.Y - pointSizeY / 2, pointSizeX, pointSizeY);
                        break;
                }

                pen.Dispose();
            }
        }



        #endregion
    }
}
