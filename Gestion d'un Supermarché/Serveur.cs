using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Gestion_d_un_Supermarché
{
    public partial class Serveur : Form
    {
        public Serveur()
        {
            InitializeComponent();
        }

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                BackColor = Color.White;
                pictureBox1.Image = Properties.Resources.ServeurNoir;
                label1.ForeColor = Color.Black;
                button1.ForeColor = Color.White;
                textBox1.BackColor = Color.White;
                textBox1.ForeColor = Color.Black;
            }
            else
            {
                BackColor = Color.Black;
                pictureBox1.Image = Properties.Resources.ServeurBlanc;
                label1.ForeColor = Color.White;
                button1.ForeColor = Color.Black;
                textBox1.BackColor = Color.FromArgb(31, 31, 31);
                textBox1.ForeColor = Color.White;
            }
        }

        private void Serveur_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default["NomServeur"].ToString();
            ModeCS();
        }

        private void Serveur_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuParametres"].Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cn = new SqlConnection("DATA SOURCE = " + textBox1.Text.Trim() + "; INTEGRATED SECURITY = SSPI; INITIAL CATALOG = Gestion_d_un_Supermarché");
                cn.Open();
                cn.Close();
                Properties.Settings.Default["NomServeur"] = textBox1.Text.Trim();
                Properties.Settings.Default.Save();
                MessageBox.Show("Connection au serveur et à la base de données réussie.", "Serveur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch
            {
                MessageBox.Show("Une erreur est survenue. Veuillez vous assurer que le nom du serveur est entré correctement, et la base de données fonctionne proprement.", "Serveur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
