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
    /// CompletionController - this controller is a base class for controllers that need a completion 
    /// methods for songs names, artists names, genres names and years.
    /// </summary>
    public abstract class CompletionController
    {

        protected readonly SignInController _signInController;
        /// <summary>
        /// The data base connector
        /// </summary>
        protected DataBaseConnector conn;
        /// <summary>
        /// The top songs cache
        /// </summary>
        private Dictionary<string, List<string>> _topSongsCache;
        /// <summary>
        /// The top artists cache
        /// </summary>
        private Dictionary<string, List<string>> _topArtistsCache;
        /// <summary>
        /// The top places cache
        /// </summary>
        private Dictionary<string, List<string>> _topPlacesCache;
        /// <summary>
        /// The top generes cache
        /// </summary>
        private Dictionary<string, List<string>> _topGeneresCache;
        /// <summary>
        /// The years list
        /// </summary>
        private List<string> _yearsList;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompletionController"/> class.
        /// </summary>
        protected CompletionController()
        {

            _signInController = SignInController.GetInstance();
            _topArtistsCache = new Dictionary<string, List<string>>();
            _topPlacesCache = new Dictionary<string, List<string>>();
            _topSongsCache = new Dictionary<string, List<string>>();
            _topGeneresCache = new Dictionary<string, List<string>>();
            _yearsList = new List<string>
            {
                "1900 - 1940",
                "1940 - 1960",
                "1960 - 1980",
                "1980 - 2000",
                "2000 - 2018"
            };
            conn = DataBaseConnector.GetInstance();
        }

        /// <summary>
        /// Gets the top songs names.
        /// </summary>
        /// <param name="songName">Name of the song.</param>
        /// <returns></returns>
        public List<string> GetTopSongsNames(string songName)
        {
            if (_topSongsCache.ContainsKey(songName))
                return _topSongsCache[songName];
            MySqlCommand command = IKnowWhatIWantQuriesBank.GetSongsNamesQuery(conn.Connection);
            command.Parameters["@songName"].Value = "%" + songName.ToLower() + "%";
            List<string> result = conn.ExecuteOneColumnCommand(command);
            _topSongsCache.Add(songName, result);
            return result;
        }

        /// <summary>
        /// Gets the top artists names.
        /// </summary>
        /// <param name="artistName">Name of the artist.</param>
        /// <returns></returns>
        public List<string> GetTopArtistsNames(string artistName)
        {
            if (_topArtistsCache.ContainsKey(artistName))
                return _topArtistsCache[artistName];
            MySqlCommand command = IKnowWhatIWantQuriesBank.GetArtistsNamesQuery(conn.Connection);
            command.Parameters["@artistName"].Value = "%" + artistName.ToLower() + "%";
            List<string> result = conn.ExecuteOneColumnCommand(command);
            _topArtistsCache.Add(artistName, result);
            return result;
        }

        /// <summary>
        /// Gets the top places names.
        /// </summary>
        /// <param name="placeName">Name of the place.</param>
        /// <returns></returns>
        public List<string> GetTopPlacesNames(string placeName)
        {
            if (_topPlacesCache.ContainsKey(placeName))
                return _topPlacesCache[placeName];
            MySqlCommand command = IKnowWhatIWantQuriesBank.GetPlacesNamesQuery(conn.Connection);
            command.Parameters["@placeName"].Value = "%" + placeName.ToLower() + "%";
            List<string> result = conn.ExecuteOneColumnCommand(command);
            _topPlacesCache.Add(placeName, result);
            return result;
        }

        /// <summary>
        /// Gets the years list.
        /// </summary>
        /// <param name="Yaer">The yaer.</param>
        /// <returns></returns>
        public List<string> GetYearsList(string Yaer)
        {
            return _yearsList.FindAll(s => s.Contains(Yaer));
        }

        /// <summary>
        /// Gets the top genres names.
        /// </summary>
        /// <param name="genreName">Name of the genre.</param>
        /// <returns></returns>
        public List<string> GetTopGenresNames(string genreName)
        {
            if (_topGeneresCache.ContainsKey(genreName))
                return _topGeneresCache[genreName];
            MySqlCommand command = IKnowWhatIWantQuriesBank.GetGenresNamesQuery(conn.Connection);
            command.Parameters["@genreName"].Value = "%" + genreName.ToLower() + "%";
            List<string> result = conn.ExecuteOneColumnCommand(command);
            _topGeneresCache.Add(genreName, result);
            return result; 
        }
    }
}
