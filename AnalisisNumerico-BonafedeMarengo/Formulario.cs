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

        private void btnU1ReglaFalsa_Click(object sender, EventArgs e)
        {

        }

        private void btnU1Tangente_Click(object sender, EventArgs e)
        {

        }

        private void btnU1Secante_Click(object sender, EventArgs e)
        {

        }

        private void btnU1Biseccion_Click(object sender, EventArgs e)
        {

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
