using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bc_prace.Controls.MyGraphControl.Entities
{
    public class DHatch : DEntity, IDrawable
    {
        public DEntity[] Path { get; set; }

        public override GeometricExtension GeometricExtents => null;

        public override void Draw(Graphics g)
        {
            GraphicsPath path = new GraphicsPath();
            //path
            //g.FillPath()
        }
    }
}
