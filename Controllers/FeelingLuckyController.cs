using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseLayer;

namespace Controllers
{
    public class FeelingLuckyController
    {
        private DataBaseConnector db;
        private readonly SignInController _signInController;

        public FeelingLuckyController()
        {
            db = DataBaseConnector.GetInstance();
            _signInController = SignInController.GetInstance();
        }

        public SignInController SignInController
        {
            get => _signInController;
        }

        public string GetResult()
        {
            LuckyExecuter luckyExecuter = new LuckyExecuter(db, SignInController.ConnectedUser);
            return luckyExecuter.Execute();
        }
    }
}
