using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombies.Entidades
{
    public class Enemigo : Personaje
    {
        public Enemigo(Point posicionInicial, int velocidad, Size tamanio, int puntosDeVida) : base(posicionInicial, velocidad, tamanio, puntosDeVida)
        {
            base.sprites.Add("north", "jugador_north");
            base.sprites.Add("east", "jugador_east");
            base.sprites.Add("south", "jugador_south");
            base.sprites.Add("west", "jugador_west");
            this.DireccionFacing = 'n';
        }

        public void GirarDireccionPersonaje(Point posicionPersonaje)
        {
            Random random = new Random();
            if (random.Next(0, 100) > 50)
            {
                return;
            }

            int deltaX = this.posicion.X - posicionPersonaje.X;
            int deltaY = this.posicion.Y - posicionPersonaje.Y;

            if (deltaX == 0 || deltaY == 0)
            {
                if(deltaX == 0)
                {
                    if(deltaY >= 0)
                    {
                        DireccionFacing = 'n';
                    } else
                    {
                        DireccionFacing = 's';
                    }
                } else if(deltaY == 0)
                {
                    if (deltaX >= 0)
                    {
                        DireccionFacing = 'w';
                    }
                    else
                    {
                        DireccionFacing = 'e';
                    }
                }
            }
            else if (deltaX <= deltaY)
            {
                if (deltaX >= 0)
                {
                    DireccionFacing = 'w';
                } else
                {
                    DireccionFacing = 'e';
                }
            }
            else
            {
                if(deltaY >= 0)
                {
                    DireccionFacing = 'n';

                } else
                {
                    DireccionFacing = 's';
                }
            }
        }
    }
}
