using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bc_prace.Controls.MyGraphControl.Entities
{
    public class GeometricExtension
    {
        public PointF Minimum { get; set; }
        public PointF Maximum { get; set; }

        public GeometricExtension()
        {
            Minimum = new PointF();
            Maximum = new PointF();
        }

        public GeometricExtension(PointF minimum, PointF maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public GeometricExtension(float minX, float minY, float maxX, float maxY)
        {
            Minimum = new PointF(minX, minY);
            Maximum = new PointF(maxX, maxY);
        }

        public GeometricExtension(double minX, double minY, double maxX, double maxY)
        {
            Minimum = new PointF((float)minX, (float)minY);
            Maximum = new PointF((float)maxX, (float)maxY);
        }

        public float GetWidth()
        {
            return Maximum.X - Minimum.X;
        }

        public float GetHeight()
        {
            return Maximum.Y - Minimum.Y;
        }
    }
}
