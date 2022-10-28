using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jueguito
{
    public class Gasolina : Entidad
    {
        int cantidad;

        public int Cantidad
        {
            get
            {
                return this.cantidad;
            }
        }
        public Gasolina(Point posicionInicial, int velocidad, int cantidad) : base(posicionInicial, velocidad)
        {
            this.cantidad = cantidad;
            base.sprites.Add("north", "gasolina_north");
            base.renderEntidad.Size = new Size(40, 40);
        }
    }
}
