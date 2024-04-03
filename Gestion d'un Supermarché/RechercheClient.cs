using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class RechercheClient : Form
    {
        public RechercheClient()
        {
            InitializeComponent();
        }

        readonly Regex CIN = new Regex("^[A-Za-z]{2}\\d{1,6}$");
        readonly Regex NomPrenom = new Regex("^[A-Za-z]+$");
        readonly Regex Age = new Regex("^\\d{1,3}$");
        readonly Regex Telephone = new Regex("^0\\d{9}$");
        readonly Regex Ville = new Regex("^[A-Za-z]+(\\s[A-Za-z]+)*$");
        readonly Outils outils = new Outils();
        string txtCIN, txtNom, txtPrenom, txtAge, txtTelephone, txtVille;

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
                comboBox1.BackColor = Color.White;
                comboBox1.ForeColor = Color.Black;
                contextMenuStrip1.BackColor = Color.Black;
                effacerToutToolStripMenuItem.BackColor = Color.White;
                pictureBox1.Image = Properties.Resources.CINNoir;
                pictureBox2.Image = Properties.Resources.NomPrénomNoir;
                pictureBox3.Image = Properties.Resources.NomPrénomNoir;
                pictureBox4.Image = Properties.Resources.AgeNoir;
                pictureBox5.Image = Properties.Resources.SexeNoir;
                pictureBox6.Image = Properties.Resources.TéléphoneNoir;
                pictureBox9.Image = Properties.Resources.VilleNoir;
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
                comboBox1.BackColor = Color.Black;
                comboBox1.ForeColor = Color.White;
                contextMenuStrip1.BackColor = Color.White;
                effacerToutToolStripMenuItem.BackColor = Color.Black;
                pictureBox1.Image = Properties.Resources.CINBlanc;
                pictureBox2.Image = Properties.Resources.NomPrénomBlanc;
                pictureBox3.Image = Properties.Resources.NomPrénomBlanc;
                pictureBox4.Image = Properties.Resources.AgeBlanc;
                pictureBox5.Image = Properties.Resources.SexeBlanc;
                pictureBox6.Image = Properties.Resources.TéléphoneBlanc;
                pictureBox9.Image = Properties.Resources.VilleBlanc;
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
                }
            }
        }

        private void RechercheClient_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox) control.Enabled = false;
            }
            comboBox1.Enabled = false;
            ModeCS();
        }

        private void RechercheClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuRecherche"].Show();
        }

        private void RechercheClient_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = Width - (318 + 28);
            dataGridView1.Height = Height - (11 + 51);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtCIN = textBox1.Text.Trim().ToUpper(); 
            txtNom = textBox2.Text.Trim().ToUpper(); 
            txtPrenom = textBox3.Text.Trim().ToUpper(); 
            txtAge = textBox4.Text.Trim(); 
            txtTelephone = textBox5.Text.Trim(); 
            txtVille = textBox6.Text.Trim().ToUpper();
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked && !checkBox6.Checked && !checkBox7.Checked)
            {
                MessageBox.Show("Veuillez choisir au moins un critère.", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string requete = "SELECT * FROM Client WHERE";
                bool prequel = false, incorrect = false;
                if (checkBox1.Checked)
                {
                    if (string.IsNullOrEmpty(txtCIN) || !CIN.IsMatch(txtCIN))
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND CIN = '" + txtCIN + "'";
                        }
                        else
                        {
                            requete += " CIN = '" + txtCIN + "'";
                            prequel = true;
                        }
                    }
                }
                if (checkBox2.Checked)
                {
                    if (string.IsNullOrEmpty(txtNom) || !NomPrenom.IsMatch(txtNom))
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
                    if (string.IsNullOrEmpty(txtPrenom) || !NomPrenom.IsMatch(txtPrenom))
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND Prénom = '" + txtPrenom + "'";
                        }
                        else
                        {
                            requete += " Prénom = '" + txtPrenom + "'";
                            prequel = true;
                        }
                    }
                }
                if (checkBox4.Checked)
                {
                    if (string.IsNullOrEmpty(txtAge) || !Age.IsMatch(txtAge) || int.Parse(txtAge) <= 0)
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND Age = " + txtAge;
                        }
                        else
                        {
                            requete += " Age = " + txtAge;
                            prequel = true;
                        }
                    }
                }
                if (checkBox5.Checked)
                {
                    if (comboBox1.SelectedItem == null)
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND Sexe = '" + comboBox1.SelectedItem + "'";
                        }
                        else
                        {
                            requete += " Sexe = '" + comboBox1.SelectedItem + "'";
                            prequel = true;
                        }
                    }
                }
                if (checkBox6.Checked)
                {
                    if (string.IsNullOrEmpty(txtTelephone) || !Telephone.IsMatch(txtTelephone))
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND Téléphone = '" + txtTelephone + "'";
                        }
                        else
                        {
                            requete += " Téléphone = '" + txtTelephone + "'";
                            prequel = true;
                        }
                    }
                }
                if (checkBox7.Checked)
                {
                    if (string.IsNullOrEmpty(txtVille) || !Ville.IsMatch(txtVille))
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND Ville = '" + txtVille + "'";
                        }
                        else
                        {
                            requete += " Ville = '" + txtVille + "'";
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
                    }
                    else
                    {
                        MessageBox.Show("Client(s) introuvable(s).", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                textBox3.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
                textBox3.Clear();
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                textBox4.Enabled = true;
            }
            else
            {
                textBox4.Enabled = false;
                textBox4.Clear();
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedItem = null;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                textBox5.Enabled = true;
            }
            else
            {
                textBox5.Enabled = false;
                textBox5.Clear();
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
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
