using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class MenuMaj : Form
    {
        public MenuMaj()
        {
            InitializeComponent();
        }

        MajClient mclt;
        MajEmploye memp;
        MajProduit mprd;
        MajCommande mcmd;
        readonly Outils outils = new Outils();

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                contextMenuStrip1.BackColor = Color.Black;
                toolStripMenuItem1.BackColor = Color.White;
                toolStripMenuItem1.ForeColor = Color.Black;
                sUPPRIMERTOUTLESEMPLOYESToolStripMenuItem.BackColor = Color.White;
                sUPPRIMERTOUTLESEMPLOYESToolStripMenuItem.ForeColor = Color.Black;
                sUPPRIMERTOUTLESPRODUITSToolStripMenuItem.BackColor = Color.White;
                sUPPRIMERTOUTLESPRODUITSToolStripMenuItem.ForeColor = Color.Black;
                sUPPRIMERTOUTLESCOMMANDESToolStripMenuItem.BackColor = Color.White;
                sUPPRIMERTOUTLESCOMMANDESToolStripMenuItem.ForeColor = Color.Black;
                qUITTERToolStripMenuItem.BackColor = Color.White;
                qUITTERToolStripMenuItem.ForeColor = Color.Black;
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
                toolStripMenuItem1.BackColor = Color.Black;
                toolStripMenuItem1.ForeColor = Color.White;
                sUPPRIMERTOUTLESEMPLOYESToolStripMenuItem.BackColor = Color.Black;
                sUPPRIMERTOUTLESEMPLOYESToolStripMenuItem.ForeColor = Color.White;
                sUPPRIMERTOUTLESPRODUITSToolStripMenuItem.BackColor = Color.Black;
                sUPPRIMERTOUTLESPRODUITSToolStripMenuItem.ForeColor = Color.White;
                sUPPRIMERTOUTLESCOMMANDESToolStripMenuItem.BackColor = Color.Black;
                sUPPRIMERTOUTLESCOMMANDESToolStripMenuItem.ForeColor = Color.White;
                qUITTERToolStripMenuItem.BackColor = Color.Black;
                qUITTERToolStripMenuItem.ForeColor = Color.White;
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

        private void MenuMaj_Load(object sender, EventArgs e)
        {
            mclt = new MajClient();
            memp = new MajEmploye();
            mprd = new MajProduit();
            mcmd = new MajCommande();
            label1.Left = -label1.Width;
            ModeCS();
        }

        private void MenuMaj_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuPrincipale"].Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            try
            {
                mclt.Show();
            }
            catch
            {
                mclt = new MajClient();
                mclt.Show();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["Utilisateur"].ToString().Equals("admin"))
            {
                Hide();
                try
                {
                    memp.Show();
                }
                catch
                {
                    memp = new MajEmploye();
                    memp.Show();
                }
            }
            else
            {
                MessageBox.Show("Seul le directeur peut accéder à cette forme.", "Màj d'un employé", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Hide();
            try
            {
                mprd.Show();
            }
            catch
            {
                mprd = new MajProduit();
                mprd.Show();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Hide();
            try
            {
                mcmd.Show();
            }
            catch
            {
                mcmd = new MajCommande();
                mcmd.Show();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (outils.Remplie("SELECT * FROM Client"))
            {
                if (outils.Remplie("SELECT * FROM Commande"))
                {
                    MessageBox.Show("Impossible de supprimer les clients, car un ou plusieurs clients existent dans la liste des commandes.", "Supprimer tout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (MessageBox.Show("Voulez-vous vraiment supprimer ces clients?", "Supprimer tout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        outils.ExecuterRequete("DELETE FROM Client");
                        MessageBox.Show("Tout les clients ont été supprimer avec succès.", "Supprimer tout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucun client trouvé.", "Supprimer tout", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void sUPPRIMERTOUTLESEMPLOYESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (outils.Remplie("SELECT * FROM Employé"))
            {
                if (outils.Remplie("SELECT * FROM Commande"))
                {
                    MessageBox.Show("Impossible de supprimer les employés, car un ou plusieurs employés existent dans la liste des commandes.", "Supprimer tout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (MessageBox.Show("Voulez-vous vraiment supprimer ces employés?", "Supprimer tout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        outils.ExecuterRequete("DELETE FROM Employé");
                        MessageBox.Show("Tout les employés ont été supprimer avec succès.", "Supprimer tout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucun employé trouvé.", "Supprimer tout", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void sUPPRIMERTOUTLESPRODUITSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (outils.Remplie("SELECT * FROM Produit"))
            {
                if (outils.Remplie("SELECT * FROM Commande"))
                {
                    MessageBox.Show("Impossible de supprimer les produits, car un ou plusieurs produits existent dans la liste des commandes.", "Supprimer tout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (MessageBox.Show("Voulez-vous vraiment supprimer ces produits?", "Supprimer tout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        outils.ExecuterRequete("DELETE FROM Produit");
                        MessageBox.Show("Tout les produits ont été supprimer avec succès.", "Supprimer tout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucun produit trouvé.", "Supprimer tout", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void sUPPRIMERTOUTLESCOMMANDESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (outils.Remplie("SELECT * FROM Commande"))
            {
                if (MessageBox.Show("Voulez-vous vraiment supprimer ces commandes?", "Supprimer tout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    outils.ExecuterRequete("DELETE FROM Details_Commande");
                    outils.ExecuterRequete("DELETE FROM Commande");
                    MessageBox.Show("Tout les commandes ont été supprimer avec succès.", "Supprimer tout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Aucune commande trouvé.", "Supprimer tout", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void qUITTERToolStripMenuItem_Click(object sender, EventArgs e)
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
