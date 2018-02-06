using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// GenreExecuter - this class reprsent the executer of the genre query.
    /// </summary>
    /// <seealso cref="DataBaseLayer.IExecuter" />
    public class GenreExecuter : IExecuter
    {
        /// <summary>
        /// The genre name
        /// </summary>
        private string genreName;
        /// <summary>
        /// The database connector
        /// </summary>
        private DataBaseConnector conn;
        /// <summary>
        /// The user
        /// </summary>
        private User user;
        /// <summary>
        /// The queries list
        /// </summary>
        private HeuristicsBank.GenreHeuristics queriesList;
        /// <summary>
        /// The random
        /// </summary>
        private static Random rand = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="GenreExecuter"/> class.
        /// </summary>
        /// <param name="genreName">Name of the genre.</param>
        /// <param name="conn">The connection.</param>
        /// <param name="user">The user.</param>
        public GenreExecuter(string genreName, DataBaseConnector conn, User user)
        {
            this.genreName = genreName;
            this.conn = conn;
            this.user = user;
            this.queriesList = HeuristicsBank.GetGenreHeuristics(this.conn);
        }

        /// <summary>
        /// Gets the genre entity.
        /// </summary>
        /// <param name="genre">The genre name.</param>
        /// <returns>Genre instance</returns>
     /*   private Genre GetGenre(string genre)
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
        } */

        /// <summary>
        /// Gets the genre entity, that represent the user's favourite genre.
        /// </summary>
        /// <returns>Genre instance</returns>
        private Genre GetGenre()
        {
            /* MySqlCommand command = new MySqlCommand();
             command.Connection = conn.Connection;
             command.CommandText = "select genere_name from genres where id_genre = " + user.GenreId;
             List<string> result = conn.ExecuteOneColumnCommand(command);
             if (result.Count <= 0)
             {
                 return null;
             }

             Genre genre = new Genre(result[0], user.GenreId);
             return genre; */
            return Entities.EntitiesFactory.GetGenreFromGenreId(user.GenreId, conn);
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <returns>
        /// string that reprsent the result
        /// </returns>
        public string Execute()
        {
            Genre genre = null;
            if (!genreName.Equals(string.Empty))
            {
                genre = Entities.EntitiesFactory.GetGenreFromGenreName(genreName, conn);
                if (genre == null)
                {
                    return "No such genre";
                }
            }

            Delegate[] arr = queriesList.GetInvocationList();
            int queryNum = rand.Next(arr.Length);
            SelfExecuterHeuristics h = ((HeuristicsBank.GenreHeuristics)arr[queryNum])(user, genre);
            return h.Executer(conn);
        }

        /// <summary>
        /// Gets the string that repsent that the query returned nothing.
        /// </summary>
        /// <returns>
        /// the sorry message
        /// </returns>
        public string GetSorryMsg()
        {
            return string.Empty;
        }
    }
}
