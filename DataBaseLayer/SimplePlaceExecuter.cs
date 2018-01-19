using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    public class SimplePlaceExecuter : IExecuter
    {
        private MySqlCommand command;
        private DataBaseConnector conn;
        private const string sorryMsg = "Sorry, we couldn't find you an answer, please try again with another parameters.";

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
                builder.Append("Artists living at this place : ");
                foreach(string artist in places[place])
                {
                    builder.AppendFormat("\n\t" + artist);
                    builder.AppendLine();
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        // what to print if day or month is null?
        private StringBuilder PlaceString(Dictionary<string,string> place)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Place name : " + place["area_name"]);
            builder.AppendLine("Artist Name : " + place["artist_name"]);

            return builder;
        }

        public string GetSorryMsg()
        {
            return sorryMsg;
        }
    }
}
