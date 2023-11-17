using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bc_prace.Controls.MyGraphControl.Entities
{
    public class DCurve : DEntity, IDrawable
    {
        public DCurve()
        {
            Points = new List<DPoint>();
            Visible = true;
        }

        public List<DPoint> Points { get; set; }

        private PointStyle _pointStyle;
        public PointStyle PointStyle
        {
            get { return _pointStyle; }
            set
            {
                _pointStyle = value;
                foreach (DPoint point in Points)
                {
                    point.PointStyle = value;
                }
            }
        }

        public void AddPoint(DPoint point)
        {
            point.PointStyle = this.PointStyle;
            point.PointSize = this.PointSize;
            point.CanSelect = this.CanSelect;
            point.Color = this.Color;
            point.EnableScale = this.EnablePointScale;
            point.SelectedColor = this.SelectedColor;
            point.Visible = this.ShowPoints;
            this.Points.Add(point);
        }

        private bool _showPoints;

        public bool ShowPoints
        {
            get { return _showPoints; }
            set
            {
                _showPoints = value;
                foreach (DPoint point in Points)
                {
                    point.Visible = value;
                }
            }
        }

        private float _pointSize;
        public float PointSize
        {
            get { return _pointSize; }
            set
            {
                _pointSize = value;
                foreach (DPoint point in Points)
                {
                    point.PointSize = value;
                }
            }
        }

        private bool _enablePointScale;
        public bool EnablePointScale
        {
            get { return _enablePointScale; }
            set
            {
                _enablePointScale = value;
                foreach (DPoint point in Points)
                {
                    point.EnableScale = value;
                }
            }
        }

        public bool Closed { get; set; }

        #region IDrawable Members

        private Color _color;
        public override Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                foreach (DPoint point in Points)
                {
                    point.Color = value;
                }
            }
        }

        private Color _selectedColor;
        public override Color SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                foreach (DPoint point in Points)
                {
                    point.SelectedColor = value;
                }
            }
        }




        private float _penWidth;
        public override float PenWidth
        {
            get { return _penWidth; }
            set
            {
                _penWidth = value;
                foreach (DPoint point in Points)
                {
                    point.PenWidth = value;
                }
            }
        }

        public override GeometricExtension GeometricExtents
        {
            get
            {
                float xmin = float.MaxValue, xmax = float.MinValue;
                float ymin = float.MaxValue, ymax = float.MinValue;
                foreach (DPoint point in Points)
                {
                    if (point.Position.X < xmin)
                        xmin = point.Position.X;
                    if (point.Position.X > xmax)
                        xmax = point.Position.X;
                    if (point.Position.Y < ymin)
                        ymin = point.Position.Y;
                    if (point.Position.Y > ymax)
                        ymax = point.Position.Y;
                }
                return new GeometricExtension(xmin, ymin, xmax, ymax);
            }
        }

        public override void Draw(Graphics e)
        {
            if (Visible)
            {
                Pen pen = new Pen(this.Color);
                pen.Width = this.PenWidth;
                if (this.Selected)
                    pen.Color = this.SelectedColor;
                if (Points.Count < 2)
                    return;
                //for (int i = 0; i < Points.Count; i++)
                //{
                //    Points[i].Draw(e);
                //    if (i > 0)
                //    {
                //        float x1 = Points[i - 1].Position.X;
                //        float y1 = Points[i - 1].Position.Y;
                //        float x2 = Points[i].Position.X;
                //        float y2 = Points[i].Position.Y;
                //        e.DrawLine(pen, x1, -y1, x2, -y2);
                //    }
                //}
                //if (Closed)
                //{
                //    float x1 = Points[0].Position.X;
                //    float y1 = Points[0].Position.Y;
                //    float x2 = Points[Points.Count - 1].Position.X;
                //    float y2 = Points[Points.Count - 1].Position.Y;
                //    e.DrawLine(pen, x1, -y1, x2, -y2);
                //}
                int length = Points.Count;
                PointF[] points;
                if (Closed || IsClosed())
                {
                    points = new PointF[length + 2];
                }
                else
                {
                    points = new PointF[length];
                }

                for (int i = 0; i < length; i++)
                {
                    points[i] = new PointF(Points[i].Position.X, -Points[i].Position.Y);
                    Points[i].Draw(e);
                }

                if (Closed || IsClosed())
                {
                    points[length] = new PointF(Points[0].Position.X, -Points[0].Position.Y);
                    points[length + 1] = new PointF(Points[1].Position.X, -Points[1].Position.Y);
                }
                e.DrawLines(pen, points);
                pen.Dispose();
            }
        }

        private bool IsClosed()
        {
            DPoint first = Points[0];
            DPoint last = Points[Points.Count - 1];
            bool eq = first.Position.X == last.Position.X;
            eq = eq && first.Position.Y == last.Position.Y;
            return eq;
        }


        /// <summary>
        /// Ořeže kživku
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="xEnd"></param>
        /// <returns></returns>
        public DCurve TrimCurve(float xStart, float xEnd)
        {
            DCurve ret = new DCurve();
            ret.CanSelect = this.CanSelect;
            ret.Closed = this.Closed;
            ret.Color = this.Color;
            ret.EnablePointScale = this.EnablePointScale;
            ret.PenWidth = this.PenWidth;
            ret.PointSize = this.PointSize;
            ret.PointStyle = this.PointStyle;
            ret.Selected = this.Selected;
            ret.SelectedColor = this.SelectedColor;
            ret.ShowPoints = this.ShowPoints;
            ret.Visible = this.Visible;

            foreach (DPoint p in this.Points)
            {
                if (p.Position.X >= xStart && p.Position.X <= xEnd)
                    ret.Points.Add(p);
            }

            return ret;
        }

        #endregion
    }
}
