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
        string updateString = "UPDATE dbo.popelnice SET naplnenost = @napln, prevraceni = @prev, pozar = @pozar, lokace_sirka = @sir, lokace_delka = @del  WHERE Id = @ID";

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

            command.Parameters.AddWithValue("@ID", line[0]);

            CmdExecute("Update Failed");

            command.Dispose();
        }

        static void Main(string[] args)
        {
            SerialPort port = new SerialPort("COM11", 9600, Parity.None, 8, StopBits.One);
            Program prog = new Program();

            port.Open();
            prog.SQLConnect();

            while (true) {
                string read = port.ReadLine();
                if (read != null)
                {
                    string[] readAr = read.Split(';');
                    prog.SQLupdate(readAr);
                    foreach (string r in readAr)
                    {
                        Console.WriteLine(r);
                    }
                }
            }
        }
        
        
    }
}
