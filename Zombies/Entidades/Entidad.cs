using Zombies.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Zombies
{
    public class Entidad
    {
        protected PictureBox renderEntidad;
        protected Point posicion;
        protected char direccionFacing;
        protected int velocidad;
        protected Dictionary<string, string> sprites;

        public Entidad(Point posicionInicial, int velocidad, Size tamanio)
        {
            this.posicion = posicionInicial;
            this.velocidad = velocidad;
            this.sprites = new Dictionary<string, string>();
            this.renderEntidad = new PictureBox();
            this.renderEntidad.BackgroundImageLayout = ImageLayout.Stretch;
            this.renderEntidad.Size = tamanio;
        }
        public PictureBox RenderPictureBox
        {
            get
            {
                return this.renderEntidad;
            }
        }
        public char DireccionFacing
        {
            get
            {
                return this.direccionFacing;
            }
            set
            {
                if(value == this.direccionFacing)
                {
                    return;
                }

                this.direccionFacing = value;
                switch (direccionFacing)
                {
                    case 'n':
                        this.CargarSprite("north"); 
                        break;
                    case 's':
                        this.CargarSprite("south");
                        break;
                    case 'e':
                        this.CargarSprite("east");
                        break;
                    case 'w':
                        this.CargarSprite("west");
                        break;
                    default:
                        this.CargarSprite("north");
                        break;
                }
            }
        }
        public int X
        {
            get
            {
                return this.posicion.X;
            }
            set
            {
                this.posicion.X = value;
            }
        }

        public int Y
        {
            get
            {
                return this.posicion.Y;
            }
            set
            {
                this.posicion.Y = value;
            }
        }

        public int Velocidad
        {
            get
            {
                return this.velocidad;
            }
        }


        private void CargarSprite(string direccion)
        {
            this.renderEntidad.Image = (Image)Resources.ResourceManager.GetObject(sprites[direccion])!;
            if(this.renderEntidad.Image == null)
            {
                this.renderEntidad.Image = (Image)Resources.ResourceManager.GetObject("sin_sprite")!;
            }
        }
        public virtual void MoverseEnDireccionFacing()
        {
            if (velocidad < 1)
            {
                return;
            }
            switch (direccionFacing)
            {
                case 'n':
                    this.Y -= velocidad;
                    break;
                case 's':
                    this.Y += velocidad;
                    break;
                case 'e':
                    this.X += velocidad;
                    break;
                case 'w':
                    this.X -= velocidad;
                    break;
                default:
                    break;
            }
        }

        public void Render()
        {
            this.renderEntidad.Location = new Point(this.X, this.Y);
        }

        public bool ChequearColision(Entidad entidad)
        {
            return this.renderEntidad.Bounds.IntersectsWith(entidad.renderEntidad.Bounds);
        }
    }
}
