using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Project.Client.Logic;

namespace Project.Client.ViewModel
{
    public class PlaceViewModel : FirstChoiseViewModel
    {

        public PlaceViewModel(string name)
        {
            type = EntityType.AREA;
            _name = name;
            _getParamViewModels = new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel(Consts.Area.PlaceName, getParamOptions:_dbManager.GetTopPlacesNameStartWith),
                new GetParamViewModel(Consts.Area.ArtistWhoLivedThere, getParamOptions:_dbManager.GetTopArtistNamesStartWith),
                new GetParamViewModel(Consts.Area.SongWrittenThere,getParamOptions:_dbManager.GetTopSongsNameStartWith)
            };
        }
        
        public override ObservableCollection<GetParamViewModel> GetRequestParams()
        {
            return _getParamViewModels;
        }
        
    }
}
