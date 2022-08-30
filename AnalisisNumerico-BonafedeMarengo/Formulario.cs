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

        #region Toolstrip

        private void tstUnidad1_Click(object sender, EventArgs e)
        {
            gbxU1Entradas.Enabled = true;
            gbxU1Metodos.Enabled = true;
            gbxU1Salidas.Enabled = true;
        }

        private void tstUnidad2_Click(object sender, EventArgs e)
        {

        }

        private void tstUnidad3_Click(object sender, EventArgs e)
        {

        }

        private void tstUnidad4_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Unidad 1

        private void btnU1Biseccion_Click(object sender, EventArgs e)
        {
            // AGREGAR VALIDACIONES
            Entrada entrada = new Entrada()
            {
                Funcion = txtU1Funcion.Text,
                Xi = Convert.ToDouble(txtU1XIzquierda.Text),
                Xd = Convert.ToDouble(txtU1XDerecha.Text),
                Tolerancia = Convert.ToDouble(txtU1Tolerancia.Text),
                MaxIter = Convert.ToInt32(txtU1MaxIteraciones.Text)
            };
            MostrarResultados(Main.Bisección(entrada));
        }

        private void btnU1ReglaFalsa_Click(object sender, EventArgs e)
        {
            // AGREGAR VALIDACIONES
            Entrada entrada = new Entrada()
            {
                Funcion = txtU1Funcion.Text,
                Xi = Convert.ToDouble(txtU1XIzquierda.Text),
                Xd = Convert.ToDouble(txtU1XDerecha.Text),
                Tolerancia = Convert.ToDouble(txtU1Tolerancia.Text),
                MaxIter = Convert.ToInt32(txtU1MaxIteraciones.Text)
            };
            MostrarResultados(Main.ReglaFalsa(entrada));
        }

        private void btnU1Tangente_Click(object sender, EventArgs e)
        {
            // AGREGAR VALIDACIONES
            Entrada entrada = new Entrada()
            {
                Funcion = txtU1Funcion.Text,
                Xi = Convert.ToDouble(txtU1XIzquierda.Text),
                Tolerancia = Convert.ToDouble(txtU1Tolerancia.Text),
                MaxIter = Convert.ToInt32(txtU1MaxIteraciones.Text)
            };
            MostrarResultados(Main.Tangente(entrada));
        }

        private void btnU1Secante_Click(object sender, EventArgs e)
        {
            // AGREGAR VALIDACIONES
            Entrada entrada = new Entrada()
            {
                Funcion = txtU1Funcion.Text,
                Xi = Convert.ToDouble(txtU1XIzquierda.Text),
                Xd = Convert.ToDouble(txtU1XDerecha.Text),
                Tolerancia = Convert.ToDouble(txtU1Tolerancia.Text),
                MaxIter = Convert.ToInt32(txtU1MaxIteraciones.Text)
            };
            MostrarResultados(Main.Secante(entrada));
        }

        private void MostrarResultados(Salida salida)
        {
            if (!salida._Error)
            {
                txtMetodo.Text = salida._Metodo;
                txtConverge.Text = salida.Converge.ToString();
                txtCantIteraciones.Text = salida.Iteraciones.ToString();
                txtErrorRelativo.Text = salida.ErrorRelativo.ToString();
                txtRaiz.Text = salida.Raiz.ToString();
            }
            else 
            {
                MessageBox.Show(salida._MsjError, $"Error - Método: {salida._Metodo}", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
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
