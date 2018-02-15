//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Windows.Documents;
//using Project.Client.Logic;

//namespace Project.Client.ViewModel
//{
//    public class ResultViewModel : BaseViewModel
//    {
//        private ResultParams _result;
//        private string _resultInfo;

//        public ResultViewModel(ResultParams result)
//        {
//            _result = result;
//            Init();
//        }

//        public void Init()
//        {
//            StringBuilder info = new StringBuilder();
//            if (_result.Entities.Count > 1)
//            {
//                info.Append($"We Found You {_result.Entities.Count} {GetType()}s With: \n");
//            }
//            else
//            {
//                info.Append($"We Found You A {GetType()} With: \n");
//            }

//            Dictionary<string, string> resultsNamesAndFields = Consts.FieldsDictionary[_result.Type];
//            foreach (var entity in _result.Entities)
//            {
//                foreach (var Name in resultsNamesAndFields.Keys)
//                {
//                    string field = resultsNamesAndFields[Name];
//                    if (entity.FieldsNames.Contains(field))
//                    {
//                        info.Append($"\t{Name} {entity[field]}\n");
//                    }

//                }

//            }

//            ResultInfo = info.ToString();
//        }

//        public string ResultInfo
//        {
//            get => _resultInfo;
//            set
//            {
//                if (_resultInfo == value)
//                    return;
//                _resultInfo = value;
//                OnPropertyChanged("ResultInfo");
//            }
//        }

//        public string GetType()
//        {
//            switch (_result.Type)
//            {
//                case EntityType.SONG:
//                    return "Song";
//                case EntityType.AREA:
//                    return "Place";
//                case EntityType.ARTIST:
//                    return "Artist";
//            }

//            return null;
//        }


//    }
//}