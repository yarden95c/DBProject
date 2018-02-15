using DataBaseLayer;

namespace Controllers
{
    /// <summary>
    /// This class connecting between the profile gui and the model.
    /// </summary>
    /// <seealso cref="Controllers.CompletionController" />
    public class ProfileController : CompletionController
    {
        /// <summary>
        /// Gets the name of the genre.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string GetGenreName(int id)
        {
            return GenreExecuter.GetGenreNameByID(id, this.conn);
        }

        /// <summary>
        /// Gets the name of the place.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string GetPlaceName(int id)
        {
            return PlaceExecuter.GetPlaceNameById(id, conn);
        }

        /// <summary>
        /// Gets the name of the artist.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string GetArtistName(int id)
        {
            return SimpleArtistExecuter.GetArtistNameFromId(id, conn);
        }
    }
}