using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.SMSMicroservice.Database
{
    public interface IDatabaseBuilder
    {
        void OnConfiguring(DbContextOptionsBuilder optionsBuilder);
    }
}
