using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiguresOperation
{
    public partial class FrmPentagono : Form
    {
        private static FrmPentagono mInstance;
        private Pentagon obj = new Pentagon();
        public FrmPentagono()
        {
            InitializeComponent();
        }

        private void FrmPentagono_Load(object sender, EventArgs e)
        {
            obj.InitializeData(txtLength, txtPerimeter, txtArea, picCanvas);
        }

        public static FrmPentagono GetInstance()
        {
            if (mInstance == null || mInstance.IsDisposed)
            {
                mInstance = new FrmPentagono();
            }
            return mInstance;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            float moveStep = 5f;

            switch (keyData)
            {
                case Keys.Left:
                    obj.Translate(-moveStep, 0);
                    break;
                case Keys.Right:
                    obj.Translate(moveStep, 0);
                    break;
                case Keys.Up:
                    obj.Translate(0, -moveStep);
                    break;
                case Keys.Down:
                    obj.Translate(0, moveStep);
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

            picCanvas.Refresh();
            obj.PlotShape(picCanvas);
            return true;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            obj.ResetTransform();
            obj.ReadData(txtLength);
            obj.CalculatePerimeter();
            obj.CalculateArea();
            obj.PrintData(txtPerimeter, txtArea);
            picCanvas.Refresh();
            obj.PlotShape(picCanvas);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            obj.InitializeData(txtLength, txtPerimeter, txtArea, picCanvas);
            obj.ResetTransform();
            trbEscale.Value = 1;
            picCanvas.Refresh();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            obj.CloseForm(this);
        }

        private void btnIzq_Click(object sender, EventArgs e)
        {
            obj.Rotate(15);
            picCanvas.Refresh();
            obj.PlotShape(picCanvas);
        }

        private void btnDer_Click(object sender, EventArgs e)
        {
            obj.Rotate(-15);
            picCanvas.Refresh();
            obj.PlotShape(picCanvas);
        }

        private void EscalateEntry(object sender, EventArgs e)
        {
            obj.Escalate(trbEscale.Value / 2);
            obj.PlotShape(picCanvas);
        }
    }
}
