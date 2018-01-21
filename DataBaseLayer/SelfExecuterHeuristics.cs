using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// Heuristics - this class represent:
    /// 1) query.
    /// 2) the format of the string that reprsent the result of the query.
    /// 3) the executer - a method that execute the command and return an answer as a string.
    /// </summary>
    /// <seealso cref="DataBaseLayer.Heuristics" />
    public class SelfExecuterHeuristics : Heuristics
    {
        /// <summary>
        /// This delegate represent a  method that execute the command and return an answer as a string.
        /// </summary>
        /// <param name="db">The database connector.</param>
        /// <returns></returns>
        public delegate string ExecuteDel(DataBaseConnector db);

        /// <summary>
        /// The executer
        /// </summary>
        private ExecuteDel executer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfExecuterHeuristics"/> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="resultFormat">The result format.</param>
        /// <param name="executer">The executer.</param>
        public SelfExecuterHeuristics(MySqlCommand command, string resultFormat,ExecuteDel executer) : base(command,resultFormat)
        {
            this.executer = executer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfExecuterHeuristics"/> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="resultFormat">The result format.</param>
        public SelfExecuterHeuristics(MySqlCommand command, string resultFormat) : base(command, resultFormat)
        {
        }

        /// <summary>
        /// Gets or sets the executer.
        /// </summary>
        /// <value>
        /// The executer.
        /// </value>
        public ExecuteDel Executer { get => executer; set => executer = value; }
    }
}
