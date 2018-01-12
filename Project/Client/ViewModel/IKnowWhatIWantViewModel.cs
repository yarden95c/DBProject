using FirstFloor.ModernUI.Presentation;
using Project.Client.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Controllers;
using Project.Client.Logic;

namespace Project.Client.ViewModel
{
    public class IKnowWhatIWantViewModel : BaseViewModel
    {
        private ICommand _continueCommand;
        private ICommand _sendMyRequestCommand;
        private Visibility _radioButtonsVisibility;
        private Visibility _paramsVisibility;
        private Visibility _resultVisibility;
        private HorizontalAlignment _continueButtonAlignment;
        private ObservableCollection<GetParamViewModel> _requestedParams;
        private ObservableCollection<FirstChoiseViewModel> _firstChoises;
        private FirstChoiseViewModel _firstChoise;
        private string _continueButton;
        private IKnowWhatIWantController _controller;
        private string _resultInfo;
        public IKnowWhatIWantViewModel()
        {

            this.Init();

        }
        public void Init()
        {
            _controller = new IKnowWhatIWantController();
            ResultVisibility = Visibility.Collapsed;
            RadioButtonsVisibility = Visibility.Visible;
            ParamsVisibility = Visibility.Collapsed;
            FirstChoises = new ObservableCollection<FirstChoiseViewModel>
            {
                new SongViewModel("Song", _controller),
                new ArtistViewModel("Artist", _controller),
                new PlaceViewModel("Place", _controller)
            };
            foreach (var choise in FirstChoises)
            {
                choise.InitIKnowParams();
            }
            ContinueButton = "Continue";
            ContinueButtonAlignment = HorizontalAlignment.Right;
        }


        #region properties

        public ICommand ContinueCommand
        {
            get { return _continueCommand ?? (_continueCommand = new RelayCommand(param => this.OnContinuePressed())); }
        }
        public ICommand SendMyRequestCommand
        {
            get { return _sendMyRequestCommand ?? (_sendMyRequestCommand = new RelayCommand(param => this.SendRequestParams())); }
        }
        public Visibility RadioButtonsVisibility
        {
            get => _radioButtonsVisibility;
            set
            {
                if (_radioButtonsVisibility == value)
                    return;
                _radioButtonsVisibility = value;
                OnPropertyChanged("RadioButtonsVisibility");
            }
        }
        public Visibility ParamsVisibility
        {
            get => _paramsVisibility;
            set
            {
                if (_paramsVisibility == value)
                    return;
                _paramsVisibility = value;
                OnPropertyChanged("ParamsVisibility");
            }
        }

        public Visibility ResultVisibility
        {
            get => _resultVisibility;
            set

            {
                if(_resultVisibility == value)
                    return;

                _resultVisibility = value;
                OnPropertyChanged("ResultVisibility");
            }
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
        public ObservableCollection<FirstChoiseViewModel> FirstChoises
        {
            get => _firstChoises;
            set
            {
                if (_firstChoises == value)
                    return;
                _firstChoises = value;
                OnPropertyChanged("FirstChoises");
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
        public FirstChoiseViewModel FirstChoise
        {
            get => _firstChoise;
            set
            {
                if (_firstChoise == value)
                    return;
                _firstChoise = value;
                OnPropertyChanged("FirstChoise");
            }
        }

        public string ContinueButton
        {
            get => _continueButton;

            set
            {
                if (_continueButton == value)
                    return;
                _continueButton = value;
                OnPropertyChanged("ContinueButton");
            }
        }

        public HorizontalAlignment ContinueButtonAlignment
        {
            get => _continueButtonAlignment;

            set
            {
                if (_continueButtonAlignment == value)
                    return;
                _continueButtonAlignment = value;
                OnPropertyChanged("ContinueButtonAlignment");
            }
        }

        #endregion


        private void OnContinuePressed()
        {
            if (ContinueButton == "Continue")
            {
                ContinueRequest();
            }
            else
            {
                Init();
            }

        }

        private void ContinueRequest()
        {
            foreach (var first in FirstChoises)
            {
                if (first.IsChecked)
                    _firstChoise = first;
            }
            if (FirstChoise == null) return;
            RequestedParams = _firstChoise.GetRequestParams();
            RadioButtonsVisibility = Visibility.Collapsed;
            ContinueButton = "Back";
            ContinueButtonAlignment = HorizontalAlignment.Left;
            ParamsVisibility = Visibility.Visible;
        }

        private void SendRequestParams()
        {
            ResultInfo = FirstChoise.GetResultInfo();
             ResultVisibility = Visibility.Visible;
                ParamsVisibility = Visibility.Collapsed;
            
        }
    }
}
