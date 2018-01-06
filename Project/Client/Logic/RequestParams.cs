using System.Collections.Generic;
using BusinessLogic;

namespace Project.Client.Logic
{
    public class RequestParams
    {
        private Dictionary<string, string> _fieldsAndValues;
        private Dictionary<string, string> _paramsAndValues;
        private readonly EntityType _type;

        public RequestParams(EntityType type, Dictionary<string, string> paramsAndValues)
        {
            _type = type;
            _paramsAndValues = paramsAndValues;
            _fieldsAndValues = new Dictionary<string, string>();
            Init();

        }

        public Dictionary<string, string> FieldsAndValues { get => _fieldsAndValues; set => _fieldsAndValues = value; }
        public Dictionary<string, string> ParamsAndValues { get => _paramsAndValues; set => _paramsAndValues = value; }

        public EntityType Type => _type;

        public void Init()
        {
            Dictionary<string, string> fields = Consts.FieldsDictionary[_type];
            foreach (var param in _paramsAndValues.Keys)
            {
                _fieldsAndValues.Add(fields[param], _paramsAndValues[param]);
                
            }
        }
    }
}