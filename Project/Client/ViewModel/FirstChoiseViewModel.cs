using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controllers;
using Project.Client.Logic;

namespace Project.Client.ViewModel
{
    public abstract class FirstChoiseViewModel : BaseViewModel
    {
        protected bool _isChecked;
        protected string _name;
        protected ObservableCollection<GetParamViewModel> _getParamViewModels;
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

        protected Pair<int, int> GetYears(string years)
        {
            try
            {
                string fromYearString = years.Split('-')[0];

                string toYearString = years.Split('-')[1];
                int from, to;
                if (!int.TryParse(fromYearString, out from))
                {
                    from = 0000;
                }
                if (!int.TryParse(toYearString, out to))
                {
                    from = 9999;
                }
                return new Pair<int, int>(from, to);


            }
            catch (Exception e)
            {
                return new Pair<int, int>(0000, 9999);

            }
        }
    }
}
