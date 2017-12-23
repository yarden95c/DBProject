using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public enum EntityType
    {
        SONG, ARTIST, AREA
    }

    public interface ICommand
    {
         List<IEntity> Execute(EntityType entity,Dictionary<string, string> fields);
    }
}
