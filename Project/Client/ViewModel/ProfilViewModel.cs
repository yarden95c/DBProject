using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Controllers;
using DataBaseLayer;
using FirstFloor.ModernUI.Presentation;

namespace Project.Client.ViewModel
{
    public class ProfilViewModel : BaseViewModel
    {
        private SignInController _signInController;
        private ProfileController _controller;
        private ObservableCollection<GetParamViewModel> _firstDetails;
        private ObservableCollection<GetParamViewModel> _artistsCollection;
        private ObservableCollection<GetParamViewModel> _songsCollection;
        private Visibility _firstDetailsVisibility;
        private Visibility _secondDetailsVisibility;
        private ICommand _continueCommand;
        private ICommand _backCommand;
        private ICommand _editSecondDetails;


        public ProfilViewModel()
        {
            _signInController = SignInController.GetInstance();
            _controller = new ProfileController();
            FirstDetailsVisibility = Visibility.Visible;
            SecondDetailsVisibility = Visibility.Collapsed;
            User user = _signInController.ConnectedUser;
            string genre = _controller.GetGenreName(user.GenreId);
            string place = _controller.GetPlaceName(user.PlaceId);
            if (user != null)
            {
                FirstDetails = new ObservableCollection<GetParamViewModel>
                {
                    new GetParamViewModel("First Name: ", isLabel:true, givvenParam:user.FirstName),
                    new GetParamViewModel("Last Name: ", isLabel:true, givvenParam:user.LastName),
                    new GetParamViewModel("Email: ", isLabel:true, givvenParam:user.Email),
                    new GetParamViewModel("Birth day-day: ", isLabel:true, givvenParam:user.Day.ToString()),
                    new GetParamViewModel("Birth day-month: ", isLabel:true, givvenParam:user.Month.ToString()),
                    new GetParamViewModel("Birth day-year: ", isLabel:true, givvenParam:user.Year.ToString()),
                    new GetParamViewModel("Genre: ", isLabel:true, givvenParam:genre, getParamOptions:_controller.GetTopGenresNames),
                    new GetParamViewModel("Place: ", isLabel:true, givvenParam:place, getParamOptions:_controller.GetTopPlacesNames)
                };
                ArtistsCollection = new ObservableCollection<GetParamViewModel>();
                foreach (var artistId in user.Artists)
                {
                    string artist = _controller.GetArtistName(artistId);
                    ArtistsCollection.Add(new GetParamViewModel("Artist Name: ", isLabel:true, getParamOptions:_controller.GetTopArtistsNames, givvenParam:artist));
                }
                SongsCollection = new ObservableCollection<GetParamViewModel>();
                foreach (var song in user.Songs)
                {
                    SongsCollection.Add(new GetParamViewModel("Song Name: ", isLabel:true, getParamOptions:_controller.GetTopSongsNames, givvenParam:song));
                }

            }

                
        }

        public ICommand ContinueCommand
        {
            get { return _continueCommand ?? (_continueCommand = new RelayCommand(param =>
                             {
                                 FirstDetailsVisibility = Visibility.Collapsed;
                                 SecondDetailsVisibility = Visibility.Visible;
                             })); }
        }
        public ICommand EditFirstDetails
        {
            get { return _editSecondDetails ?? (_editSecondDetails = new RelayCommand(param =>
            {
                foreach (var detail in FirstDetails)
                {
                    detail.EnableEdit();
                }
            }));
            }
        }
        public ObservableCollection<GetParamViewModel> FirstDetails
        {
            get => _firstDetails;
            set
            {
                if(_firstDetails == value)
                    return;
                _firstDetails = value;
                OnPropertyChanged(nameof(FirstDetails));
            }
        }

        public ObservableCollection<GetParamViewModel> ArtistsCollection
        {
            get => _artistsCollection;
            set
            {
                if(_artistsCollection == value)
                    return;
                _artistsCollection = value;
                OnPropertyChanged(nameof(ArtistsCollection));
            }
        }

        public ObservableCollection<GetParamViewModel> SongsCollection
        {
            get => _songsCollection;
            set
            {
                if(_songsCollection == value)
                    return;
                _songsCollection = value;
                OnPropertyChanged(nameof(SongsCollection));
            }
        }

        public Visibility FirstDetailsVisibility
        {
            get => _firstDetailsVisibility;
            set
            {
                if(_firstDetailsVisibility == value)
                    return;
                _firstDetailsVisibility = value;
                OnPropertyChanged(nameof(FirstDetailsVisibility));
            }
        }

        public Visibility SecondDetailsVisibility
        {
            get => _secondDetailsVisibility;
            set
            {
                if(_secondDetailsVisibility == value)
                    return;
                _secondDetailsVisibility = value;
                OnPropertyChanged(nameof(SecondDetailsVisibility));
            }
        }
    }
}