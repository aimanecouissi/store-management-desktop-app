using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class RechercheCommande : Form
    {
        public RechercheCommande()
        {
            InitializeComponent();
            string[] nt = { "Client", "Employé", "Produit" };
            for (int i = 0; i < 3; i++)
            {
                RemplirComboBox(nt[i], i + 1);
            }
        }

        readonly Regex Numero = new Regex("^\\d+$");
        readonly Regex Quantite = new Regex("^\\d{1,5}");
        readonly Regex Prix = new Regex("^\\d+[\\.\\d+]*$");
        readonly Outils outils = new Outils();
        string txtNumero, txtQuantite, txtPrix;

        void RemplirComboBox(string NomTable, int NumeroComboBox)
        {
            outils.cn.Open();
            outils.cm = new SqlCommand("SELECT * FROM " + NomTable, outils.cn);
            outils.dr = outils.cm.ExecuteReader();
            while (outils.dr.Read())
            {
                if (NumeroComboBox.Equals(1))
                    comboBox1.Items.Add(outils.dr[0].ToString());
                if (NumeroComboBox.Equals(2))
                    comboBox2.Items.Add(outils.dr[0].ToString());
                if (NumeroComboBox.Equals(3))
                    comboBox3.Items.Add(outils.dr[0].ToString());
            }
            outils.dr.Close();
            outils.cn.Close();
        }

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                BackColor = Color.White;
                dataGridView1.BackgroundColor = Color.White;
                dataGridView1.DefaultCellStyle.BackColor = Color.White;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Black;
                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
                button1.ForeColor = Color.White;
                contextMenuStrip1.BackColor = Color.Black;
                effacerToutToolStripMenuItem.BackColor = Color.White;
                pictureBox1.Image = Properties.Resources.NuméroNoir;
                pictureBox2.Image = Properties.Resources.DateNoir;
                pictureBox3.Image = Properties.Resources.CodeNoir;
                pictureBox4.Image = Properties.Resources.PrixNoir;
                pictureBox5.Image = Properties.Resources.QuantitéNoir;
                pictureBox6.Image = Properties.Resources.CINNoir;
                pictureBox9.Image = Properties.Resources.IDNoir;
                foreach (Control control in Controls)
                {
                    if (control is TextBox)
                    {
                        control.BackColor = Color.White;
                        control.ForeColor = Color.Black;
                    }
                    if (control is Label)
                    {
                        control.ForeColor = Color.Black;
                    }
                    if (control is ComboBox)
                    {
                        control.BackColor = Color.White;
                        control.ForeColor = Color.Black;
                    }
                }
            }
            else
            {
                BackColor = Color.Black;
                dataGridView1.BackgroundColor = Color.Black;
                dataGridView1.DefaultCellStyle.BackColor = Color.Black;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
                dataGridView1.DefaultCellStyle.ForeColor = Color.White;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
                button1.ForeColor = Color.Black;
                contextMenuStrip1.BackColor = Color.White;
                effacerToutToolStripMenuItem.BackColor = Color.Black;
                pictureBox1.Image = Properties.Resources.NuméroBlanc;
                pictureBox2.Image = Properties.Resources.DateBlanc;
                pictureBox3.Image = Properties.Resources.CodeBlanc;
                pictureBox4.Image = Properties.Resources.PrixBlanc;
                pictureBox5.Image = Properties.Resources.QuantitéBlanc;
                pictureBox6.Image = Properties.Resources.CINBlanc;
                pictureBox9.Image = Properties.Resources.IDBlanc;
                foreach (Control control in Controls)
                {
                    if (control is TextBox)
                    {
                        control.BackColor = Color.FromArgb(31, 31, 31);
                        control.ForeColor = Color.White;
                    }
                    if (control is Label)
                    {
                        control.ForeColor = Color.White;
                    }
                    if (control is ComboBox)
                    {
                        control.BackColor = Color.FromArgb(31, 31, 31);
                        control.ForeColor = Color.White;
                    }
                }
            }
        }

        private void RechercheCommande_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox) control.Enabled = false;
                if (control is ComboBox box) box.Enabled = false;
            }
            dateTimePicker1.Enabled = false;
            ModeCS();
        }

        private void RechercheCommande_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuRecherche"].Show();
        }

        private void RechercheCommande_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = Width - (318 + 28);
            dataGridView1.Height = Height - (11 + 51);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtNumero = textBox1.Text.Trim(); 
            txtQuantite = textBox2.Text.Trim(); 
            txtPrix = textBox4.Text.Trim();
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked && !checkBox6.Checked && !checkBox7.Checked)
            {
                MessageBox.Show("Veuillez choisir au moins un critère.", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string requete = "SELECT * FROM Commande WHERE";
                bool prequel = false, incorrect = false;
                if (checkBox1.Checked)
                {
                    if (string.IsNullOrEmpty(txtNumero) || !Numero.IsMatch(txtNumero))
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND Numero = " + txtNumero;
                        }
                        else
                        {
                            requete += " Numero = " + txtNumero;
                            prequel = true;
                        }
                    }
                }
                if (checkBox2.Checked)
                {
                    if (comboBox1.SelectedItem == null)
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND CIN = '" + comboBox1.SelectedItem + "'";
                        }
                        else
                        {
                            requete += " CIN = '" + comboBox1.SelectedItem + "'";
                            prequel = true;
                        }
                    }
                }
                if (checkBox3.Checked)
                {
                    if (comboBox2.SelectedItem == null)
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND ID = " + comboBox2.SelectedItem;
                        }
                        else
                        {
                            requete += " ID = " + comboBox2.SelectedItem;
                            prequel = true;
                        }
                    }
                }
                if (checkBox4.Checked)
                {
                    if (comboBox3.SelectedItem == null)
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND Numero IN (SELECT Numero FROM Details_Commande WHERE Code = '" + comboBox3.SelectedItem + "')";
                        }
                        else
                        {
                            requete += " Numero IN (SELECT Numero FROM Details_Commande WHERE Code = '" + comboBox3.SelectedItem + "')";
                            prequel = true;
                        }
                    }
                }
                if (checkBox5.Checked)
                {
                    if (string.IsNullOrEmpty(txtQuantite) || !Quantite.IsMatch(txtQuantite) || int.Parse(txtQuantite) <= 0)
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND Numero IN (SELECT Numero FROM Details_Commande WHERE Quantité = " + txtQuantite + ")";
                        }
                        else
                        {
                            requete += " Numero IN (SELECT Numero FROM Details_Commande WHERE Quantité = " + txtQuantite + ")";
                            prequel = true;
                        }
                    }
                }
                if (checkBox6.Checked)
                {
                    if (string.IsNullOrEmpty(txtPrix) || !Prix.IsMatch(txtPrix) || float.Parse(txtPrix) <= 0)
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND PrixTotal = " + txtPrix;
                        }
                        else
                        {
                            requete += " PrixTotal = " + txtPrix;
                            prequel = true;
                        }
                    }
                }
                if (checkBox7.Checked)
                {
                    if (prequel)
                    {
                        requete += " AND DateCommande = '" + dateTimePicker1.Text + "'";
                    }
                    else
                    {
                        requete += " DateCommande = '" + dateTimePicker1.Text + "'";
                    }
                }
                if (incorrect)
                {
                    MessageBox.Show("Veuillez saisir les champs en format correcte.", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (outils.Remplie(requete))
                    {
                        dataGridView1.DataSource = outils.RemplirDataGridView(requete);
                        string[] nc = { "Numéro", "CIN du client", "ID d'employé", "Prix total", "Date de la commande" };
                        for (int i = 0; i < 5; i++)
                        {
                            dataGridView1.Columns[i].HeaderText = nc[i];
                        }
                    }
                    else
                    {
                        MessageBox.Show("Commande(s) introuvable(s).", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void effacerToutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is CheckBox box) box.Checked = false;
            }
            dataGridView1.DataSource = null;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                textBox1.Clear();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedItem = null;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                comboBox2.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
                comboBox2.SelectedItem = null;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                comboBox3.Enabled = true;
            }
            else
            {
                comboBox3.Enabled = false;
                comboBox3.SelectedItem = null;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                textBox2.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
                textBox2.Clear();
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                textBox4.Enabled = true;
            }
            else
            {
                textBox4.Enabled = false;
                textBox4.Clear();
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                dateTimePicker1.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker1.Value = DateTime.Now;
            }
        }
    }
}
