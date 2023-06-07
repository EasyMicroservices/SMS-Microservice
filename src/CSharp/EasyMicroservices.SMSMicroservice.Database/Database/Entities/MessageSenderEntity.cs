using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.SMSMicroservice.Database.Entities.ManyToMany;
using EasyMicroservices.SMSMicroservice.Database.Schemas;
using System.Collections.Generic;

namespace EasyMicroservices.SMSMicroservice.Database.Entities
{
    public class MessageSenderEntity : MessageSenderSchema, IIdSchema<long>
    {
        public long Id { get; set; }

        public long? ApiUserId { get; set; }
        public ApiUserEntity ApiUser { get; set; }

        public ICollection<MessageSenderTextMessageEntity> MessageSenderTextMessages { get; set; }
    }
}
