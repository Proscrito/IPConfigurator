using System.ServiceProcess;

namespace Infrastructure.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var ServicesToRun = new ServiceBase[]
            {
                new NetworkManagementService()
            };

            ServiceBase.Run(ServicesToRun);
        }
    }
}
