using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    public class LuckyExecuter : IExecuter
    {
        private IExecuter executer;
        private DataBaseConnector db;
        private User user;

        public LuckyExecuter(DataBaseConnector db,User user)
        {
            this.db = db;
            this.user = user;
        }

        public string Execute()
        {
            string result;
            do
            {
                executer = ExecuterFactory.GetExecuter(user, db);
                result = executer.Execute();
            } while (result.Equals(executer.GetSorryMsg()));

            return result;
        }

        public string GetSorryMsg()
        {
            throw new NotImplementedException();
        }
    }
}
