using EasyMicroservices.SMSMicroservice.Contracts.Common;

namespace EasyMicroservices.SMSMicroservice.Contracts.Requests
{
    public class SingleTextMessageRequestContract : TextMessageContract
    {
        public string ToPhoneNumber { get; set; }
    }
}
