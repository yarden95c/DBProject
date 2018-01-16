using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseLayer;

namespace Controllers
{
    public class SignInController
    {
        private static User connectedUser;
        private DataBaseConnector db;
        private static SignInController _instance;
        private SignInController()
        {
            connectedUser = null;
            db = new DataBaseConnector();
        }
        public static SignInController GetInstance()
        {
            return _instance ?? (_instance = new SignInController());
        }
        public User ConnectedUser { get => connectedUser; }

        public bool SignIn(string email, string password)
        {
            connectedUser = new SignInExecuter(email, password, db).Execute();
            return ConnectedUser != null;
        }

        public void SignOut()
        {
            connectedUser = null;
        }

        
    }
}
