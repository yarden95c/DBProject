using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer.Entities
{
    /// <summary>
    /// Class that generate entities from database records.
    /// </summary>
    public class EntitiesFactory
    {
        /// <summary>
        /// Gets the name of the song from song.
        /// </summary>
        /// <param name="songName">Name of the song.</param>
        /// <param name="conn">The connection.</param>
        /// <returns></returns>
        public static Song GetSongFromSongName(string songName, DataBaseConnector conn)
        {

            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = "select id_song from songs where lower(song_name) = \"" + songName.ToLower() + "\"";
            List<string> result = conn.ExecuteOneColumnCommand(command);
            if (result.Count <= 0)
            {
                return null;
            }

            Song song = new Song(songName, result[0]);
            return song;
        }

        /// <summary>
        /// Gets the song from song identifier.
        /// </summary>
        /// <param name="songId">The song identifier.</param>
        /// <param name="conn">The connection.</param>
        /// <returns></returns>
        public static Song GetSongFromSongId(string songId, DataBaseConnector conn)
        {

            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = "select song_name from songs where id_song = \"" + songId + "\"";
            List<string> result = conn.ExecuteOneColumnCommand(command);
            if (result.Count <= 0)
            {
                return null;
            }

            Song song = new Song(result[0], songId);
            return song;
        }

        /// <summary>
        /// Gets the name of the artist from artist.
        /// </summary>
        /// <param name="artistName">Name of the artist.</param>
        /// <param name="conn">The connection.</param>
        /// <returns></returns>
        public static Artist GetArtistFromArtistName(string artistName, DataBaseConnector conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = "select * from artists where lower(artist_name) = \"" + artistName.ToLower() + "\" limit 1";
            List<Dictionary<string, string>> result = conn.ExecuteCommand(command);
            if(result.Count == 0)
            {
                return null;
            }

            Artist artist = new Artist();
            artist.Name = result[0]["artist_name"];
            artist.Id = result[0]["id_artist"];
            artist.Day = result[0]["begin_date_day"];
            artist.Month = result[0]["begin_date_month"];
            artist.Year = result[0]["begin_date_year"];
            return artist;
        }

        /// <summary>
        /// Gets the artist from artist identifier.
        /// </summary>
        /// <param name="artistId">The artist identifier.</param>
        /// <param name="conn">The connection.</param>
        /// <returns></returns>
        public static Artist GetArtistFromArtistId(string artistId, DataBaseConnector conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = "select * from artists where id_artist = \"" + artistId + "\" limit 1";
            List<Dictionary<string, string>> result = conn.ExecuteCommand(command);
            if (result.Count == 0)
            {
                return null;
            }

            Artist artist = new Artist();
            artist.Name = result[0]["artist_name"];
            artist.Id = result[0]["id_artist"];
            artist.Day = result[0]["begin_date_day"];
            artist.Month = result[0]["begin_date_month"];
            artist.Year = result[0]["begin_date_year"];
            return artist;
        }

        /// <summary>
        /// Gets the name of the place from place.
        /// </summary>
        /// <param name="placeName">Name of the place.</param>
        /// <param name="conn">The connection.</param>
        /// <returns></returns>
        public static Place GetPlaceFromPlaceName(string placeName, DataBaseConnector conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = "select id_area from area where lower(area_name) = \"" + placeName.ToLower() + "\"";
            List<string> result = conn.ExecuteOneColumnCommand(command);
            if (result.Count <= 0)
            {
                return null;
            }

            Place place = new Place(int.Parse(result[0]), placeName);
            return place;
        }

        /// <summary>
        /// Gets the place from place identifier.
        /// </summary>
        /// <param name="placeId">The place identifier.</param>
        /// <param name="conn">The connection.</param>
        /// <returns></returns>
        public static Place GetPlaceFromPlaceId(int placeId, DataBaseConnector conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = "select area_name from area where id_area = " + placeId;
            List<string> result = conn.ExecuteOneColumnCommand(command);
            if (result.Count <= 0)
            {
                return null;
            }

            Place place = new Place(placeId, result[0]);
            return place;
        }

        /// <summary>
        /// Gets the name of the genre from genre.
        /// </summary>
        /// <param name="genreName">Name of the genre.</param>
        /// <param name="conn">The connection.</param>
        /// <returns></returns>
        public static Genre GetGenreFromGenreName(string genreName, DataBaseConnector conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = "select id_genre from genres where lower(genere_name) = \"" + genreName.ToLower() + "\"";
            List<string> result = conn.ExecuteOneColumnCommand(command);
            if (result.Count <= 0)
            {
                return null;
            }

            Genre genreObject = new Genre(genreName, int.Parse(result[0]));
            return genreObject;
        }

        /// <summary>
        /// Gets the genre from genre identifier.
        /// </summary>
        /// <param name="genreId">The genre identifier.</param>
        /// <param name="conn">The connection.</param>
        /// <returns></returns>
        public static Genre GetGenreFromGenreId(int genreId, DataBaseConnector conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = "select genere_name from genres where id_genre = " + genreId;
            List<string> result = conn.ExecuteOneColumnCommand(command);
            if (result.Count <= 0)
            {
                return null;
            }

            Genre genre = new Genre(result[0], genreId);
            return genre;
        }

        /// <summary>
        /// Gets the user from email and password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="conn">The connection.</param>
        /// <returns></returns>
        public static User GetUserFromEmailAndPassword(string email,string password,DataBaseConnector conn)
        {
            MySqlCommand getUser = UserQueryBank.GetSimpleUserQuery(conn.Connection);
            getUser.Parameters["@email"].Value = email;
            getUser.Parameters["@password"].Value = password;
            List<Dictionary<string, string>> result = conn.ExecuteCommand(getUser);
            if (result.Count == 0)
            {
                return null;
            }

            User user = new User();
            Dictionary<string, string> userAsDictionary = result[0];
            user.Id = int.Parse(userAsDictionary["id_user"]);
            user.FirstName = userAsDictionary["first_name"];
            user.LastName = userAsDictionary["last_name"];
            user.Email = userAsDictionary["email"];
            user.PlaceId = int.Parse(userAsDictionary["id_area"]);
            user.GenreId = int.Parse(userAsDictionary["id_favorite_genre"]);
            user.Year = int.Parse(userAsDictionary["birthday_year"]);
            user.Day = int.Parse(userAsDictionary["birthday_day"]);
            user.Month = int.Parse(userAsDictionary["birthday_month"]);

            MySqlCommand getSongs = UserQueryBank.GetUserSongsQuery(conn.Connection);
            getSongs.Parameters["@userId"].Value = user.Id;
            List<string> songs = conn.ExecuteOneColumnCommand(getSongs);
            user.Songs = songs;

            MySqlCommand getArtists = UserQueryBank.GetUserArtistsQuery(conn.Connection);
            getArtists.Parameters["@userId"].Value = user.Id;
            List<string> artists = conn.ExecuteOneColumnCommand(getArtists);
            foreach (string artist in artists)
            {
                user.AddArtist(int.Parse(artist));
            }

            return user;
        }
    }
}
