using System;
namespace BusinessLogic
{
    public class EntityFactory
    {
        public static IEntity CreateEntity(EntityType type)
        {
            switch(type)
            {
                case EntityType.AREA:
                    return new Area();

                case EntityType.ARTIST:
                    return new Artist();

                case EntityType.SONG:
                    return new Song();

                default:
                    return null;
            }
        }
    }
}
