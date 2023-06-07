using EasyMicroservices.Database.EntityFrameworkCore.Providers;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.SMS.Interfaces;
using EasyMicroservices.SMS.Kavenegar.Providers;
using EasyMicroservices.SMSMicroservice.Database.Contexts;
using EasyMicroservices.SMSMicroservice.Tests.Database;

namespace EasyMicroservices.SMSMicroservice.Tests
{
    public class DependencyManagerTest : DependencyManager
    {
        public override IDatabase GetDatabase()
        {
            return new EntityFrameworkCoreDatabaseProvider(new SMSContext(new DatabaseBuilderTest()));
        }

        public override ISMSProvider GetSMSProvider()
        {
            return new KavenegarSMSProvider("TestApiKey", $"http://localhost:{StartUpBaseTest.ServerPort}");
        }
    }
}
