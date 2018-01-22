using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// DataBaseConnector - this is the class that connect the application to the database.
    /// </summary>
    public class DataBaseConnector
    {
        /// <summary>
        /// The database name
        /// </summary>
        private string dbName;
        /// <summary>
        /// The user name
        /// </summary>
        private string userName;
        /// <summary>
        /// The password
        /// </summary>
        private string password;
        /// <summary>
        /// The server name
        /// </summary>
        private string serverName;
        /// <summary>
        /// The port
        /// </summary>
        private string port;
        /// <summary>
        /// The connection
        /// </summary>
        private MySqlConnection conn;
        /// <summary>
        /// The instance
        /// </summary>
        private static DataBaseConnector instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="DataBaseConnector"/> class from being created.
        /// </summary>
        private DataBaseConnector()
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

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns> an instance of this class </returns>
        public static DataBaseConnector GetInstance()
        {
            return instance ?? (instance = new DataBaseConnector());
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public MySqlConnection Connection
        {
            get => this.conn;
        }


        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns> a list of dictionaries, when each dictionary reprsent a record (row) in the database. </returns>
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

        /// <summary>
        /// Executes a one column command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns> a list of the records that was returned from the database. </returns>
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

        /// <summary>
        /// Executes a scalar command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns> a number that reprsent the result of the command. </returns>
        public int ExecuteScalarCommand(MySqlCommand command)
        {
            return int.Parse(command.ExecuteScalar() + "");
        }

        /// <summary>
        /// Stops the connection.
        /// </summary>
        public void StopConnection()
        {
            conn.Close();
        }
    }
}
