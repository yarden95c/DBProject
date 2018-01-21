using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    /// <summary>
    /// Genre - this class represent a genre entity.
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// The name
        /// </summary>
        private string name;
        /// <summary>
        /// The identifier
        /// </summary>
        private int id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Genre"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        public Genre(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Genre"/> class.
        /// </summary>
        public Genre()
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
        public int Id { get => id; set => id = value; }
    }
}
