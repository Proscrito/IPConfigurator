using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Core.Service
{
    public class NetworkController : ApiController
    {
        private readonly INetworkManager _networkManager;

        public NetworkController(INetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public IEnumerable<string> Get(string ip = null)
        {
            var result = new List<string>();

            try
            {
                var results = _networkManager.GetIpList()
                    .Select(x => $"{x.InterfaceName} - {x.InterfaceId} : {x.Address} : {x.SubnetMask}");
                result.AddRange(results);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.GetBaseException().ToString());
                result.Add($"Cannot get ip list: {e.Message}");
            }

            return result;
        }

        public string Post(IPDescriptor value)
        {
            var success = ValidateValue(value);

            if (success)
            {
                try
                {
                    success = _networkManager.AddIpAddress(value);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.GetBaseException().ToString());
                    success = false;
                }
            }
            
            return success ? "ok" : "fail";
        }

        private bool ValidateValue(IPDescriptor value)
        {
            //to add ip we need to have interface id, ip and mask
            if (value?.Address == null || value.InterfaceId == null || value.SubnetMask == null)
            {
                return false;
            }

            //ip and mask should be valid
            var ipValid = IPAddress.TryParse(value.Address, out var addressStub);
            var maskValid = IPAddress.TryParse(value.SubnetMask, out var maskStub);

            return ipValid && maskValid;
        }
    }
}
