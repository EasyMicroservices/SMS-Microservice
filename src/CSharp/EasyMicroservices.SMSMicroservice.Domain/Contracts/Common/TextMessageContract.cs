using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.SMSMicroservice.Contracts.Common
{
    public class TextMessageContract
    {
        public List<string> Senders { get; set; }
        public string Text { get; set; }
    }
}
