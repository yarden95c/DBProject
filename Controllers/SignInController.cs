using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseLayer;

namespace Controllers
{
    /// <summary>
    /// SignInController - this is the controller of the sign in button.
    /// </summary>
    public class SignInController
    {
        /// <summary>
        /// The connected user
        /// </summary>
        private static User connectedUser;
        /// <summary>
        /// The database connector
        /// </summary>
        private DataBaseConnector db;
        /// <summary>
        /// The instance of this controller
        /// </summary>
        private static SignInController _instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="SignInController"/> class from being created.
        /// </summary>
        private SignInController()
        {
            connectedUser = null;
            db = DataBaseConnector.GetInstance();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns> an instance of this class </returns>
        public static SignInController GetInstance()
        {
            return _instance ?? (_instance = new SignInController());
        }

        /// <summary>
        /// Gets the connected user.
        /// </summary>
        /// <value>
        /// The connected user.
        /// </value>
        public User ConnectedUser { get => connectedUser = new User
        {
            Id = 2,
            Artists = new List<int> { 6046, 8391, 4490, 18 },
            Day = 25,
            Email = "dui@Craseo.net",
            FirstName = "Nathan",
            LastName = "Naomi",
            Month = 4,
            PlaceId = 16521,
            Songs = new List<string> { "TRAGIHZ12903CD2CBD",
                "TRAAPCL128F930CB6C",
                "TRABFIY128F92D1A12",
                "TRABFIY128F92D1A12",
                "TRAEMKM128F145FCFE" },
            Year = 1950
        }; }

        /// <summary>
        /// Sign in the user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns> true if the user logged in successfully, and false otherwise. </returns>
        public bool SignIn(string email, string password)
        {
            connectedUser = new SignInExecuter(email, password, db).Execute();
            return ConnectedUser != null;
        }

        /// <summary>
        /// Sign out the user.
        /// </summary>
        public void SignOut()
        {
            connectedUser = null;
        }
    }
}
