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
                new GetParamViewModel("Artist Name:",fieldName:"name" ,getParamOptions: _dbManager.GetTopArtistNamesStartWith),
                new GetParamViewModel("Year Of Birth: ",fieldName:"begin_date_year" ,getParamOptions:_dbManager.GetTopArtistDateOfBirthsStartWith),
                new GetParamViewModel("Song Of This Artist: ", fieldName:"list_of_songs",getParamOptions: _dbManager.GetTopSongsNameStartWith),
                new GetParamViewModel("Living Place: ", fieldName:"area_Id", getParamOptions:_dbManager.GetTopPlacesNameStartWith)
            };
        }
    }
}
