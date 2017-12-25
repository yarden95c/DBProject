using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Client.ViewModel
{
    public class PlaceViewModel : FirstChoiseViewModel
    {

        public PlaceViewModel(string name)
        {
            _name = name;
        }
        
        public override ObservableCollection<GetParamViewModel> GetRequestParams()
        {
            return new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel("Place Name:",fieldName:"name_area", getParamOptions:_dbManager.GetTopPlacesNameStartWith),
                new GetParamViewModel("Artist Who Lived There: ", fieldName:"list_of_artists", getParamOptions:_dbManager.GetTopArtistNamesStartWith),
                new GetParamViewModel("Song WrittenThere: ",fieldName:"list_of_artists" ,getParamOptions:_dbManager.GetTopSongsNameStartWith)
            };
        }
    }
}
