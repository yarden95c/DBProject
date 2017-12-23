using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Client.ViewModel
{
    public class ArtistViewModel : FirstChoiseViewModel
    {

        public ArtistViewModel(string name)
        {
            _name = name;
            
        }
        public override ObservableCollection<GetParamViewModel> GetRequestParams()
        {
            return new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel("Artist Name:", _dbManager.GetTopArtistNamesStartWith),
                new GetParamViewModel("Date Of Birth: ", _dbManager.GetTopArtistDateOfBirthsStartWith),
                new GetParamViewModel("Song Of This Artist: ", _dbManager.GetTopSongsNameStartWith),
                new GetParamViewModel("Livving Place: ", _dbManager.GetTopPlacesNameStartWith)
            };
        }
    }
}
