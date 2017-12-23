using Project.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Client.ViewModel
{
    public abstract class FirstChoiseViewModel : BaseViewModel
    {
        protected bool _isChecked;
        protected string _name;
        protected DbManager _dbManager = new DbManager();
        public string Name
        {
            get
            {
                return _name;
            }
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
            get
            {
                return _isChecked;
            }
            set
            {
                if (_isChecked == value)
                    return;
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        public abstract ObservableCollection<GetParamViewModel> GetRequestParams();
       
    }
}
