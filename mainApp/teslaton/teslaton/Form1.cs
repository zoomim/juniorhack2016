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

namespace teslaton
{

    public partial class Form1 : Form
    {
        string connection_string = @"Server=tcp:uzubase.database.windows.net,1433;Initial Catalog=Dbase;Persist Security Info=False;User ID=uzuyami;Password=HoSEk258;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public DataTable tabulka;

    public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabulka = GetResultsTable();
            dataGridView1.DataSource = tabulka;
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
            while (myReader.Read())
            {
                tabulka.Rows.Add(myReader[0].ToString(), myReader[1].ToString(), myReader[2].ToString(), myReader[3].ToString(), myReader[4].ToString(), myReader[5].ToString());
            }


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
            return table;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 urlform = new Form2();
            urlform.Show();
        }
    }
}
