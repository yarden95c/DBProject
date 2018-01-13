using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    class Artist : IEquatable<Artist>
    {
        private string id;
        private string name;
        private string day;
        private string month;
        private string year;
        private List<string> songs;

        public Artist()
        {
            songs = new List<string>();
            id = null;
            name = null;
            day = null;
            month = null;
            year = null;
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Day { get => day; set => day = value; }
        public string Month { get => month; set => month = value; }
        public string Year { get => year; set => year = value; }
  //      public List<string> Songs { get => songs; }

        public void AddSong(string song)
        {
            songs.Add(song);
        }

        public bool Equals(Artist other)
        {
            bool result = this.id.Equals(other.id);
            return result;
        }

        // what to print if day or month or songs is null?
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Artist name : " + name);
            if(day.Equals(string.Empty))
            {
                day = "unknown";
            }
            if(month.Equals(string.Empty))
            {
                month = "unknown";
            }
            builder.AppendFormat("Birthday : {0}/{1}/{2}\n", day, month, Year);
            builder.Append("Songs list : ");

            foreach(string song in songs)
            {
                builder.AppendFormat("\n\t" + song);
            }
            
           // builder.AppendLine(String.Join("\n", values));
            return builder.ToString();
                
        }
    }
}
