using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    /// <summary>
    /// User - this class represent a user entity.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The identifier
        /// </summary>
        private int id;
        /// <summary>
        /// The email
        /// </summary>
        private string email;
        /// <summary>
        /// The first name
        /// </summary>
        private string firstName;
        /// <summary>
        /// The last name
        /// </summary>
        private string lastName;
        /// <summary>
        /// The genre identifier
        /// </summary>
        private int genreId;
        /// <summary>
        /// The place identifier
        /// </summary>
        private int placeId;
        /// <summary>
        /// The day
        /// </summary>
        private int day;
        /// <summary>
        /// The month
        /// </summary>
        private int month;
        /// <summary>
        /// The year
        /// </summary>
        private int year;
        /// <summary>
        /// The songs
        /// </summary>
        private List<string> songs;
        /// <summary>
        /// The artists
        /// </summary>
        private List<int> artists;

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            Songs = new List<string>();
            Artists = new List<int>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get => id; set => id = value; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get => email; set => email = value; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get => firstName; set => firstName = value; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get => lastName; set => lastName = value; }
        /// <summary>
        /// Gets or sets the genre identifier.
        /// </summary>
        /// <value>
        /// The genre identifier.
        /// </value>
        public int GenreId { get => genreId; set => genreId = value; }
        /// <summary>
        /// Gets or sets the place identifier.
        /// </summary>
        /// <value>
        /// The place identifier.
        /// </value>
        public int PlaceId { get => placeId; set => placeId = value; }
        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>
        /// The day.
        /// </value>
        public int Day { get => day; set => day = value; }
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public int Month { get => month; set => month = value; }
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get => year; set => year = value; }
        /// <summary>
        /// Gets or sets the songs.
        /// </summary>
        /// <value>
        /// The songs.
        /// </value>
        public List<string> Songs { get => songs; set => songs = value; }
        /// <summary>
        /// Gets or sets the artists.
        /// </summary>
        /// <value>
        /// The artists.
        /// </value>
        public List<int> Artists { get => artists; set => artists = value; }

        /// <summary>
        /// Adds the song.
        /// </summary>
        /// <param name="songId">The song identifier.</param>
        public void AddSong(string songId)
        {
            this.songs.Add(songId);
        }

        /// <summary>
        /// Adds the artist.
        /// </summary>
        /// <param name="artistId">The artist identifier.</param>
        public void AddArtist(int artistId)
        {
            this.artists.Add(artistId);
        }
    }
}
