using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseLayer;
using MySql.Data.MySqlClient;

namespace Controllers
{
    public class IKnowWhatIWantController
    {
        private DataBaseConnector conn;
        private Dictionary<string, List<string>> _topSongsCache;
        private Dictionary<string, List<string>> _topArtistsCache;
        private Dictionary<string, List<string>> _topPlacesCache;
        private List<string> _yearsList;
        public IKnowWhatIWantController()
        {
            _topArtistsCache = new Dictionary<string, List<string>>();
            _topPlacesCache = new Dictionary<string, List<string>>();
            _topSongsCache = new Dictionary<string, List<string>>();
            _yearsList = new List<string>
            {
                "1940 - 1960",
                "1960 - 1980",
                "1980 - 2000",
                "2000 - 2018"
            };
            conn = new DataBaseConnector();
        }

        public List<string> GetTopSongsNames(string songName)
        {
            if (_topSongsCache.ContainsKey(songName))
                return _topSongsCache[songName];
            MySqlCommand command = IKnowWhatIWantQuriesBank.GetSongsNamesQuery(conn.Connection);
            command.Parameters["@songName"].Value = "%" + songName.ToLower() + "%";
            List<string> responsList = conn.ExecuteOneColumnCommand(command);
            _topSongsCache.Add(songName, responsList);
            return responsList;
        }

        public List<string> GetTopArtistsNames(string artistName)
        {
            if (_topArtistsCache.ContainsKey(artistName))
                return _topArtistsCache[artistName];
            MySqlCommand command = IKnowWhatIWantQuriesBank.GetArtistsNamesQuery(conn.Connection);
            command.Parameters["@artistName"].Value = "%" + artistName.ToLower() + "%";
            List<string> responsList = conn.ExecuteOneColumnCommand(command);
            _topArtistsCache.Add(artistName, responsList);
            return responsList;

        }

        public List<string> GetTopPlacesNames(string placeName)
        {
            if (_topPlacesCache.ContainsKey(placeName))
                return _topPlacesCache[placeName];
            MySqlCommand command = IKnowWhatIWantQuriesBank.GetPlacesNamesQuery(conn.Connection);
            command.Parameters["@placeName"].Value = "%" + placeName.ToLower() + "%";
            List<string> responsList = conn.ExecuteOneColumnCommand(command);
            _topPlacesCache.Add(placeName, responsList);
            return responsList;

        }

        public List<string> GetYearsList(string Yaer)
        {
            return _yearsList.FindAll(s => s.Contains(Yaer));
        }

        public string GetSong(string songName, string artistName, int fromYear, int toYear)
        {
            /* MySqlCommand command = IKnowWhatIWantQuriesBank.GetSongQuery(conn.Connection);
             command.Parameters["@songName"].Value = "%"+songName+"%";
             command.Parameters["@artistName"].Value = "%"+artistName+"%";
             command.Parameters["@fromYear"].Value = fromYear;
             command.Parameters["@toYear"].Value = toYear;

             List<Dictionary<string,string>> result=conn.ExecuteCommand(command);
             foreach(Dictionary<string,string> dict in result)
             {
                 foreach(string key in dict.Keys)
                 {
                     Console.WriteLine(key + ": " + dict[key]);
                 }
             } */

            SimpleSongExecuter executer = new SimpleSongExecuter(conn, songName.ToLower(), artistName.ToLower(), fromYear, toYear);
            return executer.Execute();
        }

        public string GetArtist(string artistName, string songName, int fromYear, int toYear)
        {
            SimpleArtistExecuter executer = new SimpleArtistExecuter(conn, songName.ToLower(), artistName.ToLower(), fromYear, toYear);
            return executer.Execute();
        }

        public string GetPlace(string placeName, string artistName)
        {
            SimplePlaceExecuter executer = new SimplePlaceExecuter(conn, placeName.ToLower(), artistName.ToLower());
            return executer.Execute();
        }
    }
}
