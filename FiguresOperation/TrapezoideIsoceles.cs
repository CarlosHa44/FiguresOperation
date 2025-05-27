using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiguresOperation
{
    internal class TrapezoideIsoceles
    {
        private float mBaseMayor;   // Longitud de la base mayor
        private float mBaseMenor;   // Longitud de la base menor
        private float mAltura;       // Altura del trapezoide
        private float mPerimeter;
        private float mArea;

        private float mRotation = 0f;
        private Point mPosition = new Point(0, 0);
        private float mScale = 1.0f;

        private Graphics mGraph;
        private const float SF = 20;
        private Pen mPen;

        public TrapezoideIsoceles()
        {
            mBaseMayor = 0.0f; mBaseMenor = 0.0f; mAltura = 0.0f;
            mPerimeter = 0.0f; mArea = 0.0f;
            mPen = new Pen(Color.Blue, 3);
        }

        public void Rotate(float angle)
        {
            mRotation += angle;
            mRotation = mRotation % 360;
        }

        public void Translate(float x, float y)
        {
            mPosition.X += (int)x;
            mPosition.Y += (int)y;
        }

        public void Escalate(float scale)
        {
            mScale = scale;
            if (mScale < 0.1f) mScale = 0.1f;
            if (mScale > 8f) mScale = 8f;
        }

        public void ResetTransform()
        {
            mRotation = 0f;
            mPosition.X = 0;
            mPosition.Y = 0;
            mScale = 1.0f;
        }

        public void ReadData(TextBox txtBaseMayor, TextBox txtBaseMenor, TextBox txtAltura)
        {
            try
            {
                mBaseMayor = float.Parse(txtBaseMayor.Text);
                mBaseMenor = float.Parse(txtBaseMenor.Text);
                mAltura = float.Parse(txtAltura.Text);

                // Validar que la base mayor sea mayor que la menor
                if (mBaseMayor <= mBaseMenor)
                    throw new Exception("La base mayor debe ser más grande que la base menor");

                if (mAltura <= 0)
                    throw new Exception("La altura debe ser mayor que cero");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Entrada inválida: {ex.Message}", "Error");
            }
        }

        public void CalculatePerimeter()
        {
            float diferenciaBases = (mBaseMayor - mBaseMenor) / 2;
            float lado = (float)Math.Sqrt((diferenciaBases * diferenciaBases) + (mAltura * mAltura));
            mPerimeter = mBaseMayor + mBaseMenor + (2 * lado);
        }

        public void CalculateArea()
        {
            mArea = ((mBaseMayor + mBaseMenor) / 2) * mAltura;
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = mPerimeter.ToString("0.00");
            txtArea.Text = mArea.ToString("0.00");
        }

        public void InitializeData(TextBox txtBaseMayor, TextBox txtBaseMenor, TextBox txtAltura,
                                 TextBox txtPerimeter, TextBox txtArea, PictureBox picCanvas)
        {
            mBaseMayor = 0.0f; mBaseMenor = 0.0f; mAltura = 0.0f;
            mPerimeter = 0.0f; mArea = 0.0f;

            txtBaseMayor.Text = ""; txtBaseMenor.Text = ""; txtAltura.Text = "";
            txtPerimeter.Text = ""; txtArea.Text = "";
            txtBaseMayor.Focus();
            picCanvas.Refresh();
        }

        public void PlotShape(PictureBox picCanvas)
        {
            mGraph = picCanvas.CreateGraphics();
            picCanvas.Refresh();

            float centerX = picCanvas.Width / 2.0f + mPosition.X;
            float centerY = picCanvas.Height / 2.0f + mPosition.Y;

            float baseMayorScaled = mBaseMayor * SF * mScale;
            float baseMenorScaled = mBaseMenor * SF * mScale;
            float alturaScaled = mAltura * SF * mScale;

            float diferencia = (baseMayorScaled - baseMenorScaled) / 2;

            PointF[] points = new PointF[4];
            points[0] = new PointF(-baseMayorScaled / 2, alturaScaled / 2);          // Esquina superior izquierda
            points[1] = new PointF(baseMayorScaled / 2, alturaScaled / 2);           // Esquina superior derecha
            points[2] = new PointF(baseMenorScaled / 2, -alturaScaled / 2);          // Esquina inferior derecha
            points[3] = new PointF(-baseMenorScaled / 2, -alturaScaled / 2);         // Esquina inferior izquierda

            if (mRotation != 0)
            {
                double radians = mRotation * Math.PI / 180.0;
                float cos = (float)Math.Cos(radians);
                float sin = (float)Math.Sin(radians);

                for (int i = 0; i < points.Length; i++)
                {
                    float x = points[i].X;
                    float y = points[i].Y;
                    points[i].X = x * cos - y * sin;
                    points[i].Y = x * sin + y * cos;
                }
            }

            for (int i = 0; i < points.Length; i++)
            {
                points[i].X += centerX;
                points[i].Y += centerY;
            }

            mGraph.DrawPolygon(mPen, points);
        }

        public void CloseForm(Form objForm)
        {
            objForm.Close();
        }
    }
}
