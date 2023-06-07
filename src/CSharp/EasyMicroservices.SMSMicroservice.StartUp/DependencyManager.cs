using EasyMicroservices.Configuration.Interfaces;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.Cores.Database.Logics;
using EasyMicroservices.Database.EntityFrameworkCore.Providers;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.CompileTimeMapper.Interfaces;
using EasyMicroservices.Mapper.CompileTimeMapper.Providers;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.SMS.Interfaces;
using EasyMicroservices.SMS.Kavenegar.Providers;
using EasyMicroservices.SMSMicroservice.Database.Contexts;
using EasyMicroservices.SMSMicroservice.Helpers;
using EasyMicroservices.SMSMicroservice.Interfaces;
using System;
using System.Linq;

namespace EasyMicroservices.SMSMicroservice
{
    public class DependencyManager : IDependencyManager
    {
        public virtual IConfigProvider GetConfigProvider()
        {
            throw new NotImplementedException();
        }

        public virtual IContractLogic<TEntity, TRequestContract, TResponseContract, long> GetContractLogic<TEntity, TRequestContract, TResponseContract>()
            where TEntity : class, IIdSchema<long>
            where TResponseContract : class
        {
            return new LongIdMappedDatabaseLogicBase<TEntity, TRequestContract, TResponseContract>(GetDatabase().GetReadableOf<TEntity>(), GetDatabase().GetWritableOf<TEntity>(), GetMapper());
        }

        public virtual IDatabase GetDatabase()
        {
            return new EntityFrameworkCoreDatabaseProvider(new SMSContext(new DatabaseBuilder()));
        }

        public virtual IMapperProvider GetMapper()
        {
            var mapper = new CompileTimeMapperProvider();
            foreach (var type in typeof(ApplicationManager).Assembly.GetTypes())
            {
                if (typeof(IMapper).IsAssignableFrom(type))
                {
                    var instance = Activator.CreateInstance(type, mapper);
                    var returnTypes = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(x => x.ReturnType != typeof(object) && x.Name == "Map").Select(x => x.ReturnType).ToArray();
                    mapper.AddMapper(returnTypes[0], returnTypes[1], (IMapper)instance);
                }
            }
            return mapper;
            //return new AutoMapperProvider(new MapperConfiguration(cfg =>
            //{
            //    //cfg.CreateMap<TextMessageEntity, SingleTextMessageRequest>()
            //    //    .ForMember(x => x.Senders, x => x.MapFrom(j => j.MessageSenderTextMessages.Select(n => n.MessageSender.PhoneNumber)))
            //    //    .ForMember(x => x.ToNumber, x => x.MapFrom(j => j.PhoneNumberTextMessages.Select(n => n.PhoneNumberModel.Number).FirstOrDefault()))
            //    //    .ReverseMap();

            //    //cfg.CreateMap<SingleTextMessageRequestContract, TextMessageEntity>()
            //    //    .ForMember(x => x.MessageSenderTextMessages, x => x.MapFrom(j => j.Senders.Select(n => n.MessageSender.PhoneNumber)))
            //    //    .ForMember(x => x.PhoneNumberTextMessages, x => x.MapFrom(async j =>
            //    //    {
            //    //        new PhoneNumberTextMessageEntity()
            //    //        {

            //    //        }
            //    //    }))
            //    //    .ReverseMap();
            //}));
        }

        public virtual ISMSProvider GetSMSProvider()
        {
            return new KavenegarSMSProvider("TestApiKey");
        }
    }
}
