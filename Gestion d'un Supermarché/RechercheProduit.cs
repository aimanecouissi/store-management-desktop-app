using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class RechercheProduit : Form
    {
        public RechercheProduit()
        {
            InitializeComponent();
        }

        readonly Regex Code = new Regex("^\\d{12}$");
        readonly Regex Quantite = new Regex("^\\d{1,5}$");
        readonly Regex Prix = new Regex("^\\d+[\\.\\d+]*$");
        readonly Outils outils = new Outils();
        string txtCode, txtNom, txtQuantite, txtPrix;

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
                contextMenuStrip1.BackColor = Color.Black;
                effacerToutToolStripMenuItem.BackColor = Color.White;
                button1.ForeColor = Color.White;
                pictureBox1.Image = Properties.Resources.CodeNoir;
                pictureBox2.Image = Properties.Resources.NomPrénomNoir;
                pictureBox4.Image = Properties.Resources.CategorieNoir;
                pictureBox5.Image = Properties.Resources.MarqueNoir;
                pictureBox6.Image = Properties.Resources.QuantitéNoir;
                pictureBox9.Image = Properties.Resources.PrixNoir;
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
                contextMenuStrip1.BackColor = Color.White;
                effacerToutToolStripMenuItem.BackColor = Color.Black;
                button1.ForeColor = Color.Black;
                pictureBox1.Image = Properties.Resources.CodeBlanc;
                pictureBox2.Image = Properties.Resources.NomPrénomBlanc;
                pictureBox4.Image = Properties.Resources.CategorieBlanc;
                pictureBox5.Image = Properties.Resources.MarqueBlanc;
                pictureBox6.Image = Properties.Resources.QuantitéBlanc;
                pictureBox9.Image = Properties.Resources.PrixBlanc;
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

        private void ProduitRecherche_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox) control.Enabled = false;
                if (control is ComboBox) control.Enabled = false;
            }
            ModeCS();
        }

        private void ProduitRecherche_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuRecherche"].Show();
        }

        private void ProduitRecherche_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = Width - (318 + 28);
            dataGridView1.Height = Height - (11 + 51);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtCode = textBox1.Text.Trim(); 
            txtNom = textBox2.Text.Trim().ToUpper(); 
            txtQuantite = textBox5.Text.Trim(); 
            txtPrix = textBox6.Text.Trim();
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked && !checkBox6.Checked)
            {
                MessageBox.Show("Veuillez choisir au moins un critère.", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string requete = "SELECT * FROM Produit WHERE";
                bool prequel = false, incorrect = false;
                if (checkBox1.Checked)
                {
                    if (string.IsNullOrEmpty(txtCode) || !Code.IsMatch(txtCode))
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND Code = '" + txtCode + "'";
                        }
                        else
                        {
                            requete += " Code = '" + txtCode + "'";
                            prequel = true;
                        }
                    }
                }
                if (checkBox2.Checked)
                {
                    if (string.IsNullOrEmpty(txtNom))
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND Nom = '" + txtNom + "'";
                        }
                        else
                        {
                            requete += " Nom = '" + txtNom + "'";
                            prequel = true;
                        }
                    }
                }
                if (checkBox3.Checked)
                {
                    if (comboBox1.SelectedItem == null)
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND Categorie = '" + comboBox1.SelectedItem + "'";
                        }
                        else
                        {
                            requete += " Categorie = '" + comboBox1.SelectedItem + "'";
                            prequel = true;
                        }
                    }
                }
                if (checkBox4.Checked)
                {
                    if (comboBox2.SelectedItem == null)
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND Marque = '" + comboBox2.SelectedItem + "'";
                        }
                        else
                        {
                            requete += " Marque = '" + comboBox2.SelectedItem + "'";
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
                            requete += " AND QuantitéStock = " + txtQuantite;
                        }
                        else
                        {
                            requete += " QuantitéStock = " + txtQuantite;
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
                            requete += " AND Prix = " + txtPrix;
                        }
                        else
                        {
                            requete += " Prix = " + txtPrix;
                        }
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
                        string[] nc = { "Code", "Nom", "Categorie", "Marque", "Quantité en stock", "Prix" };
                        for (int i = 0; i < 6; i++)
                        {
                            dataGridView1.Columns[i].HeaderText = nc[i];
                        }
                    }
                    else
                    {
                        MessageBox.Show("Produit(s) introuvable(s).", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                textBox2.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
                textBox2.Clear();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedItem = null;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                comboBox2.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
                comboBox2.SelectedItem = null;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                textBox5.Enabled = true;
            }
            else
            {
                textBox5.Enabled = false;
                textBox5.Clear();
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                textBox6.Enabled = true;
            }
            else
            {
                textBox6.Enabled = false;
                textBox6.Clear();
            }
        }
    }
}
