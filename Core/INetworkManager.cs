using System.Collections.Generic;

namespace Core
{
    public interface INetworkManager
    {
        IEnumerable<IPDescriptor> GetIpList(string currentIp = null);
        bool AddIpAddress(IPDescriptor ipDescriptor);
    }
}
