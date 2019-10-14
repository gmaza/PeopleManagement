using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PM.Infrastructure.EF.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Infrastructure
{
    public static class InfrastructureServiceCOnfigurationExtension
    {
        public static void RegisterDomain(this IServiceCollection services, IConfiguration conf)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PMContext>();
            optionsBuilder.UseSqlServer(conf.GetConnectionString("DefaultConnection"));

            services.AddDbContext<PMContext>(options => options.UseSqlServer(conf["ConnectionStrings:DefaultConnection"]));
        }
    }
}
