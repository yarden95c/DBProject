using System.Linq;
using DataBaseLayer;
using DataBaseLayer.Executers;
using System.Collections.Generic;
using DataBaseLayer.Entities;

namespace Controllers
{
    public class SignUpController : CompletionController
    {

        private SignUpExecuter executer;
        private string password;

        public SignUpController()
        {
            executer = new SignUpExecuter();
        }

        public bool IsValidGenre(string genreName)
        {
            return GetTopGenresNames(genreName).Any(g => g.Equals(genreName));
        }

        public bool IsValidPlace(string placeName)
        {
            return GetTopPlacesNames(placeName).Any(p => p.Equals(placeName));
        }

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

    }
}