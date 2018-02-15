using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// Heuristics - this class represent: query, and the format of the string that reprsent the result of the query.
    /// </summary>
    public class Heuristics
    {
        /// <summary>
        /// The command (query)
        /// </summary>
        private MySqlCommand command;
        /// <summary>
        /// The result format
        /// </summary>
        private string resultFormat;
        /// <summary>
        /// Initializes a new instance of the <see cref="Heuristics" /> class.
        /// </summary>
        public Heuristics()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Heuristics" /> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="resultFormat">The result format.</param>
        public Heuristics(MySqlCommand command, string resultFormat)
        {
            this.command = command;
            this.resultFormat = resultFormat;
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public MySqlCommand Command { get => command; set => command = value; }
        /// <summary>
        /// Gets or sets the result format.
        /// </summary>
        /// <value>
        /// The result format.
        /// </value>
        public string ResultFormat { get => resultFormat; set => resultFormat = value; }
    }
}
