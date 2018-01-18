using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    public class PlaceExecuter
    {
        delegate SelfExecuterHeuristics PlaceHeuristics(User user, Place place);

        private string placeName;
        private DataBaseConnector conn;
        private User user;
        private PlaceHeuristics queriesList;
        private static Random rand = new Random();

        public PlaceExecuter(string placeName,DataBaseConnector db,User user)
        {
            this.placeName = placeName;
            this.conn = db;
            this.user = user;

            queriesList += (User u, Place p) =>
             {
                 MySqlCommand command = new MySqlCommand();
                 command.CommandText = "select count(id_artist) from artists where id_area = " + p.Id;
                 command.Connection = conn.Connection;
                 string format = "The number of artists from " + p.Name + " is {0}";
                 SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                 {
                     int num = connector.ExecuteScalarCommand(command);
                     return string.Format(format, num);
                 };
                 return new SelfExecuterHeuristics(command, format,del);
             };

            queriesList += (User u, Place p) =>
            {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "select count(id_artist) from artists left join genresbyartist using(id_artist) where id_area = " + p.Id + " and id_genre = " + u.GenreId;
                command.Connection = conn.Connection;
                string format = "The number of artists tagged as your favourite genere and from " + p.Name + " is {0}";
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    int num = connector.ExecuteScalarCommand(command);
                    return string.Format(format, num);
                };
                return new SelfExecuterHeuristics(command, format, del);
            };

            queriesList += (User u, Place p) =>
            {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "select artist_name from artists where id_area = " + p.Id +" limit 10";
                command.Connection = conn.Connection;
                string format = "These are artists from " + p.Name +":\n{0}";
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<string> result = connector.ExecuteOneColumnCommand(command);
                    if(result.Count==0)
                    {
                        return "There are no artists from " + p.Name;
                    }
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach(string artist in result)
                    {
                        stringBuilder.AppendLine(artist);
                    }
                    return string.Format(format, stringBuilder.ToString());
                };
                return new SelfExecuterHeuristics(command, format, del);
            };

            queriesList += (User u, Place p) =>
            {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "select distinct song_name from songs where lower(song_name) like \"%" + p.Name.ToLower() + "%\" limit 10";
                command.Connection = conn.Connection;
                string format = "These are songs with \"" + p.Name + "\" in there title:\n{0}";
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<string> result = connector.ExecuteOneColumnCommand(command);
                    if (result.Count == 0)
                    {
                        return "There are no songs with \"" + p.Name+"\" in there title";
                    }
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (string song in result)
                    {
                        stringBuilder.AppendLine(song);
                    }
                    return string.Format(format, stringBuilder.ToString());
                };
                return new SelfExecuterHeuristics(command, format, del);
            };

        }


        private Place GetPlace(string placeName)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = "select id_area from area where lower(area_name) = \"" + placeName.ToLower() + "\"";
            List<string> result = conn.ExecuteOneColumnCommand(command);
            if(result.Count <= 0)
            {
                return null;
            }

            Place place = new Place(int.Parse(result[0]), placeName);
            return place;
        }

        private Place GetPlace()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = "select area_name from area where id_area = " + user.PlaceId;
            List<string> result = conn.ExecuteOneColumnCommand(command);
            if (result.Count <= 0)
            {
                return null;
            }

            Place place = new Place(user.PlaceId, result[0]);
            return place;
        }

        public string Execute()
        {
            Place place;
            if(placeName.Equals(string.Empty))
            {
                place = GetPlace();
            } else
            {
                place = GetPlace(placeName);
            }

            if(place == null)
            {
                return "No such place";
            }

            Delegate[] arr = queriesList.GetInvocationList();
            int queryNum = rand.Next(arr.Length);
            SelfExecuterHeuristics h = ((PlaceHeuristics)arr[queryNum])(user,place);
            return h.Executer(conn);



        }
    }
}
