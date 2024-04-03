using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class MotPasse : Form
    {
        public MotPasse()
        {
            InitializeComponent();
        }

        readonly Outils outils = new Outils();

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                BackColor = Color.White;
                pictureBox1.Image = Properties.Resources.MDPNoir;
                label1.ForeColor = Color.Black;
                button1.ForeColor = Color.White;
                textBox1.BackColor = Color.White;
                textBox1.ForeColor = Color.Black;
            }
            else
            {
                BackColor = Color.Black;
                pictureBox1.Image = Properties.Resources.MDPBlanc;
                label1.ForeColor = Color.White;
                button1.ForeColor = Color.Black;
                textBox1.BackColor = Color.FromArgb(31, 31, 31);
                textBox1.ForeColor = Color.White;
            }
        }

        private void MotPasse_Load(object sender, EventArgs e)
        {
            ModeCS();
        }

        private void MotPasse_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuParametres"].Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string utilisateur = Properties.Settings.Default["Utilisateur"].ToString();
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez saisir un mot de passe.", "Changer le mot de passe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (utilisateur.Equals("admin"))
                {
                    Properties.Settings.Default["MDPAdmin"] = textBox1.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Mot de passe changé avec succés.", "Changer le mot de passe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    outils.ExecuterRequete("UPDATE Employé SET MotPasse = '" + textBox1.Text + "' WHERE ID = " + utilisateur);
                    MessageBox.Show("Mot de passe changé avec succés.", "Changer le mot de passe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }
    }
}
