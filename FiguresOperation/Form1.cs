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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void romboToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRombo frm = FrmRombo.GetInstance();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(0, 0);
            frm.WindowState = FormWindowState.Maximized;
            frm.BringToFront();
            frm.Show();
        }

        private void pentagonoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPentagono frm = FrmPentagono.GetInstance();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(0, 0);
            frm.WindowState = FormWindowState.Maximized;
            frm.BringToFront();
            frm.Show();
        }
    }
}
