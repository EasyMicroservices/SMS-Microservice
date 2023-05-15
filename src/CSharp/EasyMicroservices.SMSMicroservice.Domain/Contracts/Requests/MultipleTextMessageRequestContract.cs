using EasyMicroservices.SMSMicroservice.Contracts.Common;
using System.Collections.Generic;

namespace EasyMicroservices.SMSMicroservice.Contracts.Requests
{
    public class MultipleTextMessageRequestContract : TextMessageContract
    {
        public List<string> ToPhoneNumbers { get; set; }
    }
}
