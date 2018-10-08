using System;
using Core.Service;

namespace Infrastructure.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.LogManager.GetLogger(typeof(Program));

            using (new NetworkService())
            {
                Console.ReadKey();
            }
        }
    }
}
