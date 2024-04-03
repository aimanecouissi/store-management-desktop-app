using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class MenuExportationHTML : Form
    {
        public MenuExportationHTML()
        {
            InitializeComponent();
        }

        readonly Outils outils = new Outils();

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                contextMenuStrip1.BackColor = Color.Black;
                quitterToolStripMenuItem.BackColor = Color.White;
                quitterToolStripMenuItem.ForeColor = Color.Black;
                BackColor = Color.White;
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
                contextMenuStrip1.BackColor = Color.White;
                quitterToolStripMenuItem.BackColor = Color.Black;
                quitterToolStripMenuItem.ForeColor = Color.White;
                BackColor = Color.Black;
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

        private void MenuExportationHTML_Load(object sender, EventArgs e)
        {
            label1.Left = -label1.Width;
            ModeCS();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Left = (label1.Left < Width) ? label1.Left + 1 : -label1.Width;
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuExportationHTML_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuExportationType"].Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (outils.Remplie("SELECT * FROM Client"))
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    outils.ExporterHTML("SELECT * FROM Client", folderBrowserDialog1.SelectedPath + "\\Liste des clients.html", "CLIENTS", "#ffbe0b", "black");
                    MessageBox.Show("Client(s) exporté(s) avec succès!", "Exportation HTML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Rien à exporter.", "Exportation HTML", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (outils.Remplie("SELECT * FROM Employé"))
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    outils.ExporterHTML("SELECT * FROM Employé", folderBrowserDialog1.SelectedPath + "\\Liste des employé.html", "EMPLOYÉS", "#fb5607");
                    MessageBox.Show("Employé(s) exporté(s) avec succès!", "Exportation HTML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Rien à exporter.", "Exportation HTML", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (outils.Remplie("SELECT * FROM Produit"))
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    outils.ExporterHTML("SELECT * FROM Produit", folderBrowserDialog1.SelectedPath + "\\Liste des produit.html", "PRODUITS", "#8338ec");
                    MessageBox.Show("Produit(s) exporté(s) avec succès!", "Exportation HTML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Rien à exporter.", "Exportation HTML", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (outils.Remplie("SELECT * FROM Commande"))
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    outils.ExporterHTML("SELECT * FROM Commande", folderBrowserDialog1.SelectedPath + "\\Liste des commande.html", "COMMANDES", "#3a86ff");
                    MessageBox.Show("Commande(s) exporté(s) avec succès!", "Exportation HTML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Rien à exporter.", "Exportation HTML", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
