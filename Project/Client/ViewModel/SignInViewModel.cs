using System.Collections.ObjectModel;

namespace Project.Client.ViewModel
{
    public class SignInViewModel : BaseViewModel
    {
        private ObservableCollection<GetParamViewModel> _requestedParams;
        public SignInViewModel()
        {
            RequestedParams = new ObservableCollection<GetParamViewModel>
            {
                new GetParamViewModel("User Name"),
                new GetParamViewModel("Password", isPassword:true)
            };
            
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

    }
}