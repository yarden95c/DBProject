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

        public IKnowWhatIWantController()
        {
            conn = new DataBaseConnector();
        }
        public List<string> GetTopSongsNames(string songName)
        {
            MySqlCommand command = IKnowWhatIWantQuriesBank.GetSongsNamesQuery(conn.Connection);
            command.Parameters["@songName"].Value = "%" + songName.ToLower() + "%";
            return conn.ExecuteOneColumnCommand(command);
        }

        public List<string> GetTopArtistsNames(string artistName)
        {
            MySqlCommand command = IKnowWhatIWantQuriesBank.GetArtistsNamesQuery(conn.Connection);
            command.Parameters["@artistName"].Value = "%" + artistName.ToLower() + "%";
            return conn.ExecuteOneColumnCommand(command);
        }

        public List<string> GetTopPlacesNames(string placeName)
        {
            MySqlCommand command = IKnowWhatIWantQuriesBank.GetPlacesNamesQuery(conn.Connection);
            command.Parameters["@placeName"].Value = "%" + placeName.ToLower() + "%";
            return conn.ExecuteOneColumnCommand(command);
        }

        public string GetSong(string songName,string artistName,int fromYear,int toYear)
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

        public string GetArtist(string artistName,string songName,int fromYear, int toYear)
        {
            SimpleArtistExecuter executer = new SimpleArtistExecuter(conn, songName.ToLower(), artistName.ToLower(), fromYear, toYear);
            return executer.Execute();
        }

        public string GetPlace(string placeName,string artistName)
        {
            SimplePlaceExecuter executer = new SimplePlaceExecuter(conn, placeName.ToLower(), artistName.ToLower());
            return executer.Execute();
        }
    }
}
