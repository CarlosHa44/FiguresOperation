using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiguresOperation
{
    internal class Romboide
    {
        private float mBase;       // Longitud de la base
        private float mHeight;      // Altura
        private float mSideAngle;   // Ángulo entre base y lado (en grados)
        private float mPerimeter;
        private float mArea;

        private float mRotation = 0f;
        private Point mPosition = new Point(0, 0);
        private float mScale = 1.0f;

        private Graphics mGraph;
        private const float SF = 20;
        private Pen mPen;

        public Romboide()
        {
            mBase = 0.0f; mHeight = 0.0f; mSideAngle = 0.0f;
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

        public void ReadData(TextBox txtBase, TextBox txtHeight, TextBox txtSideAngle)
        {
            try
            {
                mBase = float.Parse(txtBase.Text);
                mHeight = float.Parse(txtHeight.Text);
                mSideAngle = float.Parse(txtSideAngle.Text);

                // Validar ángulo
                if (mSideAngle <= 0 || mSideAngle >= 180)
                    throw new Exception("El ángulo debe estar entre 0 y 180 grados");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Entrada inválida: {ex.Message}", "Error");
            }
        }

        public void CalculatePerimeter()
        {
            float sideLength = mHeight / (float)Math.Sin(mSideAngle * Math.PI / 180);
            mPerimeter = 2 * (mBase + sideLength);
        }

        public void CalculateArea()
        {
            mArea = mBase * mHeight;
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = mPerimeter.ToString("0.00");
            txtArea.Text = mArea.ToString("0.00");
        }

        public void InitializeData(TextBox txtBase, TextBox txtHeight, TextBox txtSideAngle,
                                TextBox txtPerimeter, TextBox txtArea, PictureBox picCanvas)
        {
            mBase = 0.0f; mHeight = 0.0f; mSideAngle = 0.0f;
            mPerimeter = 0.0f; mArea = 0.0f;

            txtBase.Text = ""; txtHeight.Text = ""; txtSideAngle.Text = "";
            txtPerimeter.Text = ""; txtArea.Text = "";
            txtBase.Focus();
            picCanvas.Refresh();
        }

        public void PlotShape(PictureBox picCanvas)
        {
            mGraph = picCanvas.CreateGraphics();
            picCanvas.Refresh();

            float centerX = picCanvas.Width / 2.0f + mPosition.X;
            float centerY = picCanvas.Height / 2.0f + mPosition.Y;

            float baseScaled = mBase * SF * mScale;
            float heightScaled = mHeight * SF * mScale;

            float skew = heightScaled / (float)Math.Tan(mSideAngle * Math.PI / 180);

            PointF[] points = new PointF[4];
            points[0] = new PointF(0, 0);
            points[1] = new PointF(baseScaled, 0);
            points[2] = new PointF(baseScaled + skew, heightScaled);
            points[3] = new PointF(skew, heightScaled);


            for (int i = 0; i < points.Length; i++)
            {
                points[i].X -= (baseScaled + skew) / 2;
                points[i].Y -= heightScaled / 2;
            }

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
