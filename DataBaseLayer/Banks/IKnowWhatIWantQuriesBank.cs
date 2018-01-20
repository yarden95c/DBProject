using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// IKnowWhatIWantQuriesBank - this class is a bank for different kinds of queries for the "I know what I want" button.
    /// </summary>
    public class IKnowWhatIWantQuriesBank
    {
        /// <summary>
        /// Gets the song query.
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <returns> MySqlCommand - query of song </returns>
        public static MySqlCommand GetSongQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "select DISTINCT song_name,release_date_year,artist_name " +
                                  "from songs left join songsbyartist using(id_song) left join artists using(id_artist) " +
                                  "where lower(song_name) like @songName "+
                                  "and release_date_year between @fromYear and @toYear "+
                                  "and lower(artist_name) like @artistName limit 10";
            command.Connection = conn;
            command.Parameters.AddWithValue("@songName", "%%");
            command.Parameters.AddWithValue("@artistName", "%%");
            command.Parameters.AddWithValue("@fromYear", 0);
            command.Parameters.AddWithValue("@toYear", 9999);
            command.Prepare();
            
            return command;
        }

        /// <summary>
        /// Gets the artist query.
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <returns> MySqlCommand - query of artist </returns>
        public static MySqlCommand GetArtistQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
             command.CommandText = "select DISTINCT id_artist,artist_name,song_name,begin_date_day,begin_date_month,begin_date_year from artists left join songsbyartist using(id_artist) left join songs using(id_song) " +
                                   "where lower(artist_name) like @artistName " +
                                   "and lower(song_name) like @songName " +
                                   "and begin_date_year between @fromYear and @toYear limit 10; ";

             command.Connection = conn;
             command.Parameters.AddWithValue("@songName", "%%");
             command.Parameters.AddWithValue("@artistName", "%%");
             command.Parameters.AddWithValue("@fromYear", 0);
             command.Parameters.AddWithValue("@toYear", 9999);
             command.Prepare();

             return command; 
        }

        /// <summary>
        /// Gets the place query.
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <param name="artistName">if set to <c>true</c> [artist name]- meaning the artist name is present.</param>
        /// <param name="placeName">if set to <c>true</c> [place name]- meaning the place name is present.</param>
        /// <returns> MySqlCommand - query of place </returns>
        public static MySqlCommand GetPlaceQuery(MySqlConnection conn, bool artistName,bool placeName)
        {
            MySqlCommand command = new MySqlCommand();

            if (artistName && placeName)
            {
                command.CommandText = "select area_name, artist_name from area left join artists using (id_area) where lower(area_name) like @placeName and lower(artist_name) like @artistName  limit 10;";
            } else if (placeName)
            {
                command.CommandText = "select area_name, artist_name from (select area_name, IFNULL(artist_name, \"\") as artist_name from area left join artists using (id_area)) as t " +
                                      "where lower(area_name) like @placeName order by area_name limit 10;";
            } else if (artistName)
            {
                command.CommandText = "select area_name, artist_name from area left join artists using (id_area) where lower(artist_name) like @artistName order by area_name limit 10;";
            } else
            {
                command.CommandText = "select area_name, artist_name from area left join artists using (id_area) " +
                                      "limit 10;";
            }


            command.Connection = conn;
            if(placeName)
            {
                command.Parameters.AddWithValue("@placeName", "%%");
            }
            if(artistName)
            {
                command.Parameters.AddWithValue("@artistName", "%%");
            }
            command.Prepare();

            return command;
        }

        /// <summary>
        /// Gets the songs names query.
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <returns> MySqlCommand - query of song names </returns>
        public static MySqlCommand GetSongsNamesQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "select DISTINCT song_name from songs where song_name like @songName limit 10;";

            command.Connection = conn;
            command.Parameters.AddWithValue("@songName", "%%");
            command.Prepare();

            return command;
        }


        /// <summary>
        /// Gets the artists names query.
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <returns> MySqlCommand - query of artists names </returns>
        public static MySqlCommand GetArtistsNamesQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "select DISTINCT artist_name from artists where artist_name like @artistName limit 10;";

            command.Connection = conn;
            command.Parameters.AddWithValue("@artistName", "%%");
            command.Prepare();

            return command;
        }


        /// <summary>
        /// Gets the places names query.
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <returns> MySqlCommand - query of places names </returns>
        public static MySqlCommand GetPlacesNamesQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "select DISTINCT area_name from area where area_name like @placeName limit 10;";

            command.Connection = conn;
            command.Parameters.AddWithValue("@placeName", "%%");
            command.Prepare();

            return command;
        }

        /// <summary>
        /// Gets the genres names query.
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <returns> MySqlCommand - query of genres names </returns>
        public static MySqlCommand GetGenresNamesQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "select DISTINCT genere_name from genres where genere_name like @genreName limit 10;";

            command.Connection = conn;
            command.Parameters.AddWithValue("@genreName", "%%");
            command.Prepare();

            return command;
        }
    }
}
