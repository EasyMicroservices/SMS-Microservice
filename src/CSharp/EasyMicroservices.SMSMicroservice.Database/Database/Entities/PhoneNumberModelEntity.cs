using EasyMicroservices.SMSMicroservice.Database.Entities.ManyToMany;
using EasyMicroservices.SMSMicroservice.Database.Schemas;
using System.Collections.Generic;

namespace EasyMicroservices.SMSMicroservice.Database.Entities
{
    public class PhoneNumberModelEntity : PhoneNumberSchema
    {
        public long Id { get; set; }

        public ICollection<PhoneNumberTextMessageEntity> PhoneNumberTextMessages { get; set; }
    }
}
