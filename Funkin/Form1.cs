using Funkin.Properties;
using Jueguito;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Funkin
{
    public partial class Form1 : Form
    {
        Jugador jugador;
        List<Entidad> entidades;
        int contadorPuntos;
        string cancion;
        bool esNotaNueva;
        bool hayTeclaApretada;
        int indiceJugador;
        int indiceCancion;
        int contadorTicks;
        public Form1()
        {
            InitializeComponent();
            this.jugador = new Jugador(new Point(120,200), 0, 100);
            this.entidades = new List<Entidad>();
            this.entidades.Add(jugador);
            this.Controls.Add(jugador.RenderPictureBox);
            this.labelInput.Text = "";
            this.indiceJugador = 0;
            this.indiceCancion = 0;
            this.contadorPuntos = 0;
            this.cancion = "DAWDASDWADSDWADWADASDASDWADAWASD";
            this.labelCancion.Text = cancion;
            this.contadorTicks = 0;
            this.esNotaNueva = false;
            this.hayTeclaApretada = false;
        }
        private void Juego_KeyDown(object sender, KeyEventArgs e)
        {
            if(ChequearColision())
            {
                hayTeclaApretada = true;
                ChequearTeclaCorrecta(e.KeyData);
            }
            hayTeclaApretada = false;
        }

        private void UpdateGameData()
        {
            contadorTicks++;
            if(contadorTicks == 15)
            {
                contadorTicks = 0;
                if (this.entidades.Count < this.cancion.Length)
                {
                    CrearNota(cancion[indiceCancion]);
                    indiceCancion++;
                }
            }
            if (indiceJugador == cancion.Length-1)
            {
                TerminarJuego();
                return;
            }
            if (entidades.Count > 1 && !((Nota)entidades[indiceJugador+1]).FueApretada && ((Nota)entidades[indiceJugador + 1]).ChequearColision(jugador) &&((ChequearColision() && hayTeclaApretada) || ChequearFueraDeRango()))
            {
                this.indiceJugador++;
                hayTeclaApretada = false;
            }
            MoverNotas();
        }

        private void Render()
        {
            this.lblIndice.Text = indiceJugador.ToString();
            this.lblPuntos.Text = contadorPuntos.ToString();
            foreach (Entidad entidad in this.entidades)
            {
                entidad.Render();
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateGameData();
            Render();
        }
        private void CrearNota(char notaChar)
        {
            int posicionX = 125;
            switch (notaChar)
            {
                case 'W':
                    notaChar = 'w';
                    break;
                case 'A':
                    notaChar = 'a';
                    posicionX += 100;
                    break;
                case 'S':
                    notaChar = 's';
                    posicionX += 200;
                    break;
                case 'D':
                    notaChar = 'd';
                    posicionX += 300;
                    break;
            }
            Nota notaEntidad = new Nota(new Point(posicionX, this.Height * 90 / 100), 10, $"nota_{notaChar}");
            this.Controls.Add(notaEntidad.RenderPictureBox);
            this.entidades.Add(notaEntidad);
        }

        private void MoverNotas()
        {
            foreach (Entidad entidad in this.entidades)
            {
                entidad.MoverseEnDireccionFacing();
            }
        }

        private void ChequearTeclaCorrecta(Keys tecla)
        {
            if (this.cancion[indiceJugador-1] == (char)tecla && !((Nota)entidades[indiceJugador + 1]).FueApretada)
            {
                ((Nota)entidades[indiceJugador + 1]).ReproducirSonido();
                this.contadorPuntos++;
            }
        }

        private bool ChequearColision()
        {
            foreach (Entidad entidad in this.entidades)
            {
                if(entidad is Nota nota && jugador.ChequearColision(nota))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ChequearFueraDeRango()
        {
            foreach (Entidad entidad in this.entidades)
            {
                if(entidad is Nota nota && nota.Y - 50 < jugador.Y)
                {
                    return true;
                }
            }
            return false;
        }

        private void TerminarJuego()
        {
            this.timer1.Stop();
            this.labelCancion.Text = "Fin de la cancion";
        }
    }
}
