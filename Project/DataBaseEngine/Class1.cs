using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace DataBaseEngine
{
    public class DataBaseEngine
    {
        private SqlConnection sqlConn;
        private MySqlConnection mySqlConn;
        private MySqlCommand myCommand;
        public DataBaseEngine()
        {
            sqlConn = new SqlConnection("user id=root;" +
                                       "password=root;server=127.0.0.1:3306;" +
                                       "Trusted_Connection=yes;" +
                                       "database=music;");
            mySqlConn = new MySqlConnection("Server = 127.0.0.1; Port = 3306; Database = music_fact_generator; Uid = root; Pwd = root;");


                mySqlConn.Open();
                myCommand = new MySqlCommand();

         
        }

        public MySqlDataReader ExecuteSimpleQuery(string tableName, List<string> columns, string condition,int limit)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ");
            foreach (string column in columns)
            {
                stringBuilder.Append(column + ",");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append(" from " + tableName);
            stringBuilder.Append(" " + condition);
            stringBuilder.Append(" limit " + limit);

            myCommand.Connection = mySqlConn;
            myCommand.CommandText = stringBuilder.ToString();
            return myCommand.ExecuteReader();
        }


        public MySqlDataReader ExecuteSimpleQuery(string query)
        {
            MySqlCommand myCommand = new MySqlCommand(query, mySqlConn);
            return myCommand.ExecuteReader();
        }

        public void Stop()
        {
            try
            {
                mySqlConn.Close();
            } catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

   
}
