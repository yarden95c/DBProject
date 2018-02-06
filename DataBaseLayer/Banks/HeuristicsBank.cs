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
                } else
                {
                    commandText = "select count(*) as num,genere_name from (select id_artist from favoriteartistbyuser where id_user = "+ u.Id+") as t left join genresbyartist using(id_artist) left join genres using (id_genre) where genere_name is not null and genere_name = \""+g.Name+"\" group by id_genre limit 1;";
                }

                format = "There are {0} of your favourite artists that tagged as the genre \"{1}\"";
                SelfExecuterHeuristics heuristics = HeuristicsFactory.CreateSelfExecuterHeuristics(commandText, format, conn);
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<Dictionary<string, string>> result = connector.ExecuteCommand(heuristics.Command);
                    if(result.Count == 0)
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
