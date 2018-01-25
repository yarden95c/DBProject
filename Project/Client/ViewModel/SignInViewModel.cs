using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Controllers;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;

namespace Project.Client.ViewModel
{
    public class SignInViewModel : BaseViewModel
    {
        private ObservableCollection<GetParamViewModel> _requestedParams;
        private readonly SignInController _controller;
        private Visibility _beforeSignInVisibility;
        private Visibility _afterSignInVisibility;
        private ICommand _signInCommand;
        private ICommand _signOutCommand;
        public SignInViewModel()
        {
            _controller = SignInController.GetInstance();
            Init();
        }

        public void Init()
        {
            AfterSignInVisibility = Visibility.Collapsed;
            BeforeSignInVisibility = Visibility.Visible;

            RequestedParams = new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel("Email"),
                new GetParamViewModel("Password", isPassword:true)
            };
            RequestedParams[0].GivvenParam = "at@Maecenas.co.uk";
            RequestedParams[1].GivvenParam = "TDH23BLR0JK";
        }

        public Visibility BeforeSignInVisibility
        {
            get => _beforeSignInVisibility;
            set
            {
                if(_beforeSignInVisibility == value)
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
                if(_afterSignInVisibility == value)
                    return;
                _afterSignInVisibility = value;
                OnPropertyChanged(nameof(AfterSignInVisibility));
            }
        }
        public ICommand SignInCommand
        {
            get { return _signInCommand ?? (_signInCommand = new RelayCommand(param => this.SignIn())); }

        }

        public ICommand SignOutCommand
        {
            get { return _signOutCommand ?? (_signOutCommand = new RelayCommand(param => this.SignOut())); }

        }
        public ObservableCollection<GetParamViewModel> RequestedParams
        {
            get => _requestedParams;
            set
            {
                if(_requestedParams == value)
                    return;
                _requestedParams = value;
                OnPropertyChanged("RequestedParams");
            }
        }

        private void SignIn()
        {
            bool success = _controller.SignIn(RequestedParams[0].GivvenParam, RequestedParams[1].GivvenParam);
            if (success)
            {
                AfterSignInVisibility = Visibility.Visible;
                BeforeSignInVisibility = Visibility.Collapsed;
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ModernDialog.ShowMessage("Sorry, wrong email or password.", "ERROR", MessageBoxButton.OK);
                });
            }
        }

        private void SignOut()
        {
            _controller.SignOut();
            Init();
        }
    }
}