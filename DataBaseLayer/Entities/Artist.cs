using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    /// <summary>
    /// Artist - this class represent an artist entity.
    /// </summary>
    /// <seealso cref="System.IEquatable{DataBaseLayer.Artist}" />
    public class Artist : IEquatable<Artist>
    {
        /// <summary>
        /// The identifier
        /// </summary>
        private string id;
        /// <summary>
        /// The name
        /// </summary>
        private string name;
        /// <summary>
        /// The day
        /// </summary>
        private string day;
        /// <summary>
        /// The month
        /// </summary>
        private string month;
        /// <summary>
        /// The year
        /// </summary>
        private string year;
        /// <summary>
        /// The songs
        /// </summary>
        private List<string> songs;

        /// <summary>
        /// Initializes a new instance of the <see cref="Artist" /> class.
        /// </summary>
        public Artist()
        {
            songs = new List<string>();
            id = null;
            name = null;
            day = null;
            month = null;
            year = null;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get => id; set => id = value; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get => name; set => name = value; }
        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>
        /// The day.
        /// </value>
        public string Day { get => day; set => day = value; }
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public string Month { get => month; set => month = value; }
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public string Year { get => year; set => year = value; }
        /// <summary>
        /// Gets the songs.
        /// </summary>
        /// <value>
        /// The songs.
        /// </value>
        public List<string> Songs { get => songs; }

        /// <summary>
        /// Adds the song.
        /// </summary>
        /// <param name="song">The song.</param>
        public void AddSong(string song)
        {
            songs.Add(song);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Artist other)
        {
            bool result = this.id.Equals(other.id);
            return result;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
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
            if (songs.Count > 0)
            {
                builder.Append("Songs list : ");

                foreach (string song in songs)
                {
                    builder.AppendFormat("\n\t" + song);
                }
            }
            
            return builder.ToString();
        }
    }
}
