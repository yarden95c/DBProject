using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DB
{
    class DbManager
    {
        public List<string> GetTopArtistNamesStartWith(string prefix)
        {
            List<string> l =  new List<string> { "AAAA", "ABA" };
            return l.FindAll(s => s.StartsWith(prefix));
        }
        public List<string> GetTopSongsNameStartWith(string prefix)
        {
            List<string> l = new List<string> { "BLABLA", "BBLALA", "BGVLA" };
            return l.FindAll(s => s.StartsWith(prefix));
        }
        public List<string> GetTopPlacesNameStartWith(string prefix)
        {
            List<string> l = new List<string> { "BLABLA", "BBLALA", "BGVLA" };
            return l.FindAll(s => s.StartsWith(prefix));
        }
        public List<string> GetTopArtistDateOfBirthsStartWith(string prefix)
        {
            List<string> l = new List<string> { "2000", "1998", "1988" };
            return l.FindAll(s => s.StartsWith(prefix));
        }
        public List<string> GetTopSongsYearsStartWith(string prefix)
        {
            List<string> l = new List<string> { "2000", "1998", "1988" };
            return l.FindAll(s => s.StartsWith(prefix));
        }
 
    }
}
