using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombies.Entidades
{
    public interface IArma
    {
        Entidad Atacar(Point posicionJugador, char direccion);
    }
}
