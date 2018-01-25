using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Controllers;
using Project.Client.Logic;

namespace Project.Client.ViewModel
{
    public class ArtistViewModel : FirstChoiseViewModel
    {
        
        public ArtistViewModel(string name, IKnowWhatIWantController controller)
        {
            Controller = controller;
            type = EntityType.ARTIST;
            _name = name;
            _isChecked = false;

        }
        public override ObservableCollection<GetParamViewModel> GetRequestParams()
        {
            return _getParamViewModels;
        }

        public override void InitIKnowParams()
        {
            _getParamViewModels = new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel(Consts.Artist.ArtistName ,getParamOptions: Controller.GetTopArtistsNames),
                new GetParamViewModel(Consts.Artist.SongOfArtist ,getParamOptions:Controller.GetTopSongsNames),
                new GetParamViewModel(Consts.Artist.YearOfBirth, getParamOptions:Controller.GetYearsList)
            };
        }

        public override string GetResultInfo()
        {
            string artistName = _getParamViewModels[0].GivvenParam;
            string songName = _getParamViewModels[1].GivvenParam;
            Pair<int, int> years = GetYears(_getParamViewModels[2].GivvenParam);
            return Controller.GetArtist(artistName, songName, years.Key, years.Value);
 
        }
    }
}
