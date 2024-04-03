using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class MenuExportationXML : Form
    {
        public MenuExportationXML()
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

        private void MenuExportationXML_Load(object sender, EventArgs e)
        {
            label1.Left = -label1.Width;
            ModeCS();
        }

        private void MenuExportationXML_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuExportationType"].Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (outils.Remplie("SELECT * FROM Client"))
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    outils.ActualiserDataSet("Client");
                    outils.ds.Tables["Client"].WriteXml(folderBrowserDialog1.SelectedPath + "\\Liste des Clients.xml");
                    outils.ds.Tables["Client"].WriteXmlSchema(folderBrowserDialog1.SelectedPath + "\\Liste des Clients.xsd");
                    MessageBox.Show("Client(s) exporté(s) avec succès!", "Exportation XML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Rien à exporter.", "Exportation XML", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (outils.Remplie("SELECT * FROM Employé"))
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    outils.ActualiserDataSet("Employé");
                    outils.ds.Tables["Employé"].WriteXml(folderBrowserDialog1.SelectedPath + "\\Liste des Employés.xml");
                    outils.ds.Tables["Employé"].WriteXmlSchema(folderBrowserDialog1.SelectedPath + "\\Liste des Employés.xsd");
                    MessageBox.Show("Employé(s) exporté(s) avec succès!", "Exportation XML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Rien à exporter.", "Exportation XML", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (outils.Remplie("SELECT * FROM Produit"))
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    outils.ActualiserDataSet("Produit");
                    outils.ds.Tables["Produit"].WriteXml(folderBrowserDialog1.SelectedPath + "\\Liste des Produits.xml");
                    outils.ds.Tables["Produit"].WriteXmlSchema(folderBrowserDialog1.SelectedPath + "\\Liste des Produits.xsd");
                    MessageBox.Show("Produit(s) exporté(s) avec succès!", "Exportation XML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Rien à exporter.", "Exportation XML", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (outils.Remplie("SELECT * FROM Commande"))
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    outils.ActualiserDataSet("Commande");
                    outils.ds.Tables["Commande"].WriteXml(folderBrowserDialog1.SelectedPath + "\\Liste des Commandes.xml");
                    outils.ds.Tables["Commande"].WriteXmlSchema(folderBrowserDialog1.SelectedPath + "\\Liste des Commandes.xsd");
                    MessageBox.Show("Commande(s) exporté(s) avec succès!", "Exportation XML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Rien à exporter.", "Exportation XML", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
