using BusinessLogic;
using System;
using System.Collections.Generic;

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
            return BuildResultList(prefix, "name", EntityType.ARTIST);
        }



        public List<string> GetTopSongsNameStartWith(string prefix)
        {
            return BuildResultList(prefix, "song_name", EntityType.SONG);
        }


        public List<string> GetTopPlacesNameStartWith(string prefix)
        {
            return BuildResultList(prefix, "name_area", EntityType.AREA);
        }


        public List<string> GetTopArtistDateOfBirthsStartWith(string prefix)
        {
            return BuildResultList(prefix, "begin_date_year", EntityType.ARTIST);
        }

        public List<string> GetTopSongsYearsStartWith(string prefix)
        {
            return BuildResultList(prefix, "release_date_year", EntityType.SONG);
        }

        private List<string> BuildResultList(string prefix, string fieldName,
            EntityType type)
        {
            var l = new List<string>();
            try
            {
                var d = new Dictionary<string, string> {{fieldName, prefix}};
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


