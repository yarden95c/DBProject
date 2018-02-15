using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// SimpleArtistExecuter - this class reprsent an executer of a simple artist query,
    /// for the "I know what I want" button.
    /// </summary>
    /// <seealso cref="DataBaseLayer.IExecuter" />
    public class SimpleArtistExecuter : IExecuter
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
        /// Initializes a new instance of the <see cref="SimpleArtistExecuter" /> class.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="songName">Name of the song.</param>
        /// <param name="artistName">Name of the artist.</param>
        /// <param name="fromYear">From year.</param>
        /// <param name="toYear">To year.</param>
        public SimpleArtistExecuter(DataBaseConnector db,string songName,string artistName, int fromYear, int toYear)
        {
            this.conn = db;
            SetQuery(songName, artistName, fromYear, toYear);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleArtistExecuter"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public SimpleArtistExecuter(DataBaseConnector db)
        {
            this.conn = db;
        }

        /// <summary>
        /// Sets the query.
        /// </summary>
        /// <param name="songName">Name of the song.</param>
        /// <param name="artistName">Name of the artist.</param>
        /// <param name="fromYear">From year.</param>
        /// <param name="toYear">To year.</param>
        /// <returns> true if succeeded false otherwise </returns>
        public bool SetQuery(string songName, string artistName, int fromYear, int toYear)
        {
            try
            {
                bool songNamePresent = !songName.Equals(string.Empty);
                bool artistNamePresent = !artistName.Equals(string.Empty);
                this.command = IKnowWhatIWantQuriesBank.GetArtistQuery(artistNamePresent,songNamePresent,this.conn.Connection);
                if (songNamePresent)
                {
                    command.Parameters["@songName"].Value = "%" + songName + "%";
                }
                if (artistNamePresent)
                {
                    command.Parameters["@artistName"].Value = "%" + artistName + "%";
                }
                command.Parameters["@fromYear"].Value = fromYear;
                command.Parameters["@toYear"].Value = toYear;
                return true;
            }
            catch (Exception e)
            {
                return false;
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
                if (!artist["song_name"].Equals(string.Empty))
                {
                    artists.Find(artistObject.Equals).AddSong(artist["song_name"]);
                }
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("We found you the following artists:");
            builder.AppendLine();
            foreach (Artist artist in artists)
            {
                builder.AppendLine(artist.ToString());
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

        /// <summary>
        /// Gets the artist name from identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="conn">The connection.</param>
        /// <returns> the artist name </returns>
        public static string GetArtistNameFromId(int id, DataBaseConnector conn)
        {
            return Entities.EntitiesFactory.GetArtistFromArtistId(id.ToString(), conn).Name;
        }
    }
}
