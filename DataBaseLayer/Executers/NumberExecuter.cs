using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    /// <summary>
    /// NumberExecuter - this class represent the executer of the number query.
    /// </summary>
    /// <seealso cref="DataBaseLayer.IExecuter" />
    public class NumberExecuter : IExecuter
    {

        /// <summary>
        /// The queries list
        /// </summary>
        private HeuristicsBank.NumberHeuristics queriesList;
        /// <summary>
        /// The user
        /// </summary>
        private User user;
        /// <summary>
        /// The database connector
        /// </summary>
        private DataBaseConnector conn;
        /// <summary>
        /// The random
        /// </summary>
        private static Random rand = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberExecuter"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="connector">The connector.</param>
        public NumberExecuter(User user, DataBaseConnector connector)
        {
            this.user = user;
            this.conn = connector;
            this.queriesList = HeuristicsBank.GetNumberHeuristics(this.conn);
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <returns>
        /// string that reprsent the result
        /// </returns>
        public string Execute()
        {
            Delegate[] arr = queriesList.GetInvocationList();
            int queryNum = rand.Next(arr.Length);
            Heuristics h = ((HeuristicsBank.NumberHeuristics)arr[queryNum])(user);
            int number = conn.ExecuteScalarCommand(h.Command);
            return string.Format(h.ResultFormat, number);
        }

        /// <summary>
        /// Gets the string that repsent that the query returned nothing.
        /// </summary>
        /// <returns>
        /// the sorry message
        /// </returns>
        public string GetSorryMsg()
        {
            return string.Empty;
        }
    }
}
