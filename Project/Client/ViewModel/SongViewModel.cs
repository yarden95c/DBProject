using Project.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BusinessLogic;
using Controllers;
using Project.Client.Logic;

namespace Project.Client.View
{
    public class SongViewModel : FirstChoiseViewModel
    {
        
        public SongViewModel(string name, IKnowWhatIWantController controller)
        {
            Controller = controller;
            _name = name;
            type = EntityType.SONG;
        }
        public override ObservableCollection<GetParamViewModel> GetRequestParams()
        {
            return _getParamViewModels;
        }

        public override void InitIKnowParams()
        {
            _getParamViewModels = new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel(Consts.Song.SongName, getParamOptions:Controller.GetTopSongsNames),
                new GetParamViewModel(Consts.Song.WhoSingIt, getParamOptions:Controller.GetTopArtistsNames),
                new GetParamViewModel(Consts.Song.Year, getParamOptions:Controller.GetYearsList)
            };

        }

        public override string GetResultInfo()
        {
            string songName = _getParamViewModels[0].GivvenParam;
            string artistName = _getParamViewModels[1].GivvenParam;
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
                return Controller.GetSong(songName, artistName, from, to);


            }
            catch (Exception e)
            {
                return Controller.GetSong(songName, artistName, 0000, 9999);

            }


        }
    }
}
