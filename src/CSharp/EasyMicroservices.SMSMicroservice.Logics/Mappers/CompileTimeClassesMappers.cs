using System.Threading.Tasks;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.Mapper.CompileTimeMapper.Interfaces;
using System.Linq;
using EasyMicroservices.SMSMicroservice.DatabaseLogics;

namespace CompileTimeMapper
{
    public class TextMessageEntity_SingleTextMessageRequestContract_Mapper : IMapper
    {
        readonly IMapperProvider _mapper;
        public TextMessageEntity_SingleTextMessageRequestContract_Mapper(IMapperProvider mapper)
        {
            _mapper = mapper;
        }

        public global::EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity Map(global::EasyMicroservices.SMSMicroservice.Contracts.Requests.SingleTextMessageRequestContract fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = new global::EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity()
            {
                ToPhoneNumbers = fromObject.ToPhoneNumber,
                Text = fromObject.Text,
                Status = EasyMicroservices.SMSMicroservice.DataTypes.MessageStatusType.Queue,
            };
            return mapped;
        }

        public global::EasyMicroservices.SMSMicroservice.Contracts.Requests.SingleTextMessageRequestContract Map(global::EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = new global::EasyMicroservices.SMSMicroservice.Contracts.Requests.SingleTextMessageRequestContract()
            {
                ToPhoneNumber = fromObject.ToPhoneNumbers,
                Senders = fromObject.MessageSenderTextMessages.Select(x => x.MessageSender.PhoneNumber).ToList(),
                Text = fromObject.Text,
            };
            return mapped;
        }

        public async Task<global::EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity> MapAsync(global::EasyMicroservices.SMSMicroservice.Contracts.Requests.SingleTextMessageRequestContract fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = Map(fromObject, uniqueRecordId, language, parameters);
            mapped.MessageSenderTextMessages = await MessageSenderTextMessageDatabaseLogic.GetMessageSenders(fromObject.Senders.ToArray());
            return mapped;
        }

        public async Task<global::EasyMicroservices.SMSMicroservice.Contracts.Requests.SingleTextMessageRequestContract> MapAsync(global::EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = Map(fromObject, uniqueRecordId, language, parameters);
            return mapped;
        }
        public object MapObject(object fromObject, string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            if (fromObject.GetType() == typeof(EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity))
                return Map((EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity)fromObject, uniqueRecordId, language, parameters);
            return Map((EasyMicroservices.SMSMicroservice.Contracts.Requests.SingleTextMessageRequestContract)fromObject, uniqueRecordId, language, parameters);
        }
        public async Task<object> MapObjectAsync(object fromObject, string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            if (fromObject.GetType() == typeof(EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity))
                return await MapAsync((EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity)fromObject, uniqueRecordId, language, parameters);
            return await MapAsync((EasyMicroservices.SMSMicroservice.Contracts.Requests.SingleTextMessageRequestContract)fromObject, uniqueRecordId, language, parameters);
        }
    }
    public class TextMessageEntity_SingleTextMessageRequest_Mapper : IMapper
    {
        readonly IMapperProvider _mapper;
        public TextMessageEntity_SingleTextMessageRequest_Mapper(IMapperProvider mapper)
        {
            _mapper = mapper;
        }

        public global::EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity Map(global::EasyMicroservices.SMS.Models.Requests.SingleTextMessageRequest fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = new global::EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity()
            {
                ToPhoneNumbers = fromObject.ToNumber,
                Text = fromObject.Text,
                Status = EasyMicroservices.SMSMicroservice.DataTypes.MessageStatusType.Queue,
            };
            return mapped;
        }

        public global::EasyMicroservices.SMS.Models.Requests.SingleTextMessageRequest Map(global::EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = new global::EasyMicroservices.SMS.Models.Requests.SingleTextMessageRequest()
            {
                ToNumber = fromObject.ToPhoneNumbers,
                Senders = fromObject.MessageSenderTextMessages.Select(x => x.MessageSender.PhoneNumber).ToList(),
                Text = fromObject.Text,
            };
            return mapped;
        }

        public async Task<global::EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity> MapAsync(global::EasyMicroservices.SMS.Models.Requests.SingleTextMessageRequest fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = Map(fromObject, uniqueRecordId, language, parameters);
            return mapped;
        }

        public async Task<global::EasyMicroservices.SMS.Models.Requests.SingleTextMessageRequest> MapAsync(global::EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = Map(fromObject, uniqueRecordId, language, parameters);
            return mapped;
        }
        public object MapObject(object fromObject, string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            if (fromObject.GetType() == typeof(EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity))
                return Map((EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity)fromObject, uniqueRecordId, language, parameters);
            return Map((EasyMicroservices.SMS.Models.Requests.SingleTextMessageRequest)fromObject, uniqueRecordId, language, parameters);
        }
        public async Task<object> MapObjectAsync(object fromObject, string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            if (fromObject.GetType() == typeof(EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity))
                return await MapAsync((EasyMicroservices.SMSMicroservice.Database.Entities.TextMessageEntity)fromObject, uniqueRecordId, language, parameters);
            return await MapAsync((EasyMicroservices.SMS.Models.Requests.SingleTextMessageRequest)fromObject, uniqueRecordId, language, parameters);
        }
    }
}