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
        const double ZOOM_STEP = 0.2;

        double min, max, range;
        private double xmin;
        private double ymin;
        private double xmax;
        private double ymax;
        private double xstep;

        private string formula;

        private Point previousLocation;

        Bitmap bmp_dot;
        Button dot;        

        List<double[]> values; // actual points on the curve

        IList<double[]> EntryPoints;

        public Graficador()
        {
            InitializeComponent();

            this.pictureBoxContainer.Dock = DockStyle.Fill;
            this.pictureBoxContainer.Controls.Add(pictureBoxAxis);
            this.pictureBoxAxis.Location = new Point(0, 0);
            this.pictureBoxAxis.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.pictureBoxAxis.MouseWheel += PictureBox_MouseWheel;
            this.pictureBoxAxis.MouseDown += PictureBox_MouseDown;
            this.pictureBoxAxis.MouseUp += PictureBox_MouseUp;

            dot = new Button();
            this.pictureBoxAxis.Controls.Add(dot);
            dot.Visible = false;
            dot.Size = new Size(200, 20);
            dot.FlatStyle = FlatStyle.Flat;
            dot.FlatAppearance.BorderSize = 0;
            dot.ForeColor = Color.Black;
            dot.TextAlign = ContentAlignment.BottomLeft;
            DrawDot();
            dot.Image = bmp_dot;
            dot.ImageAlign = ContentAlignment.MiddleLeft;
        }
		
		/// <summary>
        /// Grafica un eje cartesiano junto con puntos de entrada y una funcion ingresada por parámetro.
        /// La función tiene que ser sin espacios blancos entre caracteres.
        /// "scale" acepta 3 valores: [0] xmin, [1] xmax, [2] xstep.
        /// </summary>
        /// <param name="entryPoints">Puntos de entrada</param>
        /// <param name="function">Función resultante</param>
        /// <param name="scale">Escala</param>
        public void Graficar(IList<double[]> entryPoints, string function)
        {
            this.EntryPoints = entryPoints;
            this.SetValidFormula(function);
            this.SetScale(entryPoints);
            this.values = CreateOutput();
            if (this.values == null)
            {
                MessageBox.Show("Invalid formula!");
                return;
            }
            this.CalcRange();
            this.DrawGraph(entryPoints);
            this.DrawGrid();
            this.SetMouseActions();
        }

        private void SetValidFormula(string function)
        {
            this.formula = function.Replace(",", ".");
            this.formula = formula.Replace(" ", "");
            
            if (this.formula.Contains("y="))
            {
                this.formula = this.formula.Replace("y=", "");
            }

            if (this.formula.Contains("+-"))
            {
                this.formula = this.formula.Replace("+-", "-");
            }

            if (!this.formula.Contains('*'))
            {
                this.formula = this.formula.Replace("x", "*x");
            }
        }

        private void SetScale(IList<double[]> entryPoints)
        {
            this.xmin = -10;
            this.xmax = 10;
            this.xstep = 1;
            foreach (var point in entryPoints)
            {
                if (Math.Abs(point[0]) > this.xmax)
                {
                    this.xmax = Math.Abs(point[0]);
                }
                if (Math.Abs(point[1]) > this.xmax)
                {
                    this.xmax = Math.Abs(point[1]);
                }
            }
            if (this.xmax > 10 && this.xmax <= 20)
            {
                this.xmax += 5;
            }
            else if (this.xmax > 20)
            {
                this.xmax += 10;
            }

            this.xmin = -(this.xmax);

            if (this.formula.Contains('^'))
            {
                this.xstep = 0.1;
            }
        }

        private void SetMouseActions()
        {
            if (this.xmin == -10 && this.xmax == 10)
            {
                this.pictureBoxAxis.MouseWheel -= PictureBox_MouseWheel;
                this.pictureBoxAxis.MouseDown -= PictureBox_MouseDown;
                this.pictureBoxAxis.MouseUp -= PictureBox_MouseUp;
            }
            else
            {
                this.pictureBoxAxis.MouseWheel += PictureBox_MouseWheel;
                this.pictureBoxAxis.MouseDown += PictureBox_MouseDown;
                this.pictureBoxAxis.MouseUp += PictureBox_MouseUp;
            }
        }

        private void DrawGrid(double corr_factor = 1)
        {
            const int NUM_SECTION = 20;
            int section_w = 0, section_h = 0;
            // calculates section sizes and draws a grid for this.pictureBoxAxis background

            int w = (int)Math.Round(pictureBoxAxis.Width * ZOOM_MAX);
            int h = (int)Math.Round(pictureBoxAxis.Height * ZOOM_MAX);
            int _section_w = (int)((Math.Round(range / NUM_SECTION, GetDecimalPlaces((decimal)xstep)) / range) * w / corr_factor);
            int _section_h = (int)((Math.Round(range / NUM_SECTION, GetDecimalPlaces((decimal)xstep)) / range) * h / corr_factor);
            if (_section_w == section_w && _section_h == section_h && corr_factor != 1) return;
            section_h = _section_h;
            section_w = _section_w;
            Bitmap bmp = new Bitmap(w, h);
            Pen pen = new Pen(Brushes.LightGray);
            Pen pen_axis = new Pen(Brushes.Black);
            Graphics g = Graphics.FromImage(bmp);
            Font font = new Font("Arial", (float)section_w / 3.5f);
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

        private void DrawGraph(IList<double[]> entryPoints, double corr_factor = 1)
        {
            const int NUM_SECTION = 20;
            int section_w = 0, section_h = 0;
            // calculates section sizes and draws the points and the function line.

            int w = (int)Math.Round(pictureBoxAxis.Width * ZOOM_MAX);
            int h = (int)Math.Round(pictureBoxAxis.Height * ZOOM_MAX);
            int _section_w = (int)((Math.Round(range / NUM_SECTION, GetDecimalPlaces((decimal)xstep)) / range) * w / corr_factor);
            int _section_h = (int)((Math.Round(range / NUM_SECTION, GetDecimalPlaces((decimal)xstep)) / range) * h / corr_factor);
            if (_section_w == section_w && _section_h == section_h && corr_factor != 1) return;
            section_h = _section_h;
            section_w = _section_w;

            // draws 2d graph from a list of points            
            Bitmap bmp = new Bitmap(w,h);
            Graphics g = Graphics.FromImage(bmp);

            //Dibuja los puntos de entrada
            int x, x2, y, y2;
            foreach (var entryPoint in entryPoints)
            {
                x = (int)Math.Round(bmp.Width * (entryPoint[0] - min) / range);
                x2 = (int)Math.Round(bmp.Width * (entryPoint[0] - 0.1 - min) / range);
                y = (int)Math.Round(bmp.Height * (1 - (entryPoint[1] - min) / range));
                y2 = (int)Math.Round(bmp.Height * (1 - (entryPoint[1] - 0.1 - min) / range));

                g.FillRectangle(Brushes.Blue, x, y, (float)section_w / 5, (float)section_h / 5);
                g.FillRectangle(Brushes.Blue, x2, y, (float)section_w / 5, (float)section_h / 5);
                g.FillRectangle(Brushes.Blue, x, y2, (float)section_w / 5, (float)section_h / 5);
                g.FillRectangle(Brushes.Blue, x2, y2, (float)section_w / 5, (float)section_h / 5);
            }

            // Dibuja la gráfica de la función
            List<Point> points = values.Select(t =>
                                        new Point(
                                            (int)Math.Round(bmp.Width * (t[0] - min) / range),
                                            (int)Math.Round(bmp.Height * (1 - (t[1] - min) / range))
                                        )
                                    ).ToList();
            Pen pen = new Pen(Brushes.DarkRed, (float)section_w / 10);
            g.DrawLines(pen, points.ToArray<Point>());

            g.Dispose();

            pictureBoxAxis.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxAxis.Image = bmp;
        }

        private void ZoomGraph(int sgn)
        {
            // calculates new width, height, and location of this.pictureBoxAxis (for zooming)
            // sgn = 1 is zoom in
            // sgn = -1 is zoom out

            double width = this.pictureBoxAxis.Width + sgn * ZOOM_STEP * this.pictureBoxContainer.Width;
            double height;
            if (width > this.pictureBoxContainer.Width * ZOOM_MAX) // if out of upper bounds, set to max
            {
                width = this.pictureBoxContainer.Width * ZOOM_MAX;
                height = this.pictureBoxContainer.Height * ZOOM_MAX;
            }
            else if (width < this.pictureBoxContainer.Width) // if less then lower bounds, set to min
            {
                width = this.pictureBoxContainer.Width;
                height = this.pictureBoxContainer.Height;
            }
            else
            {
                height = this.pictureBoxAxis.Height + sgn * ZOOM_STEP * this.pictureBoxContainer.Height;
            }
            if (width == this.pictureBoxAxis.Width) return;
            Point location = this.pictureBoxAxis.Location;
            location = new Point(
                location.X + (int)Math.Round((this.pictureBoxAxis.Width - width) / 2),
                location.Y + (int)Math.Round((this.pictureBoxAxis.Height - height) / 2)
                );
            this.pictureBoxAxis.Width = (int)Math.Round(width);
            this.pictureBoxAxis.Height = (int)Math.Round(height);
            //stop crossing container border inward
            LocationCorrection(ref location);
            this.pictureBoxAxis.Location = location;
            // Redraw grid on every round zoom factor
            double corr_factor = (double)this.pictureBoxAxis.Width / (double)this.pictureBoxContainer.Width;
            corr_factor = Math.Floor(corr_factor);
            if (corr_factor % 2 == 0 && sgn > 0 || corr_factor % 2 == 1 && sgn < 0)
            {
                DrawGraph(this.EntryPoints,corr_factor);
                DrawGrid(corr_factor);
            }
        }

        private void MoveGraph(Point loc)
        {
            // changes location of this.pictureBoxAxis based on (mouse) location

            Point location = this.pictureBoxAxis.Location;
            location.Offset(loc.X - previousLocation.X, loc.Y - previousLocation.Y);
            LocationCorrection(ref location);
            this.pictureBoxAxis.Location = location;
        }

        private void LocationCorrection(ref Point location)
        {
            // corrects the location of this.pictureBoxAxis if it gets inside the boudaries of this.pictureBoxContainer
            if (location.X > 0)
                location.X = 0;
            if (location.Y > 0)
                location.Y = 0;
            if (location.X + this.pictureBoxAxis.Width < this.pictureBoxContainer.Width)
                location.X = this.pictureBoxContainer.Width - this.pictureBoxAxis.Width;
            if (location.Y + this.pictureBoxAxis.Height < this.pictureBoxContainer.Height)
                location.Y = this.pictureBoxContainer.Height - this.pictureBoxAxis.Height;
        }

        private bool IsMouseOutside()
        {
            // checks if mouse is outside of boundaries of this.pictureBoxContainer
            Point mousePos_container = this.pictureBoxContainer.PointToClient(Control.MousePosition);
            return mousePos_container.X < 0 
                    || mousePos_container.X > this.pictureBoxContainer.Width 
                    || mousePos_container.Y < 0 
                    || mousePos_container.Y > this.pictureBoxContainer.Height;            
        }

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ZoomGraph(e.Delta > 0 ? 1 : -1);
        }

        private void PictureBox_LeftMouseMove(object sender, MouseEventArgs e)
        {
            MoveGraph(e.Location);
        }

        private void PictureBox_RightMouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseOutside())
                return;
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.pictureBoxAxis.MouseMove -= PictureBox_LeftMouseMove;
                Cursor = Cursors.Default;
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.pictureBoxAxis.MouseMove -= PictureBox_RightMouseMove;
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.pictureBoxAxis.Image == null)
                return;
            if (e.Button == MouseButtons.Left)
            {
                this.pictureBoxAxis.MouseMove += PictureBox_LeftMouseMove;
                previousLocation = e.Location;
                Cursor = Cursors.Hand;
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.pictureBoxAxis.MouseMove += PictureBox_RightMouseMove;
            }
        }

        private void DrawDot()
        {
            // draws a red dot used for marking a point on the graph
            bmp_dot = new Bitmap(dot.Size.Height, dot.Size.Height);
            Graphics g = Graphics.FromImage(bmp_dot);
            g.FillEllipse(Brushes.Red, 0, 0, bmp_dot.Width / 2, bmp_dot.Height / 2);
            g.Dispose();
        }
    }
}
