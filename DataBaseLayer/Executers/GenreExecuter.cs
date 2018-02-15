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
        /// Initializes a new instance of the <see cref="GenreExecuter" /> class.
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
        /// <returns>
        /// Genre instance
        /// </returns>
        private Genre GetGenre()
        {
            return Entities.EntitiesFactory.GetGenreFromGenreId(user.GenreId, conn);
        }

        /// <summary>
        /// Gets the genre name by identifier.
        /// </summary>
        /// <param name="genreID">The genre identifier.</param>
        /// <param name="conn">The connection.</param>
        /// <returns></returns>
        public static string GetGenreNameByID(int genreID, DataBaseConnector conn)
        {
            return Entities.EntitiesFactory.GetGenreFromGenreId(genreID, conn).Name;
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
