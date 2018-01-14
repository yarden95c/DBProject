using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    public class SignInExecuter
    {
        private string email;
        private string password;
        private DataBaseConnector conn;
        public SignInExecuter(string email,string password,DataBaseConnector conn)
        {
            this.email = email;
            this.password = password;
            this.conn = conn;
        }

        public User Execute()
        {
            MySqlCommand getUser = UserQueryBank.GetSimpleUserQuery(conn.Connection);
            getUser.Parameters["@email"].Value = email;
            getUser.Parameters["@password"].Value = password;
            List<Dictionary<string, string>> result = conn.ExecuteCommand(getUser);
            if(result.Count == 0)
            {
                return null;
            }

            User user = new User();
            Dictionary<string, string> userAsDictionary = result[0];
            user.Id = int.Parse(userAsDictionary["id_user"]);
            user.FirstName = userAsDictionary["first_name"];
            user.LastName = userAsDictionary["last_name"];
            user.Email = userAsDictionary["email"];
            user.PlaceId = int.Parse(userAsDictionary["id_area"]);
            user.GenreId = int.Parse(userAsDictionary["id_favorite_genre"]);
            user.Year = int.Parse(userAsDictionary["birthday_year"]);
            user.Day = int.Parse(userAsDictionary["birthday_day"]);
            user.Month = int.Parse(userAsDictionary["birthday_month"]);

            MySqlCommand getSongs = UserQueryBank.GetUserSongsQuery(conn.Connection);
            getSongs.Parameters["@userId"].Value = user.Id;
            List<string> songs = conn.ExecuteOneColumnCommand(getSongs);
            user.Songs = songs;

            MySqlCommand getArtists = UserQueryBank.GetUserArtistsQuery(conn.Connection);
            getArtists.Parameters["@userId"].Value = user.Id;
            List<string> artists = conn.ExecuteOneColumnCommand(getArtists);
            foreach(string artist in artists)
            {
                user.AddArtist(int.Parse(artist));
            }

            return user;
        }
    }
}
