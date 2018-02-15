using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseLayer;

namespace Controllers
{
    /// <summary>
    /// FeelingLuckyController - this is the controller for the "I'm feeling lucky" button.
    /// </summary>
    public class FeelingLuckyController
    {
        /// <summary>
        /// The database connector
        /// </summary>
        private DataBaseConnector db;
        /// <summary>
        /// The sign in controller
        /// </summary>
        private readonly SignInController _signInController;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeelingLuckyController" /> class.
        /// </summary>
        public FeelingLuckyController()
        {
            db = DataBaseConnector.GetInstance();
            _signInController = SignInController.GetInstance();
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
        /// Gets the result of the "I'm feeling lucky" query.
        /// </summary>
        /// <returns>
        /// a string that represent the result
        /// </returns>
        public string GetResult()
        {
            LuckyExecuter luckyExecuter = new LuckyExecuter(db, SignInController.ConnectedUser);
            return luckyExecuter.Execute();
        }
    }
}
