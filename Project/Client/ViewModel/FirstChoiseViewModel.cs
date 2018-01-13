using Project.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Controllers;
using Project.Client.Logic;

namespace Project.Client.ViewModel
{
    public abstract class FirstChoiseViewModel : BaseViewModel
    {
        protected bool _isChecked;
        protected string _name;
       // protected static DbManager _dbManager = new DbManager();
        protected ObservableCollection<GetParamViewModel> _getParamViewModels;
        protected EntityType type;
        protected IKnowWhatIWantController Controller;
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked == value)
                    return;
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        public abstract ObservableCollection<GetParamViewModel> GetRequestParams();
        public abstract void InitIKnowParams();
        public abstract string GetResultInfo();
         
     /*   public ResultParams GetEntityFromRequest()
        {
            Dictionary<string, string> paramsAndValues = new Dictionary<string, string>();
            foreach (var paramViewModel in _getParamViewModels)
            {
                paramsAndValues.Add(paramViewModel.NameOfParam, paramViewModel.GivvenParam);
            }
            RequestParams request = new RequestParams(type, paramsAndValues);
            return _dbManager.GetResult(request);
        } */
    }
}
