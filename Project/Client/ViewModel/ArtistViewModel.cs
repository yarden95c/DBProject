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
                //new GetParamViewModel(Consts.Artist.LivingPlace,getParamOptions: Controller.GetTopPlacesNames),
                new GetParamViewModel(Consts.Artist.YearOfBirth, getParamOptions:Controller.GetYearsList)
            };
        }

        public override string GetResultInfo()
        {
            string artistName = _getParamViewModels[0].GivvenParam;
            string songName = _getParamViewModels[1].GivvenParam;
            //string place = _getParamViewModels[2].GivvenParam;
            try
            {
                string fromYearString = _getParamViewModels[2].GivvenParam.Split('-')[0].Remove(4);
                string toYearString = _getParamViewModels[2].GivvenParam.Split('-')[1].Remove(0);
                int from, to;
                if (!int.TryParse(fromYearString, out from))
                {
                    from = 0000;
                }
                if (!int.TryParse(toYearString, out to))
                {
                    from = 9999;
                }
                return Controller.GetArtist(artistName, songName, from, to);


            }
            catch (Exception e)
            {
                return Controller.GetSong(artistName, songName, 0000, 9999);

            }
        }
    }
}
