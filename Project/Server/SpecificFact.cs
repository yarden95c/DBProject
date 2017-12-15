using Project.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Server
{
    class SpecificFact
    {
        private DbManager _dbManager;
        public SpecificFact(DbManager dbManager)
        {
            _dbManager = dbManager;
        }

    }
}
