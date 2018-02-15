using System.Linq;
using DataBaseLayer;
using DataBaseLayer.Executers;
using System.Collections.Generic;
using DataBaseLayer.Entities;

namespace Controllers
{
    /// <summary>
    /// This class connecting between the sign up gui and the model.
    /// </summary>
    /// <seealso cref="Controllers.CompletionController" />
    public class SignUpController : CompletionController
    {

        /// <summary>
        /// The executer
        /// </summary>
        private SignUpExecuter executer;
        /// <summary>
        /// The password
        /// </summary>
        private string password;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpController"/> class.
        /// </summary>
        public SignUpController()
        {
            executer = new SignUpExecuter();
        }

        /// <summary>
        /// Determines whether [is valid genre] [the specified genre name].
        /// </summary>
        /// <param name="genreName">Name of the genre.</param>
        /// <returns>
        ///   <c>true</c> if [is valid genre] [the specified genre name]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidGenre(string genreName)
        {
            return GetTopGenresNames(genreName).Any(g => g.Equals(genreName));
        }

        /// <summary>
        /// Determines whether [is valid place] [the specified place name].
        /// </summary>
        /// <param name="placeName">Name of the place.</param>
        /// <returns>
        ///   <c>true</c> if [is valid place] [the specified place name]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidPlace(string placeName)
        {
            return GetTopPlacesNames(placeName).Any(p => p.Equals(placeName));
        }

        /// <summary>
        /// Signs up.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="day">The day.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <param name="password">The password.</param>
        /// <param name="genreName">Name of the genre.</param>
        /// <param name="placeName">Name of the place.</param>
        /// <returns></returns>
        public bool SignUp(string firstName, string lastName, string email, int day, int month, int year, string password, string genreName, string placeName)
        {
            bool result = executer.Execute(firstName, lastName, email, day, month, year, password, genreName, placeName);
            if (result)
            {
                SignInController.GetInstance().SignIn(email, password);
                this.password = password;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds the songs.
        /// </summary>
        /// <param name="songs">The songs.</param>
        /// <returns></returns>
        public bool AddSongs(List<string> songs)
        {
            SignInController signIn = SignInController.GetInstance();
            List<string> result = executer.AddSongsToUser(signIn.ConnectedUser, songs);

            if (result.Count != 0)
            {
                signIn.ConnectedUser.Songs = result;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds the artists.
        /// </summary>
        /// <param name="artists">The artists.</param>
        /// <returns></returns>
        public bool AddArtists(List<string> artists)
        {
            SignInController signIn = SignInController.GetInstance();
            List<int> result = executer.AddArtistsToUser(signIn.ConnectedUser, artists);

            if (result.Count != 0)
            {
                signIn.ConnectedUser.Artists = result;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is already sign in].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is already sign in]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAlreadySignIn()
        {
            return _signInController.ConnectedUser != null;
        }

    }
}