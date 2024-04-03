using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class MajEmploye : Form
    {
        public MajEmploye()
        {
            InitializeComponent();
        }

        readonly Regex ID = new Regex("^\\d{1,4}$");
        readonly Regex NomPrenom = new Regex("^[A-Za-z]+$");
        readonly Regex Age = new Regex("^\\d{1,3}$");
        readonly Regex Telephone = new Regex("^0\\d{9}$");
        readonly Outils outils = new Outils();
        string txtID, txtNom, txtPrenom, txtAge, txtTelephone, txtEmail, txtMPD;

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

        void Effacer()
        {
            string temp = outils.NiceID(textBox1.Text.Trim());
            foreach (Control control in Controls)
            {
                if (control is TextBox box) box.Clear();
            }
            comboBox1.SelectedItem = null;
            LabelCentre("-");
            textBox1.Text = temp;
            textBox1.Select();
        }

        void Afficher(List<int> Position)
        {
            textBox1.Text = outils.ds.Tables["Employé"].Rows[Position[0]][0].ToString();
            textBox2.Text = outils.ds.Tables["Employé"].Rows[Position[0]][1].ToString();
            textBox3.Text = outils.ds.Tables["Employé"].Rows[Position[0]][2].ToString();
            textBox4.Text = outils.ds.Tables["Employé"].Rows[Position[0]][3].ToString();
            comboBox1.SelectedItem = outils.ds.Tables["Employé"].Rows[Position[0]][4].ToString();
            textBox5.Text = outils.ds.Tables["Employé"].Rows[Position[0]][5].ToString();
            textBox6.Text = outils.ds.Tables["Employé"].Rows[Position[0]][6].ToString();
            textBox7.Text = outils.ds.Tables["Employé"].Rows[Position[0]][7].ToString();
        }

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                BackColor = Color.White;
                comboBox1.BackColor = Color.White;
                comboBox1.ForeColor = Color.Black;
                pictureBox1.Image = Properties.Resources.IDNoir;
                pictureBox2.Image = Properties.Resources.NomPrénomNoir;
                pictureBox3.Image = Properties.Resources.NomPrénomNoir;
                pictureBox4.Image = Properties.Resources.AgeNoir;
                pictureBox5.Image = Properties.Resources.SexeNoir;
                pictureBox6.Image = Properties.Resources.TéléphoneNoir;
                pictureBox7.Image = Properties.Resources.MDPNoir;
                pictureBox9.Image = Properties.Resources.EmailNoir;
                btnAjouter.Image = Properties.Resources.AjouterLeave;
                btnRechercher.Image = Properties.Resources.RechercherLeave;
                btnModifier.Image = Properties.Resources.ModifierLeave;
                btnSupprimer.Image = Properties.Resources.SupprimerLeave;
                btnEffacer.Image = Properties.Resources.EffacerLeave;
                btnPremier.Image = Properties.Resources.PremierLeave;
                btnPrecedent.Image = Properties.Resources.PrécédentLeave;
                btnSuivant.Image = Properties.Resources.SuivantLeave;
                btnDernier.Image = Properties.Resources.DernierLeave;
                btnAuto.Image = Properties.Resources.AutoNoir;
                foreach (Control control in Controls)
                {
                    if (control is Label) control.ForeColor = Color.Black;
                    if (control is TextBox)
                    {
                        control.ForeColor = Color.Black;
                        control.BackColor = Color.White;
                    }
                }
            }
            else
            {
                BackColor = Color.Black;
                comboBox1.BackColor = Color.Black;
                comboBox1.ForeColor = Color.White;
                pictureBox1.Image = Properties.Resources.IDBlanc;
                pictureBox2.Image = Properties.Resources.NomPrénomBlanc;
                pictureBox3.Image = Properties.Resources.NomPrénomBlanc;
                pictureBox4.Image = Properties.Resources.AgeBlanc;
                pictureBox5.Image = Properties.Resources.SexeBlanc;
                pictureBox6.Image = Properties.Resources.TéléphoneBlanc;
                pictureBox7.Image = Properties.Resources.MDPBlanc;
                pictureBox9.Image = Properties.Resources.EmailBlanc;
                btnAjouter.Image = Properties.Resources.AjouterLeaveSombre;
                btnRechercher.Image = Properties.Resources.RechercherLeaveSombre;
                btnModifier.Image = Properties.Resources.ModifierLeaveSombre;
                btnSupprimer.Image = Properties.Resources.SupprimerLeaveSombre;
                btnEffacer.Image = Properties.Resources.EffacerLeaveSombre;
                btnPremier.Image = Properties.Resources.PremierLeaveSombre;
                btnPrecedent.Image = Properties.Resources.PrécédentLeaveSombre;
                btnSuivant.Image = Properties.Resources.SuivantLeaveSombre;
                btnDernier.Image = Properties.Resources.DernierLeaveSombre;
                btnAuto.Image = Properties.Resources.AutoBlanc;
                foreach (Control control in Controls)
                {
                    if (control is Label) control.ForeColor = Color.White;
                    if (control is TextBox)
                    {
                        control.ForeColor = Color.White;
                        control.BackColor = Color.FromArgb(31, 31, 31);
                    }
                }
            }
        }

        void LabelCentre(string Texte)
        {
            label2.Text = Texte;
            label2.Left = Width / 2 - (label2.Width / 2) - 6;
        }

        private void MajEmploye_Load(object sender, EventArgs e)
        {
            LabelCentre("-");
            ModeCS();
        }

        private void MajEmploye_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                    if (!string.IsNullOrEmpty(control.Text))
                    {
                        if (MessageBox.Show("Voulez-vous vraiment fermer cette fenêtre?", "Màj d'un employé", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            e.Cancel = true;
                            break;
                        }
                        else
                        {
                            Application.OpenForms["MenuMaj"].Show();
                            break;
                        }
                    }
                    else
                    {
                        Application.OpenForms["MenuMaj"].Show();
                        break;
                    }
            }
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            txtID = textBox1.Text.Trim();
            txtNom = textBox2.Text.Trim().ToUpper();
            txtPrenom = textBox3.Text.Trim().ToUpper();
            txtAge = textBox4.Text.Trim();
            txtTelephone = textBox5.Text.Trim();
            txtEmail = textBox6.Text.Trim();
            txtMPD = textBox7.Text.Trim();
            if (string.IsNullOrEmpty(txtID) || string.IsNullOrEmpty(txtNom) || string.IsNullOrEmpty(txtPrenom) || string.IsNullOrEmpty(txtAge) || string.IsNullOrEmpty(txtTelephone) || string.IsNullOrEmpty(txtEmail) || string.IsNullOrEmpty(txtMPD))
            {
                MessageBox.Show("Veuillez remplir tout les champs.", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                foreach (Control control in Controls)
                {
                    if (!(control is TextBox)) continue;
                    if (!string.IsNullOrEmpty(control.Text.Trim())) continue;
                    control.Select();
                }
            }
            else
            {
                if (ID.IsMatch(txtID) && NomPrenom.IsMatch(txtNom) && NomPrenom.IsMatch(txtPrenom) && Age.IsMatch(txtAge) && Telephone.IsMatch(txtTelephone) && Email(txtEmail) && comboBox1.SelectedItem != null && int.Parse(txtAge) > 0)
                {
                    if (outils.Remplie("SELECT * FROM Employé WHERE ID = '" + outils.NiceID(txtID) + "'"))
                    {
                        textBox1.Select();
                        MessageBox.Show("Cet employé existe déjà.", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        outils.ExecuterRequete("INSERT INTO Employé VALUES ('" + outils.NiceID(txtID) + "','" + txtNom + "','" + txtPrenom + "'," + txtAge + ",'" + comboBox1.SelectedItem + "','" + txtTelephone + "','" + txtEmail + "','" + txtMPD + "')");
                        LabelCentre((outils.Position("Employé", txtID)[0] + 1).ToString());
                        MessageBox.Show("Employé ajouté avec succès.", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez remplir les champs en format correcte.", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            txtID = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(txtID) || !ID.IsMatch(txtID))
            {
                Effacer();
                MessageBox.Show("Veuillez saisir l'ID ou entrer le en format correcte.", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                outils.cm = new SqlCommand("SELECT * FROM Employé WHERE ID = '" + outils.NiceID(txtID) + "'", outils.cn);
                outils.cn.Open();
                outils.dr = outils.cm.ExecuteReader();
                if (outils.dr.Read())
                {
                    textBox2.Text = outils.dr[1].ToString();
                    textBox3.Text = outils.dr[2].ToString();
                    textBox4.Text = outils.dr[3].ToString();
                    comboBox1.SelectedItem = outils.dr[4].ToString();
                    textBox5.Text = outils.dr[5].ToString();
                    textBox6.Text = outils.dr[6].ToString();
                    textBox6.Text = outils.dr[7].ToString();
                    outils.dr.Close();
                    outils.cn.Close();
                    LabelCentre((outils.Position("Employé", outils.NiceID(txtID))[0] + 1).ToString());
                }
                else
                {
                    outils.dr.Close();
                    outils.cn.Close();
                    Effacer(); MessageBox.Show("Cet employé n'existe pas.", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            txtID = textBox1.Text.Trim();
            txtNom = textBox2.Text.Trim().ToUpper();
            txtPrenom = textBox3.Text.Trim().ToUpper();
            txtAge = textBox4.Text.Trim();
            txtTelephone = textBox5.Text.Trim();
            txtEmail = textBox6.Text.Trim();
            txtMPD = textBox7.Text.Trim();
            if (string.IsNullOrEmpty(txtID) || !ID.IsMatch(txtID))
            {
                textBox1.Select();
                MessageBox.Show("Veuillez saisir l'ID ou entrer le en format correcte.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (outils.Remplie("SELECT * FROM Employé WHERE ID = '" + outils.NiceID(txtID) + "'"))
                {
                    if (string.IsNullOrEmpty(txtNom) && string.IsNullOrEmpty(txtPrenom) && string.IsNullOrEmpty(txtAge) && string.IsNullOrEmpty(txtTelephone) && string.IsNullOrEmpty(txtEmail) && comboBox1.SelectedItem == null && string.IsNullOrEmpty(txtMPD))
                    {
                        textBox2.Select();
                        MessageBox.Show("Veuillez saisir au moins un champs à modifier.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string requete = "UPDATE Employé SET";
                        bool prequel = false, incorrect = false;
                        if (!string.IsNullOrEmpty(txtNom))
                        {
                            if (NomPrenom.IsMatch(txtNom))
                            {
                                if (prequel)
                                {
                                    requete += ",Nom = '" + txtNom + "'";
                                }
                                else
                                {
                                    requete += " Nom = '" + txtNom + "'";
                                    prequel = true;
                                }
                            }
                            else
                            {
                                incorrect = true;
                            }
                        }
                        if (!string.IsNullOrEmpty(txtPrenom))
                        {
                            if (NomPrenom.IsMatch(txtPrenom))
                            {
                                if (prequel)
                                {
                                    requete += ",Prénom = '" + txtPrenom + "'";
                                }
                                else
                                {
                                    requete += " Prénom = '" + txtPrenom + "'";
                                    prequel = true;
                                }
                            }
                            else
                            {
                                incorrect = true;
                            }
                        }
                        if (!string.IsNullOrEmpty(txtAge))
                        {
                            if (Age.IsMatch(txtAge) && int.Parse(txtAge) > 0)
                            {
                                if (prequel)
                                {
                                    requete += ",Age = " + txtAge;
                                }
                                else
                                {
                                    requete += " Age = " + txtAge;
                                    prequel = true;
                                }
                            }
                            else
                            {
                                incorrect = true;
                            }
                        }
                        if (comboBox1.SelectedItem != null)
                        {
                            if (prequel)
                            {
                                requete += ",Sexe = '" + comboBox1.SelectedItem + "'";
                            }
                            else
                            {
                                requete += " Sexe = '" + comboBox1.SelectedItem + "'";
                                prequel = true;
                            }
                        }
                        if (!string.IsNullOrEmpty(txtTelephone))
                        {
                            if (Telephone.IsMatch(txtTelephone))
                            {
                                if (prequel)
                                {
                                    requete += ",Téléphone = '" + txtTelephone + "'";
                                }
                                else
                                {
                                    requete += " Téléphone = '" + txtTelephone + "'";
                                    prequel = true;
                                }
                            }
                            else
                            {
                                incorrect = true;
                            }
                        }
                        if (!string.IsNullOrEmpty(txtEmail))
                        {
                            if (Email(txtEmail))
                            {
                                if (prequel)
                                {
                                    requete += ", Email = '" + txtEmail + "'";
                                }
                                else
                                {
                                    requete += " Email = '" + txtEmail + "'";
                                    prequel = true;
                                }
                            }
                            else
                            {
                                incorrect = true;
                            }
                        }
                        if (!string.IsNullOrEmpty(txtMPD))
                        {
                            if (prequel)
                            {
                                requete += ", MotPasse = '" + txtMPD + "'";
                            }
                            else
                            {
                                requete += " MotPasse = '" + txtMPD + "'";
                            }
                        }
                        if (incorrect)
                        {
                            MessageBox.Show("Veuillez saisir les champs à modifier en format correcte.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (MessageBox.Show("Voulez-vous vraiment modifier cet employé?", "Modifier", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                requete += " WHERE ID = '" + outils.NiceID(txtID) + "'";
                                outils.ExecuterRequete(requete);
                                MessageBox.Show("Employé modifié avec succès.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                {
                    textBox1.Select();
                    MessageBox.Show("Cet employé n'existe pas.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            txtID = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(txtID) || !ID.IsMatch(txtID))
            {
                Effacer();
                MessageBox.Show("Veuillez saisir l'ID ou entrer le en format correcte.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (outils.Remplie("SELECT * FROM Employé WHERE ID = '" + outils.NiceID(txtID) + "'"))
                {
                    if (outils.Remplie("SELECT * FROM Commande WHERE ID = '" + outils.NiceID(txtID) + "'"))
                    {
                        textBox1.Select();
                        MessageBox.Show("Impossible de supprimer cet employé, car il existe dans la liste des commandes.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (MessageBox.Show("Voulez-vous vraiment supprimer cet employé?", "Supprimer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string temp = outils.IDSuivant("Employé", outils.NiceID(txtID));
                            outils.ExecuterRequete("DELETE FROM Employé WHERE ID = '" + outils.NiceID(txtID) + "'");
                            textBox1.Text = temp;
                            Effacer();
                            MessageBox.Show("Employé supprimer avec succès.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    Effacer();
                    MessageBox.Show("Cet employé n'existe pas.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnEffacer_Click(object sender, EventArgs e)
        {
            Effacer();
            textBox1.Text = string.Empty;
        }

        private void btnPremier_Click(object sender, EventArgs e)
        {
            txtID = textBox1.Text.Trim();
            if (outils.Remplie("SELECT * FROM Employé"))
            {
                LabelCentre((outils.Navigation("Employé", outils.NiceID(txtID), 1)[0] + 1).ToString());
                Afficher(outils.Navigation("Employé", outils.NiceID(txtID), 1));
            }
        }

        private void btnPrecedent_Click(object sender, EventArgs e)
        {
            txtID = textBox1.Text.Trim();
            if (outils.Remplie("SELECT * FROM Employé"))
            {
                LabelCentre((outils.Navigation("Employé", outils.NiceID(txtID), 2)[0] + 1).ToString());
                Afficher(outils.Navigation("Employé", outils.NiceID(txtID), 2));
            }
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            txtID = textBox1.Text.Trim();
            if (outils.Remplie("SELECT * FROM Employé"))
            {
                LabelCentre((outils.Navigation("Employé", outils.NiceID(txtID), 3)[0] + 1).ToString());
                Afficher(outils.Navigation("Employé", outils.NiceID(txtID), 3));
            }
        }

        private void btnDernier_Click(object sender, EventArgs e)
        {
            txtID = textBox1.Text.Trim();
            if (outils.Remplie("SELECT * FROM Employé"))
            {
                LabelCentre((outils.Navigation("Employé", outils.NiceID(txtID), 4)[0] + 1).ToString());
                Afficher(outils.Navigation("Employé", outils.NiceID(txtID), 4));
            }
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            outils.ActualiserDataSet("Employé");
            if (outils.ds.Tables["Employé"].Rows.Count == 0)
            {
                textBox1.Text = "1";
                textBox1.Text = outils.NiceID(textBox1.Text.Trim());
            }
            else
            {
                textBox1.Text = (outils.ds.Tables["Employé"].Rows.Count + 1).ToString();
                textBox1.Text = outils.NiceID(textBox1.Text.Trim());
            }
            Effacer();
            textBox2.Select();
        }

        private void btnAjouter_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnAjouter.Image = Properties.Resources.AjouterEnter;
            }
            else
            {
                btnAjouter.Image = Properties.Resources.AjouterEnterSombre;
            }
        }

        private void btnAjouter_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnAjouter.Image = Properties.Resources.AjouterLeave;
            }
            else
            {
                btnAjouter.Image = Properties.Resources.AjouterLeaveSombre;
            }
        }

        private void btnRechercher_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnRechercher.Image = Properties.Resources.RechercherEnter;
            }
            else
            {
                btnRechercher.Image = Properties.Resources.RechercherEnterSombre;
            }
        }

        private void btnRechercher_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnRechercher.Image = Properties.Resources.RechercherLeave;
            }
            else
            {
                btnRechercher.Image = Properties.Resources.RechercherLeaveSombre;
            }
        }

        private void btnModifier_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnModifier.Image = Properties.Resources.ModifierEnter;
            }
            else
            {
                btnModifier.Image = Properties.Resources.ModifierEnterSombre;
            }
        }

        private void btnModifier_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnModifier.Image = Properties.Resources.ModifierLeave;
            }
            else
            {
                btnModifier.Image = Properties.Resources.ModifierLeaveSombre;
            }
        }

        private void btnSupprimer_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnSupprimer.Image = Properties.Resources.SupprimerEnter;
            }
            else
            {
                btnSupprimer.Image = Properties.Resources.SupprimerEnterSombre;
            }
        }

        private void btnSupprimer_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnSupprimer.Image = Properties.Resources.SupprimerLeave;
            }
            else
            {
                btnSupprimer.Image = Properties.Resources.SupprimerLeaveSombre;
            }
        }

        private void btnEffacer_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnEffacer.Image = Properties.Resources.EffacerEnter;
            }
            else
            {
                btnEffacer.Image = Properties.Resources.EffacerEnterSombre;
            }
        }

        private void btnEffacer_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnEffacer.Image = Properties.Resources.EffacerLeave;
            }
            else
            {
                btnEffacer.Image = Properties.Resources.EffacerLeaveSombre;
            }
        }

        private void btnPremier_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnPremier.Image = Properties.Resources.PremierEnter;
            }
            else
            {
                btnPremier.Image = Properties.Resources.PremierEnterSombre;
            }
        }

        private void btnPremier_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnPremier.Image = Properties.Resources.PremierLeave;
            }
            else
            {
                btnPremier.Image = Properties.Resources.PremierLeaveSombre;
            }
        }

        private void btnPrecedent_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnPrecedent.Image = Properties.Resources.PrécédentEnter;
            }
            else
            {
                btnPrecedent.Image = Properties.Resources.PrécédentEnterSombre;
            }
        }

        private void btnPrecedent_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnPrecedent.Image = Properties.Resources.PrécédentLeave;
            }
            else
            {
                btnPrecedent.Image = Properties.Resources.PrécédentLeaveSombre;
            }
        }

        private void btnSuivant_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnSuivant.Image = Properties.Resources.SuivantEnter;
            }
            else
            {
                btnSuivant.Image = Properties.Resources.SuivantEnterSombre;
            }
        }

        private void btnSuivant_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnSuivant.Image = Properties.Resources.SuivantLeave;
            }
            else
            {
                btnSuivant.Image = Properties.Resources.SuivantLeaveSombre;
            }
        }

        private void btnDernier_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnDernier.Image = Properties.Resources.DernierEnter;
            }
            else
            {
                btnDernier.Image = Properties.Resources.DernierEnterSombre;
            }
        }

        private void btnDernier_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnDernier.Image = Properties.Resources.DernierLeave;
            }
            else
            {
                btnDernier.Image = Properties.Resources.DernierLeaveSombre;
            }
        }
    }
}
