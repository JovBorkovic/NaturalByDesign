using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NbdAplication.Data;

[assembly: HostingStartup(typeof(NbdAplication.Areas.Identity.IdentityHostingStartup))]
namespace NbdAplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                builder.ConfigureServices((context, services) =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(
                            context.Configuration.GetConnectionString("DefaultConnection")));


                    //services.AddDefaultIdentity<IdentityUser>(options => options
                    //    .SignIn.RequireConfirmedAccount = false)
                    //    .AddEntityFrameworkStores<ApplicationDbContext>();
                });

            });
        }
    }
}