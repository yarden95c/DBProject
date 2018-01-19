using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    public class GenreExecuter : IExecuter
    {
        delegate SelfExecuterHeuristics GenreHeuristics(User user, Genre genre);

        private string genreName;
        private DataBaseConnector conn;
        private User user;
        private GenreHeuristics queriesList;
        private static Random rand = new Random();

        public GenreExecuter(string genreName, DataBaseConnector conn, User user)
        {
            this.genreName = genreName;
            this.conn = conn;
            this.user = user;

            queriesList += (User u, Genre g) => {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "select count(id_artist) from genresbyartist where id_genre = " + g.Id;
                command.Connection = conn.Connection;
                string format = "The number of artists tagged as the genre \"" + g.Name + "\" is {0}";
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    int num = connector.ExecuteScalarCommand(command);
                    return string.Format(format, num);
                };
                return new SelfExecuterHeuristics(command, format, del);
            };

            queriesList += (User u, Genre g) => {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "select artist_name,num_of_hits from artists left join genresbyartist using (id_artist) where id_genre = " + g.Id + " order by num_of_hits desc limit 1";
                command.Connection = conn.Connection;
                string format = "The most popular artist that tagged as the genre \"" + g.Name + "\" is {0}, with {1} number of hits in our database";
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<Dictionary<string, string>> result = connector.ExecuteCommand(command);
                    if(result.Count == 0)
                    {
                        return "There are no artists tagged as the genre \"" + g.Name + "\"";
                    }
                    return string.Format(format, result[0]["artist_name"], result[0]["num_of_hits"]);
                };
                return new SelfExecuterHeuristics(command, format, del);
            };
        }

        private Genre GetGenre(string genre)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = "select id_genre from genres where lower(genere_name) = \"" + genre.ToLower() + "\"";
            List<string> result = conn.ExecuteOneColumnCommand(command);
            if (result.Count <= 0)
            {
                return null;
            }

            Genre genreObject = new Genre(genre, int.Parse(result[0]));
            return genreObject;
        }

        private Genre GetGenre()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = "select genere_name from genres where id_genre = " + user.GenreId;
            List<string> result = conn.ExecuteOneColumnCommand(command);
            if (result.Count <= 0)
            {
                return null;
            }

            Genre genre = new Genre(result[0], user.GenreId);
            return genre;
        }

        public string Execute()
        {
            Genre genre;
            if (genreName.Equals(string.Empty))
            {
                genre = GetGenre();
            }
            else
            {
                genre = GetGenre(genreName);
            }

            if (genre == null)
            {
                return "No such genre";
            }

            Delegate[] arr = queriesList.GetInvocationList();
            int queryNum = rand.Next(arr.Length);
            SelfExecuterHeuristics h = ((GenreHeuristics)arr[queryNum])(user,genre);
            return h.Executer(conn);
        }

        public string GetSorryMsg()
        {
            return string.Empty;
        }
    }
}
