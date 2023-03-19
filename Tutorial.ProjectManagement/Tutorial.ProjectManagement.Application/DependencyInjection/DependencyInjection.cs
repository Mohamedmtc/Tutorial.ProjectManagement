using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tutorial.ProjectManagement.Application.Contracts;

namespace Tutorial.ProjectManagement.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplicationDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(IMediatorImplementor).Assembly));
        }
    }
}
