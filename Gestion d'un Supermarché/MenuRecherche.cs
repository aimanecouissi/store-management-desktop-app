using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class MenuRecherche : Form
    {
        public MenuRecherche()
        {
            InitializeComponent();
        }

        RechercheClient crech;
        RechercheEmploye erech;
        RechercheProduit prech;
        RechercheCommande corech;

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                BackColor = Color.White;
                contextMenuStrip1.BackColor = Color.Black;
                quitterToolStripMenuItem.BackColor = Color.White;
                quitterToolStripMenuItem.ForeColor = Color.Black;
                pictureBox1.Image = Properties.Resources.ClientLeave;
                pictureBox2.Image = Properties.Resources.EmployéLeave;
                pictureBox3.Image = Properties.Resources.ProduitLeave;
                pictureBox4.Image = Properties.Resources.CommandeLeave;
                foreach (Control control in Controls)
                {
                    if (control is Label) control.ForeColor = Color.Black;
                }
                label1.ForeColor = Color.DimGray;
            }
            else
            {
                BackColor = Color.Black;
                contextMenuStrip1.BackColor = Color.White;
                quitterToolStripMenuItem.BackColor = Color.Black;
                quitterToolStripMenuItem.ForeColor = Color.White;
                pictureBox1.Image = Properties.Resources.ClientLeaveSombre;
                pictureBox2.Image = Properties.Resources.EmployéLeaveSombre;
                pictureBox3.Image = Properties.Resources.ProduitLeaveSombre;
                pictureBox4.Image = Properties.Resources.CommandeLeaveSombre;
                foreach (Control control in Controls)
                {
                    if (control is Label) control.ForeColor = Color.White;
                }
            }
        }

        private void MenuRecherche_Load(object sender, EventArgs e)
        {
            crech = new RechercheClient();
            erech = new RechercheEmploye();
            prech = new RechercheProduit();
            corech = new RechercheCommande();
            label1.Left = -label1.Width;
            ModeCS();
        }

        private void MenuRecherche_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuPrincipale"].Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            try
            {
                crech.Show();
            }
            catch
            {
                crech = new RechercheClient();
                crech.Show();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["Utilisateur"].ToString().Equals("admin"))
            {
                Hide();
                try
                {
                    erech.Show();
                }
                catch
                {
                    erech = new RechercheEmploye();
                    erech.Show();
                }
            }
            else
            {
                MessageBox.Show("Seul le directeur peut accéder à cette forme.", "Recherche des employés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Hide();
            try
            {
                prech.Show();
            }
            catch
            {
                prech = new RechercheProduit();
                prech.Show();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Hide();
            try
            {
                corech.Show();
            }
            catch
            {
                corech = new RechercheCommande();
                corech.Show();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Left = (label1.Left < Width) ? label1.Left + 1 : -label1.Width;
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox1.Image = Properties.Resources.ClientEnter;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.ClientEnterSombre;
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox1.Image = Properties.Resources.ClientLeave;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.ClientLeaveSombre;
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox2.Image = Properties.Resources.EmployéEnter;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.EmployéEnterSombre;
            }
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox2.Image = Properties.Resources.EmployéLeave;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.EmployéLeaveSombre;
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox3.Image = Properties.Resources.ProduitEnter;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.ProduitEnterSombre;
            }
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox3.Image = Properties.Resources.ProduitLeave;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.ProduitLeaveSombre;
            }
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox4.Image = Properties.Resources.CommandeEnter;
            }
            else
            {
                pictureBox4.Image = Properties.Resources.CommandeEnterSombre;
            }
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox4.Image = Properties.Resources.CommandeLeave;
            }
            else
            {
                pictureBox4.Image = Properties.Resources.CommandeLeaveSombre;
            }
        }
    }
}
