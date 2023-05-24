namespace EasyMicroservices.SMSMicroservice.Database.Entities.ManyToMany
{
    public class PhoneNumberTextMessageEntity
    {
        public long PhoneNumberId { get; set; }
        public long TextMessageId { get; set; }

        public PhoneNumberEntity PhoneNumber { get; set; }
        public TextMessageEntity TextMessage { get; set; }
    }
}
