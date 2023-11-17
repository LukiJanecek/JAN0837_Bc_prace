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
    public abstract class DEntity : IDrawable
    {
        /// <summary>
        /// Barva objektu
        /// </summary>
        public virtual Color Color { get; set; }

        /// <summary>
        /// Barva objektu, ktery je vybran
        /// </summary>
        public virtual Color SelectedColor { get; set; }

        /// <summary>
        /// Je objekt vybratelny
        /// </summary>
        public virtual bool CanSelect { get; set; }

        /// <summary>
        /// Viditelnost objektu
        /// </summary>
        public virtual bool Visible { get; set; }

        /// <summary>
        /// Je item vybrany
        /// </summary>
        public virtual bool Selected { get; set; }

        /// <summary>
        /// Sirka pera
        /// </summary>
        public virtual float PenWidth { get; set; }

        public virtual string Layer { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public abstract GeometricExtension GeometricExtents { get; }

        public abstract void Draw(Graphics e);
    }
}
