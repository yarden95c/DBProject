using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    public abstract class EntityBase<T> : IEntity
    {
        protected Dictionary<string, string> item;
        protected List<string> fieldNames;
        protected StringBuilder conditions;
        protected Pair<string, T> extraField;

        public EntityBase()
        {
            item = new Dictionary<string, string>();
            fieldNames = new List<string>();
        }

        public List<string> FieldsNames
        {
            get => fieldNames;
        }

        public T ExtraField 
        { 
            get => extraField.Value; 
            set => extraField.Value = value; 
        }

        public abstract IEntity AddValueToExtraField(MySqlDataReader queryResult);


        public string this[string fieldName]
        {
            get => item[fieldName];
            set
            {
                if (fieldNames.Contains(fieldName))
                    item[fieldName] = value;
                else
                    throw new Exception("not existing field name");
            }
        }

        public abstract string buildExtraQuery(IEntity entity);

        public abstract string ExtraFieldToString();

    }
}
