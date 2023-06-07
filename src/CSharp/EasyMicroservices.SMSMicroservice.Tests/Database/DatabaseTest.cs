using EasyMicroservices.Database.EntityFrameworkCore.Providers;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.SMSMicroservice.Database.Contexts;
using EasyMicroservices.SMSMicroservice.Database.Entities;
using System.Threading.Tasks;
using Xunit;

namespace EasyMicroservices.SMSMicroservice.Tests.Database
{
    public class DatabaseTest
    {
        public DatabaseTest()
        {

        }

        public IDatabase GetContext()
        {
            return new EntityFrameworkCoreDatabaseProvider(new SMSContext(new DatabaseBuilderTest()));
        }

        [Fact]
        public async Task TestInsert()
        {
            var context = GetContext().GetQueryOf<ApiUserEntity>();
            var entity = new ApiUserEntity()
            {
                Key = "Ali",
                Password = "password",
                UserName = "username"
            };
            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            var find = await context.FirstOrDefaultAsync(x => x.UserName == entity.UserName);
            Assert.Equal(find.UserName, entity.UserName);
        }
    }
}
