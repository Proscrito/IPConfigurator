using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Service;

namespace Infrastructure.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new NetworkService())
            {
                Console.ReadKey();
            }
        }
    }
}
