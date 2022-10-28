using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombies.Entidades
{
    public class Arco : Arma    
    {
        public Arco(Point posicionInicial, int velocidad, Size tamanio, int puntosDeDanio) : base(posicionInicial, velocidad, tamanio, puntosDeDanio)
        {
        }

        public override Entidad Atacar(Point posicionJugador, char direccion)
        {
            return new Flecha(posicionJugador, 30, new Size(10, 10), direccion);
        }
    }
}
