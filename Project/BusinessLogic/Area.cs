using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    public class Area : EntityBase<List<string>>
    {
        public Area() : base()
        {
            fieldNames.Add("id_area");
            fieldNames.Add("name_area");
            fieldNames.Add("area_type");
            extraField = new Pair<String, List<String>>();
            extraField.Value = new List<string>();
            extraField.Key = "list_of_artists";
        }

        public override IEntity AddValueToExtraField(MySqlDataReader queryResult)
        {
            while (queryResult.Read())
            {
                extraField.Value.Add(queryResult["name"].ToString());
            }
            queryResult.Close();
            return this;    
        }

        public override string buildExtraQuery(IEntity entity)
        {
            string id = entity["id_area"];
            return "SELECT name FROM Artists where area_id=" + id;
        }

        public override string ExtraFieldToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in extraField.Value)
            {
                sb.Append(str + "\n");
            }
            return sb.ToString();
        }
    }
}
