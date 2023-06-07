using EasyMicroservices.SMSMicroservice.Database.Entities;
using EasyMicroservices.SMSMicroservice.Database.Entities.ManyToMany;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EasyMicroservices.SMSMicroservice.Tests.SMS
{
    public class TextMessageWorkerEngineUnitTest : StartUpBaseTest
    {
        async Task<ApiUserEntity> GetApiUser()
        {
            var dbReader = GetReadableOf<ApiUserEntity>();
            var user = await dbReader.FirstOrDefaultAsync();
            if (user == null)
            {
                user = new ApiUserEntity()
                {
                    Key = "apitest",
                    UserName = "username",
                    Password = "password",
                };
                var dbWriter = GetWritableOf<ApiUserEntity>();
                await dbWriter.AddAsync(user);
                await dbWriter.SaveChangesAsync();
            }
            return user;
        }

        async Task<MessageSenderEntity> GetMessageSender(string senderPhoneNumber)
        {
            var user = await GetApiUser();
            var dbReader = GetReadableOf<MessageSenderEntity>();
            var sender = await dbReader.FirstOrDefaultAsync(x => x.ApiUserId == user.Id && x.PhoneNumber == senderPhoneNumber);
            if (sender == null)
            {
                sender = new MessageSenderEntity()
                {
                    ApiUserId = user.Id,
                    PhoneNumber = senderPhoneNumber
                };
                var dbWriter = GetWritableOf<MessageSenderEntity>();
                await dbWriter.AddAsync(sender);
                await dbWriter.SaveChangesAsync();
            }
            return sender;
        }

        [Theory]
        [InlineData("+989111111111", "09391111111", "hello world")]
        public async Task SendTextMessageTest(string senderNumber, string sendTo, string message)
        {
            await CheckStarted();
            var sender = await GetMessageSender(senderNumber);
            var dbWriter = GetWritableOf<TextMessageEntity>();

            var entityResult = await dbWriter.AddAsync(new TextMessageEntity()
            {
                Text = message,
                ApiUserId = sender.ApiUserId,
                MessageSenderTextMessages = new List<MessageSenderTextMessageEntity>()
                {
                    new MessageSenderTextMessageEntity()
                    {
                         MessageSenderId = sender.Id
                    }
                },
                ToPhoneNumbers = sendTo,
                Status = DataTypes.MessageStatusType.Queue
            });
            await dbWriter.SaveChangesAsync();

            TextMessageEntity textMessage = default;
            int count = 0;
            do
            {
                var dbReader = GetReadableOf<TextMessageEntity>();
                textMessage = await dbReader.FirstOrDefaultAsync(x => x.Id == entityResult.Entity.Id);
                if (textMessage.Status != DataTypes.MessageStatusType.Queue)
                    break;
                await Task.Delay(TimeSpan.FromSeconds(1));
                count++;
                if (count == 10)
                {
                    throw new Exception("time out! message is not go to queue!");
                }
            }
            while (true);
            Assert.Equal(DataTypes.MessageStatusType.Sent, textMessage.Status);
        }
    }
}
