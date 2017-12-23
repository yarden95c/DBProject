using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    public interface IEntity
    {
        string this[string fieldName]
        {
            get;
            set;
        }

        List<string> FieldsNames
        {
            get;
        }
        string buildExtraQuery(IEntity entity);

        IEntity AddValueToExtraField(MySqlDataReader queryResult);

        string ExtraFieldToString();

        //T ExtraField
        //{
        //    get;
        //    set;
        //}

    }
}
