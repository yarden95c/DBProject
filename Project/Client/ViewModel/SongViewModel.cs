using Project.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BusinessLogic;
using Project.Client.Logic;

namespace Project.Client.View
{
    public class SongViewModel : FirstChoiseViewModel
    {
        public SongViewModel(string name)
        {
            _name = name;
            type = EntityType.SONG;
            _getParamViewModels = new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel(Consts.Song.SongName,getParamOptions:_dbManager.GetTopSongsNameStartWith),
                new GetParamViewModel(Consts.Song.WhoSingIt, getParamOptions:_dbManager.GetTopArtistNamesStartWith),
                new GetParamViewModel(Consts.Song.Year,getParamOptions:_dbManager.GetTopSongsYearsStartWith)
            };
        }
        public override ObservableCollection<GetParamViewModel> GetRequestParams()
        {
            return _getParamViewModels;
        }


    }
}
