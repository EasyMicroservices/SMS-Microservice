using EasyMicroservices.Configuration.Interfaces;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.SMS.Interfaces;

namespace EasyMicroservices.SMSMicroservice.Interfaces
{
    public interface IDependencyManager
    {
        IDatabase GetDatabase();
        IMapperProvider GetMapper();
        ISMSProvider GetSMSProvider();
        IConfigProvider GetConfigProvider();
    }
}
