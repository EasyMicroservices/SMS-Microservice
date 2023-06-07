namespace EasyMicroservices.SMSMicroservice.Database.Entities.ManyToMany
{
    public class MessageSenderTextMessageEntity
    {
        public long MessageSenderId { get; set; }
        public long TextMessageId { get; set; }

        public MessageSenderEntity MessageSender { get; set; }
        public TextMessageEntity TextMessage { get; set; }
    }
}
