using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bc_prace.Controls.MyGraphControl.Entities
{
    public enum VerticalAlign
    {
        Top, Center, Bottom
    }

    public enum HorizontalAlign
    {
        Left, Center, Right
    }

    public class DText : DEntity, IDrawable
    {
        public DText()
        {
            Font = new Font("Arial", 8);
            Color = Color.Black;
            Visible = true;
        }

        public VerticalAlign VerticalAlign { get; set; }
        public HorizontalAlign HorizontalAlign { get; set; }
        public float Angle { get; set; }
        public string Text { get; set; }
        public Font Font { get; set; }
        public PointF Location { get; set; }
        public bool EnableScaling { get; set; }

        #region IDrawable Members        

        public override GeometricExtension GeometricExtents
        {
            get
            {
                return new GeometricExtension(Location, Location);
            }
        }

        public override void Draw(Graphics e)
        {
            if (Visible)
            {
                Brush b = new SolidBrush(Color);
                if (Selected)
                    b = new SolidBrush(SelectedColor);
                Font font = Font;

                float x = Location.X;
                float y = Location.Y;

                Matrix transform = e.Transform;

                if (EnableScaling == false)
                {
                    e.ScaleTransform(1 / transform.Elements[0], 1 / transform.Elements[3]);
                    x = x * transform.Elements[0];
                    y = y * transform.Elements[3];
                }

                float bulhar2 = -e.MeasureString(Text, font).Height * 0.25F;
                x += bulhar2;

                switch (HorizontalAlign)
                {
                    case HorizontalAlign.Center:
                        x = x - e.MeasureString(Text, font).Width / 2;
                        break;
                    case HorizontalAlign.Right:
                        x = x - e.MeasureString(Text, font).Width;
                        break;
                }

                float bulhar = 0.75f;
                switch (VerticalAlign)
                {
                    case VerticalAlign.Center:
                        y = y + e.MeasureString(Text, font).Height * 0.5F * bulhar;
                        break;
                    case VerticalAlign.Bottom:
                        y = y + e.MeasureString(Text, font).Height * bulhar;
                        break;
                }
                try
                {


                    //if (Angle != 0)
                    if (Angle == 90 || Angle == -90 || Angle == 270)
                    {
                        Matrix t2 = e.Transform;
                        // Save the graphics state.
                        GraphicsState state = e.Save();
                        e.ResetTransform();

                        // Rotate.
                        e.RotateTransform(Angle);
                        var t3 = e.Transform;
                        e.ScaleTransform(t2.Elements[0], t2.Elements[3], MatrixOrder.Append);
                        e.TranslateTransform(t2.OffsetX, t2.OffsetY, MatrixOrder.Append);
                        e.DrawString(Text, font, b, new PointF(t3.Elements[0] * (x) + t3.Elements[1] * (-y), t3.Elements[2] * (x) + t3.Elements[3] * (-y)));

                        // Restore the graphics state.
                        e.Restore(state);
                    }
                    else
                    {
                        e.DrawString(Text, font, b, new PointF(x, -y));
                    }
                }
                catch (Exception)
                {

                }
                //e.DrawLine(new Pen(Color.Red), Location.X, -Location.Y, Location.X, -y);
                e.Transform = transform;
                b.Dispose();
            }
        }

        #endregion
    }
}
