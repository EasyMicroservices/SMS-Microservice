using EasyMicroservices.Cores.Database.Logics;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.SMSMicroservice.Database.Entities;
using EasyMicroservices.SMSMicroservice.Interfaces;
using System.Dynamic;

namespace EasyMicroservices.SMSMicroservice.DatabaseLogics
{
    public class TextMessageDatabaseLogic : LongIdDatabaseLogicBase<TextMessageEntity>
    {
        public TextMessageDatabaseLogic(IDatabase database, IMapperProvider mapperProvider) : base(database.GetReadableOf<TextMessageEntity>(), database.GetWritableOf<TextMessageEntity>(), mapperProvider)
        {

        }

        public static TextMessageDatabaseLogic CreateInstance(IDependencyManager dependencyManager)
        {
            return new TextMessageDatabaseLogic(dependencyManager.GetDatabase(), dependencyManager.GetMapper());
        }
    }
}
