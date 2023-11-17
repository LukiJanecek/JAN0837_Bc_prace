using Bc_prace.Controls.MyGraphControl.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bc_prace.Controls.MyGraphControl.Entities
{
    public interface IDrawable
    {
        /// <summary>
        /// Barva objektu
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// Barva objektu, ktery je vybran
        /// </summary>
        Color SelectedColor { get; set; }

        /// <summary>
        /// Je objekt vybratelny
        /// </summary>
        bool CanSelect { get; set; }

        /// <summary>
        /// Viditelnost objektu
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// Je item vybrany
        /// </summary>
        bool Selected { get; set; }

        string Layer { get; }

        /// <summary>
        /// Sirka pera
        /// </summary>
        float PenWidth { get; set; }

        GeometricExtension GeometricExtents { get; }

        /// <summary>
        /// Metoda, ktera vykresli objekt
        /// </summary>
        /// <param name="e"></param>
        void Draw(Graphics e);
    }
}
