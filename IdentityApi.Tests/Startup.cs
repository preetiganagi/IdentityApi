using Autofac.Extensions.DependencyInjection;
using IdentityApi.Areas.Identity.Data;
using IdentityApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.DependencyInjection;

namespace IdentityApi.Tests
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder) =>
            hostBuilder.ConfigureAppConfiguration(lb => lb.AddJsonFile("appsettings.json", false, true))
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());

        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug))
            //.AddDbContext<IdentityApiContext>(options =>
            //        options.UseSqlServer(
            //            _configuration.GetSection("ConnectionStrings").GetSection("IdentityApiContextConnection").Value
            //            ));


            //services.AddDefaultIdentity<IdentityApiUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddRoles<IdentityRole>()
            //        .AddEntityFrameworkStores<IdentityApiContext>();
        }
    }
}