
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.SMSMicroservice.Contracts.Common;
using EasyMicroservices.SMSMicroservice.Contracts.Requests;
using EasyMicroservices.SMSMicroservice.Database;
using EasyMicroservices.SMSMicroservice.Database.Contexts;
using EasyMicroservices.SMSMicroservice.Database.Entities;
using EasyMicroservices.SMSMicroservice.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace EasyMicroservices.SMSMicroservice.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped((serviceProvider) => new DependencyManager().GetContractLogic<TextMessageEntity, SingleTextMessageRequestContract, TextMessageContract>());
            builder.Services.AddHttpContextAccessor();
            //builder.Services.AddDbContext<SMSContext>(options => new DatabaseBuilder().OnConfiguring(options));
            builder.Services.AddScoped<IDatabaseBuilder>(serviceProvider => new DatabaseBuilder());

            var app = builder.Build();
            app.UseDeveloperExceptionPage();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            StartUp startUp = new StartUp();
            await  startUp.Run(new DependencyManager());
            app.Run();
        }
    }
}