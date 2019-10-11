//using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PM.Application.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Application
{
    public static class ApplicationServiceConfigurationExtension
    {
        public static void RegisterApplication(this IServiceCollection services)
        {
            services.AddSingleton<IPeopleApplication, PeopleApplication>();
        }
    }
}
