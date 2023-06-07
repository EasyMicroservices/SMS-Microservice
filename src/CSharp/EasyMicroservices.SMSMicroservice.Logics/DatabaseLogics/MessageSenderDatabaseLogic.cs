using EasyMicroservices.Cores.Database.Logics;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.SMSMicroservice.Database.Entities;
using EasyMicroservices.SMSMicroservice.Database.Entities.ManyToMany;
using EasyMicroservices.SMSMicroservice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.SMSMicroservice.DatabaseLogics
{
    public class MessageSenderDatabaseLogic : LongIdDatabaseLogicBase<MessageSenderEntity>
    {
        public MessageSenderDatabaseLogic(IDatabase database, IMapperProvider mapperProvider) : base(database.GetReadableOf<MessageSenderEntity>(), database.GetWritableOf<MessageSenderEntity>(), mapperProvider)
        {

        }

        public static MessageSenderDatabaseLogic CreateInstance(IDependencyManager dependencyManager)
        {
            return new MessageSenderDatabaseLogic(dependencyManager.GetDatabase(), dependencyManager.GetMapper());
        }
    }
}