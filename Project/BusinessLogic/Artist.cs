using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    class Artist : EntityBase<List<string>>
    {
        public Artist() : base()
        {
            fieldNames.Add("id_artist");
            fieldNames.Add("name");
            fieldNames.Add("begin_date_year");
            fieldNames.Add("begin_date_month");
            fieldNames.Add("begin_date_day");
            fieldNames.Add("end_date_year");
            fieldNames.Add("end_date_month");
            fieldNames.Add("end_date_day");
            fieldNames.Add("area_Id");
            fieldNames.Add("gender");
            fieldNames.Add("type");
            fieldNames.Add("comment");
            fieldNames.Add("num_of_hits");
            extraField = new Pair<String,List<String>>();
            extraField.Value = new List<string>();
            extraField.Key = "list_of_songs";

        }


        public override IEntity AddValueToExtraField(MySqlDataReader queryResult)
        {
            while (queryResult.Read())
            {
                extraField.Value.Add(queryResult["song_name"].ToString());
            }
            queryResult.Close();
            return this;        
        }

        public override string buildExtraQuery(IEntity entity)
        {
            string id = entity["id_artist"];
            return "select song_name from Songs where id_song in(SELECT id_song FROM SongsByArtist where id_artist=" +id+ ");";
        }

        public override string ExtraFieldToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(string str in extraField.Value)
            {
                sb.Append(str+"\n");
            }
            return sb.ToString();
        }
    }
}
