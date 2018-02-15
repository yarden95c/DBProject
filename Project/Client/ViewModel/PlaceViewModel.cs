using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controllers;
using Project.Client.Logic;

namespace Project.Client.ViewModel
{
    public class PlaceViewModel : FirstChoiseViewModel
    {

        public PlaceViewModel(string name, IKnowWhatIWantController controller)
        {
            Controller = controller;
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
                new GetParamViewModel(Consts.Area.PlaceName, getParamOptions:Controller.GetTopPlacesNames),
                new GetParamViewModel(Consts.Area.ArtistWhoLivedThere, getParamOptions:Controller.GetTopArtistsNames)
            };
        }

        public override string GetResultInfo()
        {
            string placeName = _getParamViewModels[0].GivvenParam;
            string artistName = _getParamViewModels[1].GivvenParam;
            return Controller.GetPlace(placeName, artistName);
        }
    }
}
