using System.ServiceProcess;

namespace Infrastructure.Service
{
    public partial class NetworkManagementService : ServiceBase
    {
        public NetworkManagementService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
