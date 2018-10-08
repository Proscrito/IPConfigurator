using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Test
{
    [TestClass]
    public class NetworkManagerTest
    {
        private INetworkManager _manager;

        [TestInitialize]
        public void Init()
        {
            _manager = new NetworkManager();
        }

        [Ignore("manual use only, for debugging purposes")]
        [TestMethod]
        public void GetIpList()
        {
            foreach (var ipAddress in _manager.GetIpList("192.168.88.105"))
            {
                var traceLine = $"{ipAddress.InterfaceId} : {ipAddress.Address} : {ipAddress.SubnetMask}";
                Trace.WriteLine(traceLine);
            }
        }

        [Ignore("manual use only, for debugging purposes")]
        [TestMethod]
        public void AddIpAddress()
        {
            var defaultIp = _manager.GetIpList("192.168.88.105").First();
            defaultIp.Address = "192.168.88.208"; // rewrite ip
            _manager.AddIpAddress(defaultIp);
        }
    }
}
