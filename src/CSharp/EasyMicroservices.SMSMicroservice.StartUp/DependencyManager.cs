using EasyMicroservices.Database.EntityFrameworkCore.Providers;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.SMS.Interfaces;
using EasyMicroservices.SMS.Kavenegar.Providers;
using EasyMicroservices.SMSMicroservice.Database.Contexts;
using EasyMicroservices.SMSMicroservice.Database;
using EasyMicroservices.SMSMicroservice.Interfaces;
using System;
using EasyMicroservices.Database.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EasyMicroservices.Configuration.Interfaces;

namespace EasyMicroservices.SMSMicroservice
{
    public class DependencyManager : IDependencyManager
    {
        public IConfigProvider GetConfigProvider()
        {
            throw new NotImplementedException();
        }

        public IDatabase GetDatabase()
        {
            return new EntityFrameworkCoreDatabaseProvider(new SMSContext(new DatabaseBuilder()));
        }

        public IMapperProvider GetMapper()
        {
            throw new NotImplementedException();
        }

        public ISMSProvider GetSMSProvider()
        {
            return new KavenegarSMSProvider("TestApiKey");
        }
    }
}
