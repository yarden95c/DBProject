using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DataBaseLayer.Entities;

namespace DataBaseLayer
{
    /// <summary>
    /// RandomRecordsBank - this class is a bank for random records from the database.
    /// </summary>
    public class RandomRecordsBank
    {
        /// <summary>
        /// The randon
        /// </summary>
        private static Random rand = new Random();
        /// <summary>
        /// The DataBase connector
        /// </summary>
        private static DataBaseConnector conn = DataBaseConnector.GetInstance();

        /// <summary>
        /// Gets a name of a random artist.
        /// </summary>
        /// <returns>artist name</returns>
        public static string GetRandomArtistName()
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT artist_name FROM artists ORDER BY RAND() LIMIT 1";
            command.Connection = conn.Connection;
            List<string> result = conn.ExecuteOneColumnCommand(command);
            return result[0];
        }

        /// <summary>
        /// Gets a name of a random song.
        /// </summary>
        /// <returns>song name</returns>
        public static string GetRandomSongName()
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT song_name FROM songs ORDER BY RAND() LIMIT 1";
            command.Connection = conn.Connection;
            List<string> result = conn.ExecuteOneColumnCommand(command);
            return result[0];
        }

        /// <summary>
        /// Gets a name of a random genre.
        /// </summary>
        /// <returns>genre name</returns>
        public static string GetRandomGenreName()
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT genere_name FROM genres ORDER BY RAND() LIMIT 1";
            command.Connection = conn.Connection;
            List<string> result = conn.ExecuteOneColumnCommand(command);
            return result[0];
        }

        /// <summary>
        /// Gets a name of a random place.
        /// </summary>
        /// <returns>place name</returns>
        public static string GetRandomPlaceName()
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT area_name FROM area ORDER BY RAND() LIMIT 1";
            command.Connection = conn.Connection;
            List<string> result = conn.ExecuteOneColumnCommand(command);
            return result[0];
        }

        /// <summary>
        /// Gets a random year.
        /// </summary>
        /// <param name="upperBound">The upper bound.</param>
        /// <returns>year</returns>
        private static int GetRandomYear(int upperBound)
        {
            return rand.Next(upperBound);
        }

        /// <summary>
        /// Gets a random years range.
        /// </summary>
        /// <returns>dictionary that represent a years range- with "from" and "to" keys</returns>
        public static Dictionary<string,int> GetRandomYears()
        {
            Dictionary<string, int> years = new Dictionary<string, int>();
            years["to"] = GetRandomYear(2018);
            years["from"] = GetRandomYear(years["to"]);
            return years;
        }

        public static Song GetRandomSongFromUser(User user)
        {
            int num = rand.Next(user.Songs.Count);
            return EntitiesFactory.GetSongFromSongId(user.Songs[num],conn);
        }

        public static Artist GetRandomArtistFromUser(User user)
        {
            int num = rand.Next(user.Artists.Count);
            return EntitiesFactory.GetArtistFromArtistId(user.Artists[num].ToString(), conn);
        }
    }
}
