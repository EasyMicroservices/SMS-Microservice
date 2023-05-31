using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.SMS.VirtualServerForTests;
using System.Threading.Tasks;

namespace EasyMicroservices.SMSMicroservice.Tests
{
    public class StartUpBaseTest
    {
        public StartUpBaseTest()
        {

        }

        static StartUp StartUp { get; set; }
        protected static DependencyManagerTest DependencyManager { get; set; }
        static SMSVirtualTestManager SMSVirtualTestManager { get; set; } = new SMSVirtualTestManager();
        public const int ServerPort = 1532;
        public static async Task CheckStarted()
        {
            if (StartUp != null)
                return;
            StartUp = new StartUp();
            DependencyManager = new DependencyManagerTest();
            await SMSVirtualTestManager.OnInitialize(ServerPort);
            await SMSVirtualTestManager.AppendService(ServerPort, "452453");
            await SMSVirtualTestManager.AppendService(ServerPort, new string[] { "452453", "452454" });

            await StartUp.Run(DependencyManager);
        }

        protected IEasyReadableQueryableAsync<TEntity> GetReadableOf<TEntity>()
            where TEntity : class
        {
            return DependencyManager.GetDatabase().GetReadableOf<TEntity>();
        }

        protected IEasyWritableQueryableAsync<TEntity> GetWritableOf<TEntity>()
            where TEntity : class
        {
            return DependencyManager.GetDatabase().GetWritableOf<TEntity>();
        }
    }
}
