using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    public class NumberExecuter
    {
        public delegate Heuristics NumberHeuristics(User user);

        private NumberHeuristics queriesList;
        private User user;
        private DataBaseConnector conn;
        private static Random rand = new Random();

        public NumberExecuter(User user, DataBaseConnector connector)
        {
            this.user = user;
            this.conn = connector;
            queriesList += (User u) => {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "select count(id_artist) from artists where id_area = " + u.PlaceId;
                command.Connection = conn.Connection;
                string format = "The number of artists from your birth place is {0}";
                return new Heuristics(command, format);
            };
            queriesList += (User u) =>
             {
                 MySqlCommand command = new MySqlCommand();
                 command.CommandText = "select count(id_artist) from artists where begin_date_year = " + u.Year;
                 command.Connection = conn.Connection;
                 string format = "The number of artists born at your birth year is {0}";
                 return new Heuristics(command, format);
             };
            queriesList += (User u) =>
            {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "select count(id_artist) from genresbyartist where id_genre = " + u.GenreId;
                command.Connection = conn.Connection;
                string format = "The number of artists from your favourite genere is {0}";
                return new Heuristics(command, format);
            };
            queriesList += (User u) =>
            {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "select count(id_song) from songs where release_date_year = " + u.Year;
                command.Connection = conn.Connection;
                string format = "The number of songs that was released during your birth year is {0}";
                return new Heuristics(command, format);
            };
            queriesList += (User u) =>
            {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "select count(id_artist) from artists where begin_date_month = " + u.Month + " and begin_date_day = " + u.Day;
                command.Connection = conn.Connection;
                string format = "The number of artists born at your birthday is {0}";
                return new Heuristics(command, format);
            };

        }

        public string Execute()
        {
            Delegate[] arr = queriesList.GetInvocationList();
            int queryNum = rand.Next(arr.Length);
            Heuristics h =((NumberHeuristics) arr[queryNum])(user);
            int number = conn.ExecuteScalarCommand(h.Command);
            return string.Format(h.ResultFormat, number);
        }
    }
}
