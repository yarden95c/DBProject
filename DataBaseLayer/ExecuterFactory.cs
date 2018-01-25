using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    /// <summary>
    /// ExecuterFactory - this class creates a random executer.
    /// </summary>
    public class ExecuterFactory
    {
        /// <summary>
        /// The random
        /// </summary>
        private static Random rand = new Random();
        /// <summary>
        /// This delegate represent a method that create an executer.
        /// </summary>
        /// <returns></returns>
        private delegate IExecuter CreateExecuter();

        /// <summary>
        /// Creates the an executer according to the specified user and database connector.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="db">The database connector.</param>
        /// <returns> CreateExecuter delegate </returns>
        private static CreateExecuter Create(User user, DataBaseConnector db)
        {
            CreateExecuter create = () =>
            {
                return new GenreExecuter(RandomRecordsBank.GetRandomGenreName(), db, user);
            };

            create += () =>
            {
                return new NumberExecuter(user, db);
            };

            create += () =>
            {
                return new PlaceExecuter(RandomRecordsBank.GetRandomPlaceName(), db, user);
            };

            create += () =>
            {
                Dictionary<string, int> years = RandomRecordsBank.GetRandomYears();
                return new SimpleArtistExecuter(db, "", RandomRecordsBank.GetRandomArtistName(), years["from"], years["to"]);
            };

            create += () =>
            {
                return new SimplePlaceExecuter(db, RandomRecordsBank.GetRandomPlaceName(), "");
            };

            create += () =>
            {
                var executer = new SimpleSongExecuter(db);
                executer.SetQuery(RandomRecordsBank.GetRandomSongName(), "", 0, 9999);
                return executer;
            };

            create += () =>
            {
                Dictionary<string, int> years = RandomRecordsBank.GetRandomYears();
                return new YearExecuter(db, years["from"], years["to"], user);
            };

            create += () =>
            {
                return new NumberExecuter(user, db);
            };

            return create;
        }

        /// <summary>
        /// Create random executer.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="db">The database connector.</param>
        /// <returns> random executer </returns>
        public static IExecuter GetExecuter(User user, DataBaseConnector db)
        {
            Delegate[] arr = Create(user,db).GetInvocationList();
            int executerNum = rand.Next(arr.Length);
            return ((CreateExecuter)arr[executerNum])();
        }
    }
}
