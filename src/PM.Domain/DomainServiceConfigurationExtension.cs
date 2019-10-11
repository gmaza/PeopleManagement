using Microsoft.Extensions.DependencyInjection;
using PM.Domain.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Domain
{
    public static class DomainServiceConfigurationExtension
    {
        public static void RegisterDomain(this IServiceCollection services)
        {
            services.AddSingleton<IPeopleDomainService, PeopleDomainService>();
        }
    }
}
