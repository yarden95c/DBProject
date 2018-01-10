using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    public class DataBaseConnector
    {
        private string dbName;
        private string userName;
        private string password;
        private string serverName;
        private string port;
        private MySqlConnection conn;
        
        public DataBaseConnector()
        {
            dbName = Properties.DBSettings.Default.DBName;
            userName = Properties.DBSettings.Default.userName;
            password = Properties.DBSettings.Default.password;
            serverName = Properties.DBSettings.Default.serverName;
            port = Properties.DBSettings.Default.port;
            conn = new MySqlConnection("Server = " + serverName + ";" +
                                        "Port = " + port + ";" +
                                        "Database = " + dbName + ";" +
                                        "Uid = " + userName + ";" +
                                        "Pwd = " + password +";"+
                                        "IgnorePrepare = false;"+
                                        "ConnectionTimeout = 60;"
                                       );
            conn.Open();
        }

        public MySqlConnection Connection
        {
            get => this.conn;
        }


        public List<Dictionary<string, string>> ExecuteCommand(MySqlCommand command)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            List<string> colNames = new List<string>();
            MySqlDataReader reader = command.ExecuteReader();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                colNames.Add(reader.GetName(i));
            }

            int index = 0;
            while (reader.Read())
            {
                result.Add(new Dictionary<string, string>());
                foreach(string colName in colNames)
                {
                    
                    result[index][colName] = reader[colName].ToString();
                }
                index++;
            }

            reader.Close();

            return result;
        }

        public List<string> ExecuteOneColumnCommand(MySqlCommand command)
        {
            List<string> result = new List<string>();
            MySqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                result.Add(reader[0].ToString());
            }

            reader.Close();

            return result;
        }

        public void StopConnection()
        {
            conn.Close();
        }
    }
}
