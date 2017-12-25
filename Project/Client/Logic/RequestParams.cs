using System.Collections.Generic;
using BusinessLogic;

namespace Project.Client.Logic
{
    public class RequestParams
    {
        private Dictionary<string, string> _fieldsAndValues;
        private readonly EntityType _type;

        public RequestParams(EntityType type, Dictionary<string, string> paramsAndValues)
        {
            _type = type;
            _fieldsAndValues = new Dictionary<string, string>();
            Init(paramsAndValues);

        }

        public Dictionary<string, string> FieldsAndValues { get => _fieldsAndValues; set => _fieldsAndValues = value; }

        public EntityType Type => _type;

        public void Init(Dictionary<string, string> paramsAndValues)
        {
            Dictionary<string, string> fields = Consts.FieldsDictionary[_type];
            foreach (var param in paramsAndValues.Keys)
            {
                _fieldsAndValues.Add(fields[param], paramsAndValues[param]);
                
            }
        }
    }
}