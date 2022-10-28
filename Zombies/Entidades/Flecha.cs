using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombies.Entidades
{
    public class Flecha : Entidad, IProyectil
    {
        decimal aceleracion;

        public Flecha(Point posicionInicial, int velocidad, Size tamanio, char direccion) : base(posicionInicial, velocidad, tamanio)
        {
            base.sprites.Add("west", "asdf");
            base.sprites.Add("east", "asdg");
            base.sprites.Add("north", "asdh");
            base.sprites.Add("south", "asdj");
            this.aceleracion = 0.3m;
            base.DireccionFacing = direccion;
        }

        public override void MoverseEnDireccionFacing()
        {
            base.velocidad = (int) (velocidad - (aceleracion * aceleracion));
            base.MoverseEnDireccionFacing();
        }
    }
}
