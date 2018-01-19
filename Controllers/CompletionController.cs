using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseLayer;
using MySql.Data.MySqlClient;

namespace Controllers
{
    public abstract class CompletionController
    {
        protected DataBaseConnector conn;
        private Dictionary<string, List<string>> _topSongsCache;
        private Dictionary<string, List<string>> _topArtistsCache;
        private Dictionary<string, List<string>> _topPlacesCache;
        private Dictionary<string, List<string>> _topGeneresCache;
        private List<string> _yearsList;

        public CompletionController()
        {
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

        public List<string> GetYearsList(string Yaer)
        {
            return _yearsList.FindAll(s => s.Contains(Yaer));
        }

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
