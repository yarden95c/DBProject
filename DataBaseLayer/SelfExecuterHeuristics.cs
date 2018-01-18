using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    public class SelfExecuterHeuristics : Heuristics
    {
        public delegate string ExecuteDel(DataBaseConnector db);

        private ExecuteDel executer;

        public SelfExecuterHeuristics(MySqlCommand command, string resultFormat,ExecuteDel executer) : base(command,resultFormat)
        {
            this.executer = executer;
        }

        public ExecuteDel Executer { get => executer; set => executer = value; }
    }
}
