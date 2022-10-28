using Jueguito.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Jueguito
{
    public class Entidad
    {
        protected PictureBox renderEntidad;
        protected Point posicion;
        protected char direccionFacing;
        protected int velocidad;
        protected Dictionary<string, string> sprites;

        public Entidad(Point posicionInicial, int velocidad)
        {
            this.posicion = posicionInicial;
            this.velocidad = velocidad;
            this.sprites = new Dictionary<string, string>();
            this.renderEntidad = new PictureBox();
            this.renderEntidad.BackgroundImageLayout = ImageLayout.Stretch;
            this.renderEntidad.Size = new Size(80, 80);
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
                this.direccionFacing = value;
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
            try
            {
                this.renderEntidad.Image = (Image)Resources.ResourceManager.GetObject(sprites[direccion])!;
            }
            catch (Exception)
            {
                this.renderEntidad.Image = (Image)Resources.ResourceManager.GetObject("sin_sprite")!;
            }
        }
        public void MoverseEnDireccionFacing()
        {
            switch (direccionFacing)
            {
                case 'n':
                    this.CargarSprite("north");
                    this.Y -= velocidad;
                    break;
                case 's':
                    this.CargarSprite("south");
                    this.Y += velocidad;
                    break;
                case 'e':
                    this.CargarSprite("east");
                    this.X += velocidad;
                    break;
                case 'w':
                    this.CargarSprite("west");
                    this.X -= velocidad;
                    break;
                default:
                    this.CargarSprite("north");
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
