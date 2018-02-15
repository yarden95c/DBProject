using System.Collections.Generic;
using DataBaseLayer;
using MySql.Data.MySqlClient;

namespace Controllers
{
    /// <summary>
    /// HitMeWithController - this is the controller for the "Hit me with" button.
    /// </summary>
    /// <seealso cref="Controllers.CompletionController" />
    public class HitMeWithController : CompletionController
    {
        /// <summary>
        /// The sign in controller
        /// </summary>
        public HitMeWithController() : base()
        {
        }

        /// <summary>
        /// Gets the sign in controller.
        /// </summary>
        /// <value>
        /// The sign in controller.
        /// </value>
        public SignInController SignInController
        {
            get => _signInController;
        }

        /// <summary>
        /// Gets the result of the place query.
        /// </summary>
        /// <param name="placeName">Name of the place.</param>
        /// <returns>
        /// a string that represent the result of the query
        /// </returns>
        public string GetPlace(string placeName)
        {
            PlaceExecuter placeExecuter = new PlaceExecuter(placeName, conn, SignInController.ConnectedUser);
            return placeExecuter.Execute();
        }

        /// <summary>
        /// Gets the result of the number query.
        /// </summary>
        /// <returns>
        /// a string that represent the result of the query
        /// </returns>
        public string GetNumber()
        {
            NumberExecuter numExecuter = new NumberExecuter(SignInController.ConnectedUser, conn);
            return numExecuter.Execute();
        }

        /// <summary>
        /// Gets the result of the year query.
        /// </summary>
        /// <param name="fromYear">From year.</param>
        /// <param name="toYear">To year.</param>
        /// <returns>
        /// a string that represent the result of the query
        /// </returns>
        public string GetYear(int fromYear,int toYear)
        {
            YearExecuter yearExecuter = new YearExecuter(conn, fromYear, toYear, SignInController.ConnectedUser);
            return yearExecuter.Execute();
        }

        /// <summary>
        /// Gets the result of the genre query.
        /// </summary>
        /// <param name="genre">The genre.</param>
        /// <returns>
        /// a string that represent the result of the query
        /// </returns>
        public string GetGenre(string genre)
        {
            GenreExecuter genreExecuter = new GenreExecuter(genre, conn, SignInController.ConnectedUser);
            return genreExecuter.Execute();
        }
    }
}