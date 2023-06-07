using EasyMicroservices.SMSMicroservice.DataTypes;
using System.Collections.Generic;

namespace EasyMicroservices.SMSMicroservice.Database.Schemas
{
    public class TextMessageSchema
    {
        /// <summary>
        /// split with ','
        /// </summary>
        public string ToPhoneNumbers { get; set; }
        public string Text { get; set; }
        public MessageStatusType Status { get; set; }
    }
}
