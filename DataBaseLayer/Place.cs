using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    class Place
    {
        private int id;
        private string name;

        public Place(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Place()
        {

        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
