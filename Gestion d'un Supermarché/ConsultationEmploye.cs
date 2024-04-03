using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class ConsultationEmploye : Form
    {
        public ConsultationEmploye()
        {
            InitializeComponent();
        }

        readonly Outils outils = new Outils();

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                contextMenuStrip1.BackColor = Color.Black;
                importFromXMLFileToolStripMenuItem.BackColor = Color.White;
                importFromXMLFileToolStripMenuItem.ForeColor = Color.Black;
                BackColor = Color.White;
                dataGridView1.BackgroundColor = Color.White;
                dataGridView1.DefaultCellStyle.BackColor = Color.White;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Black;
                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            }
            else
            {
                contextMenuStrip1.BackColor = Color.White;
                importFromXMLFileToolStripMenuItem.BackColor = Color.Black;
                importFromXMLFileToolStripMenuItem.ForeColor = Color.White;
                BackColor = Color.Black;
                dataGridView1.BackgroundColor = Color.Black;
                dataGridView1.DefaultCellStyle.BackColor = Color.Black;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
                dataGridView1.DefaultCellStyle.ForeColor = Color.White;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }

        void EnTete()
        {
            string[] nc = { "ID", "Nom", "Prénom", "Age", "Sexe", "Téléphone", "Email", "Mot de passe" };
            for (int i = 0; i < 8; i++)
            {
                dataGridView1.Columns[i].HeaderText = nc[i];
            }
        }

        private void ConsultationEmploye_Load(object sender, EventArgs e)
        {
            ModeCS();
            dataGridView1.DataSource = outils.RemplirDataGridView("SELECT * FROM Employé");
            EnTete();
        }

        private void ConsultationEmploye_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuConsultationTableau"].Show();
        }

        private void importFromXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "XSD files|*.xsd";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fichierXSD = openFileDialog1.FileName;
                    openFileDialog1.Filter = "XML files|*.xml";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string fichierXML = openFileDialog1.FileName;
                        dataGridView1.DataSource = outils.ImporterXML("Employé", fichierXML, fichierXSD);
                        EnTete();
                    }
                }
                catch
                {
                    MessageBox.Show("Veuillez choisir des fichiers XSD et XML valides.", "Importation des employés", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
