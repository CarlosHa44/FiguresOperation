using System;
using System.Drawing;
using System.Windows.Forms;

namespace FiguresOperation
{
    internal class Rhombus
    {
        private float mDiagonal1; // Diagonal mayor
        private float mDiagonal2; // Diagonal menor
        private float mPerimeter;
        private float mArea;

        private float mRotation = 0f;
        private Point mPosition = new Point(0, 0);
        private float mScale = 1.0f;

        private Graphics mGraph;
        private const float SF = 20;
        private Pen mPen;

        public Rhombus()
        {
            mDiagonal1 = 0.0f; mDiagonal2 = 0.0f;
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

        public void ReadData(TextBox txtDiagonal1, TextBox txtDiagonal2)
        {
            try
            {
                mDiagonal1 = float.Parse(txtDiagonal1.Text);
                mDiagonal2 = float.Parse(txtDiagonal2.Text);
            }
            catch
            {
                MessageBox.Show("Invalid input...", "Error Message");
            }
        }

        public void PerimeterRhombus()
        {
            // Perimeter of a rhombus is 4 * side length
            // Side length can be calculated using Pythagoras theorem with half diagonals
            float side = (float)Math.Sqrt(Math.Pow(mDiagonal1 / 2, 2) + Math.Pow(mDiagonal2 / 2, 2));
            mPerimeter = 4 * side;
        }

        public void AreaRhombus()
        {
            // Area of a rhombus is (diagonal1 * diagonal2) / 2
            mArea = (mDiagonal1 * mDiagonal2) / 2;
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = mPerimeter.ToString();
            txtArea.Text = mArea.ToString();
        }

        public void InitializeData(TextBox txtDiagonal1, TextBox txtDiagonal2, TextBox txtPerimeter, TextBox txtArea, PictureBox picCanvas)
        {
            mDiagonal1 = 0.0f; mDiagonal2 = 0.0f;
            mPerimeter = 0.0f; mArea = 0.0f;

            txtDiagonal1.Text = ""; txtDiagonal2.Text = "";
            txtPerimeter.Text = ""; txtArea.Text = "";
            txtDiagonal1.Focus();
            picCanvas.Refresh();
        }

        public void PlotShape(PictureBox picCanvas)
        {
            mGraph = picCanvas.CreateGraphics();
            picCanvas.Refresh();

            float centerX = picCanvas.Width / 2.0f + mPosition.X;
            float centerY = picCanvas.Height / 2.0f + mPosition.Y;

            // Calculate rhombus dimensions
            float d1 = mDiagonal1 * SF * mScale;
            float d2 = mDiagonal2 * SF * mScale;

            PointF[] points = new PointF[4];
            points[0] = new PointF(0, -d2 / 2);    // Top
            points[1] = new PointF(d1 / 2, 0);     // Right
            points[2] = new PointF(0, d2 / 2);      // Bottom
            points[3] = new PointF(-d1 / 2, 0);     // Left

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
