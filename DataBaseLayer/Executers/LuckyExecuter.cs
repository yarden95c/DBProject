using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    /// <summary>
    /// LuckyExecuter - this class reprsent the query executer for the "I'm feeling lucky" button.
    /// </summary>
    /// <seealso cref="DataBaseLayer.IExecuter" />
    public class LuckyExecuter : IExecuter
    {
        /// <summary>
        /// The executer
        /// </summary>
        private IExecuter executer;
        /// <summary>
        /// The database connector
        /// </summary>
        private DataBaseConnector db;
        /// <summary>
        /// The user
        /// </summary>
        private User user;

        /// <summary>
        /// Initializes a new instance of the <see cref="LuckyExecuter" /> class.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="user">The user.</param>
        public LuckyExecuter(DataBaseConnector db,User user)
        {
            this.db = db;
            this.user = user;
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <returns>
        /// string that reprsent the result
        /// </returns>
        public string Execute()
        {
            string result;
            do
            {
                executer = ExecuterFactory.GetExecuter(user, db);
                result = executer.Execute();
            } while (result.Equals(executer.GetSorryMsg()));

            return result;
        }

        /// <summary>
        /// Gets the string that repsent that the query returned nothing.
        /// </summary>
        /// <returns>
        /// the sorry message
        /// </returns>
        public string GetSorryMsg()
        {
            return "";
        }
    }
}
