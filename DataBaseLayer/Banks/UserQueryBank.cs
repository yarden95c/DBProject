using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// UserQueryBank - this class is a bank for different kinds of query about a user.
    /// </summary>
    class UserQueryBank
    {
        /// <summary>
        /// Gets a simple user query.
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <returns>MySqlCommand - simple user query</returns>
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

        /// <summary>
        /// Gets user's songs query.
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <returns>MySqlCommand - user's songs query</returns>
        public static MySqlCommand GetUserSongsQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT id_song FROM favoritesongbyuser where id_user=@userId";
            command.Parameters.AddWithValue("@userId", -1);
            command.Prepare();

            return command;
        }

        /// <summary>
        /// Gets user's artists query.
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <returns>MySqlCommand - user's artists query</returns>
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
