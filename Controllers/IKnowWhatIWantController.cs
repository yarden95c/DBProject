using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseLayer;
using MySql.Data.MySqlClient;

namespace Controllers
{
    /// <summary>
    /// IKnowWhatIWantController -  this is the controller for the "I know what I want" button.
    /// </summary>
    /// <seealso cref="Controllers.CompletionController" />
    public class IKnowWhatIWantController : CompletionController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IKnowWhatIWantController"/> class.
        /// </summary>
        public IKnowWhatIWantController() : base()
        {
        }

        /// <summary>
        /// Gets the result of the song query.
        /// </summary>
        /// <param name="songName">Name of the song.</param>
        /// <param name="artistName">Name of the artist.</param>
        /// <param name="fromYear">From year.</param>
        /// <param name="toYear">To year.</param>
        /// <returns> a string that represent the result of the query </returns>
        public string GetSong(string songName, string artistName, int fromYear, int toYear)
        {
            SimpleSongExecuter executer = new SimpleSongExecuter(conn, songName.ToLower(), artistName.ToLower(), fromYear, toYear);
            return executer.Execute();
        }

        /// <summary>
        /// Gets the result of the artist query.
        /// </summary>
        /// <param name="artistName">Name of the artist.</param>
        /// <param name="songName">Name of the song.</param>
        /// <param name="fromYear">From year.</param>
        /// <param name="toYear">To year.</param>
        /// <returns> a string that represent the result of the query </returns>
        public string GetArtist(string artistName, string songName, int fromYear, int toYear)
        {
            SimpleArtistExecuter executer = new SimpleArtistExecuter(conn, songName.ToLower(), artistName.ToLower(), fromYear, toYear);
            return executer.Execute();
        }

        /// <summary>
        /// Gets the result of the place query.
        /// </summary>
        /// <param name="placeName">Name of the place.</param>
        /// <param name="artistName">Name of the artist.</param>
        /// <returns> a string that represent the result of the query </returns>
        public string GetPlace(string placeName, string artistName)
        {
            SimplePlaceExecuter executer = new SimplePlaceExecuter(conn, placeName.ToLower(), artistName.ToLower());
            return executer.Execute();
        }
    }
}
