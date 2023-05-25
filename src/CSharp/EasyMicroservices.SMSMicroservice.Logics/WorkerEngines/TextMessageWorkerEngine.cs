using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.SMS.Interfaces;
using EasyMicroservices.SMS.Models.Requests;
using EasyMicroservices.SMSMicroservice.Database.Entities;
using EasyMicroservices.SMSMicroservice.DatabaseLogics;
using EasyMicroservices.SMSMicroservice.DataTypes;
using ServiceContracts;
using System;
using System.Threading.Tasks;

namespace EasyMicroservices.SMSMicroservice.WorkerEngines
{
    public class TextMessageWorkerEngine
    {
        public async Task Start(TimeSpan delayWhenEmpty)
        {
            while (true)
            {
                var queueMessageToSend = await TakeFromQueueAndSendMessage();

                if (!queueMessageToSend)
                    await Task.Delay(delayWhenEmpty);
            }
        }

        async Task<bool> TakeFromQueueAndSendMessage()
        {
            try
            {
                await using TextMessageDatabaseLogic logic = new TextMessageDatabaseLogic();

                var queueMessageToSend = await TakeFromQueue(logic);

                if (queueMessageToSend.TryGetResult(out TextMessageEntity textMessage))
                {
                    var result = await SendMessage(textMessage);
                    if (result)
                        textMessage.Status = MessageStatusType.Sent;
                    else
                    {
                        textMessage.Status = MessageStatusType.Exception;
                        //save exception message to db
                    }
                    await logic.Update(textMessage);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                //log to db or logger
                return false;
            }
        }

        Task<MessageContract<TextMessageEntity>> TakeFromQueue(TextMessageDatabaseLogic logic)
        {
            return logic.GetBy(x => x.Status == MessageStatusType.Queue);
        }

        async Task<MessageContract> SendMessage(TextMessageEntity textMessage)
        {
            ISMSProvider smsProvider = default;
            IMapperProvider mapperProvider = default;
            if (textMessage.PhoneNumberTextMessages.Count > 1)
            {
                var request = mapperProvider.Map<MultipleTextMessageRequest>(textMessage);
                var sendSMSResponse = await smsProvider.SendMultipleAsync(request);
                // save to db if exception
                return sendSMSResponse.IsSuccess;
            }
            else
            {
                var request = mapperProvider.Map<SingleTextMessageRequest>(textMessage);
                var sendSMSResponse = await smsProvider.SendSingleAsync(request);
                // save to db if exception
                return sendSMSResponse.IsSuccess;
            }
        }
    }
}
