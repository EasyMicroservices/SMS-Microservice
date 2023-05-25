using EasyMicroservices.Cores.Database.Logics;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.SMSMicroservice.Database.Entities;

namespace EasyMicroservices.SMSMicroservice.DatabaseLogics
{
    public class TextMessageDatabaseLogic : LongIdDatabaseLogicBase<TextMessageEntity>
    {
        public TextMessageDatabaseLogic(IEasyReadableQueryableAsync<TextMessageEntity> easyReadableQueryable, IMapperProvider mapperProvider) : base(easyReadableQueryable, mapperProvider)
        {

        }

        public TextMessageDatabaseLogic() : base(null, null, null)
        {

        }
    }
}
