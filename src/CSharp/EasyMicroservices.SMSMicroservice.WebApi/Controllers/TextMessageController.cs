using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.SMSMicroservice.Contracts.Common;
using EasyMicroservices.SMSMicroservice.Contracts.Requests;
using EasyMicroservices.SMSMicroservice.Database.Entities;

namespace EasyMicroservices.SMSMicroservice.WebApi.Controllers
{
    public class TextMessageController : SimpleQueryServiceController<TextMessageEntity, long, SingleTextMessageRequestContract, TextMessageContract>
    {
        public TextMessageController(IContractLogic<TextMessageEntity, SingleTextMessageRequestContract, TextMessageContract, long> contractReadable) : base(contractReadable)
        {

        }
    }
}
