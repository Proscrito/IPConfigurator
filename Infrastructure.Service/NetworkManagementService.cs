using System;
using System.Diagnostics;
using System.ServiceProcess;
using Core.Service;

namespace Infrastructure.Service
{
    public partial class NetworkManagementService : ServiceBase
    {
        private NetworkService _networkService;

        public NetworkManagementService()
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.LogManager.GetLogger(typeof(Program));

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _networkService = new NetworkService();
                Trace.WriteLine("Service started");
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Unable to start network service: {e.GetBaseException()}");
            }
        }

        protected override void OnStop()
        {
            try
            {
                _networkService.Dispose();
                Trace.WriteLine("Service stopped");
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Unable to stop network service: {e.GetBaseException()}");
            }
        }
    }
}
