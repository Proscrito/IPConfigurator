using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Core
{
    public class NetworkManager : INetworkManager
    {
        public IEnumerable<IPDescriptor> GetIpList(string currentIp = null)
        {
            var ethernetInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(x => x.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                .Where(x => x.OperationalStatus == OperationalStatus.Up);

            foreach (var networkInterface in ethernetInterfaces)
            {
                var props = networkInterface.GetIPProperties();
                var addresses = props.UnicastAddresses
                    .Where(x => x.Address.AddressFamily == AddressFamily.InterNetwork)
                    .ToList();

                if (currentIp != null && addresses.All(x => x.Address.ToString() != currentIp))
                {
                    yield break;
                }

                foreach (var address in addresses)
                {
                    yield return new IPDescriptor
                    {
                        InterfaceId = networkInterface.Id,
                        InterfaceName = networkInterface.Name,
                        Address = address.Address.ToString(),
                        SubnetMask = address.IPv4Mask.ToString()
                    };
                }
            }
        }

        public bool AddIpAddress(IPDescriptor ipDescriptor)
        {
            var ethernetInterface = NetworkInterface.GetAllNetworkInterfaces()
                .Single(x => x.Id == ipDescriptor.InterfaceId);
            var props = ethernetInterface.GetIPProperties();

            if (props.UnicastAddresses.Any(x => x.Address.ToString() == ipDescriptor.Address))
            {
                return false;
            }

            var managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var instances = managementClass.GetInstances();

            foreach (ManagementObject mo in instances)
            {
                if ((bool)mo["IPEnabled"] && (string)mo["SettingID"] == ipDescriptor.InterfaceId)
                {
                    var existingV4 = ethernetInterface.GetIPProperties().UnicastAddresses
                        .Where(x => x.Address.AddressFamily == AddressFamily.InterNetwork)
                        .ToList();

                    var ipAddresses = existingV4.Select(x => x.Address.ToString()).ToList();
                    ipAddresses.Add(ipDescriptor.Address);
                    var ipSubnet = existingV4.Select(x => x.IPv4Mask.ToString()).ToList();
                    ipSubnet.Add(ipDescriptor.SubnetMask);

                    var newIP = mo.GetMethodParameters("EnableStatic");
                    newIP["IPAddress"] = ipAddresses.ToArray();
                    newIP["SubnetMask"] = ipSubnet.ToArray();

                    mo.InvokeMethod("EnableStatic", newIP, null);
                }
            }

            return true;
        }
    }
}
