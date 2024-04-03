using System;
using System.Drawing;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class RechercheEmploye : Form
    {
        public RechercheEmploye()
        {
            InitializeComponent();
        }

        readonly Regex ID = new Regex("^\\d{1,4}$");
        readonly Regex NomPrenom = new Regex("^[A-Za-z]+$");
        readonly Regex Age = new Regex("^\\d{1,3}$");
        readonly Regex Telephone = new Regex("^0\\d{9}$");
        readonly Outils outils = new Outils();
        string txtID, txtNom, txtPrenom, txtAge, txtTelephone, txtEmail, txtMDP;

        bool Email(string Email)
        {
            try
            {
                MailAddress ma = new MailAddress(Email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
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
                comboBox1.BackColor = Color.White;
                comboBox1.ForeColor = Color.Black;
                contextMenuStrip1.BackColor = Color.Black;
                effacerToutToolStripMenuItem.BackColor = Color.White;
                button1.ForeColor = Color.White;
                pictureBox1.Image = Properties.Resources.IDNoir;
                pictureBox2.Image = Properties.Resources.NomPrénomNoir;
                pictureBox3.Image = Properties.Resources.NomPrénomNoir;
                pictureBox4.Image = Properties.Resources.AgeNoir;
                pictureBox5.Image = Properties.Resources.SexeNoir;
                pictureBox6.Image = Properties.Resources.TéléphoneNoir;
                pictureBox7.Image = Properties.Resources.MDPNoir;
                pictureBox9.Image = Properties.Resources.EmailNoir;
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
                button1.ForeColor = Color.Black;
                pictureBox1.Image = Properties.Resources.IDBlanc;
                pictureBox2.Image = Properties.Resources.NomPrénomBlanc;
                pictureBox3.Image = Properties.Resources.NomPrénomBlanc;
                pictureBox4.Image = Properties.Resources.AgeBlanc;
                pictureBox5.Image = Properties.Resources.SexeBlanc;
                pictureBox6.Image = Properties.Resources.TéléphoneBlanc;
                pictureBox7.Image = Properties.Resources.MDPBlanc;
                pictureBox9.Image = Properties.Resources.EmailBlanc;
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

        private void RechercheEmploye_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox) control.Enabled = false;
            }
            comboBox1.Enabled = false;
            ModeCS();
        }

        private void RechercheEmploye_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuRecherche"].Show();
        }

        private void RechercheEmploye_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = Width - (318 + 28);
            dataGridView1.Height = Height - (11 + 51);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtID = textBox1.Text.Trim(); 
            txtNom = textBox2.Text.Trim().ToUpper(); 
            txtPrenom = textBox3.Text.Trim().ToUpper(); 
            txtAge = textBox4.Text.Trim(); 
            txtTelephone = textBox5.Text.Trim(); 
            txtEmail = textBox6.Text.Trim(); 
            txtMDP = textBox7.Text.Trim();
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked && !checkBox6.Checked && !checkBox7.Checked && !checkBox8.Checked)
            {
                MessageBox.Show("Veuillez choisir au moins un critère.", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string requete = "SELECT * FROM Employé WHERE";
                bool prequel = false, incorrect = false;
                if (checkBox1.Checked)
                {
                    if (string.IsNullOrEmpty(txtID) || !ID.IsMatch(txtID))
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND ID = " + outils.NiceID(txtID);
                        }
                        else
                        {
                            requete += " ID = " + outils.NiceID(txtID);
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
                    if (string.IsNullOrEmpty(txtEmail) || !Email(txtEmail))
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND Email = '" + txtEmail + "'";
                        }
                        else
                        {
                            requete += " Email = '" + txtEmail + "'";
                            prequel = true;
                        }
                    }
                }
                if (checkBox8.Checked)
                {
                    if (string.IsNullOrEmpty(txtMDP))
                    {
                        incorrect = true;
                    }
                    else
                    {
                        if (prequel)
                        {
                            requete += " AND MotPasse = '" + txtMDP + "'";
                        }
                        else
                        {
                            requete += " MotPasse = '" + txtMDP + "'";
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
                        string[] nc = { "ID", "Nom", "Prénom", "Age", "Sexe", "Téléphone", "Email", "Mot de passe" };
                        for (int i = 0; i < 8; i++)
                        {
                            dataGridView1.Columns[i].HeaderText = nc[i];
                        }
                    }
                    else
                    {
                        MessageBox.Show("Employé(s) introuvable(s).", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void effacerToutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            foreach (Control control in Controls)
            {
                if (control is CheckBox box) box.Checked = false;
            }
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

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                textBox7.Enabled = true;
            }
            else
            {
                textBox7.Enabled = false;
                textBox7.Clear();
            }
        }
    }
}
