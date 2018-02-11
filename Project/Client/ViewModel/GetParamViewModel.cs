using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project.Client.ViewModel
{
    public class GetParamViewModel : BaseViewModel
    {
        private string _nameOfParam;
        private string _givvenParam;
        private List<string> _paramOptions;
        private readonly Func<string, List<string>> _getParamOptions;
        private Visibility _checkBoxVisibility;
        private Visibility _textBoxVisibility;
        private Visibility _passwordBoxVisibility;
        private Visibility _labelVisibility;
        private bool _isNotWaitingForResponse;
        private readonly bool _isPassword;
        private bool _isLabel;
        public GetParamViewModel(string name, string fieldName = null, Func<string, List<string>> getParamOptions = null, bool isPassword = false, bool isLabel = false, string givvenParam = "")
        {
            _isPassword = isPassword;
            _nameOfParam = name;
            _isLabel = isLabel;
            _getParamOptions = getParamOptions;
            FieldName = fieldName;
            Init();
            SetParamOptions();
            if (isLabel)
            {
                LabelVisibility = Visibility.Visible;
                GivvenParam = givvenParam;

            }
        }

        private void Init()
        {
            if(!_isLabel)
                GivvenParam = "";
            TextBoxVisibility = Visibility.Collapsed;
            CheckBoxVisibility = Visibility.Collapsed;
            LabelVisibility = Visibility.Collapsed;
            PasswordBoxVisibility = _isPassword ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SetParamOptions()
        {
            if (_isPassword || _isLabel) return;
            IsNotWaitingForResponse = false;
            TextBoxVisibility = Visibility.Visible;
            CheckBoxVisibility = Visibility.Collapsed;
            if (_getParamOptions == null) return;
            ParamOptions = _getParamOptions(GivvenParam);
            IsNotWaitingForResponse = true;
            TextBoxVisibility = Visibility.Collapsed;
            CheckBoxVisibility = Visibility.Visible;

        }

        public void EnableEdit()
        {
            Init();
            _isLabel = false;
            SetParamOptions();
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
                if(ParamOptions == null || !ParamOptions.Contains(GivvenParam))
                    SetParamOptions();
                OnPropertyChanged("GivvenParam");
            }
        }

        public List<string> ParamOptions
        {
            get => _paramOptions;
            set
            {
                if (_paramOptions == value)
                    return;
                _paramOptions = value;
                OnPropertyChanged("ParamOptions");
            }
        }

        public bool IsNotWaitingForResponse
        {
            get => _isNotWaitingForResponse;
            set
            {
                if (_isNotWaitingForResponse == value)
                    return;
                _isNotWaitingForResponse = value;
                OnPropertyChanged("IsNotWaitingForResponse");
            }
        }

        public Visibility LabelVisibility
        {
            get => _labelVisibility;
            set
            {
                if(_labelVisibility == value)
                    return;
                _labelVisibility = value;
                OnPropertyChanged(nameof(LabelVisibility));
            }
        }
        public Visibility CheckBoxVisibility
        {
            get => _checkBoxVisibility;
            set
            {
                if (_checkBoxVisibility == value)
                    return;
                _checkBoxVisibility = value;
                OnPropertyChanged("CheckBoxVisibility");
            }
        }
        public Visibility TextBoxVisibility
        {
            get => _textBoxVisibility;
            set
            {
                if (_textBoxVisibility == value)
                    return;
                _textBoxVisibility = value;
                OnPropertyChanged("TextBoxVisibility");
            }
        }

        public Visibility PasswordBoxVisibility
        {
            get => _passwordBoxVisibility;
            set
            {
                if (_passwordBoxVisibility == value)
                    return;
                _passwordBoxVisibility = value;
                OnPropertyChanged("PasswordBoxVisibility");
            }
        }

        public string FieldName { get; set; }
    }
}
