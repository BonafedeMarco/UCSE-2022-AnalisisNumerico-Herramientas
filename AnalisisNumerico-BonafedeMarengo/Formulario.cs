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
using Entidades;
using Analisis_Numerico;

namespace AnalisisNumerico_BonafedeMarengo
{
    public partial class Formulario : Form
    {
        Graficador graficador = new Graficador();
        public Formulario()
        {
            InitializeComponent();
            
            cmbU2Metodo.SelectedIndex = 0;

            cmbU3Metodo.SelectedIndex = 0;

            cmbU4Metodo.SelectedIndex = 0;

            SetPanelGrafica();
        }

        #region Tabcontrol

        private void tbcUnidades_TabIndexChanged(object sender, EventArgs e)
        {
            if (tbcUnidades.SelectedTab == tbcUnidad3)
            {
                this.Size = new Size(815, 645); // Editar en base a lo que ocupe el graficador
            }
            else
            {
                this.Size = new Size(815, 645);
            }
        }

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

        private void MostrarResultadosU1(U1Salida salida)
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

        public U1Entrada ParseInputs()
        {
            CheckDefaults();
            U1Entrada entrada = new U1Entrada()
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
                if (col == dimension)
                {
                    for (int i = 0; i < dimension; i++)
                    {
                        Label lbl = new Label();
                        lbl.Location = new Point(puntoX, puntoY);
                        lbl.Size = new Size(espacioX, sizeY);
                        lbl.Text = "=";
                        pnlMatriz.Controls.Add(lbl);
                        pnlMatriz.Show();
                        puntoY += lbl.Size.Height + espacioY;
                    }
                    puntoY = 10;
                    puntoX += espacioX*2;
                }
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

        private double[,] GuardarMatriz()
        {
            double[,] matriz = new double[Convert.ToInt32(nudU2Dimension.Value), Convert.ToInt32(nudU2Dimension.Value) + 1];
            for (int i = 0; i < nudU2Dimension.Value; i++)
            {
                for (int j = 0; j < nudU2Dimension.Value + 1; j++)
                {
                    Control textBox = pnlMatriz.Controls.Find($"{i}r{j}c", true).First();
                    matriz[i, j] = double.Parse((textBox as TextBox).Text);
                }
            }
            return matriz;
        }

        private void btnU2Calcular_Click(object sender, EventArgs e)
        {
            U2Entrada entrada = new U2Entrada();
            entrada.Matriz = GuardarMatriz();
            entrada.Dimension = Convert.ToInt32(nudU2Dimension.Value);
            entrada.Tolerancia = Convert.ToDouble(txtU2Tolerancia.Text);
            entrada.MaxIter = (int)nudU2MaxIter.Value;
            entrada.Decimales = (int)nudU2Decimales.Value;

            switch (cmbU2Metodo.SelectedIndex)
            {
                case 0:
                    MostrarResultadosU2(Main.GaussJordan(entrada));
                    break;
                case 1:
                    MostrarResultadosU2(Main.GaussSeidel(entrada));
                    break;
                default:
                    break;
            }
        }

        private void MostrarResultadosU2(U2Salida salida)
        {
            if (!salida._Error)
            {
                string resultado = "";
                for (int i = 0; i < salida.Resultado.Length; i++)
                {
                    resultado += $"X{i+1} = {salida.Resultado[i]}\n";
                }

                MessageBox.Show(resultado, $"Resultados - {salida._Metodo}{(salida._Metodo == "Gauss-Seidel" ? $" - Iteraciones: {salida.Iteraciones}" : "")}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(salida._MsjError, $"Error - {salida._Metodo} - Iteraciones: {salida.Iteraciones}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbU2Metodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbU2Metodo.SelectedIndex == 0)
            {
                txtU2Tolerancia.Enabled = false;
                nudU2MaxIter.Enabled = false;
            }

            if (cmbU2Metodo.SelectedIndex == 1)
            {
                txtU2Tolerancia.Enabled = true;
                nudU2MaxIter.Enabled = true;
            }
        }

        #endregion

        #region Unidad 3

        public List<double[]> PuntosCargados { get; set; }

        private void SetPanelGrafica()
        {
            pnlGraficador.Controls.Clear();
            this.graficador = new Graficador();
            pnlGraficador.Controls.Add(this.graficador);
            graficador.Dock = DockStyle.Fill;
        }

        private void btnU3AgregarPunto_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtU3X.Text) && !string.IsNullOrEmpty(txtU3Y.Text))
            {
                double[] punto = new double[2] { double.Parse(txtU3X.Text), double.Parse(txtU3Y.Text) };
                if (PuntosCargados == null)
                {
                    PuntosCargados = new List<double[]>();
                }
                PuntosCargados.Add(punto);
            }
            RefreshListBox();

            txtU3X.Text = "";
            txtU3Y.Text = "";
            txtU3X.Select();
        }

        private void btnU3Ultimo_Click(object sender, EventArgs e)
        {
            PuntosCargados.RemoveAt(PuntosCargados.Count-1);
            RefreshListBox();
        }

        private void btnU3Seleccionado_Click(object sender, EventArgs e)
        {
            PuntosCargados.RemoveAt(lbxU3Puntos.SelectedIndex);
            RefreshListBox();
        }

        private void btnU3Todos_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Está seguro?", "Confirmar eliminación", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.OK)
                PuntosCargados.Clear();
            RefreshListBox();
        }

        private void btnU3Calcular_Click(object sender, EventArgs e)
        {
            U3Entrada entrada = new U3Entrada();
            entrada.Tolerancia = double.Parse(txtU3Tolerancia.Text);
            entrada.PuntosCargados = PuntosCargados;
            entrada.Grado = cmbU3Metodo.SelectedIndex == 1 ? Convert.ToInt32(nudU3Grado.Value) : 0;

            switch (cmbU3Metodo.SelectedIndex)
            {
                case 0:
                    MostrarResultadosU3(Main.RegresionLineal(entrada));
                    break;
                case 1:
                    MostrarResultadosU3(Main.RegresionPolinomial(entrada));
                    break;
                default:
                    break;
            }
        }

        private void MostrarResultadosU3(U3Salida salida)
        {
            if (!salida._Error)
            {
                txtU3Funcion.Text = salida.Funcion;
                txtU3EfectividadPorcentaje.Text = salida.PorcentajeEfectividad.ToString();
                txtU3EfectividadAjuste.Text = salida.EfectividadAjuste ? "Efectivo" : "No efectivo";

                graficador.Graficar(PuntosCargados, salida.FuncionGraficador);
            }
            else
            {
                MessageBox.Show(salida._MsjError, $"Error - {salida._Metodo}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshListBox()
        {
            lbxU3Puntos.Items.Clear();
            foreach (double[] punto in PuntosCargados)
            {
                lbxU3Puntos.Items.Add($"({punto[0]}, {punto[1]})");
            }
            gbxU3Puntos.Text = $"Puntos: {PuntosCargados.Count}";
        }

        private void cmbU3Metodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbU3Metodo.SelectedIndex == 0)
            {
                nudU3Grado.Enabled = false;
            }

            if (cmbU3Metodo.SelectedIndex == 1)
            {
                nudU3Grado.Enabled = true;
            }
        }

        private void txtU3Y_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnU3AgregarPunto.PerformClick();
            }
        }

        #endregion

        #region Unidad 4

        private void cmbU4Metodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbU4Metodo.SelectedIndex == 3)
            {
                nudU4Subintervalos.Increment = 1;
            }
            else
            {
                nudU4Subintervalos.Increment = 2;
            }
        }

        private void btnU4Calcular_Click(object sender, EventArgs e)
        {
            U4Entrada entrada = new U4Entrada()
            {
                Funcion = txtU4Funcion.Text,
                PuntoA = Convert.ToInt32(txtU4PuntoA.Text),
                PuntoB = Convert.ToInt32(txtU4PuntoB.Text),
                CantidadSubintervalos = Convert.ToInt32(nudU4Subintervalos.Value),
                Metodo = cmbU4Metodo.SelectedIndex
            };

            MostrarResultadosU4(Main.Calcular(entrada));
        }

        private void MostrarResultadosU4(U4Salida salida)
        {
            if (!salida._Error)
            {
                txtU3Funcion.Text = salida.Resultado.ToString();
            }
            else
            {
                MessageBox.Show(salida._MsjError, $"Error - {salida._Metodo}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
