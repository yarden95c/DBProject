using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    public class User
    {
        private int id;
        private string email;
        private string firstName;
        private string lastName;
        private int genreId;
        private int placeId;
        private int day;
        private int month;
        private int year;
        private List<string> songs;
        private List<int> artists;

        public User()
        {
            Songs = new List<string>();
            Artists = new List<int>();
        }

        public int Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int GenreId { get => genreId; set => genreId = value; }
        public int PlaceId { get => placeId; set => placeId = value; }
        public int Day { get => day; set => day = value; }
        public int Month { get => month; set => month = value; }
        public int Year { get => year; set => year = value; }
        public List<string> Songs { get => songs; set => songs = value; }
        public List<int> Artists { get => artists; set => artists = value; }

        public void AddSong(string songId)
        {
            this.songs.Add(songId);
        }

        public void AddArtist(int artistId)
        {
            this.artists.Add(artistId);
        }
    }
}
