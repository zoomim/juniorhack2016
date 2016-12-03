using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace teslaton
{

    public partial class Form1 : Form
    {
        string connection_string = @"Server=tcp:uzubase.database.windows.net,1433;Initial Catalog=Dbase;Persist Security Info=False;User ID=uzuyami;Password=HoSEk258;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public DataTable tabulka;

        public static string sirka;
        public static string delka;



    public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabulka = GetResultsTable();
            dataGridView1.DataSource = tabulka;

            // sirka = 49°12'53.2
            // delka = 16°33'40.2
            nacti_data();
        }

        private void nacti_data()
        {
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            if (connection.State == System.Data.ConnectionState.Open)
                label1.Text = "Connection Succeeded!";
            else
                label1.Text = "Connection Failure!";


            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from dbo.popelnice",
                                                     connection);
            myReader = myCommand.ExecuteReader();

            tabulka.Rows.Clear();
            while (myReader.Read())
            {
                tabulka.Rows.Add(myReader[0].ToString(), myReader[1].ToString(), myReader[2].ToString(), myReader[3].ToString(), myReader[4].ToString(), myReader[5].ToString(), myReader[6].ToString(), myReader[7].ToString());
            }

            connection.Close();
        }

        public DataTable GetResultsTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Naplňenost", typeof(string));
            table.Columns.Add("Převrácení", typeof(string));
            table.Columns.Add("Požár", typeof(string));
            table.Columns.Add("Lokace šířka", typeof(string));
            table.Columns.Add("Lokace délka", typeof(string));
            table.Columns.Add("Čas", typeof(string));
            table.Columns.Add("Datum", typeof(string));
            return table;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 urlform = new Form2();


            int index = dataGridView1.CurrentCell.RowIndex;
            DataRow radek = tabulka.Rows[index];
            sirka = radek[4].ToString();
            delka = radek[5].ToString();
            urlform.Show();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nacti_data();
            label1.Text = "Aktualizovano " + DateTime.Now;

            string zapalene = "";
            string povalene = "";
            int index = dataGridView1.Rows.Count - 1; //Pocet radku

            for (int i = 0; i <= index;i++)
            {
                DataRow radek = tabulka.Rows[i];
                if (radek[3].ToString() == "1")
                {
                    zapalene += " " + radek[0].ToString();
                }

                if (radek[2].ToString() == "1")
                {
                    povalene += " " + radek[0].ToString();
                }
            }

            if (zapalene != "")
            {
                MessageBox.Show("Hoří popelnice s čísly :" + zapalene);
            }

            if (povalene != "")
            {
                label3.Text = povalene;
            }
            else
            {
                label3.Text = "Nejsou";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.Rows.Count - 1;
            DataRow radek = tabulka.Rows[index];
            int pocet = int.Parse(radek[0].ToString());
            pocet += 1;


            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

             SqlCommand myCommand = new SqlCommand("INSERT INTO dbo.popelnice (Id, naplnenost) " + "VALUES (" + pocet.ToString() + ", 0)", connection);

            myCommand.ExecuteNonQuery();
            connection.Close();

            nacti_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> lines = new List<string>();
            int index = dataGridView1.Rows.Count - 1; //Pocet radku

            for (int i = 0;i <= index;i++)
            {
                DataRow radek = tabulka.Rows[i];
                int naplnenost = 0;
                if (radek[1].ToString() != null)
                naplnenost = int.Parse(radek[1].ToString());
                if (naplnenost > 75)
                {
                    lines.Add("Index: " + radek[0].ToString() + " Naplněnost: " + radek[1].ToString() + " Lokace šířka: " + radek[4].ToString() + " Lokace délka: " + radek[5].ToString());
                }
            }

            if (lines.Count > 0)
            {
                System.IO.File.WriteAllLines(Path.GetDirectoryName(Application.ExecutablePath) + "\\vydejka.txt", lines);
                MessageBox.Show("Vytisknuto !");
            }
       }
    }
}
