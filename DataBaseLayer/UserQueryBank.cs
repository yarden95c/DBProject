using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    class UserQueryBank
    {
        public static MySqlCommand GetSimpleUserQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT * FROM users where email=@email and password = @password";
            command.Parameters.AddWithValue("@email", "");
            command.Parameters.AddWithValue("@password", "");
            command.Prepare();

            return command;
        }

        public static MySqlCommand GetUserSongsQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT id_song FROM favoritesongbyuser where id_user=@userId";
            command.Parameters.AddWithValue("@userId", -1);
            command.Prepare();

            return command;
        }

        public static MySqlCommand GetUserArtistsQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT id_artist FROM favoriteartistbyuser where id_user=@userId";
            command.Parameters.AddWithValue("@userId", -1);
            command.Prepare();

            return command;
        }

    }
}
