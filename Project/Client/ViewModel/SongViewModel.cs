using Project.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Project.Client.View
{
    public class SongViewModel : FirstChoiseViewModel
    {
        public SongViewModel(string name)
        {
            _name = name;
        }
        public override ObservableCollection<GetParamViewModel> GetRequestParams()
        {
            return new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel("Song Name: ",fieldName:"song_name" ,getParamOptions:_dbManager.GetTopSongsNameStartWith),
               // new GetParamViewModel("Who Sing It: ", fieldName:"artist_name", getParamOptions:_dbManager.GetTopArtistNamesStartWith),
                //new GetParamViewModel("Year: ",fieldName:"release_date_year" ,getParamOptions:_dbManager.GetTopSongsYearsStartWith)
            };
        }
    }
}
