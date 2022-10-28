using Funkin.Properties;
using Jueguito;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Funkin
{
    public class Nota : Entidad
    {
        bool fueApretada;
        private SoundPlayer soundPlayer;

        public Nota(Point posicionInicial, int velocidad, string sprite) : base(posicionInicial, velocidad)
        {
            base.sprites.Add("north", sprite);
            base.direccionFacing = 'n';
            this.fueApretada = false;
            this.soundPlayer = new SoundPlayer();
            switch (sprite)
            {
                case "nota_w":
                    this.soundPlayer.Stream = Resources.ResourceManager.GetStream("do");
                    break;
                case "nota_a":
                    this.soundPlayer.Stream = Resources.ResourceManager.GetStream("re");

                    break;
                case "nota_s":
                    this.soundPlayer.Stream = Resources.ResourceManager.GetStream("mi");

                    break;
                case "nota_d":
                    this.soundPlayer.Stream = Resources.ResourceManager.GetStream("fa");
                    break;
            }
        }

        public bool FueApretada
        {
            get
            {
                return this.fueApretada;
            }
            set
            {
                this.fueApretada = value;
            }
        }
        public void ReproducirSonido()
        {
            this.soundPlayer.Play();
        }

    }
}
