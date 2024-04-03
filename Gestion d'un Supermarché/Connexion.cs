using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class Connexion : Form
    {
        public Connexion()
        {
            InitializeComponent();
        }

        readonly Regex ID = new Regex("^\\d{1,4}$");
        readonly Outils outils = new Outils();
        bool faiteC = false, faiteS = false;
        MenuPrincipale mp = new MenuPrincipale();

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                faiteS = false;
                if (!faiteC)
                {
                    BackColor = Color.White;
                    pictureBox1.Image = Properties.Resources.IDNoir;
                    pictureBox2.Image = Properties.Resources.MDPNoir;
                    checkBox1.ForeColor = Color.Black;
                    button1.FlatAppearance.BorderColor = Color.Black;
                    button1.BackColor = Color.Black;
                    button1.ForeColor = Color.White;
                    foreach (Control control in Controls)
                    {
                        if (control is Label) control.ForeColor = Color.Black;
                        if (control is TextBox)
                        {
                            control.ForeColor = Color.Black;
                            control.BackColor = Color.White;
                        }
                    }
                    label4.ForeColor = Color.DimGray;
                }
            }
            else
            {
                faiteC = false;
                if (!faiteS)
                {
                    BackColor = Color.Black;
                    pictureBox1.Image = Properties.Resources.IDBlanc;
                    pictureBox2.Image = Properties.Resources.MDPBlanc;
                    checkBox1.ForeColor = Color.White;
                    button1.FlatAppearance.BorderColor = Color.White;
                    button1.BackColor = Color.Black;
                    button1.ForeColor = Color.White;
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
        }

        private void Connexion_Load(object sender, EventArgs e)
        {
            label4.Left = -label4.Width;
            ModeCS();
            if ((bool)Properties.Settings.Default["SeRappeler"])
            {
                checkBox1.Checked = true;
                label1.Select();
                textBox1.Text = Properties.Settings.Default["Utilisateur"].ToString();
                if (textBox1.Text.Equals("admin"))
                {
                    textBox2.Text = Properties.Settings.Default["MDPAdmin"].ToString();
                }
                else
                {
                    outils.cm = new SqlCommand("SELECT MotPasse FROM Employé WHERE ID = '" + textBox1.Text + "'", outils.cn);
                    outils.cn.Open();
                    outils.dr = outils.cm.ExecuteReader();
                    if (outils.dr.Read()) textBox2.Text = outils.dr[0].ToString();
                    outils.dr.Close();
                    outils.cn.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Veuillez saisir l'ID et le mot de passe.", "Se connecter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (textBox1.Text.ToLower().Equals("admin"))
                {
                    if (textBox2.Text.Equals(Properties.Settings.Default["MDPAdmin"].ToString()))
                    {
                        Properties.Settings.Default["Utilisateur"] = "admin";
                        Properties.Settings.Default.Save();
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox1.Select();
                        Hide();
                        try
                        {
                            mp.Show();
                        }
                        catch
                        {
                            mp = new MenuPrincipale();
                            mp.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mot de passe incorrecte.", "Se connecter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (ID.IsMatch(textBox1.Text.Trim()))
                    {
                        try
                        {
                            if (outils.Remplie("SELECT * FROM Employé WHERE ID = " + outils.NiceID(textBox1.Text.Trim()) + " AND MotPasse = '" + textBox2.Text + "'"))
                            {
                                Properties.Settings.Default["Utilisateur"] = outils.NiceID(textBox1.Text.Trim());
                                Properties.Settings.Default.Save();
                                textBox1.Clear();
                                textBox2.Clear();
                                textBox1.Select();
                                Hide();
                                try
                                {
                                    mp.Show();
                                }
                                catch
                                {
                                    mp = new MenuPrincipale();
                                    mp.Show();
                                }
                            }
                            else
                            {
                                MessageBox.Show("ID ou mot de passe incorrecte.", "Se connecter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Une erreur est survenue. Veuillez vous assurer que le nom du serveur est entré correctement, et la base de données fonctionne proprement.", "Se connecter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("ID incorrecte.", "Se connecter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Left = (label4.Left < Width) ? label4.Left + 1 : -label4.Width;
            ModeCS();
            checkBox1.Checked = ((bool)Properties.Settings.Default["SeRappeler"]) ? true : false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default["SeRappeler"] = (checkBox1.Checked) ? true : false;
            Properties.Settings.Default.Save();
        }
    }
}
