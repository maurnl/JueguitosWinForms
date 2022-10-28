using Jueguito.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jueguito
{
    public partial class Juego : Form
    {
        Jugador jugador;
        List<Entidad> entidades;
        int probabilidadProyectil;
        int contadorTicks;
        int contadorPuntaje;
        public Juego()
        {
            InitializeComponent();
            this.Text = "Crucero que agarra parciales aprobados y esquiva tiburones";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.jugador = new Jugador(new Point(this.Width / 2,this.Height / 2), 10, 100);
            this.entidades = new List<Entidad>();
            this.entidades.Add(jugador);
            this.Controls.Add(jugador.RenderPictureBox);
            this.contadorPuntaje = 0;
            this.probabilidadProyectil = 5;
            this.contadorTicks = 0;
            this.barraVida.Value = jugador.PuntosDeVida;
            this.BackColor = Color.LightBlue;
        }

        private void Juego_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                this.jugador.DireccionFacing = 'n';
            }
            else if (e.KeyCode == Keys.A)
            {
                this.jugador.DireccionFacing = 'w';
            }
            else if (e.KeyCode == Keys.S)
            {
                this.jugador.DireccionFacing = 's';
            }
            else if (e.KeyCode == Keys.D)
            {
                this.jugador.DireccionFacing = 'e';
            }
        }

        private void UpdateGameData()
        {
            this.contadorTicks++;
            if (!this.jugador.TienePuntosDeVida || this.contadorPuntaje >= 20)
            {
                TerminarJuego(this.contadorPuntaje >= 20);
            }
            ChequearColisiones();
            GenerarProyectil();
            if(this.contadorTicks==45)
            {
                GenerarGasolina();
                this.contadorTicks = 0;
                this.jugador.RecibirDanio(10);
            }
            foreach (Entidad entidad in entidades)
            {
                entidad.MoverseEnDireccionFacing();
            }
            LimpiarProyectiles();
        }

        private void Render()
        {
            this.lblPuntos.Text = this.contadorPuntaje.ToString();
            foreach (Entidad entidad in this.entidades)
            {
                entidad.Render();
            }
        }
        private void ChequearColisiones()
        {
            int cantidad = this.entidades.Count;
            for (int i = cantidad - 1; i >= 0; i--)
            {
                if (this.entidades[i].ChequearColision(jugador))
                {
                    if(this.entidades[i] is Gasolina gasolina)
                    {
                        this.contadorPuntaje++;
                        this.jugador.RecibirDanio(-gasolina.Cantidad);
                        this.Controls.Remove(gasolina.RenderPictureBox);
                        this.entidades.RemoveAt(i);
                        probabilidadProyectil += 1;
                    }
                    else if (this.entidades[i] is Proyectil proyectil)
                    {
                        this.jugador.RecibirDanio(5);
                        this.Controls.Remove(proyectil.RenderPictureBox);
                        this.entidades.RemoveAt(i);
                    }
                    if(this.jugador.TienePuntosDeVida)
                    {
                        this.barraVida.Value = this.jugador.PuntosDeVida;
                    }
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateGameData();
            Render();
        }

        private void GenerarProyectil()
        {
            Random random = new Random();
            if (random.Next(0, 100) < this.probabilidadProyectil)
            {
                Proyectil proyectil = new Proyectil(new Point(0, random.Next(0, this.Height)), 10);
                proyectil.DireccionFacing = 'e';
                this.Controls.Add(proyectil.RenderPictureBox);
                this.entidades.Add(proyectil);
            }
        }

        private void GenerarGasolina()
        {
            Random random = new Random();
            Gasolina gasolina = new Gasolina(new Point(random.Next(0, this.Width), random.Next(0, this.Height)), 0, 10);
            this.Controls.Add(gasolina.RenderPictureBox);
            this.entidades.Add(gasolina);
        }

        private void LimpiarProyectiles()
        {
            int cantidad = entidades.Count;
            for (int i = cantidad - 1; i >= 0; i--)
            {
                if (this.entidades[i] is Proyectil && this.entidades[i].RenderPictureBox.Right > this.Width + 100)
                {
                    this.Controls.Remove(entidades[i].RenderPictureBox);
                    this.entidades.RemoveAt(i);
                }
            }
        }

        private void TerminarJuego(bool ganaste)
        {
            this.timer1.Stop();
            this.label1.Text = ganaste ? "Ganaste!!" : "Perdiste!!";
        }
    }
}
