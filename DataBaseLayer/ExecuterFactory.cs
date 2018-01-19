using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    public class ExecuterFactory
    {
        private static Random rand = new Random();
        private const int NUM_OF_EXECUTERS = 7;

        public static IExecuter GetExecuter(User user, DataBaseConnector db)
        {
            int num = rand.Next(NUM_OF_EXECUTERS);
            Dictionary<string,int> years = RandomRecordsBank.GetRandomYears();
            IExecuter result;
            switch(num)
            {
                case 0:
                    result = new GenreExecuter(RandomRecordsBank.GetRandomGenreName(),db, user);
                    break;
                case 1:
                    result = new NumberExecuter(user, db);
                    break;
                case 2:
                    result = new PlaceExecuter(RandomRecordsBank.GetRandomPlaceName(), db, user);
                    break;
                case 3:
                    result = new SimpleArtistExecuter(db, "", RandomRecordsBank.GetRandomArtistName(), years["from"], years["to"]);
                    break;
                case 4:
                    result = new SimplePlaceExecuter(db, RandomRecordsBank.GetRandomPlaceName(), "");
                    break;
                case 5:
                    result = new SimpleSongExecuter(db, RandomRecordsBank.GetRandomSongName(), "", 0, 9999);
                    break;
                case 6:
                    result = new YearExecuter(db, years["from"], years["to"],user);
                    break;
                default:
                    result = new NumberExecuter(user, db);
                    break;
            }

            return result;
        }
    }
}
