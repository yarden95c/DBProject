using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Schema;
using Controllers;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;

namespace Project.Client.ViewModel
{
    public class SignUpViewModel : BaseViewModel
    {
        private ObservableCollection<GetParamViewModel> _firstRequestedParams;
        private ObservableCollection<GetParamViewModel> _secondRequestedParams;
        private ObservableCollection<GetParamViewModel> _artistsRequestedParams;
        private ObservableCollection<GetParamViewModel> _songsRequestedParams;
        private SignUpController _controller;
        private Visibility _beforeSignInVisibility;
        private Visibility _afterSignInVisibility;
        private Visibility _firstDetails;
        private Visibility _secondDetails;
        private Visibility _lastDetails;
        private ICommand _signUpCommand;
        private ICommand _signOutCommand;
        private ICommand _continueCommand;
        private ICommand _backCommand;
        private ICommand _addArtistCommand;
        private ICommand _addSongCommand;
        public SignUpViewModel()
        {
            _controller = new SignUpController();
            Init();
        }

        public void Init()
        {
            if (_controller.IsAlreadySignIn())
            {
                SetAllVisibilitiesCollapsed();
                AfterSignInVisibility = Visibility.Visible;
                return;
            }
            SetAllVisibilitiesCollapsed();
            BeforeSignInVisibility = Visibility.Visible;
            FirstRequestedParams = new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel("First Name"),
                new GetParamViewModel("Last Name"),
                new GetParamViewModel("Email"),
                new GetParamViewModel("Password", isPassword:true),
                new GetParamViewModel("Confirm Password", isPassword:true)
            };
            SecondRequestedParams = new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel("Birth Date- day"),
                new GetParamViewModel("Birth Date- month"),
                new GetParamViewModel("Birth Date- year"),
                new GetParamViewModel("Favorite Genre", getParamOptions:_controller.GetTopGenresNames),
                new GetParamViewModel("Place", getParamOptions:_controller.GetTopPlacesNames),
            };
            ArtistsCollection = new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel("Artist Name: ", getParamOptions:_controller.GetTopArtistsNames)
            };
            SongsCollection = new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel("Song Name: ", getParamOptions:_controller.GetTopSongsNames)
            };
        }

        private void SetAllVisibilitiesCollapsed()
        {
            AfterSignInVisibility = Visibility.Collapsed;
            BeforeSignInVisibility = Visibility.Collapsed;
            FirstDetails = Visibility.Collapsed;
            SecondDetails = Visibility.Collapsed;
            LastDetails = Visibility.Collapsed;
        }

        public Visibility BeforeSignInVisibility
        {
            get => _beforeSignInVisibility;
            set
            {
                if (_beforeSignInVisibility == value)
                    return;
                _beforeSignInVisibility = value;
                OnPropertyChanged(nameof(BeforeSignInVisibility));
            }
        }

        public Visibility AfterSignInVisibility
        {
            get => _afterSignInVisibility;
            set
            {
                if (_afterSignInVisibility == value)
                    return;
                _afterSignInVisibility = value;
                OnPropertyChanged(nameof(AfterSignInVisibility));
            }
        }

        public Visibility FirstDetails
        {
            get => _firstDetails;
            set
            {
                if (_firstDetails == value)
                    return;
                _firstDetails = value;
                OnPropertyChanged(nameof(FirstDetails));
            }
        }

        public Visibility SecondDetails
        {
            get => _secondDetails;
            set
            {
                if (_secondDetails == value)
                    return;
                _secondDetails = value;
                OnPropertyChanged(nameof(SecondDetails));
            }
        }

        public Visibility LastDetails
        {
            get => _lastDetails;
            set
            {
                if (_lastDetails == value)
                    return;
                _lastDetails = value;
                OnPropertyChanged(nameof(LastDetails));
            }
        }

        public ICommand SignOutCommand
        {
            get { return _signOutCommand ?? (_signOutCommand = new RelayCommand(param => this.SignOut())); }

        }
        public ICommand SignUpCommand
        {
            get { return _signUpCommand ?? (_signUpCommand = new RelayCommand(param => this.SignUp())); }
        }

        public ICommand ContinueCommand
        {
            get
            {
                return _continueCommand ?? (_continueCommand = new RelayCommand(param => this.Continue()));
            }
        }
        public ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new RelayCommand(param => this.Back()));
            }
        }

        public ICommand AddArtistCommand
        {
            get
            {
                return _addArtistCommand ?? (_addArtistCommand = new RelayCommand(param =>
                           {
                               if (string.IsNullOrEmpty(ArtistsCollection.Last().GivvenParam) || !ArtistsCollection.Last().ParamOptions.Contains(ArtistsCollection.Last().GivvenParam))
                               {
                                   Application.Current.Dispatcher.Invoke(() =>
                                   {
                                       ModernDialog.ShowMessage("Please choose a valid artist", "ERROR", MessageBoxButton.OK);
                                   });
                                   return;
                               }
                               if (ArtistsCollection.Count > 28)
                               {
                                   Application.Current.Dispatcher.Invoke(() =>
                                   {
                                       ModernDialog.ShowMessage("Sorry, you can't choose more than 29 artists.", "ERROR", MessageBoxButton.OK);
                                   });
                                   return;

                               }
                               ArtistsCollection.Add(new GetParamViewModel("Artist Name: ",
                                   getParamOptions: _controller.GetTopArtistsNames));
                           }));
            }
        }

        public ICommand AddSongCommand
        {
            get
            {
                return _addSongCommand ?? (_addSongCommand = new RelayCommand(param =>
              {
                  if (string.IsNullOrEmpty(SongsCollection.Last().GivvenParam) || !SongsCollection.Last().ParamOptions.Contains(SongsCollection.Last().GivvenParam))
                  {
                      Application.Current.Dispatcher.Invoke(() =>
                      {
                          ModernDialog.ShowMessage("Please choose a valid song", "ERROR", MessageBoxButton.OK);
                      });
                      return;
                  }
                  if (SongsCollection.Count > 28)
                  {
                      Application.Current.Dispatcher.Invoke(() =>
                      {
                          ModernDialog.ShowMessage("Sorry, you can't choose more than 29 songs.", "ERROR", MessageBoxButton.OK);
                      });
                      return;

                  }
                  SongsCollection.Add(new GetParamViewModel("Song Name: ", getParamOptions: _controller.GetTopSongsNames));

              }));
            }
        }

        public ObservableCollection<GetParamViewModel> SongsCollection
        {
            get => _songsRequestedParams;
            set
            {
                if (_songsRequestedParams == value)
                    return;
                _songsRequestedParams = value;
                OnPropertyChanged(nameof(SongsCollection));
            }
        }
        public ObservableCollection<GetParamViewModel> FirstRequestedParams
        {
            get => _firstRequestedParams;
            set
            {
                if (_firstRequestedParams == value)
                    return;
                _firstRequestedParams = value;
                OnPropertyChanged(nameof(FirstRequestedParams));
            }
        }
        public ObservableCollection<GetParamViewModel> SecondRequestedParams
        {
            get => _secondRequestedParams;
            set
            {
                if (_secondRequestedParams == value)
                    return;
                _secondRequestedParams = value;
                OnPropertyChanged(nameof(SecondRequestedParams));
            }
        }

        public ObservableCollection<GetParamViewModel> ArtistsCollection
        {
            get => _artistsRequestedParams;
            set
            {
                if (_artistsRequestedParams == value)
                    return;
                _artistsRequestedParams = value;
                OnPropertyChanged(nameof(ArtistsCollection));
            }
        }
        public void SignUp()
        {
            try
            {
                string firstName = _firstRequestedParams[0].GivvenParam;
            string lastName = _firstRequestedParams[1].GivvenParam;
            string email = _firstRequestedParams[2].GivvenParam;
            string password = _firstRequestedParams[3].GivvenParam;
            string confirmPassword = _firstRequestedParams[4].GivvenParam;

            int day = int.Parse(_secondRequestedParams[0].GivvenParam);
            int month = int.Parse(_secondRequestedParams[1].GivvenParam);
            int year = int.Parse(_secondRequestedParams[2].GivvenParam);
            string genre = _secondRequestedParams[3].GivvenParam;
            string place = _secondRequestedParams[4].GivvenParam;

            var msg = ValidateInput(email, password, confirmPassword, genre, place);
            if (msg != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                    {
                        ModernDialog.ShowMessage(msg, "ERROR", MessageBoxButton.OK);
                    });

            }
            else
            {
                if (!_controller.SignUp(firstName, lastName, email, day, month, year, password, genre, place))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ModernDialog.ShowMessage("Sorry, we had a problem. \nPlease try again later", "ERROR",
                            MessageBoxButton.OK);
                    });
                }
                else
                {
                    SetAllVisibilitiesCollapsed();
                    AfterSignInVisibility = Visibility.Visible;
                    List<string> songs = new List<string>();
                    foreach (var song in SongsCollection)
                    {
                        songs.Add(song.GivvenParam);
                    }

                    List<string> artists = new List<string>();
                    foreach (var artist in ArtistsCollection)
                    {
                        songs.Add(artist.GivvenParam);
                    }

                    _controller.AddSongs(songs);
                    _controller.AddArtists(artists);
                }
            }
            }
            catch (Exception e)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ModernDialog.ShowMessage("Please check your birthday and try again", "ERROR",
                        MessageBoxButton.OK);
                });

            }
        }

        public string ValidateInput(string email, string password, string confPassword, string genre, string place)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                return "Please fill correct email.";
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confPassword) || !password.Equals(confPassword))
            {
                return "Passwords do not match. \nPlease try again.";
            }

            if (!_controller.IsValidGenre(genre))
            {
                return "Please choose a genre from the list.";
            }
            
            return !_controller.IsValidPlace(place) ? "Please choose a place from the list." : null;
        }

        private void SignOut()
        {
            //_controller.SignOut();
            SignInController.GetInstance().SignOut();
            Init();
        }

        private void Continue()
        {
            if (BeforeSignInVisibility == Visibility.Visible)
            {
                BeforeSignInVisibility = Visibility.Collapsed;
                FirstDetails = Visibility.Visible;
            }
            else if (FirstDetails == Visibility.Visible)
            {
                FirstDetails = Visibility.Collapsed;
                SecondDetails = Visibility.Visible;
            }
            else if (SecondDetails == Visibility.Visible)
            {
                SecondDetails = Visibility.Collapsed;
                LastDetails = Visibility.Visible;
            }
        }

        private void Back()
        {
            if (FirstDetails == Visibility.Visible)
            {
                FirstDetails = Visibility.Collapsed;
                BeforeSignInVisibility = Visibility.Visible;
            }
            else if (SecondDetails == Visibility.Visible)
            {
                SecondDetails = Visibility.Collapsed;
                FirstDetails = Visibility.Visible;
            }
            else if (LastDetails == Visibility.Visible)
            {
                LastDetails = Visibility.Collapsed;
                SecondDetails = Visibility.Visible;
            }


        }


    }
}