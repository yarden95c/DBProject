using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Controllers;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;

namespace Project.Client.ViewModel
{
    public class SignUpViewModel : BaseViewModel
    {
        private ObservableCollection<GetParamViewModel> _requestedParams;
        private SignUpController _controller;
        private Visibility _beforeSignInVisibility;
        private Visibility _afterSignInVisibility;
        private ICommand _signUpCommand;
        private ICommand _signOutCommand;

        public SignUpViewModel()
        {
            _controller = new SignUpController();
            Init();
        }

        public void Init()
        {
            BeforeSignInVisibility = Visibility.Visible;
            AfterSignInVisibility = Visibility.Collapsed;
            RequestedParams = new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel("Email"),
                new GetParamViewModel("Password", isPassword:true),
                new GetParamViewModel("Confirm Password", isPassword:true),
                new GetParamViewModel("Favorite Genre", getParamOptions:_controller.GetTopGenresNames),
                new GetParamViewModel("Place", getParamOptions:_controller.GetTopPlacesNames)
            };

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

        public ICommand SignOutCommand
        {
            get { return _signOutCommand ?? (_signOutCommand = new RelayCommand(param => this.SignOut())); }

        }
        public ICommand SignUpCommand
        {
            get { return _signUpCommand ?? (_signUpCommand = new RelayCommand(param => this.SignUp())); }
        }

        public ObservableCollection<GetParamViewModel> RequestedParams
        {
            get => _requestedParams;
            set
            {
                if (_requestedParams == value)
                    return;
                _requestedParams = value;
                OnPropertyChanged("RequestedParams");
            }
        }

        public void SignUp()
        {
            string email = _requestedParams[0].GivvenParam;
            string password = _requestedParams[1].GivvenParam;
            string confirmPassword = _requestedParams[2].GivvenParam;
            string genre = _requestedParams[3].GivvenParam;
            string place = _requestedParams[4].GivvenParam;
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
                if (!_controller.SignUp(email, password, genre, place))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ModernDialog.ShowMessage("Sorry, we had a problem. \nPlease try again later", "ERROR", MessageBoxButton.OK);
                    });
                }
                else
                {
                    BeforeSignInVisibility = Visibility.Collapsed;
                    AfterSignInVisibility = Visibility.Visible;
                }
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
            _controller.SignOut();
            Init();

        }
    }
}