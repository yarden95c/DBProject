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
        private User connectedUser;
        private DataBaseConnector db;

        public SignInController()
        {
            connectedUser = null;
            db = new DataBaseConnector();
        }

        public User ConnectedUser { get => connectedUser; }

        public bool SignIn(string email, string password)
        {
            connectedUser = new SignInExecuter(email, password, db).Execute();
            return ConnectedUser != null;
        }

        
    }
}
