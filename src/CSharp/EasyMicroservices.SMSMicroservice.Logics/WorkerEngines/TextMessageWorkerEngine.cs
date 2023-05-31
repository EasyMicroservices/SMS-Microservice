using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.SMS.Interfaces;
using EasyMicroservices.SMS.Models.Requests;
using EasyMicroservices.SMSMicroservice.Database.Entities;
using EasyMicroservices.SMSMicroservice.DatabaseLogics;
using EasyMicroservices.SMSMicroservice.DataTypes;
using EasyMicroservices.SMSMicroservice.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EasyMicroservices.SMSMicroservice.WorkerEngines
{
    public class TextMessageWorkerEngine
    {
        readonly IDependencyManager _dependencyManager;

        public TextMessageWorkerEngine(IDependencyManager dependencyManager)
        {
            _dependencyManager = dependencyManager;
        }

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
                await using var logic = TextMessageDatabaseLogic.CreateInstance(_dependencyManager);

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
                    await logic.SaveChangesAsync();
                    return textMessage.Status == MessageStatusType.Sent;
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
            return logic.GetBy(x => x.Status == MessageStatusType.Queue,
                q => q.Include(x => x.PhoneNumberTextMessages)
                .ThenInclude(x => x.PhoneNumberModel)
                .Include(x => x.MessageSenderTextMessages)
                .ThenInclude(x => x.MessageSender)
                );
        }

        async Task<MessageContract> SendMessage(TextMessageEntity textMessage)
        {
            ISMSProvider smsProvider = _dependencyManager.GetSMSProvider();
            IMapperProvider mapperProvider = _dependencyManager.GetMapper();
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
