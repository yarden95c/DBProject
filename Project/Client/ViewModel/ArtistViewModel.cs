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
    public class ArtistViewModel : FirstChoiseViewModel
    {
        
        public ArtistViewModel(string name)
        {
            type = EntityType.ARTIST;
            _name = name;
            _getParamViewModels = new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel(Consts.Artist.ArtistName ,getParamOptions: _dbManager.GetTopArtistNamesStartWith),
                new GetParamViewModel(Consts.Artist.SongOfArtist ,getParamOptions:_dbManager.GetTopArtistDateOfBirthsStartWith),
                new GetParamViewModel(Consts.Artist.LivingPlace,getParamOptions: _dbManager.GetTopSongsNameStartWith),
                new GetParamViewModel(Consts.Artist.YearOfBirth, getParamOptions:_dbManager.GetTopPlacesNameStartWith)
            };
        }
        public override ObservableCollection<GetParamViewModel> GetRequestParams()
        {
            return _getParamViewModels;
        }
        
    }
}
