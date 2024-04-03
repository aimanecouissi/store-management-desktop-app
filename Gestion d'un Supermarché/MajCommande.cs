using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class MajCommande : Form
    {
        public MajCommande()
        {
            InitializeComponent();
        }

        readonly Regex Numero = new Regex("^\\d+$");
        readonly Regex Quantite = new Regex("^[0-9]+$");
        readonly Outils outils = new Outils();
        string txtNumero, txtQuantite;

        void RemplirComboBox(string NomTable, int NumeroComboBox)
        {
            outils.da = new SqlDataAdapter("SELECT * FROM " + NomTable, outils.cn);
            outils.da.Fill(outils.ds, NomTable);
            if (NumeroComboBox.Equals(1))
            {
                comboBox1.DataSource = outils.ds.Tables[NomTable];
                comboBox1.DisplayMember = "CIN";
                comboBox1.ValueMember = "CIN";
            }
            if (NumeroComboBox.Equals(2))
            {
                comboBox2.DataSource = outils.ds.Tables[NomTable];
                comboBox2.DisplayMember = "ID";
                comboBox2.ValueMember = "ID";
            }
            if (NumeroComboBox.Equals(3))
            {
                comboBox3.DataSource = outils.ds.Tables[NomTable];
                comboBox3.DisplayMember = "Nom";
                comboBox3.ValueMember = "Code";
            }
        }

        void LabelCentre(string Texte)
        {
            label2.Text = Texte;
            label2.Left = Width / 2 - (label2.Width / 2) - 6;
        }

        void Effacer()
        {
            string temp = textBox1.Text.Trim();
            foreach (Control control in Controls)
            {
                if (control is ComboBox box1) box1.SelectedItem = null;
                if (control is TextBox box) box.Clear();
            }
            textBox2.Clear();
            textBox3.Text = DateTime.Now.ToString("yyyy-MM-dd");
            listBox1.Items.Clear();
            LabelCentre("-");
            textBox1.Text = temp; 
            textBox1.Select();
        }

        float InfoProduit(string NomTable, string NomID, string ValeurID, int Colonne)
        {
            float resultat = 0;
            outils.cm = new SqlCommand("SELECT * FROM " + NomTable + " WHERE " + NomID + " = '" + ValeurID + "'", outils.cn);
            outils.cn.Open();
            outils.dr = outils.cm.ExecuteReader();
            if (outils.dr.Read())
            {
                resultat = float.Parse(outils.dr[Colonne].ToString());
                outils.dr.Close();
                outils.cn.Close();
            }
            return resultat;
        }

        void Afficher(string NomTable, List<int> Position)
        {
            if (NomTable.Equals("Commande"))
            {
                textBox1.Text = outils.ds.Tables[NomTable].Rows[Position[0]][0].ToString();
                comboBox1.Text = outils.ds.Tables[NomTable].Rows[Position[0]][1].ToString();
                comboBox2.Text = outils.ds.Tables[NomTable].Rows[Position[0]][2].ToString();
                textBox4.Text = string.Format("{0:f2}", float.Parse(outils.ds.Tables[NomTable].Rows[Position[0]][3].ToString())) + " DH";
                textBox3.Text = Convert.ToDateTime(outils.ds.Tables[NomTable].Rows[Position[0]][4]).ToString("yyyy-MM-dd");
                comboBox3.Text = null;
                textBox2.Clear();
                listBox1.Items.Clear();
                Afficher("Details_Commande", outils.Position("Details_Commande", textBox1.Text.Trim()));
            }
            else
            {
                for (int i = 0; i < Position.Count; i++)
                {
                    listBox1.Items.Add(outils.ds.Tables[NomTable].Rows[Position[i]][1].ToString() + " * " + outils.ds.Tables[NomTable].Rows[Position[i]][2].ToString());
                }
            }
        }

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                contextMenuStrip1.BackColor = Color.Black;
                effacerDeLaListeToolStripMenuItem.BackColor = Color.White;
                effacerDeLaListeToolStripMenuItem.ForeColor = Color.Black;
                BackColor = Color.White;
                listBox1.BackColor = Color.White;
                listBox1.ForeColor = Color.Black;
                pictureBox1.Image = Properties.Resources.NuméroNoir;
                pictureBox2.Image = Properties.Resources.DateNoir;
                pictureBox3.Image = Properties.Resources.CodeNoir;
                pictureBox4.Image = Properties.Resources.PrixNoir;
                pictureBox5.Image = Properties.Resources.QuantitéNoir;
                pictureBox6.Image = Properties.Resources.CINNoir;
                pictureBox9.Image = Properties.Resources.IDNoir;
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
                btnAjouterProduit.Image = Properties.Resources.AjouterProduitLeave;
                foreach (Control control in Controls)
                {
                    if (control is Label)
                    {
                        control.ForeColor = Color.Black;
                    }
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
                contextMenuStrip1.BackColor = Color.White;
                effacerDeLaListeToolStripMenuItem.BackColor = Color.Black;
                effacerDeLaListeToolStripMenuItem.ForeColor = Color.White;
                BackColor = Color.Black;
                listBox1.BackColor = Color.FromArgb(31, 31, 31);
                listBox1.ForeColor = Color.White;
                pictureBox1.Image = Properties.Resources.NuméroBlanc;
                pictureBox2.Image = Properties.Resources.DateBlanc;
                pictureBox3.Image = Properties.Resources.CodeBlanc;
                pictureBox4.Image = Properties.Resources.PrixBlanc;
                pictureBox5.Image = Properties.Resources.QuantitéBlanc;
                pictureBox6.Image = Properties.Resources.CINBlanc;
                pictureBox9.Image = Properties.Resources.IDBlanc;
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
                btnAjouterProduit.Image = Properties.Resources.AjouterProduitLeave_Sombre;
                foreach (Control control in Controls)
                {
                    if (control is Label)
                    {
                        control.ForeColor = Color.White;
                    }
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

        private void MajCommande_Load(object sender, EventArgs e)
        {
            string[] nt = { "Client", "Employé", "Produit" };
            for (int i = 0; i < 3; i++)
            {
                RemplirComboBox(nt[i], i + 1);
            }
            Effacer();
            ModeCS();
        }

        private void MajCommande_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()) || !string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                if (MessageBox.Show("Voulez-vous vraiment fermer cette fenêtre?", "Màj d'une commande", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Application.OpenForms["MenuMaj"].Show();
                }
            }
            else
            {
                Application.OpenForms["MenuMaj"].Show();
            }
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            txtNumero = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(txtNumero) || listBox1.Items.Count == 0)
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
                if (Numero.IsMatch(txtNumero) && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
                {
                    if (outils.Remplie("SELECT * FROM Commande WHERE Numero = " + txtNumero))
                    {
                        textBox1.Select();
                        MessageBox.Show("Cette commande existe déjà.", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        outils.ExecuterRequete("INSERT INTO Commande(Numero,CIN,ID,DateCommande) VALUES(" + txtNumero + ",'" + comboBox1.SelectedValue + "','" + comboBox2.SelectedValue + "','" + textBox3.Text + "')");
                        for (int i = 0; i < listBox1.Items.Count; i++)
                        {
                            string[] DC = listBox1.Items[i].ToString().Split('*');
                            outils.ExecuterRequete("INSERT INTO Details_Commande(Numero,Code,Quantité) VALUES(" + txtNumero + ",'" + DC[0].ToString().Trim() + "'," + DC[1].ToString().Trim() + ")");
                        }
                        textBox4.Text = string.Format("{0:f2}", float.Parse(InfoProduit("Commande", "Numero", txtNumero, 3).ToString())) + " DH";
                        LabelCentre((outils.Position("Commande", txtNumero)[0] + 1).ToString());
                        comboBox3.SelectedItem = null;
                        textBox2.Clear();
                        MessageBox.Show("Commande ajouté avec succès.", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtNumero = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(txtNumero) || !Numero.IsMatch(txtNumero))
            {
                Effacer();
                MessageBox.Show("Veuillez saisir le numero ou entrer le en format correcte.", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                outils.cm = new SqlCommand("SELECT * FROM Commande WHERE Numero = " + txtNumero, outils.cn);
                outils.cn.Open();
                outils.dr = outils.cm.ExecuteReader();
                if (outils.dr.Read())
                {
                    comboBox1.SelectedText = outils.dr[1].ToString();
                    comboBox2.SelectedText = outils.dr[2].ToString();
                    comboBox3.SelectedItem = null;
                    textBox2.Clear();
                    textBox4.Text = string.Format("{0:f2}", float.Parse(outils.dr[3].ToString())) + " DH";
                    textBox3.Text = Convert.ToDateTime(outils.dr[4]).ToString("yyyy-MM-dd");
                    outils.dr.Close();
                    outils.cm = new SqlCommand("SELECT * FROM Details_Commande WHERE Numero = " + txtNumero, outils.cn);
                    outils.dr = outils.cm.ExecuteReader();
                    listBox1.Items.Clear();
                    while (outils.dr.Read())
                    {
                        listBox1.Items.Add(outils.dr[1].ToString() + " * " + outils.dr[2].ToString());
                    }
                    outils.dr.Close();
                    outils.cn.Close();
                    LabelCentre((outils.Position("Commande", txtNumero)[0] + 1).ToString());
                }
                else
                {
                    outils.dr.Close();
                    outils.cn.Close();
                    Effacer();
                    MessageBox.Show("Cette Commande n'existe pas.", "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            textBox1.Select();
            MessageBox.Show("La modification des commandes n'est pas autorisée.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            txtNumero = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(txtNumero) || !Numero.IsMatch(txtNumero))
            {
                Effacer();
                MessageBox.Show("Veuillez saisir le numero ou entrer le en format correcte.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (outils.Remplie("SELECT * FROM Commande WHERE Numero = " + txtNumero))
                {
                    if (MessageBox.Show("Voulez-vous vraiment supprimer cette commande?", "Supprimer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        textBox1.Text = outils.IDSuivant("Commande", txtNumero);
                        outils.ExecuterRequete("DELETE FROM Details_Commande WHERE Numero = " + txtNumero);
                        outils.ExecuterRequete("DELETE FROM Commande WHERE Numero = " + txtNumero);
                        Effacer();
                        MessageBox.Show("Commande supprimée avec succès.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Effacer();
                    textBox1.Focus();
                    MessageBox.Show("Cette commande n'existe pas.", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            outils.ActualiserDataSet("Commande");
            if (outils.ds.Tables["Commande"].Rows.Count == 0)
            {
                textBox1.Text = "1";
            }
            else
            {
                textBox1.Text = (outils.ds.Tables["Commande"].Rows.Count + 1).ToString();
            }
            Effacer();
        }

        private void btnAjouterProduit_Click(object sender, EventArgs e)
        {
            txtQuantite = textBox2.Text.Trim();
            if (!string.IsNullOrEmpty(txtQuantite) && comboBox3.SelectedItem != null && Quantite.IsMatch(txtQuantite))
            {
                if (int.Parse(txtQuantite) > 0)
                {
                    if (listBox1.Items.Count > 0)
                    {
                        bool copie = false;
                        for (int i = 0; i < listBox1.Items.Count; i++)
                        {
                            string[] DC = listBox1.Items[i].ToString().Split('*');
                            if (DC[0].ToString().Trim() == comboBox3.SelectedValue.ToString())
                            {
                                copie = true;
                                break;
                            }
                        }
                        if (copie)
                        {
                            MessageBox.Show("Ce produit existe déjà dans la liste.", "Ajouter un produit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            float total = 0;
                            listBox1.Items.Add(comboBox3.SelectedValue.ToString() + " * " + txtQuantite);
                            for (int i = 0; i < listBox1.Items.Count; i++)
                            {
                                string[] DC = listBox1.Items[i].ToString().Split('*');
                                total += InfoProduit("Produit", "Code", DC[0].Trim(), 5) * int.Parse(DC[1]);
                            }
                            if (total == 0)
                            {
                                textBox4.Clear();
                            }
                            else
                            {
                                textBox4.Text = string.Format("{0:f2}", total) + " DH";
                            }
                        }
                    }
                    else
                    {
                        if (InfoProduit("Produit", "Code", comboBox3.SelectedValue.ToString(), 4) >= float.Parse(txtQuantite))
                        {
                            float total = 0;
                            listBox1.Items.Add(comboBox3.SelectedValue.ToString() + " * " + txtQuantite);
                            for (int i = 0; i < listBox1.Items.Count; i++)
                            {
                                string[] DC = listBox1.Items[i].ToString().Split('*');
                                total += InfoProduit("Produit", "Code", DC[0].Trim(), 5) * int.Parse(DC[1]);
                            }
                            if (total == 0)
                            {
                                textBox4.Clear();
                            }
                            else
                            {
                                textBox4.Text = string.Format("{0:f2}", total) + " DH";
                            }
                        }
                        else
                        {
                            MessageBox.Show("La quantité choisie pour ce produit est supérieure à la quantité en stock.", "Ajouter un produit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox2.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("La quantité du produit doit être supérieure à 0.", "Ajouter un produit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Select();
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir les deux champs correctement (Nom du produit et Quantité).", "Ajouter un produit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Select();
            }
        }

        private void effacerDeLaListeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = listBox1.SelectedIndices.Count - 1; i >= 0; i--)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndices[i]);
            }
            float total = 0;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string[] DC = listBox1.Items[i].ToString().Split('*');
                total += InfoProduit("Produit", "Code", DC[0].Trim(), 5) * int.Parse(DC[1]);
            }
            if (total == 0)
            {
                textBox4.Clear();
            }
            else
            {
                textBox4.Text = string.Format("{0:f2}", float.Parse(total.ToString())) + " DH";
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                effacerDeLaListeToolStripMenuItem.PerformClick();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAjouterProduit_Click(sender, e);
            }
        }

        private void btnPremier_Click(object sender, EventArgs e)
        {
            txtNumero = textBox1.Text.Trim();
            if (outils.Remplie("SELECT * FROM Commande"))
            {
                LabelCentre((outils.Navigation("Commande", txtNumero, 1)[0] + 1).ToString());
                Afficher("Commande", outils.Navigation("Commande", txtNumero, 1));
            }
        }

        private void btnPrecedent_Click(object sender, EventArgs e)
        {
            txtNumero = textBox1.Text.Trim();
            if (outils.Remplie("SELECT * FROM Commande"))
            {
                LabelCentre((outils.Navigation("Commande", txtNumero, 2)[0] + 1).ToString());
                Afficher("Commande", outils.Navigation("Commande", txtNumero, 2));
            }
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            txtNumero = textBox1.Text.Trim();
            if (outils.Remplie("SELECT * FROM Commande"))
            {
                LabelCentre((outils.Navigation("Commande", txtNumero, 3)[0] + 1).ToString());
                Afficher("Commande", outils.Navigation("Commande", txtNumero, 3));
            }
        }

        private void btnDernier_Click(object sender, EventArgs e)
        {
            txtNumero = textBox1.Text.Trim();
            if (outils.Remplie("SELECT * FROM Commande"))
            {
                LabelCentre((outils.Navigation("Commande", txtNumero, 4)[0] + 1).ToString());
                Afficher("Commande", outils.Navigation("Commande", txtNumero, 4));
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = "1";
            textBox2.Select();
        }

        private void btnAjouterProduit_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnAjouterProduit.Image = Properties.Resources.AjouterProduitEnter;
            }
            else
            {
                btnAjouterProduit.Image = Properties.Resources.AjouterProduitEnter_Sombre;
            }
        }

        private void btnAjouterProduit_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                btnAjouterProduit.Image = Properties.Resources.AjouterProduitLeave;
            }
            else
            {
                btnAjouterProduit.Image = Properties.Resources.AjouterProduitLeave_Sombre;
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
