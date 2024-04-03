using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class MenuExportationType : Form
    {
        public MenuExportationType()
        {
            InitializeComponent();
        }

        readonly MenuExportationXML xml = new MenuExportationXML();
        readonly MenuExportationHTML html = new MenuExportationHTML();

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                contextMenuStrip1.BackColor = Color.Black;
                quitterToolStripMenuItem.BackColor = Color.White;
                quitterToolStripMenuItem.ForeColor = Color.Black;
                BackColor = Color.White;
                pictureBox1.Image = Properties.Resources.XMLLeave;
                pictureBox3.Image = Properties.Resources.HTMLLeave;
                foreach (Control control in Controls)
                {
                    if (control is Label) control.ForeColor = Color.Black;
                }
                label1.ForeColor = Color.DimGray;
            }
            else
            {
                contextMenuStrip1.BackColor = Color.White;
                quitterToolStripMenuItem.BackColor = Color.Black;
                quitterToolStripMenuItem.ForeColor = Color.White;
                BackColor = Color.Black;
                pictureBox1.Image = Properties.Resources.XMLLeaveSombre;
                pictureBox3.Image = Properties.Resources.HTMLLeaveSombre;
                foreach (Control control in Controls)
                {
                    if (control is Label) control.ForeColor = Color.White;
                }
            }
        }

        private void MenuExportationType_Load(object sender, EventArgs e)
        {
            label1.Left = -label1.Width;
            ModeCS();
        }

        private void MenuExportationType_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuPrincipale"].Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            xml.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Hide();
            html.ShowDialog();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Left = (label1.Left < Width) ? label1.Left + 1 : -label1.Width;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox1.Image = Properties.Resources.XMLEnter;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.XMLEnterSombre;
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox1.Image = Properties.Resources.XMLLeave;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.XMLLeaveSombre;
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox3.Image = Properties.Resources.HTMLEnter;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.HTMLEnterSombre;
            }
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox3.Image = Properties.Resources.HTMLLeave;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.HTMLLeaveSombre;
            }
        }
    }
}
