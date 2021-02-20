using DNS.Server;
using System;
using System.Threading.Tasks;

namespace NXMineNS
{
    class Program
    {
        const string version = "0.0.1";
        static void Main(string[] args)
        {
            Console.WriteLine("NXMineNS - Version " + version);
            Console.WriteLine("    Usage: nxminens 192.168.1.4");
            Console.WriteLine("    Replace 192.168.1.4 with your Minecraft Bedrock Server IP.");
            if (args == null || args.Length == 0)
            {
                // no arguments
            }
            else
            {
                // arguments
                try
                {
                    startServer(args[0]).Wait();
                    
                }catch(Exception ex)
                {
                    Console.WriteLine("========EXCEPTION=======");
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("========================");
                }
                
            }
            
        }
        static async Task startServer(string ip)
        {
            Console.WriteLine("Starting Nameserver to redirect all known Minecraft \"Featured Servers\" to " + ip);
            
            MasterFile masterFile = new MasterFile();
            DnsServer server = new DnsServer(masterFile, "8.8.8.8");

            // Resolve these domain to given ip
            masterFile.AddIPAddressResourceRecord("play.inpvp.net", ip);
            masterFile.AddIPAddressResourceRecord("play.lbsg.net", ip);
            masterFile.AddIPAddressResourceRecord("pe.mineplex.com", ip);
            masterFile.AddIPAddressResourceRecord("mco.cubecraft.net", ip);
            masterFile.AddIPAddressResourceRecord("geo.hivebedrock.network", ip);
            masterFile.AddIPAddressResourceRecord("play.galaxite.net", ip);

            // Log every request
            server.Requested += (sender, e) => Console.WriteLine(e.Request);
            // On every successful request log the request and the response
            server.Responded += (sender, e) => Console.WriteLine("{0} => {1}", e.Request, e.Response);
            // Log errors
            server.Errored += (sender, e) => Console.WriteLine(e.Exception.Message);

            // Start the server (by default it listents on port 53)
            Console.WriteLine("Looks like we are all set. Set your Switch's DNS to the IP of this computer!");
            await server.Listen();
            
        }
    }
}
