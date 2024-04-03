using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class MenuParametres : Form
    {
        public MenuParametres()
        {
            InitializeComponent();
        }

        readonly Serveur serveur = new Serveur();
        readonly MotPasse mdp = new MotPasse();
        bool deconnecter;

        void ModeC()
        {
            contextMenuStrip1.BackColor = Color.Black;
            quitterToolStripMenuItem.BackColor = Color.White;
            quitterToolStripMenuItem.ForeColor = Color.Black;
            pictureBox1.Image = Properties.Resources.ServeurLeave;
            pictureBox2.Image = Properties.Resources.SombreLeave;
            pictureBox3.Image = Properties.Resources.MDPLeave;
            pictureBox4.Image = Properties.Resources.DéconnecterLeave;
            label4.Text = "Mode sombre";
            label4.Left = 226;
            BackColor = Color.White;
            foreach (Control control in Controls)
            {
                if (control is Label) control.ForeColor = Color.Black;
            }
            label1.ForeColor = Color.DimGray;
        }

        void ModeS()
        {
            contextMenuStrip1.BackColor = Color.White;
            quitterToolStripMenuItem.BackColor = Color.Black;
            quitterToolStripMenuItem.ForeColor = Color.White;
            pictureBox1.Image = Properties.Resources.ServeurLeaveSombre;
            pictureBox2.Image = Properties.Resources.ClairLeave;
            pictureBox3.Image = Properties.Resources.MDPLeaveSombre;
            pictureBox4.Image = Properties.Resources.DéconnecterLeaveSombre;
            label4.Text = "Mode clair";
            label4.Left = 237;
            BackColor = Color.Black;
            foreach (Control control in Controls)
            {
                if (control is Label) control.ForeColor = Color.White;
            }
        }

        private void MenuParametres_Load(object sender, EventArgs e)
        {
            label1.Left = -label1.Width;
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                ModeC();
            }
            else
            {
                ModeS();
            }
        }

        private void MenuParametres_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!deconnecter)
                Application.OpenForms["MenuPrincipale"].Show();
            else
            {
                deconnecter = false;
                Application.OpenForms["Connexion"].Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["Utilisateur"].ToString().Equals("admin"))
            {
                Hide();
                serveur.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seul le directeur peut accéder à cette forme.", "Exportation des employés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                Properties.Settings.Default["ModeSombre"] = true;
                Properties.Settings.Default.Save();
                ModeS();
            }
            else
            {
                Properties.Settings.Default["ModeSombre"] = false;
                Properties.Settings.Default.Save();
                ModeC();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Hide();
            mdp.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["SeRappeler"] = false;
            Properties.Settings.Default.Save();
            deconnecter = true;
            Close();
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
                pictureBox1.Image = Properties.Resources.ServeurEnter;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.ServeurEnterSombre;
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox1.Image = Properties.Resources.ServeurLeave;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.ServeurLeaveSombre;
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox2.Image = Properties.Resources.SombreEnter;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.ClairEnter;
            }
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox2.Image = Properties.Resources.SombreLeave;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.ClairLeave;
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox3.Image = Properties.Resources.MDPEnter;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.MDPEnterSombre;
            }
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox3.Image = Properties.Resources.MDPLeave;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.MDPLeaveSombre;
            }
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox4.Image = Properties.Resources.DéconnecterEnter;
            }
            else
            {
                pictureBox4.Image = Properties.Resources.DéconnecterEnterSombre;
            }
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox4.Image = Properties.Resources.DéconnecterLeave;
            }
            else
            {
                pictureBox4.Image = Properties.Resources.DéconnecterLeaveSombre;
            }
        }
    }
}
