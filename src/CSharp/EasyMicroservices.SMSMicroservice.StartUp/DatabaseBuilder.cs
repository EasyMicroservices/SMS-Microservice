using EasyMicroservices.SMSMicroservice.Database;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.SMSMicroservice
{
    public class DatabaseBuilder : IDatabaseBuilder
    {
        public void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("SMS database");
            optionsBuilder.UseSqlServer("Server=.;Database=SMS;User ID=test;Password=test1234;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
