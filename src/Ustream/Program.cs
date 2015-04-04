using System;
using Nancy;
using Nancy.Hosting.Self;

namespace Ustream
{
    class Program
    {
        static void Main(string[] args)
        {
            StaticConfiguration.DisableErrorTraces = false;

            using (var host = new NancyHost(
                new Bootstrapper(), 
                new HostConfiguration
                {
                    AllowChunkedEncoding = false,
                    UnhandledExceptionCallback = e => Console.WriteLine(e.ToString()),
                    UrlReservations = new UrlReservations
                    {
                        CreateAutomatically = true
                    }
                },
                new Uri("http://localhost:80/")))
            {
                host.Start();

                Console.WriteLine("Press any key to stop the server.");
                Console.ReadKey();

                host.Stop();
            }
        }
    }
}
