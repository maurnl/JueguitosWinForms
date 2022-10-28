using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zombies.Entidades;

namespace Zombies
{
    public partial class Juego : Form
    {
        int contadorPuntos;
        int contadorTicks;
        bool teclaMovimientoPresionada;
        Jugador jugador;
        List<Entidad> entidades;

        public Juego()
        {
            InitializeComponent();
            jugador = new Jugador(new Point(350, 350), 10, new Size(50, 50), 100);
            jugador.CambiarArma(new Arco(new Point(350, 350), 10, new Size(10, 10), 5));
            entidades = new List<Entidad>();
            RegistrarEntidad(jugador);
            contadorPuntos = 0;
            contadorTicks = 0;
        }

        private void Juego_KeyDown(object sender, KeyEventArgs e)
        {
            teclaMovimientoPresionada = true;
            if(e.KeyCode == Keys.W)
            {
                jugador.DireccionFacing = 'n';
            }else if(e.KeyCode == Keys.A)
            {
                jugador.DireccionFacing = 'w';
            }
            else if(e.KeyCode == Keys.S)
            {
                jugador.DireccionFacing = 's';
            }
            else if(e.KeyCode == Keys.D)
            {
                jugador.DireccionFacing = 'e';
            }
            else if(e.KeyCode == Keys.Space)
            {
                RegistrarEntidad(jugador.Atacar());
            } 
            else if(e.KeyCode == Keys.F)
            {
                jugador.ToggleEscudo();
            }

        }
        private void Juego_KeyUp(object sender, KeyEventArgs e)
        {   
            teclaMovimientoPresionada = false;
        }

        private void UpdateGameData()
        {
            contadorTicks++;
            contadorPuntos++;
            if (teclaMovimientoPresionada)
            {
                jugador.MoverseEnDireccionFacing();
            }
            if(contadorTicks == 100)
            {
                GenerarEnemigo();
                contadorTicks = 0;
            }
            if (ChequearHayEnemigosVivos())
            {
                ChequearColisionProyectiles();
                ChequearColisionJugador();
            }
            LimpiarEntidades();
            if(!jugador.TienePuntosDeVida)
            {
                TerminarJuego(false);
            }
        }

        private void TerminarJuego(bool victoria)
        {
            timer1.Enabled = false;
        }

        private void ChequearColisionJugador()
        {
            foreach (Entidad entidad in entidades)
            {
                if (entidad is Enemigo enemigo && jugador.ChequearColision(enemigo))
                {
                    jugador.RecibirDanio(2);
                }
            }
        }

        private void Render()
        {
            progressBar1.Value = jugador.PuntosDeVida;
            label1.Text = contadorPuntos.ToString();
            lblEntidades.Text = entidades.Count.ToString();
            foreach (Entidad entidad in entidades)
            {
                if (entidad is IProyectil)
                {
                    entidad.MoverseEnDireccionFacing();
                }
                if(entidad is Enemigo enemigo)
                {
                    if(contadorTicks < 80)
                    {
                        GirarEnemigosDireccionPersonaje();
                    }
                    enemigo.MoverseEnDireccionFacing();
                }
                entidad.Render();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateGameData();
            Render();
        }

        private void GirarEnemigosDireccionPersonaje()
        {
            foreach (Entidad entidad in entidades)
            {
                if(entidad is Enemigo enemigo)
                {
                    enemigo.GirarDireccionPersonaje(jugador.RenderPictureBox.Location);
                }
            }
        }

        private void RegistrarEntidad(Entidad entidad)
        {
            if(entidad == null)
            {
                return;
            }
            entidades.Add(entidad);
            Controls.Add(entidad.RenderPictureBox);
        }

        private void EliminarEntidad(Entidad entidad)
        {
            if(entidad == null)
            {
                return;
            }
            entidades.Remove(entidad);
            Controls.Remove(entidad.RenderPictureBox);
        }

        private bool ChequearHayEnemigosVivos()
        {
            foreach (Entidad entidad in entidades)
            {
                if(entidad is Enemigo)
                {
                    return true;
                    break;
                }
            }
            return false;
        }

        private void ChequearColisionProyectiles()
        {
            foreach (Entidad entidad in entidades)
            {
                if(entidad is Enemigo entidadEnemigo)
                {
                    foreach (Entidad entidadDeNuevo in entidades)
                    {
                        if (entidadDeNuevo is Flecha flecha && entidadEnemigo.ChequearColision(flecha))
                        {
                            entidadEnemigo.RecibirDanio(100);
                        }
                    }
                }
            }
        }

        private void GenerarEnemigo()
        {
            Random random = new Random();
            Enemigo enemigo = new Enemigo(new Point(random.Next(0, Width), random.Next(0, Height)), 2, new Size(50, 50), 100);
            RegistrarEntidad(enemigo);
        }

        private void LimpiarEntidades()
        {
            Entidad entidad;
            int cantidadEntidades = entidades.Count;
            for (int i = cantidadEntidades - 1; i >= 0; i--)
            {
                entidad = entidades[i];
                if (entidad.RenderPictureBox.Left > this.Width
                    || entidad.RenderPictureBox.Left < -45
                    || entidad.RenderPictureBox.Top > this.Height
                    || entidad.RenderPictureBox.Top < -45
                    || (entidad is IProyectil && entidad.Velocidad == 0)
                    || (entidad is Enemigo && !((Enemigo)entidad).TienePuntosDeVida))
                {
                    EliminarEntidad(entidad);
                }
            }
        }
    }
}
