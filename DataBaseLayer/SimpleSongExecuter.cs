using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    public class SimpleSongExecuter
    {
        private MySqlCommand command;
        private DataBaseConnector conn;
        private const string sorryMsg = "Sorry, we couldn't find you an answer, please try again with another parameters.";

        public SimpleSongExecuter(DataBaseConnector db,string songName,string artistName, int fromYear, int toYear)
        {
            this.conn = db;
            this.command = IKnowWhatIWantQuriesBank.GetSongQuery(this.conn.Connection);
            command.Parameters["@songName"].Value = "%" + songName + "%";
            command.Parameters["@artistName"].Value = "%" + artistName + "%";
            command.Parameters["@fromYear"].Value = fromYear;
            command.Parameters["@toYear"].Value = toYear;
        }

        public string Execute()
        {
            List<Dictionary<string, string>> result = conn.ExecuteCommand(command);
            if(result.Count==0)
            {
                return sorryMsg;
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("We found you the following songs:\n");
            
            foreach(Dictionary<string,string> song in result)
            {
                builder.Append(SongString(song));
                builder.AppendLine();

            }

            return builder.ToString();
        }

        private StringBuilder SongString(Dictionary<string,string> song)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Song name : " + song["song_name"]);
            builder.AppendLine("Artist : " + song["artist_name"]);
            builder.AppendLine("Realase year : " + song["release_date_year"]);

            return builder;
        }
    }
}
