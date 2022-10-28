using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zombies.Entidades;

namespace Zombies
{
    public abstract class Personaje : Entidad
    {
        private int puntosDeVida;

        public Personaje(Point posicionInicial, int velocidad, Size tamanio, int puntosDeVida) : base(posicionInicial, velocidad, tamanio)
        {
            this.puntosDeVida = puntosDeVida;
        }


        public bool TienePuntosDeVida
        {
            get
            {
                return this.puntosDeVida > 0;
            }
        }

        public int PuntosDeVida
        {
            get
            {
                return this.puntosDeVida;
            }
        }

        public virtual void RecibirDanio(int valor)
        {
            this.puntosDeVida -= valor;
        }
    }
}
