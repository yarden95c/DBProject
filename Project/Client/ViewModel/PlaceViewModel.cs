﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Client.ViewModel
{
    class PlaceViewModel : FirstChoiseViewModel
    {

        public PlaceViewModel(string name)
        {
            _name = name;
        }
        
        public override ObservableCollection<GetParamViewModel> GetRequestParams()
        {
            return new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel("Place Name:", _dbManager.GetTopPlacesNameStartWith),
                new GetParamViewModel("Artist Who Lived There: ", _dbManager.GetTopArtistNamesStartWith),
                new GetParamViewModel("Song WrittenThere: ", _dbManager.GetTopSongsNameStartWith)
            };
        }
    }
}
