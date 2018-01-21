using System.Linq;
using DataBaseLayer;

namespace Controllers
{
    public class SignUpController : CompletionController
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

        public SignUpController()
        {
            connectedUser = null;
            db = DataBaseConnector.GetInstance();
        }

        public bool IsValidGenre(string genreName)
        {
            return GetTopGenresNames(genreName).Any(g => g.Equals(genreName));
        }

        public bool IsValidPlace(string placeName)
        {
            return GetTopPlacesNames(placeName).Any(p => p.Equals(placeName));
        }

        public bool SignUp(string email, string password, string genre, string place)
        {
            return true;
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