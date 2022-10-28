using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombies.Entidades
{
    public abstract class Arma : Entidad, IArma
    {
        private int puntosDeDanio;

        protected Arma(Point posicionInicial, int velocidad, Size tamanio) : base(posicionInicial, velocidad, tamanio)
        {
        }

        protected Arma(Point posicionInicial, int velocidad, Size tamanio, int puntosDeDanio) : this(posicionInicial, velocidad, tamanio)
        {
            this.puntosDeDanio = puntosDeDanio;
        }

        public abstract Entidad Atacar(Point posicionJugador, char direccion);
    }
}
