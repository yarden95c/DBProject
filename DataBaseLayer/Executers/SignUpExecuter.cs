using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DataBaseLayer.Entities;

namespace DataBaseLayer.Executers
{
    public class SignUpExecuter
    {
        private DataBaseConnector conn;
        private MySqlCommand addUserCommand;
        private const string addSongCommandText = "INSERT ignore INTO favoritesongbyuser (id_user, id_song) VALUES (@idUser, @idSong)";
        private const string addArtistCommandText = "INSERT ignore INTO favoriteartistbyuser (id_user, id_artist) VALUES (@idUser, @idArtist)";

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
