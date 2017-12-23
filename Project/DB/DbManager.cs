using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;

namespace Project.DB
{
    public class DbManager
    {

        private ICommand command;


        public DbManager()
        {
            DataBaseEngine.DataBaseEngine db = new DataBaseEngine.DataBaseEngine();
            this.command = new IKnowWhatIWantCommand(db);
        }


        public List<string> GetTopArtistNamesStartWith(string prefix)
        {
            return buildResultList(prefix, "name", EntityType.ARTIST);
        }



        public List<string> GetTopSongsNameStartWith(string prefix)
        {
            return buildResultList(prefix, "song_name", EntityType.SONG);
        }


        public List<string> GetTopPlacesNameStartWith(string prefix)
        {
            return buildResultList(prefix, "name_area", EntityType.AREA);
        }


        public List<string> GetTopArtistDateOfBirthsStartWith(string prefix)
        {
            return buildResultList(prefix, "begin_date_year", EntityType.ARTIST);
        }


        public List<string> GetTopSongsYearsStartWith(string prefix)
        {
            return buildResultList(prefix, "release_date_year", EntityType.SONG);
        }

        private List<string> buildResultList(string prefix, string fieldName,
            EntityType type)
        {
            List<string> l = new List<string>();
            try
            {
                Dictionary<string, string> d = new Dictionary<string, string>();
                d.Add(fieldName, prefix);
                List<IEntity> list = command.Execute(type, d);
                foreach (IEntity entity in list)
                {
                    l.Add(entity[fieldName]);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return l;
        }

    }
}


