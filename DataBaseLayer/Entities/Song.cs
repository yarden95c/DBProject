using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Song
    {
        /// <summary>
        /// The name
        /// </summary>
        private string name;
        /// <summary>
        /// The identifier
        /// </summary>
        private string id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Song"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        public Song(string name, string id)
        {
            this.name = name;
            this.id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Song"/> class.
        /// </summary>
        public Song()
        {

        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get => name; set => name = value; }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get => id; set => id = value; }
    }
}
