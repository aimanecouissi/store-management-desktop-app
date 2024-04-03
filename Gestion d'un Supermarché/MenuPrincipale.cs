using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gestion_d_un_Supermarché
{
    public partial class MenuPrincipale : Form
    {
        public MenuPrincipale()
        {
            InitializeComponent();
        }

        MenuMaj mm;
        MenuRecherche mr;
        MenuConsultationType mc;
        MenuExportationType me;
        MenuParametres mp = new MenuParametres();
        readonly About ab = new About();
        bool faiteC = false, faiteS = false;

        void ModeCS()
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                faiteS = false;
                if (!faiteC)
                {
                    faiteC = true;
                    BackColor = Color.White;
                    pictureBox1.Image = Properties.Resources.MàjLeave;
                    pictureBox2.Image = Properties.Resources.RechercheLeave;
                    pictureBox3.Image = Properties.Resources.ConsultationLeave;
                    pictureBox4.Image = Properties.Resources.ExportationLeave;
                    pictureBox5.Image = Properties.Resources.ParamètresLeave;
                    pictureBox6.Image = Properties.Resources.AboutLeave;
                    foreach (Control control in Controls)
                    {
                        if (control is Label) control.ForeColor = Color.Black;
                    }
                    label1.ForeColor = Color.DimGray;
                }
            }
            else
            {
                faiteC = false;
                if (!faiteS)
                {
                    faiteS = true;
                    BackColor = Color.Black;
                    pictureBox1.Image = Properties.Resources.MàjLeaveSombre;
                    pictureBox2.Image = Properties.Resources.RechercheLeaveSombre;
                    pictureBox3.Image = Properties.Resources.ConsultationLeaveSombre;
                    pictureBox4.Image = Properties.Resources.ExportationLeaveSombre;
                    pictureBox5.Image = Properties.Resources.ParamètresLeaveSombre;
                    pictureBox6.Image = Properties.Resources.AboutLeaveSombre;
                    foreach (Control control in Controls)
                    {
                        if (control is Label) control.ForeColor = Color.White;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mm = new MenuMaj();
            mr = new MenuRecherche();
            mc = new MenuConsultationType();
            me = new MenuExportationType();
            label1.Left = -label1.Width;
            ModeCS();
        }

        private void MenuPrincipale_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            try
            {
                mm.Show();
            }
            catch
            {
                mm = new MenuMaj();
                mm.Show();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
            try
            {
                mr.Show();
            }
            catch
            {
                mr = new MenuRecherche();
                mr.Show();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Hide();
            try
            {
                mc.Show();
            }
            catch
            {
                mc = new MenuConsultationType();
                mc.Show();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["Utilisateur"].ToString().Equals("admin"))
            {
                Hide();
                try
                {
                    me.Show();
                }
                catch
                {
                    me = new MenuExportationType();
                    me.Show();
                }
            }
            else
            {
                MessageBox.Show("Seul le directeur peut accéder à cette forme.", "Exportation des employés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Hide();
            mp.ShowDialog();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ab.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Left = (label1.Left < Width) ? label1.Left + 1 : -label1.Width;
            ModeCS();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox1.Image = Properties.Resources.MàjEnter;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.MàjEnterSombre;
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox1.Image = Properties.Resources.MàjLeave;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.MàjLeaveSombre;
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox2.Image = Properties.Resources.RechercheEnter;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.RechercheEnterSombre;
            }
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox2.Image = Properties.Resources.RechercheLeave;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.RechercheLeaveSombre;
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox3.Image = Properties.Resources.ConsultationEnter;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.ConsultationEnterSombre;
            }
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox3.Image = Properties.Resources.ConsultationLeave;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.ConsultationLeaveSombre;
            }
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox4.Image = Properties.Resources.ExportationEnter;
            }
            else
            {
                pictureBox4.Image = Properties.Resources.ExportationEnterSombre;
            }
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox4.Image = Properties.Resources.ExportationLeave;
            }
            else
            {
                pictureBox4.Image = Properties.Resources.ExportationLeaveSombre;
            }
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox5.Image = Properties.Resources.ParamètresEnter;
            }
            else
            {
                pictureBox5.Image = Properties.Resources.ParamètresEnterSombre;
            }
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox5.Image = Properties.Resources.ParamètresLeave;
            }
            else
            {
                pictureBox5.Image = Properties.Resources.ParamètresLeaveSombre;
            }
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox6.Image = Properties.Resources.AboutEnter;
            }
            else
            {
                pictureBox6.Image = Properties.Resources.AboutEnterSombre;
            }
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            if (!(bool)Properties.Settings.Default["ModeSombre"])
            {
                pictureBox6.Image = Properties.Resources.AboutLeave;
            }
            else
            {
                pictureBox6.Image = Properties.Resources.AboutLeaveSombre;
            }
        }
    }
}
