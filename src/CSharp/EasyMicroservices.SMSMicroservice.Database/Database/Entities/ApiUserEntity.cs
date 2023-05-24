using EasyMicroservices.SMSMicroservice.Database.Schemas;
using System.Collections.Generic;

namespace EasyMicroservices.SMSMicroservice.Database.Entities
{
    public class ApiUserEntity : ApiUserSchema
    {
        public long Id { get; set; }

        public ICollection<TextMessageEntity> TextMessages { get; set; }
        public ICollection<MessageSenderEntity> MessageSenders { get; set; }
    }
}
