using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Gameboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    if (config["threadCount"] != null)
                    {
                        options.ThreadCount = int.Parse(config["threadCount"]);
                    }
                })
                .UseIISIntegration()
                .UseUrls("http://localhost:10861")
                .Build();

            host.Run();
        }
    }
}
