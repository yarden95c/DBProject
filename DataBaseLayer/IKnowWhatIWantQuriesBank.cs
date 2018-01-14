using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    public class IKnowWhatIWantQuriesBank
    {
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

           /* List<MySqlCommand> commands = new List<MySqlCommand>();
            commands.Add(new MySqlCommand());
            commands.Add(new MySqlCommand());
            foreach (MySqlCommand command in commands)
            {
                command.Connection = conn;
            }

            commands[0].CommandText = "select id_artist,artist_name,begin_date_day,begin_date_month,begin_date_year from artists " +
                                      "where lower(artist_name) like @artistName " +
                                      "and begin_date_year between @fromYear and @toYear;";
            commands[0].Parameters.AddWithValue("@artistName", "%%");
            commands[0].Parameters.AddWithValue("@fromYear", 0);
            commands[0].Parameters.AddWithValue("@toYear", 9999);

            commands[1].CommandText = "select song_name from songsbyartist left join songs using(id_song) where song_name like @songName and id_artist = @idArtist;";
            commands[1].Parameters.AddWithValue("@songName", "%%");
            commands[1].Parameters.AddWithValue("@idArtist", "");

            foreach (MySqlCommand command in commands)
            {
                command.Prepare();
            }

            return commands; */
        }

        public static MySqlCommand GetPlaceQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
            /* command.CommandText = "select area_name,artist_name from area left join artists using (id_area) "+
                                   "where lower(area_name) like @placeName "+
                                   "and lower(artist_name) like @artistName order by area_name;" ; */

            command.CommandText = "select area_name, artist_name from (select area_name, IFNULL(artist_name, \"\") as artist_name from area left join artists using (id_area)) as t " +
                                  "where lower(area_name) like @placeName " +
                                  "and lower(artist_name) like @artistName order by area_name;";


            command.Connection = conn;
            command.Parameters.AddWithValue("@placeName", "%%");
            command.Parameters.AddWithValue("@artistName", "%%");
            command.Prepare();

            return command;
        }

        public static MySqlCommand GetSongsNamesQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "select DISTINCT song_name from songs where song_name like @songName limit 10;";

            command.Connection = conn;
            command.Parameters.AddWithValue("@songName", "%%");
            command.Prepare();

            return command;
        }


        public static MySqlCommand GetArtistsNamesQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "select DISTINCT artist_name from artists where artist_name like @artistName limit 10;";

            command.Connection = conn;
            command.Parameters.AddWithValue("@artistName", "%%");
            command.Prepare();

            return command;
        }


        public static MySqlCommand GetPlacesNamesQuery(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "select DISTINCT area_name from area where area_name like @placeName limit 10;";

            command.Connection = conn;
            command.Parameters.AddWithValue("@placeName", "%%");
            command.Prepare();

            return command;
        }
    }
}
