using EasyMicroservices.SMSMicroservice.Database;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.SMSMicroservice
{
    public class DatabaseBuilder : IDatabaseBuilder
    {
        public void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("SMS database");
            //optionsBuilder.UseSqlServer("connection string");
        }
    }
}
