using EasyMicroservices.SMSMicroservice.Database.Entities;
using EasyMicroservices.SMSMicroservice.Database.Entities.ManyToMany;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.SMSMicroservice.Database.Contexts
{
    public class SMSContext : DbContext
    {
        IDatabaseBuilder _builder;
        public SMSContext(IDatabaseBuilder builder = default)
        {
            _builder = builder;
        }

        public DbSet<ApiUserEntity> ApiUsers { get; set; }
        public DbSet<MessageSenderEntity> MessageSenders { get; set; }
        public DbSet<MessageSenderTextMessageEntity> MessageSenderTextMessages { get; set; }
        public DbSet<TextMessageEntity> TextMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_builder != null)
                _builder.OnConfiguring(optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApiUserEntity>(model =>
            {
                model.HasKey(r => r.Id);
            });

            modelBuilder.Entity<MessageSenderEntity>(model =>
            {
                model.HasKey(x => x.Id);

                model.HasOne(x => x.ApiUser)
                   .WithMany(x => x.MessageSenders)
                   .HasForeignKey(x => x.ApiUserId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TextMessageEntity>(model =>
            {
                model.HasKey(x => x.Id);

                model.Property(x => x.Status)
                .HasColumnType("VARCHAR(24)")
                .HasConversion<string>();

                model.HasOne(x => x.ApiUser)
                   .WithMany(x => x.TextMessages)
                   .HasForeignKey(x => x.ApiUserId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MessageSenderTextMessageEntity>(model =>
            {
                model.HasKey(x => new { x.TextMessageId, x.MessageSenderId });
                model.HasOne(x => x.MessageSender)
                   .WithMany(x => x.MessageSenderTextMessages)
                   .HasForeignKey(x => x.MessageSenderId).OnDelete(DeleteBehavior.Restrict);

                model.HasOne(x => x.TextMessage)
                   .WithMany(x => x.MessageSenderTextMessages)
                   .HasForeignKey(x => x.TextMessageId).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}