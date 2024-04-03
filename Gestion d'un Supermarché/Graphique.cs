using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class Graphique : Form
    {
        public Graphique()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.Interval = 1;
        }

        readonly Outils outils = new Outils();
        readonly string[] req = { "SELECT CIN, COUNT(Numero) FROM Commande GROUP BY CIN", "SELECT Code, SUM(Quantité) FROM Details_Commande GROUP BY Code", "SELECT Code, QuantitéStock FROM Produit" };

        void RemplirChart(string Requete)
        {
            chart1.Series[0].Points.Clear();
            outils.cn.Open();
            outils.cm = new SqlCommand(Requete, outils.cn);
            outils.dr = outils.cm.ExecuteReader();
            while (outils.dr.Read())
            {
                chart1.Series[0].Points.AddXY(outils.dr[0], outils.dr[1]);
            }
            outils.dr.Close();
            outils.cn.Close();
        }

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                BackColor = Color.White;
                chart1.BackColor = Color.White;
                chart1.ChartAreas[0].BackColor = Color.White;
                chart1.ChartAreas[0].AxisX.LineColor = Color.Black;
                chart1.ChartAreas[0].AxisY.LineColor = Color.Black;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Black;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Black;
                chart1.ChartAreas[0].AxisX.MajorTickMark.LineColor = Color.Black;
                chart1.ChartAreas[0].AxisY.MajorTickMark.LineColor = Color.Black;
                chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
                chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
                chart1.Series[0].LabelBackColor = Color.White;
                chart1.Series[0].LabelForeColor = Color.Black;
                foreach (Control control in Controls)
                {
                    if (control is RadioButton) control.ForeColor = Color.Black;
                }
            }
            else
            {
                BackColor = Color.Black;
                chart1.BackColor = Color.Black;
                chart1.ChartAreas[0].BackColor = Color.Black;
                chart1.ChartAreas[0].AxisX.LineColor = Color.White;
                chart1.ChartAreas[0].AxisY.LineColor = Color.White;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;
                chart1.ChartAreas[0].AxisX.MajorTickMark.LineColor = Color.White;
                chart1.ChartAreas[0].AxisY.MajorTickMark.LineColor = Color.White;
                chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
                chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
                chart1.Series[0].LabelBackColor = Color.Black;
                chart1.Series[0].LabelForeColor = Color.White; foreach (Control control in Controls)
                {
                    if (control is RadioButton) control.ForeColor = Color.White;
                }
            }
        }

        private void Graphique_Load(object sender, EventArgs e)
        {
            radioButton3.Checked = true;
            ModeCS();
            RemplirChart(req[2]);
        }

        private void Graphique_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["MenuConsultationType"].Show();
        }

        private void graphConsultation_Resize(object sender, EventArgs e)
        {
            radioButton1.Left = radioButton2.Left - radioButton1.Width;
            radioButton2.Left = Width / 2 - radioButton2.Width / 2;
            radioButton3.Left = radioButton2.Left + radioButton2.Width;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RemplirChart(req[0]);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RemplirChart(req[1]);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            RemplirChart(req[2]);
        }
    }
}
