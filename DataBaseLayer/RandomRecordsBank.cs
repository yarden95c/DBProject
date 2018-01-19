using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    public class RandomRecordsBank
    {
        private static Random rand = new Random();
        private static DataBaseConnector conn = DataBaseConnector.GetInstance();

        public static string GetRandomArtistName()
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT artist_name FROM artists ORDER BY RAND() LIMIT 1";
            command.Connection = conn.Connection;
            List<string> result = conn.ExecuteOneColumnCommand(command);
            return result[0];
        }

        public static string GetRandomSongName()
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT song_name FROM songs ORDER BY RAND() LIMIT 1";
            command.Connection = conn.Connection;
            List<string> result = conn.ExecuteOneColumnCommand(command);
            return result[0];
        }

        public static string GetRandomGenreName()
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT genere_name FROM genres ORDER BY RAND() LIMIT 1";
            command.Connection = conn.Connection;
            List<string> result = conn.ExecuteOneColumnCommand(command);
            return result[0];
        }

        public static string GetRandomPlaceName()
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT area_name FROM area ORDER BY RAND() LIMIT 1";
            command.Connection = conn.Connection;
            List<string> result = conn.ExecuteOneColumnCommand(command);
            return result[0];
        }

        private static int GetRandomYear(int upperBound)
        {
            return rand.Next(upperBound);
        }

        public static Dictionary<string,int> GetRandomYears()
        {
            Dictionary<string, int> years = new Dictionary<string, int>();
            years["to"] = GetRandomYear(2018);
            years["from"] = GetRandomYear(years["to"]);
            return years;
        }
    }
}
