using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jueguito
{
    public class Jugador : Entidad
    {
        private int puntosDeVida;

        public Jugador(Point posicionInicial, int velocidad, int puntosDeVida) : base(posicionInicial, velocidad)
        {
            base.sprites.Add("north", "crucero_north");
            base.sprites.Add("south", "crucero_south");
            base.sprites.Add("east", "crucero_east");
            base.sprites.Add("west", "crucero_west");
            this.puntosDeVida = puntosDeVida;
            base.direccionFacing = 'n';
            base.renderEntidad.Size = new Size(80, 80);
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


        public void RecibirDanio(int valor)
        {
            this.puntosDeVida -= valor;
        }
    }
}
