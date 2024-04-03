using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class MajProduit : Form
    {
        public MajProduit()
        {
            InitializeComponent();
        }

        readonly Regex Code = new Regex("^\\d{12}$");
        readonly Regex Quantite = new Regex("^\\d{1,5}$");
        readonly Regex Prix = new Regex("^\\d+[\\.\\d+]*$");
        readonly Outils outils = new Outils();
        string txtCode, txtNom, txtQuantite, txtPrix;

        void Effacer()
        {
            string temp = textBox1.Text;
            foreach (Control control in Controls)
            {
                if (control is TextBox box) box.Clear();
                if (control is ComboBox box1) box1.SelectedItem = null;
            }
            LabelCentre("-");
            textBox1.Text = temp;
            textBox1.Select();
        }

        void Afficher(List<int> Position)
        {
            textBox1.Text = outils.ds.Tables["Produit"].Rows[Position[0]][0].ToString();
            textBox2.Text = outils.ds.Tables["Produit"].Rows[Position[0]][1].ToString();
            comboBox1.SelectedItem = outils.ds.Tables["Produit"].Rows[Position[0]][2].ToString();
            comboBox2.SelectedItem = outils.ds.Tables["Produit"].Rows[Position[0]][3].ToString();
            textBox5.Text = outils.ds.Tables["Produit"].Rows[Position[0]][4].ToString();
            textBox6.Text = string.Format("{0:f2}", float.Parse(outils.ds.Tables["Produit"].Rows[Position[0]][5].ToString()));
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
                pictureBox1.Image = Properties.Resources.CodeNoir;
                pictureBox2.Image = Properties.Resources.NomPrénomNoir;
                pictureBox4.Image = Properties.Resources.CategorieNoir;
                pictureBox5.Image = Properties.Resources.MarqueNoir;
                pictureBox6.Image = Properties.Resources.QuantitéNoir;
                pictureBox9.Image = Properties.Resources.PrixNoir;
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
                    if (control is ComboBox)
                    {
                        control.ForeColor = Color.Black;
                        control.BackColor = Color.White;
                    }
                }
            }
            else
            {
                BackColor = Color.Black;
                pictureBox1.Image = Properties.Resources.CodeBlanc;
                pictureBox2.Image = Properties.Resources.NomPrénomBlanc;
                pictureBox4.Image = Properties.Resources.CategorieBlanc;
                pictureBox5.Image = Properties.Resources.MarqueBlanc;
                pictureBox6.Image = Properties.Resources.QuantitéBlanc;
                pictureBox9.Image = Properties.Resources.PrixBlanc;
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
                    if (control is ComboBox)
                    {
                        control.ForeColor = Color.White;
                        control.BackColor = Color.FromArgb(31, 31, 31);
                    }
                }
            }
        }

        private void MajProduit_Load(object sender, EventArgs e)
        {
            LabelCentre("-");
            ModeCS();
        }

        private void MajProduit_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                    if (!string.IsNullOrEmpty(control.Text.Trim()))
                    {
                        if (MessageBox.Show("Voulez-vous vraiment fermer cette fenêtre?", "Màj d'un produit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
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
            txtCode = textBox1.Text.Trim(); 
            txtNom = textBox2.Text.Trim().ToUpper(); 
            txtQuantite = textBox5.Text.Trim(); 
            txtPrix = textBox6.Text.Trim();
            if (string.IsNullOrEmpty(txtCode) || string.IsNullOrEmpty(txtNom) || string.IsNullOrEmpty(txtQuantite) || string.IsNullOrEmpty(txtPrix))
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
                if (Code.IsMatch(txtCode) && Quantite.IsMatch(txtQuantite) && Prix.IsMatch(txtPrix) && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && int.Parse(txtQuantite) > 0 && float.Parse(txtPrix) > 0)
                {
                    if (outils.Remplie("SELECT * FROM Produit WHERE Code = '" + txtCode + "'"))
                    {
                        textBox1.Select();
                        MessageBox.Show("Ce produit existe déjà.", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        outils.ExecuterRequete("INSERT INTO Produit VALUES('" + txtCode + "','" + txtNom + "','" + comboBox1.SelectedItem + "','" + comboBox2.SelectedItem + "'," + txtQuantite + "," + txtPrix + ")");
                        LabelCentre((outils.Position("Produit", txtCode)[0] + 1).ToString());
                        MessageBox.Show("Produit ajouté avec succès.", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtCode = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(txtCode) || !Code.IsMatch(txtCode))
            {
                Effacer();
                MessageBox.Show("Veuillez saisir le Code ou entrer le en format correcte.", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                outils.cm = new SqlCommand("SELECT * FROM Produit WHERE Code = '" + txtCode + "'", outils.cn);
                outils.cn.Open();
                outils.dr = outils.cm.ExecuteReader();
                if (outils.dr.Read())
                {
                    textBox2.Text = outils.dr[1].ToString();
                    comboBox1.SelectedItem = outils.dr[2].ToString();
                    comboBox2.SelectedItem = outils.dr[3].ToString();
                    textBox5.Text = outils.dr[4].ToString();
                    textBox6.Text = string.Format("{0:f2}", float.Parse(outils.dr[5].ToString()));
                    outils.dr.Close();
                    outils.cn.Close();
                    LabelCentre((outils.Position("Produit", txtCode)[0] + 1).ToString());
                }
                else
                {
                    outils.dr.Close();
                    outils.cn.Close();
                    Effacer();
                    textBox1.Focus();
                    MessageBox.Show("Ce produit n'existe pas.", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            txtCode = textBox1.Text.Trim(); 
            txtNom = textBox2.Text.Trim().ToUpper(); 
            txtQuantite = textBox5.Text.Trim(); 
            txtPrix = textBox6.Text.Trim();
            if (string.IsNullOrEmpty(txtCode) || !Code.IsMatch(txtCode))
            {
                textBox1.Select();
                MessageBox.Show("Veuillez saisir le code ou entrer le en format correcte.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (outils.Remplie("SELECT * FROM Produit WHERE Code = '" + txtCode + "'"))
                {
                    if (string.IsNullOrEmpty(txtNom) && comboBox1.SelectedItem == null && comboBox2.SelectedItem == null && string.IsNullOrEmpty(txtQuantite) && string.IsNullOrEmpty(txtPrix))
                    {
                        textBox2.Select();
                        MessageBox.Show("Veuillez saisir au moins un champs à modifier.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string requete = "UPDATE Produit SET";
                        bool prequel = false, incorrect = false;
                        if (!string.IsNullOrEmpty(txtNom))
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
                        if (comboBox1.SelectedItem != null)
                        {
                            if (prequel)
                            {
                                requete += ",Categorie = '" + comboBox1.SelectedItem + "'";
                            }
                            else
                            {
                                requete += " Categorie = '" + comboBox1.SelectedItem + "'";
                                prequel = true;
                            }
                        }
                        if (comboBox2.SelectedItem != null)
                        {
                            if (prequel)
                            {
                                requete += ",Marque = '" + comboBox2.SelectedItem + "'";
                            }
                            else
                            {
                                requete += " Marque = '" + comboBox2.SelectedItem + "'";
                                prequel = true;
                            }
                        }
                        if (!string.IsNullOrEmpty(txtQuantite))
                        {
                            if (Quantite.IsMatch(txtQuantite) && int.Parse(txtQuantite) > 0)
                            {
                                if (prequel)
                                {
                                    requete += ",QuantitéStock = " + txtQuantite;
                                }
                                else
                                {
                                    requete += " QuantitéStock = " + txtQuantite;
                                    prequel = true;
                                }
                            }
                            else
                            {
                                incorrect = true;
                            }
                        }
                        if (!string.IsNullOrEmpty(txtPrix))
                        {
                            if (Prix.IsMatch(txtPrix) && float.Parse(txtPrix) > 0)
                            {
                                if (prequel)
                                {
                                    requete += ",Prix = " + txtPrix;
                                }
                                else
                                {
                                    requete += " Prix = " + txtPrix;
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
                            if (MessageBox.Show("Voulez-vous vraiment modifier ce produit?", "Modifier", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                requete += " WHERE Code = '" + txtCode + "'";
                                outils.ExecuterRequete(requete);
                                MessageBox.Show("Produit modifié avec succès.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ce produit n'existe pas.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Select();
                }
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            txtCode = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(txtCode) || !Code.IsMatch(txtCode))
            {
                Effacer();
                MessageBox.Show("Veuillez saisir le code ou entrer le en format correcte.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (outils.Remplie("SELECT * FROM Produit WHERE Code = '" + txtCode + "'"))
                {
                    if (outils.Remplie("SELECT * FROM Details_Commande WHERE Code = '" + txtCode + "'"))
                    {
                        MessageBox.Show("Impossible de supprimer ce produit, car il existe dans la liste des commandes.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Focus();
                    }
                    else
                    {
                        if (MessageBox.Show("Voulez-vous vraiment supprimer ce produit?", "Supprimer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            textBox1.Text = outils.IDSuivant("Produit", txtCode);
                            outils.ExecuterRequete("DELETE FROM Produit WHERE Code = '" + txtCode + "'");
                            Effacer();
                            MessageBox.Show("Produit supprimer avec succès.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    Effacer();
                    MessageBox.Show("Ce produit n'existe pas.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnEffacer_Click(object sender, EventArgs e)
        {
            Effacer();
            textBox1.Text = string.Empty;
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            string codeGen = string.Empty;
            Random rnd = new Random();
            while (codeGen.Length < 12)
            {
                codeGen += rnd.Next(0, 10).ToString();
            }
            textBox1.Text = codeGen;
            Effacer();
            textBox2.Select();
        }

        private void btnPremier_Click(object sender, EventArgs e)
        {
            txtCode = textBox1.Text.Trim();
            if (outils.Remplie("SELECT * FROM Produit"))
            {
                LabelCentre((outils.Navigation("Produit", txtCode, 1)[0] + 1).ToString());
                Afficher(outils.Navigation("Produit", txtCode, 1));
            }
        }

        private void btnPrecedent_Click(object sender, EventArgs e)
        {
            txtCode = textBox1.Text.Trim();
            if (outils.Remplie("SELECT * FROM Produit"))
            {
                LabelCentre((outils.Navigation("Produit", txtCode, 2)[0] + 1).ToString());
                Afficher(outils.Navigation("Produit", txtCode, 2));
            }
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            txtCode = textBox1.Text.Trim();
            if (outils.Remplie("SELECT * FROM Produit"))
            {
                LabelCentre((outils.Navigation("Produit", txtCode, 3)[0] + 1).ToString());
                Afficher(outils.Navigation("Produit", txtCode, 3));
            }
        }

        private void btnDernier_Click(object sender, EventArgs e)
        {
            txtCode = textBox1.Text.Trim();
            if (outils.Remplie("SELECT * FROM Produit"))
            {
                LabelCentre((outils.Navigation("Produit", txtCode, 4)[0] + 1).ToString());
                Afficher(outils.Navigation("Produit", txtCode, 4));
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
