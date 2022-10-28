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
            base.sprites.Add("north", "barra");
            base.direccionFacing = 'n';
            this.puntosDeVida = puntosDeVida;
            base.RenderPictureBox.Size = new Size(350, 42);
            base.RenderPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
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
