using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// HeuristicsBank - this class is a bank for different kinds of heuristics.
    /// </summary>
    public class HeuristicsBank
    {
        /// <summary>
        /// This delegate represent a method that create SelfExecuterHeuristics for the genre query, according to the specified user and genre.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="genre">The genre.</param>
        /// <returns></returns>
        public delegate SelfExecuterHeuristics GenreHeuristics(User user, Genre genre);
        /// <summary>
        /// This delegate represent a method that create Heuristics for the number query, according to the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public delegate Heuristics NumberHeuristics(User user);
        /// <summary>
        /// This delegate represent a method that create SelfExecuterHeuristics for the place query, according to the specified user and place.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="place">The place.</param>
        /// <returns></returns>
        public delegate SelfExecuterHeuristics PlaceHeuristics(User user, Place place);
        /// <summary>
        /// This delegate represent a method that create SelfExecuterHeuristics for the year query, according to the specified user and years.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="fromYear">From year.</param>
        /// <param name="toYear">To year.</param>
        /// <returns></returns>
        public delegate SelfExecuterHeuristics YearHeuristics(User user, int fromYear, int toYear);

        /// <summary>
        /// Gets the genre heuristics.
        /// </summary>
        /// <param name="conn">The database connector.</param>
        /// <returns> GenreHeuristics delegate </returns>
        public static GenreHeuristics GetGenreHeuristics(DataBaseConnector conn)
        {
            GenreHeuristics queriesList = (User u, Genre g) =>
            {
                if (g == null)
                {
                    g = Entities.EntitiesFactory.GetGenreFromGenreId(u.GenreId, conn);
                }
                string format = "The top 10 popular bands tagged as the genre \"" + g.Name + "\" are:\n{0}";
                string commandText = "select distinct artists.artist_name from artists,genresbyartist where artists.type = \"Group\" and artists.id_artist = genresbyartist.id_artist and genresbyartist.id_genre = " + g.Id + " group by artists.id_artist order by  sum(num_of_hits) DESC limit 10;";
                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics(commandText, format, conn);
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<Dictionary<string, string>> result = conn.ExecuteCommand(heuristics.Command);
                    if (result.Count == 0)
                    {
                        return "There are no bands tagged as the genre \"" + g.Name + "\"";
                    }

                    StringBuilder builder = new StringBuilder();
                    foreach (Dictionary<string, string> record in result)
                    {
                        builder.AppendLine(record["artist_name"]);
                    }

                    return string.Format(heuristics.ResultFormat, builder.ToString());
                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, Genre g) =>
            {
                if (g == null)
                {
                    g = Entities.EntitiesFactory.GetGenreFromGenreId(u.GenreId, conn);
                }
                string format = "The top 10 popular places with artists tagged as the genre \"" + g.Name + "\" are:\n{0}";
                string commandText = "select area.area_name, area_type from area,artists,genresbyartist where area.id_area = artists.id_area and artists.id_artist = genresbyartist.id_artist and genresbyartist.id_genre = " + g.Id + " group by artists.id_area order by sum(num_of_hits) DESC limit 10;";
                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics(commandText, format, conn);
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<Dictionary<string, string>> result = conn.ExecuteCommand(heuristics.Command);
                    if (result.Count == 0)
                    {
                        return "There are no places with artists tagged as the genre \"" + g.Name + "\"";
                    }

                    StringBuilder builder = new StringBuilder();
                    foreach (Dictionary<string, string> record in result)
                    {
                        builder.AppendLine(record["area_name"] + " (" + record["area_type"] + ")");
                    }

                    return string.Format(heuristics.ResultFormat, builder.ToString());
                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, Genre g) =>
            {
                if (g == null)
                {
                    g = Entities.EntitiesFactory.GetGenreFromGenreId(u.GenreId, conn);
                }
                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select count(id_artist) from genresbyartist where id_genre = " + g.Id, "The number of artists tagged as the genre \"" + g.Name + "\" is {0}", conn);
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    int num = connector.ExecuteScalarCommand(heuristics.Command);
                    return string.Format(heuristics.ResultFormat, num);
                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, Genre g) =>
            {
                if (g == null)
                {
                    g = Entities.EntitiesFactory.GetGenreFromGenreId(u.GenreId, conn);
                }
                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select artist_name,num_of_hits from artists left join genresbyartist using (id_artist) where id_genre = " + g.Id + " order by num_of_hits desc limit 1", "The most popular artist that tagged as the genre \"" + g.Name + "\" is {0}, with {1} number of hits in our database", conn);
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<Dictionary<string, string>> result = connector.ExecuteCommand(heuristics.Command);
                    if (result.Count == 0)
                    {
                        return "There are no artists tagged as the genre \"" + g.Name + "\"";
                    }
                    return string.Format(heuristics.ResultFormat, result[0]["artist_name"], result[0]["num_of_hits"]);
                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, Genre g) =>
            {
                string commandText, format;
                if (g == null)
                {
                    commandText = "select count(*) as num,genere_name from (select id_artist from favoriteartistbyuser where id_user = " + u.Id + ") as t left join genresbyartist using(id_artist) left join genres using (id_genre) where genere_name is not null group by id_genre limit 1;";
                }
                else
                {
                    commandText = "select count(*) as num,genere_name from (select id_artist from favoriteartistbyuser where id_user = " + u.Id + ") as t left join genresbyartist using(id_artist) left join genres using (id_genre) where genere_name is not null and genere_name = \"" + g.Name + "\" group by id_genre limit 1;";
                }

                format = "There are {0} of your favourite artists that tagged as the genre \"{1}\"";
                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics(commandText, format, conn);
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<Dictionary<string, string>> result = connector.ExecuteCommand(heuristics.Command);
                    if (result.Count == 0)
                    {
                        return string.Format(heuristics.ResultFormat, 0, g.Name);
                    }
                    return string.Format(heuristics.ResultFormat, result[0]["num"], result[0]["genere_name"]);
                };
                heuristics.Executer = del;
                return heuristics;
            };

            return queriesList;
        }


        /// <summary>
        /// Gets the number heuristics.
        /// </summary>
        /// <param name="conn">The database connector.</param>
        /// <returns> NumberHeuristics delegate </returns>
        public static NumberHeuristics GetNumberHeuristics(DataBaseConnector conn)
        {
            NumberHeuristics queriesList = (User u) =>
            {
                string commandText = "select count(id_artist) from artists where lower(artist_name) like \"%" + u.FirstName.ToLower() + "%\" or lower(artist_name) like \"%" + u.LastName.ToLower() + "%\"";
                return HeuristicsFactory.CreateHeuristics(commandText, "The number of artists that their names is like your first or last name is: {0}", conn);
            };
            queriesList += (User u) =>
            {
                string commandText = "select count(id_artist) from artists where lower(artist_name) like \"%" + u.FirstName.ToLower() + "%\" or lower(artist_name) like \"%" + u.LastName.ToLower() + "%\"";
                return HeuristicsFactory.CreateHeuristics(commandText, "The number of artists that their names is like your first or last name is: {0}", conn);
            };
            queriesList += (User u) =>
            {
                return HeuristicsFactory.CreateHeuristics("select count(songsbyartist.id_song) from songsbyartist,genresbyartist,users,genres where songsbyartist.id_artist = genresbyartist.id_artist and users.id_user = " + u.Id + " and genres.id_genre = users.id_favorite_genre and genresbyartist.id_genre = genres.id_genre;", "The number of songs from your favourite genre is: {0}", conn);
            };
            queriesList += (User u) =>
              {
                  return HeuristicsFactory.CreateHeuristics("select count(id_artist) from artists where id_area in(select distinct artists.id_area from artists,favoriteartistbyuser where artists.id_artist = favoriteartistbyuser.id_artist and favoriteartistbyuser.id_user= " + u.Id + ");", "The number of artist that has the same place as your favorite artists is: {0}", conn);
              };
            queriesList += (User u) =>
            {
                return HeuristicsFactory.CreateHeuristics("select count(id_artist) from artists where id_area = " + u.PlaceId, "The number of artists from your birth place is {0}", conn);
            };
            queriesList += (User u) =>
            {
                return HeuristicsFactory.CreateHeuristics("select count(id_artist) from artists where begin_date_year = " + u.Year, "The number of artists born at your birth year is {0}", conn);
            };
            queriesList += (User u) =>
            {
                return HeuristicsFactory.CreateHeuristics("select count(id_artist) from genresbyartist where id_genre = " + u.GenreId, "The number of artists from your favourite genere is {0}", conn);
            };
            queriesList += (User u) =>
            {
                return HeuristicsFactory.CreateHeuristics("select count(id_song) from songs where release_date_year = " + u.Year, "The number of songs that was released during your birth year is {0}", conn);
            };
            queriesList += (User u) =>
            {
                return HeuristicsFactory.CreateHeuristics("select count(id_artist) from artists where begin_date_month = " + u.Month + " and begin_date_day = " + u.Day, "The number of artists born at your birthday is {0}", conn);
            };

            return queriesList;
        }

        /// <summary>
        /// Gets the place heuristics.
        /// </summary>
        /// <param name="conn">The database connector.</param>
        /// <returns> PlaceHeuristics delegate </returns>
        public static PlaceHeuristics GetPlaceHeuristics(DataBaseConnector conn)
        {
            PlaceHeuristics queriesList = (User u, Place p) =>
            {
                if (p == null)
                {
                    p = Entities.EntitiesFactory.GetPlaceFromPlaceId(u.PlaceId, conn);
                }
                string format = "The top 10 popular female singers from " + p.Name + " are:\n{0}";
                string commandText = "select distinct artist_name,num_of_hits from artists where id_area=" + p.Id + " and gender=\"Female\" order by num_of_hits desc limit 10;";
                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics(commandText, format, conn);
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<Dictionary<string, string>> result = conn.ExecuteCommand(heuristics.Command);
                    if (result.Count == 0)
                    {
                        return "There are no female singers from " + p.Name;
                    }

                    StringBuilder builder = new StringBuilder();
                    foreach (Dictionary<string, string> record in result)
                    {
                        builder.AppendLine(record["artist_name"] + " with " + record["num_of_hits"] + " hits in our database");
                    }
                    return string.Format(heuristics.ResultFormat, builder.ToString());
                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, Place p) =>
           {
               if (p == null)
               {
                   p = Entities.EntitiesFactory.GetPlaceFromPlaceId(u.PlaceId, conn);
               }
               SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select count(id_artist) from artists where id_area = " + p.Id, "The number of artists from " + p.Name + " is {0}", conn);
               SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
               {
                   int num = connector.ExecuteScalarCommand(heuristics.Command);
                   return string.Format(heuristics.ResultFormat, num);
               };
               heuristics.Executer = del;
               return heuristics;
           };

            queriesList += (User u, Place p) =>
            {
                if (p == null)
                {
                    p = Entities.EntitiesFactory.GetPlaceFromPlaceId(u.PlaceId, conn);
                }
                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select count(id_artist) from artists left join genresbyartist using(id_artist) where id_area = " + p.Id + " and id_genre = " + u.GenreId, "The number of artists tagged as your favourite genere and from " + p.Name + " is {0}", conn);
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    int num = connector.ExecuteScalarCommand(heuristics.Command);
                    return string.Format(heuristics.ResultFormat, num);
                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, Place p) =>
            {
                if (p == null)
                {
                    p = Entities.EntitiesFactory.GetPlaceFromPlaceId(u.PlaceId, conn);
                }
                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select artist_name from artists where id_area = " + p.Id + " limit 10", "These are artists from " + p.Name + ":\n{0}", conn);
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<string> result = connector.ExecuteOneColumnCommand(heuristics.Command);
                    if (result.Count == 0)
                    {
                        return "There are no artists from " + p.Name;
                    }
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (string artist in result)
                    {
                        stringBuilder.AppendLine(artist);
                    }
                    return string.Format(heuristics.ResultFormat, stringBuilder.ToString());
                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, Place p) =>
            {
                if (p == null)
                {
                    p = Entities.EntitiesFactory.GetPlaceFromPlaceId(u.PlaceId, conn);
                }
                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select distinct song_name from songs where lower(song_name) like \"%" + p.Name.ToLower() + "%\" limit 10", "These are songs with \"" + p.Name + "\" in there title:\n{0}", conn);
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<string> result = connector.ExecuteOneColumnCommand(heuristics.Command);
                    if (result.Count == 0)
                    {
                        return "There are no songs with \"" + p.Name + "\" in there title";
                    }
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (string song in result)
                    {
                        stringBuilder.AppendLine(song);
                    }
                    return string.Format(heuristics.ResultFormat, stringBuilder.ToString());
                };
                heuristics.Executer = del;
                return heuristics;
            };

            return queriesList;
        }

        /// <summary>
        /// Gets the year heuristics.
        /// </summary>
        /// <param name="conn">The database connector.</param>
        /// <returns> YearHeuristics delegate </returns>
        public static YearHeuristics GetYearHeuristics(DataBaseConnector conn)
        {
            YearHeuristics queriesList = (User u, int f, int t) =>
            {
                string format;
                if (f != t)
                {
                    format = "The top 10 popular genres between " + f + " and " + t + " are:\n{0}";
                }
                else
                {
                    format = "The top 10 popular genres in the year " + f + " are:\n{0}";
                }

                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select genere_name from genres, genresbyartist, artists where genres.id_genre = genresbyartist.id_genre and genresbyartist.id_artist = artists.id_artist and begin_date_year between " + f + " and " + t + " group by genresbyartist.id_genre order by sum(artists.num_of_hits) desc limit 10;", format, conn);

                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<Dictionary<string, string>> result = conn.ExecuteCommand(heuristics.Command);
                    if (result.Count == 0)
                    {
                        if (f != t)
                        {
                            return "We don't have records of genres between the years " + f + " and " + t;
                        }
                        else
                        {
                            return "We don't have records of genres in the year " + f;
                        }
                    }

                    StringBuilder builder = new StringBuilder();
                    foreach (Dictionary<string, string> record in result)
                    {
                        builder.AppendLine(record["genere_name"]);
                    }
                    return string.Format(heuristics.ResultFormat, builder.ToString());
                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, int f, int t) =>
            {
                string format;
                if (f != t)
                {
                    format = "The top 10 artists that released most songs between " + f + " and " + t + " are:\n{0}";
                }
                else
                {
                    format = "The top 10 artists that released most songs in the year " + f + " are:\n{0}";
                }

                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select distinct artist_name,count(*) as total from artists,songsbyartist,songs where artists.id_artist = songsbyartist.id_artist and songsbyartist.id_song=songs.id_song and release_date_year between " + f + " and " + t + " group by songsbyartist.id_artist order by count(*) desc limit 10;", format, conn);

                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<Dictionary<string, string>> result = conn.ExecuteCommand(heuristics.Command);
                    if (result.Count == 0)
                    {
                        if (f != t)
                        {
                            return "There are no artists that released songs between " + f + " and " + t;
                        }
                        else
                        {
                            return "There are no artists that released songs in the year " + f;
                        }
                    }

                    StringBuilder builder = new StringBuilder();
                    foreach (Dictionary<string, string> record in result)
                    {
                        builder.AppendLine(record["artist_name"] + " with " + record["total"] + " songs");
                    }
                    return string.Format(heuristics.ResultFormat, builder.ToString());
                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, int f, int t) =>
            {
                string format;
                if (f != t)
                {
                    format = "The number of artists that died between the years " + f + " and " + t + " is {0}";
                }
                else
                {
                    format = "The number of artists that died in the year " + f + " is {0}";
                }

                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select count(id_artist) from artists where end_date_year between " + f + " and " + t, format, conn);

                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    int num = conn.ExecuteScalarCommand(heuristics.Command);
                    return string.Format(heuristics.ResultFormat, num);
                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, int f, int t) =>
            {
                string format;
                if (f != t)
                {
                    format = "The top 10 popular songs released between " + f + " and " + t + " are:\n{0}";
                }
                else
                {
                    format = "The top 10 popular songs released in " + f + " are:\n{0}";
                }

                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select distinct song_name,num_of_hits from songs where release_date_year between " + f + " and " + t + " order by num_of_hits DESC limit 10;", format, conn);

                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<Dictionary<string, string>> result = conn.ExecuteCommand(heuristics.Command);
                    if (result.Count == 0)
                    {
                        if (f != t)
                        {
                            return "There are no songs released between " + f + " and " + t;
                        }
                        else
                        {
                            return "There are no songs released in " + f;
                        }
                    }

                    StringBuilder builder = new StringBuilder();
                    foreach (Dictionary<string, string> record in result)
                    {
                        builder.AppendLine(record["song_name"]);
                    }
                    return string.Format(heuristics.ResultFormat, builder.ToString());


                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, int f, int t) =>
             {
                 string format;
                 if (f != t)
                 {
                     format = "The most popular atrtist born between " + f + " and " + t + " is: {0}";
                 }
                 else
                 {
                     format = "The most popular atrtist born in " + f + " is: {0}";
                 }

                 SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select artist_name, num_of_hits from artists where begin_date_year between " + f + " and " + t + " order by num_of_hits DESC limit 1; ", format, conn);

                 SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                 {
                     List<Dictionary<string, string>> result = conn.ExecuteCommand(heuristics.Command);
                     if (result.Count == 0)
                     {
                         if (f != t)
                         {
                             return "There are no artists born between " + f + " and " + t;
                         }
                         else
                         {
                             return "There are no artists born in " + f;
                         }
                     }

                     return string.Format(heuristics.ResultFormat, result[0]["artist_name"]);
                 };
                 heuristics.Executer = del;
                 return heuristics;
             };

            queriesList += (User u, int f, int t) =>
            {
                string format;
                if (f != t)
                {
                    format = "The top 10 popular artists that born between " + f + " and " + t + " are:\n{0}";
                }
                else
                {
                    format = "The top 10 popular artists that born in the year " + f + " are:\n{0}";
                }

                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select artist_name,num_of_hits from artists where begin_date_year between " + f + " and " + t + " group by id_artist order by num_of_hits DESC limit 10;", format, conn);

                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<Dictionary<string, string>> result = conn.ExecuteCommand(heuristics.Command);
                    if (result.Count == 0)
                    {
                        if (f != t)
                        {
                            return "There are no artists born between " + f + " and " + t;
                        }
                        else
                        {
                            return "There are no artists born in " + f;
                        }
                    }

                    StringBuilder builder = new StringBuilder();
                    foreach (Dictionary<string, string> record in result)
                    {
                        builder.AppendLine(record["artist_name"]);
                    }
                    return string.Format(heuristics.ResultFormat, builder.ToString());
                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, int f, int t) =>
              {
                  string format;
                  if (f != t)
                  {
                      format = "The number of artists born between " + f + " and " + t + " is {0}";
                  }
                  else
                  {
                      format = "The number of artists born in " + f + " is {0}";
                  }

                  SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select count(id_artist) from artists where begin_date_year between " + f + " and " + t, format, conn);

                  SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                  {
                      int num = connector.ExecuteScalarCommand(heuristics.Command);
                      return string.Format(format, num);
                  };
                  heuristics.Executer = del;
                  return heuristics;
              };

            queriesList += (User u, int f, int t) =>
            {
                string format;
                if (f != t)
                {
                    format = "The number of songs released between " + f + " and " + t + " is {0}";
                }
                else
                {
                    format = "The number of songs released in " + f + " is {0}";
                }

                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select count(id_song) from songs where release_date_year between " + f + " and " + t, format, conn);

                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    int num = connector.ExecuteScalarCommand(heuristics.Command);
                    return string.Format(format, num);
                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, int f, int t) =>
            {
                string format;
                if (f != t)
                {
                    format = "These are artists born between " + f + " and " + t + ":\n{0}";
                }
                else
                {
                    format = "These are artists born in " + f + ":\n{0}";
                }

                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select artist_name from artists where begin_date_year between " + f + " and " + t + " limit 10", format, conn);

                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<string> result = connector.ExecuteOneColumnCommand(heuristics.Command);
                    if (result.Count == 0)
                    {
                        if (f != t)
                        {
                            return "There are no artists born between " + f + " and " + t;
                        }
                        else
                        {
                            return "There are no artists born in " + f;
                        }
                    }
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (string artist in result)
                    {
                        stringBuilder.AppendLine(artist);
                    }
                    return string.Format(format, stringBuilder.ToString());
                };
                heuristics.Executer = del;
                return heuristics;
            };

            queriesList += (User u, int f, int t) =>
            {
                string format;
                if (f != t)
                {
                    format = "These are songs released between " + f + " and " + t + ":\n{0}";
                }
                else
                {
                    format = "These are songs released in " + f + ":\n{0}";
                }

                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics("select song_name from songs where release_date_year between " + f + " and " + t + " limit 10", format, conn);

                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<string> result = connector.ExecuteOneColumnCommand(heuristics.Command);
                    if (result.Count == 0)
                    {
                        if (f != t)
                        {
                            return "There are no songs released between " + f + " and " + t;
                        }
                        else
                        {
                            return "There are no songs released in " + f;
                        }
                    }
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (string artist in result)
                    {
                        stringBuilder.AppendLine(artist);
                    }
                    return string.Format(format, stringBuilder.ToString());
                };
                heuristics.Executer = del;
                return heuristics;
            };

            return queriesList;
        }
    }
}
