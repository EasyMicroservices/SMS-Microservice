using EasyMicroservices.SMSMicroservice.Database.Entities;
using EasyMicroservices.SMSMicroservice.Database.Entities.ManyToMany;
using EasyMicroservices.SMSMicroservice.Helpers;
using EasyMicroservices.SMSMicroservice.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMicroservices.SMSMicroservice.DatabaseLogics
{
    public class MessageSenderTextMessageDatabaseLogic
    {
        public static MessageSenderDatabaseLogic CreateInstance(IDependencyManager dependencyManager)
        {
            return new MessageSenderDatabaseLogic(dependencyManager.GetDatabase(), dependencyManager.GetMapper());
        }

        public static async Task<List<MessageSenderTextMessageEntity>> GetMessageSenders(string[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return null;
            await using var database = ApplicationManager.Instance.DependencyManager.GetDatabase();
            var readable = database.GetReadableOf<MessageSenderEntity>();
            var result = await readable
                 .Where(x => numbers.Contains(x.PhoneNumber)).ToListAsync();
            if (result.Count != numbers.Length)
            {
                var writable = ApplicationManager.Instance.DependencyManager.GetDatabase().GetWritableOf<MessageSenderEntity>();
                List<MessageSenderEntity> addedResult = new List<MessageSenderEntity>();
                foreach (var number in numbers)
                {
                    if (!await readable.AnyAsync(x => x.PhoneNumber == number))
                    {
                        addedResult.Add((await writable.AddAsync(new MessageSenderEntity()
                        {
                            PhoneNumber = number,
                        })).Entity);
                    }
                }
                await writable.SaveChangesAsync();
                result.AddRange(addedResult);
            }

            return result.Select(x => new MessageSenderTextMessageEntity()
            {
                MessageSenderId = x.Id
            }).ToList();
        }
    }
}
