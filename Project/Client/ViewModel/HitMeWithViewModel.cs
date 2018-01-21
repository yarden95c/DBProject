using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Controllers;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;

namespace Project.Client.ViewModel
{
    public class HitMeWithViewModel : BaseViewModel
    {
        private readonly HitMeWithController _controller;
        private GetParamViewModel _getParam;
        private ICommand _sendMyRequestCommand;
        private ICommand _startAgainCommand;
        private ICommand _backCommand;
        private ICommand _placeButton;
        private ICommand _numberButton;
        private ICommand _yearButton;
        private ICommand _genreButton;
        private Visibility _buttonsVisibility;
        private Visibility _paramVisibility;
        private Visibility _resultVisibility;
        private Visibility _loaderVisibility;
        private Visibility _backButtonVisibility;
        private Visibility _startAgainVisibility;

        private string _resultInfo;
        public HitMeWithViewModel()
        {
           
            _controller = new HitMeWithController();
            //if (_controller.SignInController.ConnectedUser == null)
            //{
            //    Application.Current.Dispatcher.Invoke(() =>
            //    {
            //        ModernDialog.ShowMessage("Sorry, if you want to use this button you to need sign in first.", "ERROR", MessageBoxButton.OK);
            //        ModernWindow m = (ModernWindow)Application.Current.MainWindow;
            //        m.ContentSource = m.MenuLinkGroups[0].Links[1].Source;
            //    });
            //}
            ParamVisibility = Visibility.Collapsed;
            ResultVisibility = Visibility.Collapsed;
            LoaderVisibility = Visibility.Collapsed;
            BackButtonVisibility = Visibility.Collapsed;
            StartAgainVisibility = Visibility.Collapsed;
            ButtonsVisibility = Visibility.Visible;

        }

        public Visibility ButtonsVisibility
        {
            get => _buttonsVisibility;
            set
            {
                if (_buttonsVisibility == value)
                    return;
                _buttonsVisibility = value;
                OnPropertyChanged(nameof(ButtonsVisibility));

            }
        }

        public Visibility BackButtonVisibility
        {
            get => _backButtonVisibility;
            set
            {
                if (_backButtonVisibility == value)
                    return;
                _backButtonVisibility = value;
                OnPropertyChanged(nameof(BackButtonVisibility));
            }
        }

        public Visibility StartAgainVisibility
        {
            get => _startAgainVisibility;
            set
            {
                if (_startAgainVisibility == value)
                    return;
                _startAgainVisibility = value;
                OnPropertyChanged(nameof(StartAgainVisibility));
            }
        }

        public GetParamViewModel GetParam
        {
            get => _getParam;
            set
            {
                if (_getParam == value)
                    return;
                _getParam = value;
                OnPropertyChanged(nameof(GetParam));
            }
        }

        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set

            {
                if (_loaderVisibility == value)
                    return;

                _loaderVisibility = value;
                OnPropertyChanged("LoaderVisibility");
            }
        }
        public Visibility ParamVisibility
        {
            get => _paramVisibility;
            set
            {
                if (_paramVisibility == value)
                    return;
                _paramVisibility = value;
                OnPropertyChanged(nameof(ParamVisibility));
            }
        }

        public Visibility ResultVisibility
        {
            get => _resultVisibility;
            set

            {
                if (_resultVisibility == value)
                    return;

                _resultVisibility = value;
                OnPropertyChanged("ResultVisibility");
            }
        }

        public ICommand StartAgainCommand
        {
            get
            {
                return _startAgainCommand ?? (_startAgainCommand = new RelayCommand(param =>
          {
              GetParam = null;
              ResultInfo = null;
              ParamVisibility = Visibility.Collapsed;
              ResultVisibility = Visibility.Collapsed;
              LoaderVisibility = Visibility.Collapsed;
              StartAgainVisibility = Visibility.Collapsed;
              ButtonsVisibility = Visibility.Visible;
          }));
            }
        }

        public ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new RelayCommand(param =>
          {
              GetParam = null;
              ParamVisibility = Visibility.Collapsed;
              ParamVisibility = Visibility.Collapsed;
              BackButtonVisibility = Visibility.Collapsed;
              ButtonsVisibility = Visibility.Visible;

          }));
            }
        }

        public ICommand SendMyRequestCommand
        {
            get { return _sendMyRequestCommand ?? (_sendMyRequestCommand = new RelayCommand(param => this.SendRequestParams())); }
        }
        public ICommand PlaceButton
        {
            get
            {
                return _placeButton ?? (_placeButton = new RelayCommand(param =>
          {
              GetParam = new GetParamViewModel("Place", getParamOptions: _controller.GetTopPlacesNames);
              SetVisibilities();
          }));
            }

        }
        public ICommand NumberButton
        {
            get
            {
                return _numberButton ?? (_numberButton = new RelayCommand(param =>
          {
              GetParam = new GetParamViewModel("Number");
              ButtonsVisibility = Visibility.Collapsed;
              SendRequestParams();
          }));
            }

        }
        public ICommand YearButton
        {
            get
            {
                return _yearButton ?? (_yearButton = new RelayCommand(param =>
          {
              GetParam = new GetParamViewModel("Year", getParamOptions:_controller.GetYearsList);
              SetVisibilities();

          }));
            }

        }
        public ICommand GenreButton
        {
            get
            {
                return _genreButton ?? (_genreButton = new RelayCommand(param =>
          {
              GetParam = new GetParamViewModel("Genre", getParamOptions: _controller.GetTopGenresNames);
              SetVisibilities();

          }));
            }

        }
        public string ResultInfo
        {
            get => _resultInfo;
            set
            {
                if (_resultInfo == value)
                    return;
                _resultInfo = value;
                OnPropertyChanged("ResultInfo");
            }
        }
        private void SendRequestParams()
        {
            ParamVisibility = Visibility.Collapsed;
            BackButtonVisibility = Visibility.Collapsed;
            LoaderVisibility = Visibility.Visible;
            Task.Run(() =>
            {
                var nameOfParam = GetParam.NameOfParam;
                switch (nameOfParam)
                {
                    case "Genre":
                        ResultInfo = _controller.GetGenre(GetParam.GivvenParam);
                        break;
                    case "Year":
                        // in the year input box- range of years should appear
                        if (!int.TryParse(GetParam.GivvenParam.Split('-')[0], out var from))
                        {
                            from = -1;
                        }
                        if (!int.TryParse(GetParam.GivvenParam.Split('-')[1], out var to))
                        {
                            to = -1;
                        }

                        // if no year was entered, one should send -1,-1
                        ResultInfo = _controller.GetYear(from, to);
                        break;
                    case "Number":
                        ResultInfo = _controller.GetNumber();
                        break;
                    case "Place":
                        ResultInfo = _controller.GetPlace(GetParam.GivvenParam);
                        break;
                    default:
                        break;
                }
                LoaderVisibility = Visibility.Collapsed;
                ResultVisibility = Visibility.Visible;
                StartAgainVisibility = Visibility.Visible;
            });
            Task.WaitAll();

        }
        private void SetVisibilities()
        {
            BackButtonVisibility = Visibility.Visible;
            ParamVisibility = Visibility.Visible;
            ButtonsVisibility = Visibility.Collapsed;
        }
    }
}