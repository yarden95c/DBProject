using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    /// <summary>
    /// SignInExecuter - this class reprsent an executer for the sign in query.
    /// </summary>
    public class SignInExecuter
    {
        /// <summary>
        /// The email
        /// </summary>
        private string email;
        /// <summary>
        /// The password
        /// </summary>
        private string password;
        /// <summary>
        /// The database connector
        /// </summary>
        private DataBaseConnector conn;
        /// <summary>
        /// Initializes a new instance of the <see cref="SignInExecuter"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="conn">The connection.</param>
        public SignInExecuter(string email,string password,DataBaseConnector conn)
        {
            this.email = email;
            this.password = password;
            this.conn = conn;
        }

        /// <summary>
        /// Executes the user query.
        /// </summary>
        /// <returns> User instance, that reprsent the user that signed in.</returns>
        public User Execute()
        {
            return Entities.EntitiesFactory.GetUserFromEmailAndPassword(email, password, conn);
        }
    }
}
