using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Data.SqlClient;
using System.Data;

namespace nahrada_wifi
{
    class Program
    {
        string ConnectionString = @"Server=tcp:uzubase.database.windows.net,1433;Initial Catalog=Dbase;Persist Security Info=False;User ID=uzuyami;Password=HoSEk258;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        string updateString = "UPDATE dbo.popelnice SET naplnenost = @napln, prevraceni = @prev, pozar = @pozar, lokace_sirka = @sir, lokace_delka = @del, cas = @cas, datum = @datum  WHERE Id = @ID";
        //string insertString1 = "INSERT dbo.data SET naplnenost = @napln, prevraceni = @prev, pozar = @pozar, lokace_sirka = @sir, lokace_delka = @del, cas = @cas, datum = @datum";
        string insertString = "INSERT INTO dbo.data(Id_popelnice,naplnenost, prevraceni, pozar, lokace_sirka, lokace_delka, cas, datum) VALUES(@ID_p, @napln, @prev, @pozar , @sir, @del,@cas,@datum)";
        bool isconnected = false;

        SqlConnection connection;
        SqlCommand command;

        public void SQLConnect()
        {
            connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                isconnected = true;
            }
            catch
            {
                //isconnected = false;
                connection.Dispose();
                throw new Exception("Connection failed!");
            }
        }

        public void SQLDisconnect()
        {
            if (isconnected) connection.Close();
        }

        void CmdExecute(string ErrMsg)
        {
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine(ErrMsg);
            }
        }


        void SQLupdate(string[] line)
        {
            command = new SqlCommand(updateString, connection);

            command.Parameters.AddWithValue("@napln", line[1]);
            command.Parameters.AddWithValue("@prev", line[2]);
            command.Parameters.AddWithValue("@pozar", line[3]);
            command.Parameters.AddWithValue("@sir", line[4]);
            command.Parameters.AddWithValue("@del", line[5]);
            command.Parameters.AddWithValue("@cas", DateTime.Now.ToString("HH:mm:ss"));
            command.Parameters.AddWithValue("@datum", DateTime.Now.ToString("yyyy:MM:dd"));

            command.Parameters.AddWithValue("@ID", line[0]);

            CmdExecute("Update Failed");

            command.Dispose();
        }

        void SQLinsert(string[] line)
        {
            command = new SqlCommand(insertString, connection);

            command.Parameters.AddWithValue("@ID_p", line[0]);
            command.Parameters.AddWithValue("@napln", line[1]);
            command.Parameters.AddWithValue("@prev", line[2]);
            command.Parameters.AddWithValue("@pozar", line[3]);
            command.Parameters.AddWithValue("@sir", line[4]);
            command.Parameters.AddWithValue("@del", line[5]);
            command.Parameters.AddWithValue("@cas", DateTime.Now.ToString("HH:mm:ss"));
            command.Parameters.AddWithValue("@datum", DateTime.Now.ToString("yyyy:MM:dd"));

            CmdExecute("Insert Failed");

            command.Dispose();
        }

        static void Main(string[] args)
        {
            SerialPort port = new SerialPort("COM12", 9600, Parity.None, 8, StopBits.One);
            Program prog = new Program();

            port.Open();

            while (true) {
                string read = port.ReadLine();
                if (read != null)
                {
                    string[] readAr = read.Split(';');
                    prog.SQLConnect();
                    prog.SQLupdate(readAr);
                    prog.SQLinsert(readAr);
                    prog.SQLDisconnect();
                    foreach (string r in readAr)
                    {
                        Console.WriteLine(r);
                    }
                }
            }
        }
        
        
    }
}
