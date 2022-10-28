using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jueguito
{
    public class Proyectil : Entidad
    {
        public Proyectil(Point posicionInicial, int velocidad) : base(posicionInicial, velocidad)
        {
            base.sprites.Add("east", "proyectil_east");
            base.renderEntidad.Size = new Size(35, 25);
        }
    }
}
