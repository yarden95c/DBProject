using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Client.ViewModel
{
    class GetParamViewModel : BaseViewModel
    {
        private string _nameOfParam;
        private string _givvenParam;
        private List<string> _paramOptions;
        private Func<string, List<string>> _getParamOptions;
        private bool _isNotWaitingForResponse; 
        public GetParamViewModel(string name, Func<string, List<string>> getParamOptions = null)
        {
            _nameOfParam = name;
            _getParamOptions = getParamOptions;
            GivvenParam = "";
            SetParamOptions();

        }

        private void SetParamOptions()
        {
            IsNotWaitingForResponse = false;
            if (_getParamOptions == null) return;
            ParamOptions = _getParamOptions(GivvenParam);
            IsNotWaitingForResponse = true;

        }
        public string NameOfParam
        {
            get => _nameOfParam;
            set
            {
                if (_nameOfParam == value)
                    return;
                _nameOfParam = value;
                OnPropertyChanged("NameOfParam");
            }
        }
        public string GivvenParam
        {
            get => _givvenParam;
            set
            {
                if (_givvenParam == value)
                    return;
                _givvenParam = value;
                SetParamOptions();
                OnPropertyChanged("GivvenParam");
            }
        }

        public List<string> ParamOptions
        {
            get =>_paramOptions;
            set
            {
                if (_paramOptions == value)
                    return;
                _paramOptions = value;
                OnPropertyChanged("ParamOptions");
            }
        }

        public bool IsNotWaitingForResponse {
            get => _isNotWaitingForResponse;
            set
            {
                if (_isNotWaitingForResponse == value)
                    return;
                 _isNotWaitingForResponse = value;
                OnPropertyChanged("IsNotWaitingForResponse");
            } }
    }
}
