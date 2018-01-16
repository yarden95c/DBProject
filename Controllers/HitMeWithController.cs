using System.Collections.Generic;
using DataBaseLayer;
using MySql.Data.MySqlClient;

namespace Controllers
{
    public class HitMeWithController
    {
        private readonly SignInController _signInController;
        public HitMeWithController()
        {
            _signInController = SignInController.GetInstance();
        }

        public List<string> GetTopPlacesNames(string placeName)
        {
            List<string> responsList = new List<string>{"BLABLA", "BLABLA2"};
            return responsList;
        }
        public List<string> GetTopGenresNames(string genreName)
        {
            List<string> responsList = new List<string> { "BLABLA", "BLABLA2" };
            return responsList;
        }
        public SignInController SignInController
        {
            get => _signInController;
        }

        public string GetPlace(string placeName)
        {
            return placeName;
        }

        public string GetNumber(int num)
        {
            return num.ToString();
        }

        public string GetYear(int year)
        {
            return year.ToString();
        }

        public string GetGenre(string genre)
        {
            return genre;
        }
    }
}