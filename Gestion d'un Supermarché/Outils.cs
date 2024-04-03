using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Gestion_d_un_Supermarché
{
    class Outils
    {
        public SqlConnection cn = new SqlConnection("DATA SOURCE = " + Properties.Settings.Default["NomServeur"].ToString() + "; INTEGRATED SECURITY = SSPI; INITIAL CATALOG = Supermarché");
        public SqlCommand cm;
        public SqlDataReader dr;
        public SqlDataAdapter da;
        public DataSet ds = new DataSet("GestionSupermarché");

        public void ActualiserDataSet(string NomTable)
        {
            da = new SqlDataAdapter("SELECT * FROM " + NomTable, cn);
            da.Fill(ds, NomTable);
            ds.Tables[NomTable].Clear();
            da = new SqlDataAdapter("SELECT * FROM " + NomTable, cn);
            da.Fill(ds, NomTable);
        }

        public DataTable RemplirDataGridView(string Requete)
        {
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(Requete, cn);
            da.Fill(dt);
            return dt;
        }

        public bool Remplie(string Requete)
        {
            cn.Open();
            cm = new SqlCommand(Requete, cn);
            dr = cm.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Close();
                cn.Close();
                return true;
            }
            else
            {
                dr.Close();
                cn.Close();
                return false;
            }
        }

        public int NombreLigne(string NomTable)
        {
            int nl = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM " + NomTable, cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                nl++;
            }
            dr.Close();
            cn.Close();
            return nl;
        }

        public List<int> Position(string NomTable, string ValeurID)
        {
            ActualiserDataSet(NomTable);
            List<int> ps = new List<int> { -1 };
            for (int i = 0; i < ds.Tables[NomTable].Rows.Count; i++)
            {
                if (ds.Tables[NomTable].Rows[i][0].ToString() == ValeurID)
                {
                    if (ps[0] == -1)
                    {
                        ps[0] = i;
                    }
                    else
                    {
                        ps.Add(i);
                    }
                }
            }
            return ps;
        }

        public void ExecuterRequete(string Requete)
        {
            cm = new SqlCommand(Requete, cn);
            cn.Open();
            cm.ExecuteNonQuery();
            cn.Close();
        }

        public List<int> Navigation(string NomTable, string ValeurID, int Direction)
        {
            ActualiserDataSet(NomTable);
            List<int> ps = new List<int>();
            if (Direction.Equals(1))
            {
                ps.Add(0);
            }
            else if (Direction.Equals(2))
            {
                if (Position(NomTable, ValeurID)[0] > 0)
                {
                    ps.Add(Position(NomTable, ValeurID)[0] - 1);
                }
                else
                {
                    ps.Add(ds.Tables[NomTable].Rows.Count - 1);
                }
            }
            else if (Direction.Equals(3))
            {
                if ((Position(NomTable, ValeurID)[0] < ds.Tables[NomTable].Rows.Count - 1) && Position(NomTable, ValeurID)[0] > -1)
                {
                    ps.Add(Position(NomTable, ValeurID)[0] + 1);
                }
                else
                {
                    ps.Add(0);
                }
            }
            else
            {
                ps.Add(ds.Tables[NomTable].Rows.Count - 1);
            }
            return ps;
        }

        public string IDSuivant(string NomTable, string ValeurID)
        {
            string ids;
            if (Position(NomTable, ValeurID)[0] > 0)
                ids = ds.Tables[NomTable].Rows[Position(NomTable, ValeurID)[0] - 1][0].ToString();
            else if (Position(NomTable, ValeurID)[0] == 0 && NombreLigne(NomTable) >= 2)
                ids = ds.Tables[NomTable].Rows[Position(NomTable, ValeurID)[0] + 1][0].ToString();
            else
                ids = string.Empty;
            return ids;
        }

        public string NiceID(string Textbox)
        {
            string nice = string.Empty;
            if (Textbox.Length < 4)
            {
                int tbl = 4 - Textbox.Length;
                for (int i = 1; i <= tbl; i++)
                {
                    nice += "0";
                }
                return nice + Textbox;
            }
            else
            {
                return Textbox;
            }
        }

        public DataTable ImporterXML(string NomTable, string FichierXML, string FichierXSD)
        {
            DataTable dt = new DataTable(NomTable);
            dt.ReadXmlSchema(FichierXSD);
            dt.ReadXml(FichierXML);
            return dt;
        }

        public void ExporterHTML(string Requete, string Chemin, string Liste, string Couleur1, string Couleur2 = "white")
        {
            DataTable dt = RemplirDataGridView(Requete);
            StreamWriter sw = new StreamWriter(Chemin);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<html>");
            sb.AppendFormat("<head>");
            sb.AppendFormat("<title>");
            sb.AppendFormat("Liste des {0}", Liste);
            sb.AppendFormat("</title>");
            sb.AppendFormat("<style>");
            sb.AppendFormat("td, th {{text-align: center; padding: 5px;}}");
            sb.AppendFormat("tr:nth-child(even) {{background-color: lightgray;}}");
            sb.AppendFormat("tr:hover {{background-color: black; color: white;}}");
            sb.AppendFormat("</style>");
            sb.AppendFormat("</head>");
            sb.AppendFormat("<body style = 'font-family: calibri;'>");
            sb.AppendFormat("<h1 align = 'center'>LISTE DES {0}</h1>", Liste);
            sb.AppendFormat("<table style = 'border-collapse: collapse; width: 100%;'>");
            sb.AppendFormat("<tr>");
            foreach (DataColumn dc in dt.Columns)
            {
                sb.AppendFormat("<th style = 'background-color: {0}; color: {1};'>{2}</th>", Couleur1, Couleur2, dc.ColumnName.ToUpper());
            }
            sb.AppendFormat("</tr>");
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendFormat("<tr>");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.AppendFormat("<td>{0}</td>", dr[i].ToString());
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</table>");
            sb.AppendFormat("</body>");
            sb.AppendFormat("</html>");
            sw.WriteLine(sb.ToString());
            sw.Close();
        }
    }
}
