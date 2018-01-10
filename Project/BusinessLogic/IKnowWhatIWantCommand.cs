using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseEngine;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    public class IKnowWhatIWantCommand : ICommand
    {
        private DataBaseEngine.DataBaseEngine db;
        private const int LIMIT_QUERY= 10;
        public IKnowWhatIWantCommand(DataBaseEngine.DataBaseEngine db)
        {
            this.db = db;
        }
        public List<IEntity> Execute(EntityType entity,Dictionary<string, string> fields)
        {
            List<IEntity> temp = new List<IEntity>();
            string tableName = null;
            IEntity addedEntity = null;
            switch(entity)
            {
                case EntityType.SONG:
                    tableName = "songs";
                    break;
                case EntityType.ARTIST:
                    tableName = "artists";
                    break;
                case EntityType.AREA:
                    tableName = "area";
                    break;
            }

            List<string> columns = new List<string>();
            columns.Add("*");


            //select song_name from final.Songs where id_song in (SELECT id_song FROM final.SongsByArtist where id_artist = 48248);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("where ");
            foreach (string key in fields.Keys)
            {
                stringBuilder.Append("LOWER("+key+")");
                stringBuilder.Append(" like \"%"+fields[key].ToLower()+"%\" and ");
            }
            stringBuilder.Remove(stringBuilder.Length - 5, 5);

            MySqlDataReader queryResult = db.ExecuteSimpleQuery(tableName, columns, stringBuilder.ToString(),LIMIT_QUERY);
            while (queryResult.Read())
            {
                addedEntity = EntityFactory.CreateEntity(entity);

                //   Console.WriteLine(myReader["name"].ToString());
                foreach(string field in addedEntity.FieldsNames)
                {
                    //Console.WriteLine(queryResult[field].ToString());
                    addedEntity[field] = queryResult[field].ToString();
                }
                temp.Add(addedEntity);
            }
            queryResult.Close();

            return AddExtraField(temp);
        }

        private List<IEntity> AddExtraField(List<IEntity> temp)
        {
            List<IEntity> result = new List<IEntity>();
            foreach (IEntity en in temp)
            {
                MySqlDataReader queryResult = db.ExecuteSimpleQuery(en.buildExtraQuery(en));
                result.Add(en.AddValueToExtraField(queryResult));

            }
            return result;
        }

    }
}
