using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    public class Genre
    {
        private string name;
        private int id;

        public Genre(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public Genre()
        {

        }

        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
    }
}
