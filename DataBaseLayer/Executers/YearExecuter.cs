using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// YearExecuter - this class reprsent the executer of the year query.
    /// </summary>
    /// <seealso cref="DataBaseLayer.IExecuter" />
    public class YearExecuter : IExecuter
    {
        /// <summary>
        /// The database connector
        /// </summary>
        private DataBaseConnector conn;
        /// <summary>
        /// From year
        /// </summary>
        private int fromYear;
        /// <summary>
        /// To year
        /// </summary>
        private int toYear;
        /// <summary>
        /// The user
        /// </summary>
        private User user;
        /// <summary>
        /// The queries list
        /// </summary>
        private HeuristicsBank.YearHeuristics queriesList;
        /// <summary>
        /// The random
        /// </summary>
        private static Random rand = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="YearExecuter"/> class.
        /// </summary>
        /// <param name="db">The database connector.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="user">The user.</param>
        public YearExecuter(DataBaseConnector db,int from,int to,User user)
        {
            this.conn = db;
            this.user = user;
            this.fromYear = from;
            this.toYear = to;
            queriesList = HeuristicsBank.GetYearHeuristics(this.conn);

        }

        /// <summary>
        /// Gets the birth year of the user.
        /// </summary>
        /// <returns>
        /// the birth year of the user.
        /// </returns>
        private int GetYear()
        {
            return user.Year;
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <returns>
        /// string that reprsent the result
        /// </returns>
        public string Execute()
        {
            if (fromYear == -1 && toYear == -1)
            {
                fromYear = GetYear();
                toYear = GetYear();
            }

            Delegate[] arr = queriesList.GetInvocationList();
            int queryNum = rand.Next(arr.Length);
            SelfExecuterHeuristics h = ((HeuristicsBank.YearHeuristics)arr[queryNum])(user,fromYear,toYear);
            return h.Executer(conn);
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
