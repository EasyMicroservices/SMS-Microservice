using AutoMapper;
using EasyMicroservices.Configuration.Interfaces;
using EasyMicroservices.Database.EntityFrameworkCore.Providers;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.AutoMapper.Providers;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.SMS.Interfaces;
using EasyMicroservices.SMS.Kavenegar.Providers;
using EasyMicroservices.SMS.Models.Requests;
using EasyMicroservices.SMSMicroservice.Database.Contexts;
using EasyMicroservices.SMSMicroservice.Database.Entities;
using EasyMicroservices.SMSMicroservice.Interfaces;
using EasyMicroservices.SMSMicroservice.Tests.Database;
using System;
using System.Linq;

namespace EasyMicroservices.SMSMicroservice.Tests
{
    public class DependencyManagerTest : IDependencyManager
    {
        public IConfigProvider GetConfigProvider()
        {
            throw new NotImplementedException();
        }

        public IDatabase GetDatabase()
        {
            return new EntityFrameworkCoreDatabaseProvider(new SMSContext(new DatabaseBuilderTest()));
        }

        public IMapperProvider GetMapper()
        {
            return new AutoMapperProvider(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TextMessageEntity, SingleTextMessageRequest>()
                .ForMember(x => x.Senders, x => x.MapFrom(j => j.MessageSenderTextMessages.Select(n => n.MessageSender.PhoneNumber)))
                .ForMember(x => x.ToNumber, x => x.MapFrom(j => j.PhoneNumberTextMessages.Select(n => n.PhoneNumberModel.Number).FirstOrDefault()));
                //cfg.CreateMap<PostEntity, PostContract>();
            }));
        }

        public ISMSProvider GetSMSProvider()
        {
            return new KavenegarSMSProvider("TestApiKey", $"http://localhost:{StartUpBaseTest.ServerPort}");
        }
    }
}
