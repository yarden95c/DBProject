using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// SimplePlaceExecuter - this class reprsent an executer of a simple place query,
    /// for the "I know what I want" button.
    /// </summary>
    /// <seealso cref="DataBaseLayer.IExecuter" />
    public class SimplePlaceExecuter : IExecuter
    {
        /// <summary>
        /// The command
        /// </summary>
        private MySqlCommand command;
        /// <summary>
        /// The database connector
        /// </summary>
        private DataBaseConnector conn;
        /// <summary>
        /// The sorry MSG
        /// </summary>
        private const string sorryMsg = "Sorry, we couldn't find you an answer, please try again with another parameters.";

        /// <summary>
        /// Initializes a new instance of the <see cref="SimplePlaceExecuter"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="placeName">Name of the place.</param>
        /// <param name="artistName">Name of the artist.</param>
        public SimplePlaceExecuter(DataBaseConnector db,string placeName, string artistName)
        {
            this.conn = db;
            bool placePresent = !placeName.Equals(string.Empty);
            bool artistPresent = !artistName.Equals(string.Empty);
            this.command = IKnowWhatIWantQuriesBank.GetPlaceQuery(this.conn.Connection,artistPresent,placePresent);
            if(placePresent)
            {
                command.Parameters["@placeName"].Value = "%" + placeName + "%";
            }
            if(artistPresent)
            {
                command.Parameters["@artistName"].Value = "%" + artistName + "%";
            }
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <returns>
        /// string that reprsent the result
        /// </returns>
        public string Execute()
        {
            List<Dictionary<string, string>> result = conn.ExecuteCommand(command);
            if(result.Count==0)
            {
                return sorryMsg;
            }

            Dictionary<string, List<string>> places = new Dictionary<string, List<string>>();
            foreach(Dictionary<string, string> place in result)
            {
                if(!places.ContainsKey(place["area_name"]))
                {
                    places[place["area_name"]] = new List<string>();
                }
                places[place["area_name"]].Add(place["artist_name"]);

            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("We found you the following places:");
            
            foreach(string place in places.Keys)
            {
                builder.AppendLine("Place name: " + place);
                if(places[place].Count == 1 && places[place][0].Equals(string.Empty))
                {
                    builder.AppendLine("There are no artists living at this place");
                } else
                {
                    builder.Append("Artists living at this place : ");
                    foreach (string artist in places[place])
                    {
                        builder.AppendFormat("\n\t" + artist);
                        builder.AppendLine();
                    }
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets the string that repsent that the query returned nothing.
        /// </summary>
        /// <returns>
        /// the sorry message
        /// </returns>
        public string GetSorryMsg()
        {
            return sorryMsg;
        }
    }
}
