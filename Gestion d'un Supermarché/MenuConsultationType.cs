using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class MenuConsultationType : Form
    {
        public MenuConsultationType()
        {
            InitializeComponent();
        }

        readonly MenuConsultationTableau cons = new MenuConsultationTableau();
        readonly Graphique graphique = new Graphique();

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                contextMenuStrip1.BackColor = Color.Black;
                quitterToolStripMenuItem.BackColor = Color.White;
                quitterToolStripMenuItem.ForeColor = Color.Black;
                BackColor = Color.White;
                pictureBox1.Image = Properties.Resources.TableauLeave;
                pictureBox2.Image = Properties.Resources.GraphiqueLeave;
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
                pictureBox1.Image = Properties.Resources.TableauLeaveSombre;
                pictureBox2.Image = Properties.Resources.GraphiqueLeaveSombre;
                foreach (Control control in Controls)
                {
                    if (control is Label) control.ForeColor = Color.White;
                }
            }
        }

        private void MenuConsultationType_Load(object sender, EventArgs e)
        {
            label1.Left = -label1.Width;
            ModeCS();
        }

        private void MenuConsultationType_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuPrincipale"].Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            cons.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
            graphique.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Left = (label1.Left < Width) ? label1.Left + 1 : -label1.Width;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox1.Image = Properties.Resources.TableauEnter;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.TableauEnterSombre;
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox1.Image = Properties.Resources.TableauLeave;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.TableauLeaveSombre;
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox2.Image = Properties.Resources.GraphiqueEnter;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.GraphiqueEnterSombre;
            }
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox2.Image = Properties.Resources.GraphiqueLeave;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.GraphiqueLeaveSombre;
            }
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
