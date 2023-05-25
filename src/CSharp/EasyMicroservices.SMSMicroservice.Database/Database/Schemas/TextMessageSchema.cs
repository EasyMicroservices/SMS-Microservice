using EasyMicroservices.SMSMicroservice.DataTypes;

namespace EasyMicroservices.SMSMicroservice.Database.Schemas
{
    public class TextMessageSchema
    {
        public string Text { get; set; }
        public MessageStatusType Status { get; set; }
    }
}
