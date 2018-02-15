using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DataBaseLayer.Entities;

namespace DataBaseLayer.Executers
{
    /// <summary>
    /// SignUpExecuter - this class reprsent an executer for the sign up query.
    /// </summary>
    public class SignUpExecuter
    {
        /// <summary>
        /// The connection
        /// </summary>
        private DataBaseConnector conn;
        /// <summary>
        /// The add user command
        /// </summary>
        private MySqlCommand addUserCommand;
        /// <summary>
        /// The add song command text
        /// </summary>
        private const string addSongCommandText = "INSERT ignore INTO favoritesongbyuser (id_user, id_song) VALUES (@idUser, @idSong)";
        /// <summary>
        /// The add artist command text
        /// </summary>
        private const string addArtistCommandText = "INSERT ignore INTO favoriteartistbyuser (id_user, id_artist) VALUES (@idUser, @idArtist)";

        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpExecuter"/> class.
        /// </summary>
        public SignUpExecuter()
        {
            conn = DataBaseConnector.GetInstance();
            addUserCommand = new MySqlCommand();
            addUserCommand.Connection = conn.Connection;
            addUserCommand.CommandText = "INSERT INTO users (first_name, last_name, email, password, id_area, id_favorite_genre, birthday_year, birthday_month, birthday_day) VALUES(@firstName, @lastName, @email, @password, @idArea, @idGenre, @year, @month, @day)";
            addUserCommand.Parameters.AddWithValue("@firstName", "");
            addUserCommand.Parameters.AddWithValue("@lastName", "");
            addUserCommand.Parameters.AddWithValue("@email", "");
            addUserCommand.Parameters.AddWithValue("@password", "");
            addUserCommand.Parameters.AddWithValue("@idArea", -1);
            addUserCommand.Parameters.AddWithValue("@idGenre", -1);
            addUserCommand.Parameters.AddWithValue("@year", -1);
            addUserCommand.Parameters.AddWithValue("@day", -1);
            addUserCommand.Parameters.AddWithValue("@month", -1);
            addUserCommand.Prepare();

        }

        /// <summary>
        /// Gets the add song command.
        /// </summary>
        /// <param name="idUser">The identifier user.</param>
        /// <param name="idSong">The identifier song.</param>
        /// <returns> add song command </returns>
        private MySqlCommand GetAddSongCommand(int idUser, string idSong)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = addSongCommandText;
            command.Parameters.AddWithValue("@idUser", idUser);
            command.Parameters.AddWithValue("@idSong", idSong);
            command.Prepare();
            return command;
        }

        /// <summary>
        /// Gets the add artist command.
        /// </summary>
        /// <param name="idUser">The identifier user.</param>
        /// <param name="idArtist">The identifier artist.</param>
        /// <returns> add artist command </returns>
        private MySqlCommand GetAddArtistCommand(int idUser,string idArtist)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.Connection;
            command.CommandText = addArtistCommandText;
            command.Parameters.AddWithValue("@idUser", idUser);
            command.Parameters.AddWithValue("@idArtist", idArtist);
            command.Prepare();
            return command;
        }

        /// <summary>
        /// Executes the specified first name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="day">The day.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <param name="password">The password.</param>
        /// <param name="genreName">Name of the genre.</param>
        /// <param name="placeName">Name of the place.</param>
        /// <returns> true if succeeded and false otherwise </returns>
        public bool Execute(string firstName, string lastName, string email, int day, int month, int year, string password, string genreName, string placeName)
        {
            Genre genre = Entities.EntitiesFactory.GetGenreFromGenreName(genreName, conn);
            if (genre == null)
            {
                return false;
            }
            Place place = Entities.EntitiesFactory.GetPlaceFromPlaceName(placeName, conn);
            if (place == null)
            {
                return false;
            }

            addUserCommand.Parameters["@firstName"].Value = firstName;
            addUserCommand.Parameters["@lastName"].Value = lastName;
            addUserCommand.Parameters["@email"].Value = email;
            addUserCommand.Parameters["@password"].Value = password;
            addUserCommand.Parameters["@idArea"].Value = place.Id;
            addUserCommand.Parameters["@idGenre"].Value = genre.Id;
            addUserCommand.Parameters["@day"].Value = day;
            addUserCommand.Parameters["@month"].Value = month;
            addUserCommand.Parameters["@year"].Value = year;

            List<MySqlCommand> commands = new List<MySqlCommand> { addUserCommand };
            return conn.ExecuteUpdateCommands(commands);
        }

        /// <summary>
        /// Adds the songs to user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="songsNames">The songs names.</param>
        /// <returns> list of songs that added to the user </returns>
        public List<string> AddSongsToUser(User user, List<string> songsNames)
        {
            List<MySqlCommand> commands = new List<MySqlCommand>();
            List<Song> songs = new List<Song>();
            List<string> songsId = new List<string>();
            foreach (string songName in songsNames)
            {
                Song song = EntitiesFactory.GetSongFromSongName(songName, conn);
                if (song != null)
                {
                    songs.Add(song);
                }
            }

            foreach (Song song in songs)
            {
                commands.Add(GetAddSongCommand(user.Id, song.Id));
                songsId.Add(song.Id);
            }

            conn.ExecuteUpdateCommands(commands);
            return songsId;
        }

        /// <summary>
        /// Adds the artists to user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="artistsNames">The artists names.</param>
        /// <returns> list of artists added to the user </returns>
        public List<int> AddArtistsToUser(User user, List<string> artistsNames)
        {
            List<MySqlCommand> commands = new List<MySqlCommand>();
            List<Artist> artists = new List<Artist>();
            List<int> artistsId = new List<int>();

            foreach (string artistName in artistsNames)
            {
                Artist artist = EntitiesFactory.GetArtistFromArtistName(artistName, conn);
                if (artist != null)
                {
                    artists.Add(artist);
                }
            }

            foreach (Artist artist in artists)
            {
                commands.Add(GetAddArtistCommand(user.Id, artist.Id));
                artistsId.Add(int.Parse(artist.Id));
            }

            conn.ExecuteUpdateCommands(commands);
            return artistsId;
        }


    }
}
