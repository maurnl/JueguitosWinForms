using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombies.Entidades
{
    public class Jugador : Personaje, IDefensor, IAtacante
    {
        bool estaDefendiendo;
        private IArma armaActiva;

        public Jugador(Point posicionInicial, int velocidad, Size tamanio, int puntosDeVida) : base(posicionInicial, velocidad, tamanio, puntosDeVida)
        {
            base.sprites.Add("north", "jugador_north");
            base.sprites.Add("east", "jugador_east");
            base.sprites.Add("south", "jugador_south");
            base.sprites.Add("west", "jugador_west");
            this.DireccionFacing = 'n';
        }

        public IArma ArmaActiva
        {
            get
            {
                return this.armaActiva;
            }
        }

        public bool EstaDefendiendo
        {
            get
            {
                return this.estaDefendiendo;
            }
        }

        public Entidad? Atacar()
        {
            return this.estaDefendiendo ? null : this.armaActiva.Atacar(posicion, direccionFacing);
        }

        public void ToggleEscudo()
        {
            this.estaDefendiendo = !this.estaDefendiendo;
        }

        public void CambiarArma(IArma arma)
        {
            this.armaActiva = arma;
        }

        public override void RecibirDanio(int valor)
        {
            if(estaDefendiendo)
            {
                return;
            }
            base.RecibirDanio(valor);
        }
    }
}
