using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    public class SimpleArtistExecuter
    {
        private List<MySqlCommand> commands;
        private DataBaseConnector conn;
        private const string sorryMsg = "Sorry, we couldn't find you an answer, please try again with another parameters.";

        public SimpleArtistExecuter(DataBaseConnector db,string songName,string artistName, int fromYear, int toYear)
        {
            this.conn = db;
            this.commands = IKnowWhatIWantQuriesBank.GetArtistQuery(this.conn.Connection);
            commands[1].Parameters["@songName"].Value = "%" + songName + "%";
            commands[0].Parameters["@artistName"].Value = "%" + artistName + "%";
            commands[0].Parameters["@fromYear"].Value = fromYear;
            commands[0].Parameters["@toYear"].Value = toYear;
        }

        public string Execute()
        {
            List<Dictionary<string, string>> result = conn.ExecuteCommand(commands[0]);
            List<Artist> artists = new List<Artist>();

            if(result.Count==0)
            {
                return sorryMsg;
            }

            foreach (Dictionary<string, string> artist in result)
            {
                Artist artistObject = new Artist();
                artistObject.Name = artist["artist_name"];
                artistObject.Id = artist["id_artist"];
                artistObject.Day = artist["begin_date_day"];
                artistObject.Month= artist["begin_date_month"];
                artistObject.Year= artist["begin_date_year"];

                commands[1].Parameters["@idArtist"].Value = artistObject.Id;
                List<string> songs = conn.ExecuteOneColumnCommand(commands[1]);
                foreach(string song in songs)
                {
                    artistObject.AddSong(song);
                }

                artists.Add(artistObject);
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("We found you the following artists:");
            
            foreach(Artist artist in artists)
            {
                builder.Append(artist.ToString());
            }

            return builder.ToString();
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
