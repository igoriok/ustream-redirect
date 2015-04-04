using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Nancy.Hosting.Self;

namespace Ustream
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                var certificate = GetCertificate();

                store.Open(OpenFlags.ReadWrite);
                store.Add(certificate);

                try
                {
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
                        new Uri("http://localhost:80/"),
                        new Uri("https://localhost:443/")))
                    {
                        host.Start();

                        AddCertificate(certificate.GetCertHashString());

                        Console.WriteLine("Press any key to stop the server.");
                        Console.ReadKey();

                        DeleteCertificate();

                        host.Stop();
                    }
                }
                finally
                {
                    store.Remove(certificate);
                    store.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.ReadKey();
            }
        }

        private static X509Certificate2 GetCertificate()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Ustream.cert.p12"))
            {
                if (stream == null)
                    throw new ApplicationException("Certificate not found");

                var data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);

                return new X509Certificate2(data, "", X509KeyStorageFlags.MachineKeySet);
            }
        }

        private static void AddCertificate(string thumbprint)
        {
            RunCommand("netsh", string.Format("http add sslcert ipport=0.0.0.0:443 certhash={0} appid={{06aabebd-3a91-4b80-8a15-adfd3c8a0b14}} clientcertnegotiation=enable", thumbprint));
        }

        private static void DeleteCertificate()
        {
            RunCommand("netsh", "http delete sslcert ipport=0.0.0.0:443");
        }

        private static void RunCommand(string file, string args)
        {
            using (var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    FileName = file,
                    Arguments = args,
                }
            })
            {
                process.Start();

                var stdOut = process.StandardOutput.ReadToEnd();
                var stdErr = process.StandardError.ReadToEnd();

                process.WaitForExit();
            }
        }
    }
}
