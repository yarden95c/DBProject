using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Project.Client.Logic;

namespace Project.DB
{
    public class DbManager
    {

        private readonly ICommand _command;
        private readonly Mutex _dbMutex = new Mutex();

        public DbManager()
        {
            Console.WriteLine("db manager constructor");
            DataBaseEngine.DataBaseEngine db = new DataBaseEngine.DataBaseEngine();
            this._command = new IKnowWhatIWantCommand(db);
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
                _dbMutex.WaitOne();
                List<IEntity> list = _command.Execute(type, d);

                _dbMutex.ReleaseMutex();
                foreach (IEntity entity in list)
                {
                    l.Add(entity[fieldName]);
                }
            }
            catch (Exception e)
            {

                _dbMutex.ReleaseMutex();
                Console.WriteLine(e);
            }
            return l;
        }



        public ResultParams GetResult(RequestParams request)
        {
            IEntity song = EntityFactory.CreateEntity(EntityType.SONG);
            Dictionary<string, string> fieldsAndValues = request.FieldsAndValues;
            List<string> fields = song.FieldsNames;
            foreach (var field in fields)
            {
                if(fieldsAndValues.ContainsKey(field))
                song[field] = fieldsAndValues[field];
            }
            
            List<IEntity> entiies = new List<IEntity>
            {
                song
            };
            return new ResultParams(entiies, EntityType.SONG, request);

        }

    }
}


