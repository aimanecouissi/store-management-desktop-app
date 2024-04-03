using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class MajClient : Form
    {
        public MajClient()
        {
            InitializeComponent();
            AutoVille();
        }

        readonly Regex CIN = new Regex("^[A-Za-z]{2}\\d{1,6}$");
        readonly Regex NomPrenom = new Regex("^[A-Za-z]+$");
        readonly Regex Age = new Regex("^\\d{1,3}$");
        readonly Regex Telephone = new Regex("^0\\d{9}$");
        readonly Regex Ville = new Regex("^[A-Za-z]+(\\s[A-Za-z]+)*$");
        readonly Outils outils = new Outils();
        string txtCIN, txtNom, txtPrenom, txtAge, txtTelephone, txtVille;

        void Afficher(List<int> Position)
        {
            textBox1.Text = outils.ds.Tables["Client"].Rows[Position[0]][0].ToString();
            textBox2.Text = outils.ds.Tables["Client"].Rows[Position[0]][1].ToString();
            textBox3.Text = outils.ds.Tables["Client"].Rows[Position[0]][2].ToString();
            textBox4.Text = outils.ds.Tables["Client"].Rows[Position[0]][3].ToString();
            comboBox1.SelectedItem = outils.ds.Tables["Client"].Rows[Position[0]][4].ToString();
            textBox5.Text = outils.ds.Tables["Client"].Rows[Position[0]][5].ToString();
            textBox6.Text = outils.ds.Tables["Client"].Rows[Position[0]][6].ToString();
        }

        void Effacer()
        {
            string temp = textBox1.Text.Trim().ToUpper();
            foreach (Control control in Controls)
            {
                if (control is TextBox box) box.Clear();
            }
            comboBox1.SelectedItem = null;
            LabelCentre("-");
            textBox1.Text = temp;
            textBox1.Select();
        }

        void AutoVille()
        {
            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            outils.cn.Open();
            outils.cm = new SqlCommand("SELECT Ville FROM Client", outils.cn);
            outils.dr = outils.cm.ExecuteReader();
            while (outils.dr.Read()) auto.Add(outils.dr[0].ToString());
            outils.dr.Close();
            outils.cn.Close();
            textBox6.AutoCompleteCustomSource = auto;
        }

        void LabelCentre(string Texte)
        {
            label2.Text = Texte;
            label2.Left = Width / 2 - (label2.Width / 2) - 6;
        }

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                BackColor = Color.White;
                comboBox1.BackColor = Color.White;
                comboBox1.ForeColor = Color.Black;
                pictureBox1.Image = Properties.Resources.CINNoir;
                pictureBox2.Image = Properties.Resources.NomPrénomNoir;
                pictureBox3.Image = Properties.Resources.NomPrénomNoir;
                pictureBox4.Image = Properties.Resources.AgeNoir;
                pictureBox5.Image = Properties.Resources.SexeNoir;
                pictureBox6.Image = Properties.Resources.TéléphoneNoir;
                pictureBox9.Image = Properties.Resources.VilleNoir;
                btnAjouter.Image = Properties.Resources.AjouterLeave;
                btnRechercher.Image = Properties.Resources.RechercherLeave;
                btnModifier.Image = Properties.Resources.ModifierLeave;
                btnSupprimer.Image = Properties.Resources.SupprimerLeave;
                btnEffacer.Image = Properties.Resources.EffacerLeave;
                btnPremier.Image = Properties.Resources.PremierLeave;
                btnPrecedent.Image = Properties.Resources.PrécédentLeave;
                btnSuivant.Image = Properties.Resources.SuivantLeave;
                btnDernier.Image = Properties.Resources.DernierLeave;
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
                comboBox1.BackColor = Color.FromArgb(31, 31, 31);
                comboBox1.ForeColor = Color.White;
                pictureBox1.Image = Properties.Resources.CINBlanc;
                pictureBox2.Image = Properties.Resources.NomPrénomBlanc;
                pictureBox3.Image = Properties.Resources.NomPrénomBlanc;
                pictureBox4.Image = Properties.Resources.AgeBlanc;
                pictureBox5.Image = Properties.Resources.SexeBlanc;
                pictureBox6.Image = Properties.Resources.TéléphoneBlanc;
                pictureBox9.Image = Properties.Resources.VilleBlanc;
                btnAjouter.Image = Properties.Resources.AjouterLeaveSombre;
                btnRechercher.Image = Properties.Resources.RechercherLeaveSombre;
                btnModifier.Image = Properties.Resources.ModifierLeaveSombre;
                btnSupprimer.Image = Properties.Resources.SupprimerLeaveSombre;
                btnEffacer.Image = Properties.Resources.EffacerLeaveSombre;
                btnPremier.Image = Properties.Resources.PremierLeaveSombre;
                btnPrecedent.Image = Properties.Resources.PrécédentLeaveSombre;
                btnSuivant.Image = Properties.Resources.SuivantLeaveSombre;
                btnDernier.Image = Properties.Resources.DernierLeaveSombre;
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

        private void MajClient_Load(object sender, EventArgs e)
        {
            LabelCentre("-");
            ModeCS();
        }

        private void MajClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                    if (!string.IsNullOrEmpty(control.Text.Trim()))
                    {
                        if (MessageBox.Show("Voulez-vous vraiment fermer cette fenêtre?", "Màj d'un client", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
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
            txtCIN = textBox1.Text.Trim().ToUpper(); 
            txtNom = textBox2.Text.Trim().ToUpper(); 
            txtPrenom = textBox3.Text.Trim().ToUpper(); 
            txtAge = textBox4.Text.Trim(); 
            txtTelephone = textBox5.Text.Trim(); 
            txtVille = textBox6.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(txtCIN) || string.IsNullOrEmpty(txtNom) || string.IsNullOrEmpty(txtPrenom) || string.IsNullOrEmpty(txtAge) || string.IsNullOrEmpty(txtTelephone) || string.IsNullOrEmpty(txtVille))
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
                if (CIN.IsMatch(txtCIN) && NomPrenom.IsMatch(txtNom) && NomPrenom.IsMatch(txtPrenom) && Age.IsMatch(txtAge) && Telephone.IsMatch(txtTelephone) && Ville.IsMatch(txtVille) && comboBox1.SelectedItem != null && int.Parse(txtAge) > 0)
                {
                    if (outils.Remplie("SELECT * FROM Client WHERE CIN = '" + txtCIN + "'"))
                    {
                        MessageBox.Show("Ce client existe déjà.", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Select();
                    }
                    else
                    {
                        outils.ExecuterRequete("INSERT INTO Client VALUES('" + txtCIN + "','" + txtNom + "','" + txtPrenom + "'," + txtAge + ",'" + comboBox1.SelectedItem + "','" + txtTelephone + "','" + txtVille + "')");
                        LabelCentre((outils.Position("Client", txtCIN)[0] + 1).ToString());
                        MessageBox.Show("Client ajouté avec succès.", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtCIN = textBox1.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(txtCIN) || !CIN.IsMatch(txtCIN))
            {
                Effacer();
                MessageBox.Show("Veuillez saisir le CIN ou entrer le en format correcte.", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                outils.cm = new SqlCommand("SELECT * FROM Client WHERE CIN = '" + txtCIN + "'", outils.cn);
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
                    outils.dr.Close();
                    outils.cn.Close();
                    LabelCentre((outils.Position("Client", txtCIN)[0] + 1).ToString());
                }
                else
                {
                    outils.dr.Close();
                    outils.cn.Close();
                    Effacer();
                    MessageBox.Show("Ce client n'existe pas.", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            txtCIN = textBox1.Text.Trim().ToUpper(); 
            txtNom = textBox2.Text.Trim().ToUpper(); 
            txtPrenom = textBox3.Text.Trim().ToUpper(); 
            txtAge = textBox4.Text.Trim(); 
            txtTelephone = textBox5.Text.Trim(); 
            txtVille = textBox6.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(txtCIN) || !CIN.IsMatch(txtCIN))
            {
                textBox1.Select();
                MessageBox.Show("Veuillez saisir le CIN ou entrer le en format correcte.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (outils.Remplie("SELECT * FROM Client WHERE CIN = '" + txtCIN + "'"))
                {
                    if (string.IsNullOrEmpty(txtNom) && string.IsNullOrEmpty(txtPrenom) && string.IsNullOrEmpty(txtAge) && string.IsNullOrEmpty(txtTelephone) && string.IsNullOrEmpty(txtVille) && comboBox1.SelectedItem == null)
                    {
                        textBox2.Select();
                        MessageBox.Show("Veuillez saisir au moins un champs à modifier.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string requete = "UPDATE Client SET";
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
                        if (!string.IsNullOrEmpty(txtVille))
                        {
                            if (Ville.IsMatch(txtVille))
                            {
                                if (prequel)
                                {
                                    requete += ",Ville = '" + txtVille + "'";
                                }
                                else
                                {
                                    requete += " Ville = '" + txtVille + "'";
                                }
                            }
                            else
                            {
                                incorrect = true;
                            }
                        }
                        if (incorrect)
                        {
                            MessageBox.Show("Veuillez saisir les champs à modifier en format correcte.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (MessageBox.Show("Voulez-vous vraiment modifier ce client?", "Modifier", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                requete += " WHERE CIN = '" + txtCIN + "'";
                                outils.ExecuterRequete(requete);
                                MessageBox.Show("Client modifié avec succès.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                {
                    textBox1.Select();
                    MessageBox.Show("Ce client n'existe pas.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            txtCIN = textBox1.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(txtCIN) || !CIN.IsMatch(txtCIN))
            {
                Effacer();
                MessageBox.Show("Veuillez saisir le CIN ou entrer le en format correcte.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (outils.Remplie("SELECT * FROM Client WHERE CIN = '" + txtCIN + "'"))
                {
                    if (outils.Remplie("SELECT * FROM Commande WHERE CIN = '" + txtCIN + "'"))
                    {
                        textBox1.Select();
                        MessageBox.Show("Impossible de supprimer ce client, car il existe dans la liste des commandes.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (MessageBox.Show("Voulez-vous vraiment supprimer ce client?", "Supprimer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            textBox1.Text = outils.IDSuivant("Client", txtCIN);
                            outils.ExecuterRequete("DELETE FROM Client WHERE CIN = '" + txtCIN + "'");
                            Effacer();
                            MessageBox.Show("Client supprimer avec succès.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    Effacer();
                    MessageBox.Show("Ce client n'existe pas.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            txtCIN = textBox1.Text.Trim().ToUpper();
            if (outils.Remplie("SELECT * FROM Client"))
            {
                LabelCentre((outils.Navigation("Client", txtCIN, 1)[0] + 1).ToString());
                Afficher(outils.Navigation("Client", txtCIN, 1));
            }
        }

        private void btnPrecedent_Click(object sender, EventArgs e)
        {
            txtCIN = textBox1.Text.Trim().ToUpper();
            if (outils.Remplie("SELECT * FROM Client"))
            {
                LabelCentre((outils.Navigation("Client", txtCIN, 2)[0] + 1).ToString());
                Afficher(outils.Navigation("Client", txtCIN, 2));
            }
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            txtCIN = textBox1.Text.Trim().ToUpper();
            if (outils.Remplie("SELECT * FROM Client"))
            {
                LabelCentre((outils.Navigation("Client", txtCIN, 3)[0] + 1).ToString());
                Afficher(outils.Navigation("Client", txtCIN, 3));
            }
        }

        private void btnDernier_Click(object sender, EventArgs e)
        {
            txtCIN = textBox1.Text.Trim().ToUpper();
            if (outils.Remplie("SELECT * FROM Client"))
            {
                LabelCentre((outils.Navigation("Client", txtCIN, 4)[0] + 1).ToString());
                Afficher(outils.Navigation("Client", txtCIN, 4));
            }
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
