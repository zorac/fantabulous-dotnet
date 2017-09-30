using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Fantabulous.Api
{
    /// <summary>
    /// The entrypoint class for the API.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entrypoint method for the API.
        /// </summary>
        /// <param name="args">
        /// Command-line arguments
        /// </param>
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}
