using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Warehouse.Web.Areas.Identity.IdentityHostingStartup))]
namespace Warehouse.Web.Areas.Identity
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