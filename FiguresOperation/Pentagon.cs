using System;
using System.Drawing;
using System.Windows.Forms;

namespace FiguresOperation
{
    internal class Pentagon
    {
        private float mSideLength; // Longitud del lado
        private float mPerimeter;
        private float mArea;

        private float mRotation = 0f;
        private Point mPosition = new Point(0, 0);
        private float mScale = 1.0f;

        private Graphics mGraph;
        private const float SF = 20;
        private Pen mPen;

        public Pentagon()
        {
            mSideLength = 0.0f;
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

        public void ReadData(TextBox txtSideLength)
        {
            try
            {
                mSideLength = float.Parse(txtSideLength.Text);
            }
            catch
            {
                MessageBox.Show("Invalid input...", "Error Message");
            }
        }

        public void CalculatePerimeter()
        {
            mPerimeter = 5 * mSideLength;
        }

        public void CalculateArea()
        {
            mArea = (5f / 4f) * mSideLength * mSideLength * 1.37638192047f;
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = mPerimeter.ToString();
            txtArea.Text = mArea.ToString("0.00");
        }

        public void InitializeData(TextBox txtSideLength, TextBox txtPerimeter, TextBox txtArea, PictureBox picCanvas)
        {
            mSideLength = 0.0f;
            mPerimeter = 0.0f; mArea = 0.0f;

            txtSideLength.Text = "";
            txtPerimeter.Text = ""; txtArea.Text = "";
            txtSideLength.Focus();
            picCanvas.Refresh();
        }

        public void PlotShape(PictureBox picCanvas)
        {
            mGraph = picCanvas.CreateGraphics();
            picCanvas.Refresh();

            float centerX = picCanvas.Width / 2.0f + mPosition.X;
            float centerY = picCanvas.Height / 2.0f + mPosition.Y;

            float side = mSideLength * SF * mScale;

            PointF[] points = new PointF[5];

            for (int i = 0; i < 5; i++)
            {
                double angle = 2 * Math.PI * i / 5 - Math.PI / 2; 
                float radius = side / (2f * (float)Math.Sin(Math.PI / 5)); 

                points[i] = new PointF(
                    radius * (float)Math.Cos(angle),
                    radius * (float)Math.Sin(angle)
                );
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
