using EasyMicroservices.SMSMicroservice.Database.Entities;
using EasyMicroservices.SMSMicroservice.Database.Entities.ManyToMany;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.SMSMicroservice.Database.Contexts
{
    public class SMSContext : DbContext
    {
        public DbSet<ApiUserEntity> ApiUsers { get; set; }
        public DbSet<MessageSenderEntity> MessageSenders { get; set; }
        public DbSet<PhoneNumberEntity> PhoneNumbers { get; set; }
        public DbSet<MessageSenderTextMessageEntity> MessageSenderTextMessages { get; set; }
        public DbSet<PhoneNumberTextMessageEntity> PhoneNumberTextMessages { get; set; }
        public DbSet<TextMessageEntity> TextMessages { get; set; }

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

            modelBuilder.Entity<PhoneNumberEntity>(model =>
            {
                model.HasKey(x => x.Id);
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
                model.HasOne(x => x.MessageSender)
                   .WithMany(x => x.MessageSenderTextMessages)
                   .HasForeignKey(x => x.MessageSenderId).OnDelete(DeleteBehavior.Restrict);

                model.HasOne(x => x.TextMessage)
                   .WithMany(x => x.MessageSenderTextMessages)
                   .HasForeignKey(x => x.TextMessageId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PhoneNumberTextMessageEntity>(model =>
            {
                model.HasOne(x => x.PhoneNumber)
                   .WithMany(x => x.PhoneNumberTextMessages)
                   .HasForeignKey(x => x.PhoneNumberId).OnDelete(DeleteBehavior.Restrict);

                model.HasOne(x => x.TextMessage)
                   .WithMany(x => x.PhoneNumberTextMessages)
                   .HasForeignKey(x => x.TextMessageId).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}