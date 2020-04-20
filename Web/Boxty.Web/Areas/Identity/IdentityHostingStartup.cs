using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Boxty.Web.Areas.Identity.IdentityHostingStartup))]
namespace Boxty.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}