using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// HeuristicsFactory - this class creates heuristics according to the given parameters.
    /// </summary>
    class HeuristicsFactory
    {
        /// <summary>
        /// Creates the heuristics according to the given parameters.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="resultFormat">The result format.</param>
        /// <param name="conn">The database connector.</param>
        /// <returns>
        /// the heuristics
        /// </returns>
        public static Heuristics CreateHeuristics(string commandText, string resultFormat, DataBaseConnector conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = commandText;
            command.Connection = conn.Connection;
            command.CommandTimeout = 30;
            return new Heuristics(command, resultFormat);
        }

        /// <summary>
        /// Creates the self executer heuristics according to the given parameters.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="resultFormat">The result format.</param>
        /// <param name="conn">The database connector.</param>
        /// <returns>
        /// the self executer heuristics
        /// </returns>
        public static SelfExecuterHeuristics CreateSelfExecuterHeuristics(string commandText, string resultFormat, DataBaseConnector conn)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = commandText;
            command.Connection = conn.Connection;
            command.CommandTimeout = 30;
            return new SelfExecuterHeuristics(command, resultFormat);
        }
    }
}
