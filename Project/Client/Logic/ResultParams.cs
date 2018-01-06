using System.Collections.Generic;
using BusinessLogic;

namespace Project.Client.Logic
{
    public class ResultParams
    {
        private List<IEntity> _entities;
        private EntityType _type;
        private RequestParams _request;

        public ResultParams(List<IEntity> entities, EntityType type, RequestParams request)
        {
            _entities = entities;
            _type = type;
            _request = request;
        }

        public List<IEntity> Entities
        {
            get => _entities; 
            set => _entities = value;
        }

        public EntityType Type { get => _type; set => _type = value; }
        public RequestParams Request { get => _request; set => _request = value; }
    }
}