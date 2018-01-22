using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Controllers;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;

namespace Project.Client.ViewModel
{
    public class FeelingLuckyViewModel : BaseViewModel
    {
        private FeelingLuckyController _controller;
        private ICommand _startAgainCommand;
        private Visibility _startAgainVisibility;
        private Visibility _resultVisibility;
        private Visibility _loaderVisibility;
        private string _resultInfo;

        public FeelingLuckyViewModel()
        {
            _controller = new FeelingLuckyController();
            if (_controller.SignInController.ConnectedUser == null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ModernDialog.ShowMessage("Sorry, if you want to use this button you to need sign in first.", "ERROR", MessageBoxButton.OK);
                    ModernWindow m = (ModernWindow)Application.Current.MainWindow;
                    if (m != null) m.ContentSource = m.MenuLinkGroups[0].Links[1].Source;
                });
                return;
            }
            Init();
        }

        public void Init()
        {
            ResultVisibility = Visibility.Collapsed;
            StartAgainVisibility = Visibility.Collapsed;
            LoaderVisibility = Visibility.Visible;
            Task.Run(() =>
            {
                ResultInfo = _controller.GetResult();
                LoaderVisibility = Visibility.Collapsed;
                StartAgainVisibility = Visibility.Visible;
                ResultVisibility = Visibility.Visible;
            });
            Task.WaitAll();

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
        public ICommand StartAgainCommand
        {
            get
            {
                return _startAgainCommand ?? (_startAgainCommand = new RelayCommand(param => Init()));
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
    }
}