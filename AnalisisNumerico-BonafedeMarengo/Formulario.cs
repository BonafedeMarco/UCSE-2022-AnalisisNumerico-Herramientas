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
        }

        #region Unidad 1

        private void btnU1Biseccion_Click(object sender, EventArgs e)
        {
            MostrarResultados(Main.Bisección(ParseInputs()));
        }

        private void btnU1ReglaFalsa_Click(object sender, EventArgs e)
        {
            MostrarResultados(Main.ReglaFalsa(ParseInputs()));
        }

        private void btnU1Tangente_Click(object sender, EventArgs e)
        {
            MostrarResultados(Main.Tangente(ParseInputs()));
        }

        private void btnU1Secante_Click(object sender, EventArgs e)
        {
            MostrarResultados(Main.Secante(ParseInputs()));
        }

        private void MostrarResultados(Salida salida)
        {
            if (!salida._Error)
            {
                txtMetodo.Text = salida._Metodo;
                txtConverge.Text = salida.Converge.ToString();
                txtCantIteraciones.Text = salida.Iteraciones.ToString();
                txtErrorRelativo.Text = salida.ErrorRelativo.ToString();
                txtRaiz.Text = Math.Round(salida.Raiz,4).ToString();
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

        #endregion

        #region Unidad 3

        #endregion

        #region Unidad 4

        #endregion

        #region Validaciones

        #endregion
    }
}
