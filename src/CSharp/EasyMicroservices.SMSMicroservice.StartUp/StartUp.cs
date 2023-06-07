using EasyMicroservices.Database.EntityFrameworkCore.Providers;
using EasyMicroservices.SMSMicroservice.Database.Contexts;
using EasyMicroservices.SMSMicroservice.Database;
using EasyMicroservices.SMSMicroservice.WorkerEngines;
using System;
using System.Threading.Tasks;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.SMSMicroservice.Interfaces;
using EasyMicroservices.SMSMicroservice.Helpers;

namespace EasyMicroservices.SMSMicroservice
{
    public class StartUp
    {
        public Task Run(IDependencyManager dependencyManager)
        {
            ApplicationManager.Instance.DependencyManager = dependencyManager;
            var textMessageWorkerEngine = new TextMessageWorkerEngine(dependencyManager);
            _ = textMessageWorkerEngine.Start(TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }
    }
}