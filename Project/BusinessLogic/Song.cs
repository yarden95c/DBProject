using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    class Song : EntityBase<string>
    {
        
        public Song() : base()
        {
            fieldNames.Add("song_name");
            fieldNames.Add("id_song");
            fieldNames.Add("release_date_year");
            fieldNames.Add("num_of_hits");
            extraField = new Pair<String, string>();
            extraField.Key = "artist_name";
        }

        public override IEntity AddValueToExtraField(MySqlDataReader queryResult)
        {
            while (queryResult.Read())
            {
                extraField.Value=(queryResult["name"].ToString());
            }
            queryResult.Close();
            return this; 
        }

        public override string buildExtraQuery(IEntity entity)
        {
            string id = entity["id_song"];
            return "select Artists.name from Artists join SongsByArtist" +
                " on Artists.id_artist = SongsByArtist.id_artist where id_song =\"" + id + "\";";
        }

        public override string ExtraFieldToString()
        {
            return extraField.Value;
        }
    }
}
