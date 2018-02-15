using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// SimpleSongExecuter -  this class reprsent an executer of a simple song query,
    /// for the "I know what I want" button.
    /// </summary>
    /// <seealso cref="DataBaseLayer.IExecuter" />
    public class SimpleSongExecuter : IExecuter
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
        /// Initializes a new instance of the <see cref="SimpleSongExecuter" /> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public SimpleSongExecuter(DataBaseConnector db)
        {
            this.conn = db;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSongExecuter"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="songName">Name of the song.</param>
        /// <param name="artistName">Name of the artist.</param>
        /// <param name="fromYear">From year.</param>
        /// <param name="toYear">To year.</param>
        public SimpleSongExecuter(DataBaseConnector db, string songName, string artistName, int fromYear, int toYear)
        {
            this.conn = db;
            SetQuery(songName, artistName, fromYear, toYear);
        }

        /// <summary>
        /// Sets the query.
        /// </summary>
        /// <param name="songName">Name of the song.</param>
        /// <param name="artistName">Name of the artist.</param>
        /// <param name="fromYear">From year.</param>
        /// <param name="toYear">To year.</param>
        /// <returns> true if succeeded and false otherwise </returns>
        public bool SetQuery(string songName, string artistName, int fromYear, int toYear)
        {
            try
            {
                bool artistNamePresent = !artistName.Equals(string.Empty);
                bool songNamePresent = !songName.Equals(string.Empty);
                this.command = IKnowWhatIWantQuriesBank.GetSongQuery(songNamePresent, artistNamePresent, this.conn.Connection);
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

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("We found you the following songs:\n");

            foreach (Dictionary<string, string> song in result)
            {
                builder.Append(SongString(song));
                builder.AppendLine();
            }

            return builder.ToString();
        }

        /// <summary>
        /// Build a string that reprsent the song.
        /// </summary>
        /// <param name="song">The song record.</param>
        /// <returns>
        /// a string that reprsent the song.
        /// </returns>
        private StringBuilder SongString(Dictionary<string, string> song)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Song name : " + song["song_name"]);
            if (song["artist_name"].Equals(string.Empty))
            {
                builder.AppendLine("Unknown Artist");
            }
            else
            {
                builder.AppendLine("Artist : " + song["artist_name"]);
            }
            builder.AppendLine("Realase year : " + song["release_date_year"]);

            return builder;
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
        /// Gets the song name from identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="conn">The connection.</param>
        /// <returns> name of song </returns>
        public static string GetSongNameFromId(int id, DataBaseConnector conn)
        {
            return Entities.EntitiesFactory.GetSongFromSongId(id.ToString(), conn).Name;
        }
    }
}
