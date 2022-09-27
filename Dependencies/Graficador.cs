using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Globalization;

namespace Analisis_Numerico
{
    public partial class Graficador : UserControl
    {
        const string VBS_file = "script.vbs";
        const string output_file = "output.txt";
        const double ZOOM_MAX = 5;

        double min, max, range;
        private double xmin;
        private double ymin;
        private double xmax;
        private double ymax;
        private double xstep;

        private string formula;

        List<double[]> values; // actual points on the curve

        public Graficador()
        {
            InitializeComponent();
        }
		
		/// <summary>
        /// Grafica un eje cartesiano junto con puntos de entrada y una funcion ingresada por parámetro.
        /// La función tiene que ser sin espacios blancos entre caracteres.
        /// "scale" acepta 3 valores: [0] xmin, [1] xmax, [2] xstep.
        /// </summary>
        /// <param name="entryPoints">Puntos de entrada</param>
        /// <param name="function">Función resultante</param>
        /// <param name="scale">Escala</param>
        public void Graficar(IList<double[]> entryPoints, string function, double[] scale)
        {
            this.xmin = scale[0];
            this.xmax = scale[1];
            this.xstep = scale[2];
            this.formula = function;
            this.values = CreateOutput();
            if (this.values == null)
            {
                MessageBox.Show("Invalid formula!");
                return;
            }
            this.CalcRange();
            this.DrawGraph(entryPoints);
            this.DrawGrid();
        }

        private void DrawGrid()
        {
            const int NUM_SECTION = 20;
            int section_w = 0, section_h = 0;
            // calculates section sizes and draws a grid for picturebox_graph background

            int w = (int)Math.Round(pictureBoxAxis.Width * ZOOM_MAX);
            int h = (int)Math.Round(pictureBoxAxis.Height * ZOOM_MAX);
            int _section_w = (int)((Math.Round(range / NUM_SECTION, GetDecimalPlaces((decimal)xstep)) / range) * w);
            int _section_h = (int)((Math.Round(range / NUM_SECTION, GetDecimalPlaces((decimal)xstep)) / range) * h);
            if (_section_w == section_w && _section_h == section_h) return;
            section_h = _section_h;
            section_w = _section_w;
            Bitmap bmp = new Bitmap(w, h);
            Pen pen = new Pen(Brushes.LightGray);
            Pen pen_axis = new Pen(Brushes.Black);
            Graphics g = Graphics.FromImage(bmp);
            Font font = new Font("Arial", (float)section_w / 3);
            int i;
            // axis
            g.DrawLine(pen_axis, new Point(w / 2 - 1, 0), new Point(w / 2 - 1, h));
            g.DrawLine(pen_axis, new Point(w / 2, 0), new Point(w / 2, h));
            g.DrawLine(pen_axis, new Point(w / 2 + 1, 0), new Point(w / 2 + 1, h));
            g.DrawLine(pen_axis, new Point(0, h / 2 - 1), new Point(w, h / 2 - 1));
            g.DrawLine(pen_axis, new Point(0, h / 2), new Point(w, h / 2));
            g.DrawLine(pen_axis, new Point(0, h / 2 + 1), new Point(w, h / 2 + 1));
            
            // draw zero
            g.DrawString("0", font, Brushes.Black, new Point((int)Math.Round((double)w / 2 - ((double)section_w / 4)), (int)Math.Round((double)h / 2 + (double)section_h / 4)));

            string format;
            double number;
            // left half
            for (i = w / 2 - section_w; i >= 0; i -= section_w)
            {
                g.DrawLine(pen, new Point(i, 0), new Point(i, h));
                if ((i - w / 2) % (2 * section_w) == 0)
                {
                    number = (min + (double)i / w * range);
                    format = GetFormat(number);
                    g.DrawString(number.ToString(format), font, Brushes.Black, new Point(Math.Max((int)Math.Round((double)i - ((double)section_w / 4)), 0), (int)Math.Round((double)h / 2 + (double)section_h / 4)));
                }
            }
            // upper half
            for (i = h / 2 - section_h; i >= 0; i -= section_h)
            {
                g.DrawLine(pen, new Point(0, i), new Point(w, i));
                if ((i - h / 2) % (2 * section_h) == 0)
                {
                    number = (-min - (double)i / h * range);
                    format = GetFormat(number);
                    g.DrawString(number.ToString(format), font, Brushes.Black, new Point((int)Math.Round((double)w / 2 + (double)section_w / 4), Math.Max((int)Math.Round(i - ((double)section_h / 4)), 0)));
                }
            }
            // right half
            for (i = w / 2 + section_w; i <= w; i += section_w)
            {
                g.DrawLine(pen, new Point(i, 0), new Point(i, h));
                if ((i - w / 2) % (2 * section_w) == 0)
                {
                    number = (min + (double)i / w * range);
                    format = GetFormat(number);
                    g.DrawString(number.ToString(format), font, Brushes.Black, new Point((int)Math.Round((double)i - ((double)section_w / 4)), (int)Math.Round((double)h / 2 + (double)section_h / 4)));
                }
            }
            // lower half
            for (i = h / 2 + section_h; i <= h; i += section_h)
            {
                g.DrawLine(pen, new Point(0, i), new Point(w, i));
                if ((i - h / 2) % (2 * section_h) == 0)
                {
                    number = (-min - (double)i / h * range);
                    format = GetFormat(number);
                    g.DrawString(number.ToString(format), font, Brushes.Black, new Point((int)Math.Round((double)w / 2 + (double)section_w / 4), (int)Math.Round(i - ((double)section_h / 4))));
                }
            }
            g.Dispose();
            if (bmp != null)
            {
                if (pictureBoxAxis.BackgroundImage != null) pictureBoxAxis.BackgroundImage.Dispose();
                pictureBoxAxis.BackgroundImage = bmp;
                pictureBoxAxis.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void CalcRange()
        {
            ymin = xmin;
            ymax = xmax;

            // calculates size of plotting area in coordinate system measures
            max = Math.Max(Math.Max(Math.Max(Math.Abs(xmin), Math.Abs(xmax)), Math.Abs(ymin)), Math.Abs(ymax));
            min = -1 * max;
            range = max - min;
        }

        private int GetDecimalPlaces(decimal number)
        {
            return BitConverter.GetBytes(decimal.GetBits(number)[3])[2];
        }

        private string GetFormat(double number)
        {
            // takes the number of decimal places from xstep and returns it as format for number string representation

            int dec1 = GetDecimalPlaces((decimal)xstep);
            int dec2 = GetDecimalPlaces((decimal)number);
            return "N" + Math.Min(dec1, dec2).ToString();
        }

        private List<double[]> CreateOutput()
        {
            // runs generated VBS code, and reads the output (points of graph)

            List<double[]> retValue = new List<double[]>();
            double[] tmp;

            string wscript = Environment.GetEnvironmentVariable("windir") + "\\SysWOW64\\wscript.exe";
            wscript = File.Exists(wscript) ? wscript : "wscript.exe";
            CreateVBS();
            Process p = new Process();
            p.StartInfo.FileName = wscript;
            p.StartInfo.Arguments = VBS_file;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start(); // run VBS
            p.WaitForExit();
            if (p.ExitCode > 0) // error
                return null;
            FileStream fs = new FileStream(output_file, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string line;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                try
                {
                    tmp = line.Split(' ').Select(x => double.Parse(x, CultureInfo.CurrentCulture)).ToArray();
                }
                catch
                {
                    return null;
                }
                retValue.Add(tmp);
            }
            sr.Close();
            fs.Close();

            return retValue;
        }

        private void CreateVBS()
        {
            // creates VBS script to calculate the graph points based on user input

            FileStream fs = new FileStream(VBS_file, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine("Const x_min = " + xmin.ToString().Replace(',', '.'));
            sw.WriteLine("Const x_max = " + xmax.ToString().Replace(',', '.'));
            sw.WriteLine("Const x_step = " + xstep.ToString().Replace(',', '.'));
            sw.WriteLine();
            sw.WriteLine("On Error Resume Next");
            sw.WriteLine("Dim fw: Set fw = CreateObject(\"Scripting.FileSystemObject\").OpenTextFile(\"" + output_file + "\",2,true)");
            sw.WriteLine();
            sw.WriteLine("Dim x, y");
            sw.WriteLine("For x = x_min To x_max Step x_step");
            sw.WriteLine("\ty = " + formula);
            sw.WriteLine("\tIf Err.Number <> 0 Then");
            sw.WriteLine("\t\tErr.Clear");
            sw.WriteLine("\t\tfw.Close");
            sw.WriteLine("\t\tSet fw = Nothing");
            sw.WriteLine("\t\tWScript.Quit 1");
            sw.WriteLine("\tEnd If");
            sw.WriteLine("\tfw.WriteLine CStr(x) & \" \" & CStr(y)");
            sw.WriteLine("Next");
            sw.WriteLine();
            sw.WriteLine("fw.Close");
            sw.WriteLine("Set fw = Nothing");
            sw.Close();
            fs.Close();
        }

        private void DrawGraph(IList<double[]> entryPoints)
        {
            // draws 2d graph from a list of points            
            Bitmap bmp = new Bitmap((int)Math.Round(pictureBoxAxis.Width * ZOOM_MAX), (int)Math.Round(pictureBoxAxis.Height * ZOOM_MAX));
            Graphics g = Graphics.FromImage(bmp);

            //Dibuja los puntos de entrada
            int x, y;
            foreach (var entryPoint in entryPoints)
            {
                x = (int)Math.Round(bmp.Width * (entryPoint[0] - min) / range);
                y = (int)Math.Round(bmp.Height * (1 - (entryPoint[1] - min) / range));

                g.FillRectangle(Brushes.Blue, x, y, 35f, 35f);
            }

            // Dibuja la gráfica de la función
            List<Point> points = values.Select(t =>
                                        new Point(
                                            (int)Math.Round(bmp.Width * (t[0] - min) / range),
                                            (int)Math.Round(bmp.Height * (1 - (t[1] - min) / range))
                                        )
                                    ).ToList();
            Pen pen = new Pen(Brushes.DarkRed, 10f);
            g.DrawLines(pen, points.ToArray<Point>());

            g.Dispose();

            pictureBoxAxis.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxAxis.Image = bmp;
        }
    }
}
