using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseLayer
{
    public class Heuristics
    {
        private MySqlCommand command;
        private string resultFormat;
        public Heuristics()
        {

        }

        public Heuristics(MySqlCommand command, string resultFormat)
        {
            this.command = command;
            this.resultFormat = resultFormat;
        }

        public MySqlCommand Command { get => command; set => command = value; }
        public string ResultFormat { get => resultFormat; set => resultFormat = value; }
    }
}
