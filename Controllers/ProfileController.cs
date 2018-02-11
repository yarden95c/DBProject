using DataBaseLayer;

namespace Controllers
{
    public class ProfileController : CompletionController
    {
        public string GetGenreName(int id)
        {
            return GenreExecuter.GetGenreNameByID(id, this.conn);
        }

        public string GetPlaceName(int id)
        {
            return PlaceExecuter.GetPlaceNameById(id, conn);
        }

        public string GetArtistName(int id)
        {
            return SimpleArtistExecuter.GetArtistNameFromId(id, conn);
        }
    }
}