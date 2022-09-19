using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Logica;

namespace AnalisisNumerico_BonafedeMarengo
{
    public partial class Formulario : Form
    {
        public Formulario()
        {
            InitializeComponent();
            cmbU2Metodo.SelectedIndex = 0;
        }

        #region Tabcontrol
        


        #endregion

        #region Unidad 1

        private void btnU1Biseccion_Click(object sender, EventArgs e)
        {
            MostrarResultadosU1(Main.Bisección(ParseInputs()));
        }

        private void btnU1ReglaFalsa_Click(object sender, EventArgs e)
        {
            MostrarResultadosU1(Main.ReglaFalsa(ParseInputs()));
        }

        private void btnU1Tangente_Click(object sender, EventArgs e)
        {
            MostrarResultadosU1(Main.Tangente(ParseInputs()));
        }

        private void btnU1Secante_Click(object sender, EventArgs e)
        {
            MostrarResultadosU1(Main.Secante(ParseInputs()));
        }

        private void MostrarResultadosU1(Salida salida)
        {
            if (!salida._Error)
            {
                txtU1Metodo.Text = salida._Metodo;
                txtU1Converge.Text = salida.Converge.ToString();
                txtU1CantIteraciones.Text = salida.Iteraciones.ToString();
                txtU1ErrorRelativo.Text = salida.ErrorRelativo.ToString();
                txtU1Raiz.Text = Math.Round(salida.Raiz,4).ToString();
            }
            else 
            {
                MessageBox.Show(salida._MsjError, $"Error - Método: {salida._Metodo}", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public Entrada ParseInputs()
        {
            CheckDefaults();
            Entrada entrada = new Entrada()
            {
                Funcion = txtU1Funcion.Text,
                Xi = Convert.ToDouble(txtU1XIzquierda.Text),
                Xd = Convert.ToDouble(txtU1XDerecha.Text),
                Tolerancia = Convert.ToDouble(txtU1Tolerancia.Text),
                MaxIter = Convert.ToInt32(txtU1MaxIteraciones.Text)
            };
            return entrada;
        }

        private void CheckDefaults()
        {
            if (txtU1Tolerancia.Text == "")
                txtU1Tolerancia.Text = "0.0001";
            
            if (txtU1MaxIteraciones.Text == "")
                txtU1MaxIteraciones.Text = "100";            
        }
        #endregion

        #region Unidad 2

        private void btnU2Generar_Click(object sender, EventArgs e)
        {
            int dimension = (int)nudU2Dimension.Value;
            int puntoX = 10;
            int puntoY = 10;
            int espacioX = 10;
            int espacioY = 10;
            int sizeX = 75;
            int sizeY = 22;
            
            pnlMatriz.Controls.Clear();

            for (int col = 0; col < dimension+1; col++)
            {
                for (int row = 0; row < dimension; row++)
                {
                    TextBox txt = new TextBox();
                    txt.Name = row.ToString() + "r" + col.ToString() + "c";
                    txt.Location = new Point(puntoX, puntoY);
                    txt.Size = new Size(sizeX, sizeY);
                    pnlMatriz.Controls.Add(txt);
                    pnlMatriz.Show();
                    puntoY += sizeY + espacioY;
                }
                puntoY = 10;
                puntoX += sizeX + espacioX;
            }
        }

        private void btnU2Calcular_Click(object sender, EventArgs e)
        {

        }

        private void MostrarResultadosU2()
        {

        }

        #endregion

        #region Unidad 3

        #endregion

        #region Unidad 4

        #endregion

        #region Validaciones

        #endregion
    }
}
