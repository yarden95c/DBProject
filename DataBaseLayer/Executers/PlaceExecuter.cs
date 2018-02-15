using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// PlaceExecuter - this class reprsent the executer of the place query.
    /// </summary>
    /// <seealso cref="DataBaseLayer.IExecuter" />
    public class PlaceExecuter : IExecuter
    {
        /// <summary>
        /// The place name
        /// </summary>
        private string placeName;
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
        private HeuristicsBank.PlaceHeuristics queriesList;
        /// <summary>
        /// The random
        /// </summary>
        private static Random rand = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceExecuter"/> class.
        /// </summary>
        /// <param name="placeName">Name of the place.</param>
        /// <param name="db">The database.</param>
        /// <param name="user">The user.</param>
        public PlaceExecuter(string placeName, DataBaseConnector db, User user)
        {
            this.placeName = placeName;
            this.conn = db;
            this.user = user;
            this.queriesList = HeuristicsBank.GetPlaceHeuristics(this.conn);
        }

        /// <summary>
        /// Gets the place entity, that reprsent the user's place of birth / residence.
        /// </summary>
        /// <returns>Place instance</returns>
        private Place GetPlace()
        {
            return Entities.EntitiesFactory.GetPlaceFromPlaceId(user.PlaceId,conn);
        }

        public static string GetPlaceNameById(int ID, DataBaseConnector conn)
        {
            return Entities.EntitiesFactory.GetPlaceFromPlaceId(ID, conn).Name;
        }
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <returns>
        /// string that reprsent the result
        /// </returns>
        public string Execute()
        {
            Place place = null;
            if (!placeName.Equals(string.Empty))
            {
                place = Entities.EntitiesFactory.GetPlaceFromPlaceName(placeName, conn);
                if (place == null)
                {
                    return "No such place";
                }
            }
            
            Delegate[] arr = queriesList.GetInvocationList();
            int queryNum = rand.Next(arr.Length);
            SelfExecuterHeuristics h = ((HeuristicsBank.PlaceHeuristics)arr[queryNum])(user, place);
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
