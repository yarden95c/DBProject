using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    public class SimpleArtistExecuter : IExecuter
    {
        //private List<MySqlCommand> commands;
        private MySqlCommand command;
        private DataBaseConnector conn;
        private const string sorryMsg = "Sorry, we couldn't find you an answer, please try again with another parameters.";

        public SimpleArtistExecuter(DataBaseConnector db,string songName,string artistName, int fromYear, int toYear)
        {
            this.conn = db;
            this.command = IKnowWhatIWantQuriesBank.GetArtistQuery(this.conn.Connection);
            command.Parameters["@songName"].Value = "%" + songName + "%";
            command.Parameters["@artistName"].Value = "%" + artistName + "%";
            command.Parameters["@fromYear"].Value = fromYear;
            command.Parameters["@toYear"].Value = toYear;
            /*  this.commands = IKnowWhatIWantQuriesBank.GetArtistQuery(this.conn.Connection);
              commands[1].Parameters["@songName"].Value = "%" + songName + "%";
              commands[0].Parameters["@artistName"].Value = "%" + artistName + "%";
              commands[0].Parameters["@fromYear"].Value = fromYear;
              commands[0].Parameters["@toYear"].Value = toYear; */
        }


        public string Execute()
        {
            List<Dictionary<string, string>> result = conn.ExecuteCommand(command);
            if (result.Count == 0)
            {
                return sorryMsg;
            }

           List<Artist> artists = new List<Artist>();
            foreach (Dictionary<string, string> artist in result)
            {
                Artist artistObject = new Artist();
                artistObject.Name = artist["artist_name"];
                artistObject.Id = artist["id_artist"];
                artistObject.Day = artist["begin_date_day"];
                artistObject.Month = artist["begin_date_month"];
                artistObject.Year = artist["begin_date_year"];

                if (!artists.Contains(artistObject))
                {
                    artists.Add(artistObject);
                }
                artists.Find(artistObject.Equals).AddSong(artist["song_name"]);
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("We found you the following artists:");

            foreach (Artist artist in artists)
            {
                builder.AppendLine(artist.ToString());
            }

            return builder.ToString();
        }

        public string GetSorryMsg()
        {
            return sorryMsg;
        }

        // what to print if day or month or songs is null?
        /*    private StringBuilder ArtistString(Dictionary<string,string> artist)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Artist name : " + artist["artist_name"]);
                builder.AppendLine("Song Name : " + artist["song_name"]);
                builder.AppendFormat("Birthday : {0}/{1}/{2}" ,artist["begin_date_day"],artist["begin_date_month"],artist["begin_date_year"]);

                return builder;
            } */
    }
}
