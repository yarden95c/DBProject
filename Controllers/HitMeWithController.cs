using System.Collections.Generic;
using DataBaseLayer;
using MySql.Data.MySqlClient;

namespace Controllers
{
    public class HitMeWithController : CompletionController
    {
        private readonly SignInController _signInController;
        public HitMeWithController() : base()
        {
            _signInController = SignInController.GetInstance();
        }
        
        public SignInController SignInController
        {
            get => _signInController;
        }

        public string GetPlace(string placeName)
        {
            PlaceExecuter placeExecuter = new PlaceExecuter(placeName, conn, SignInController.ConnectedUser);
            return placeExecuter.Execute();
        }

        public string GetNumber()
        {
            NumberExecuter numExecuter = new NumberExecuter(SignInController.ConnectedUser, conn);
            return numExecuter.Execute();
        }

        public string GetYear(int fromYear,int toYear)
        {
            YearExecuter yearExecuter = new YearExecuter(conn, fromYear, toYear, SignInController.ConnectedUser);
            return yearExecuter.Execute();
        }

        public string GetGenre(string genre)
        {
            GenreExecuter genreExecuter = new GenreExecuter(genre, conn, SignInController.ConnectedUser);
            return genreExecuter.Execute();
        }
    }
}