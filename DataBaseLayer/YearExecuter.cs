using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    public class YearExecuter : IExecuter
    {
        public delegate SelfExecuterHeuristics YearHeuristics(User user, int fromYear, int toyYear);

        private DataBaseConnector conn;
        private int fromYear;
        private int toYear;
        private User user;
        private YearHeuristics queriesList;
        private static Random rand = new Random();

        public YearExecuter(DataBaseConnector db,int from,int to,User user)
        {
            this.conn = db;
            this.user = user;
            this.fromYear = from;
            this.toYear = to;

            queriesList += (User u, int f, int t) =>
             {
                 MySqlCommand command = new MySqlCommand();
                 command.CommandText = "select count(id_artist) from artists where begin_date_year between " + f + " and "+ t ;
                 command.Connection = conn.Connection;
                 string format;
                 if(f != t)
                 {
                     format = "The number of artists born between " + f + " and " + t + " is {0}";
                 } else
                 {
                     format = "The number of artists born in " + f + " is {0}";
                 }
                 SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                 {
                     int num = connector.ExecuteScalarCommand(command);
                     return string.Format(format, num);
                 };
                 return new SelfExecuterHeuristics(command, format, del);
             };

            queriesList += (User u, int f, int t) =>
            {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "select count(id_song) from songs where release_date_year between " + f + " and " + t ;
                command.Connection = conn.Connection;
                string format;
                if (f != t)
                {
                    format = "The number of songs released between " + f + " and " + t + " is {0}";
                } else
                {
                    format = "The number of songs released in " + f + " is {0}";
                }
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    int num = connector.ExecuteScalarCommand(command);
                    return string.Format(format, num);
                };
                return new SelfExecuterHeuristics(command, format, del);
            };

            queriesList += (User u, int f, int t) =>
            {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "select artist_name from artists where begin_date_year between " + f + " and " + t + " limit 10";
                command.Connection = conn.Connection;
                string format;
                if(f != t)
                {
                    format = "These are artists born between " + f + " and " + t + ":\n{0}";
                } else
                {
                    format = "These are artists born in " + f + ":\n{0}";
                }
                
                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<string> result = connector.ExecuteOneColumnCommand(command);
                    if (result.Count == 0)
                    {
                        if(f != t)
                        {
                            return "There are no artists born between " + f + " and " + t;
                        } else
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
                return new SelfExecuterHeuristics(command, format, del);
            };

            queriesList += (User u, int f, int t) =>
            {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "select song_name from songs where release_date_year between " + f + " and " + t + " limit 10";
                command.Connection = conn.Connection;
                string format;
                if (f != t)
                {
                    format = "These are songs released between " + f + " and " + t + ":\n{0}";
                }
                else
                {
                    format = "These are songs released in " + f + ":\n{0}";
                }

                SelfExecuterHeuristics.ExecuteDel del = (DataBaseConnector connector) =>
                {
                    List<string> result = connector.ExecuteOneColumnCommand(command);
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
                return new SelfExecuterHeuristics(command, format, del);
            };

        }

        private int GetYear()
        {
            return user.Year;
        }

        public string Execute()
        {
            if (fromYear == -1 && toYear == -1)
            {
                fromYear = GetYear();
                toYear = GetYear();
            }

            Delegate[] arr = queriesList.GetInvocationList();
            int queryNum = rand.Next(arr.Length);
            SelfExecuterHeuristics h = ((YearHeuristics)arr[queryNum])(user,fromYear,toYear);
            return h.Executer(conn);
             
        }

        public string GetSorryMsg()
        {
            return string.Empty;
        }
    }
}
