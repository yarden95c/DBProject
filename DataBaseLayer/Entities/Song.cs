using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer.Entities
{
    public class Song
    {
        private string name;
        private string id;

        public Song(string name, string id)
        {
            this.name = name;
            this.id = id;
        }

        public Song()
        {

        }

        public string Name { get => name; set => name = value; }
        public string Id { get => id; set => id = value; }
    }
}
