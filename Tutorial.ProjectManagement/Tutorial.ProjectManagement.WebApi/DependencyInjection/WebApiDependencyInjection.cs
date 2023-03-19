﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tutorial.ProjectManagement.Application.Contracts.Database;
using Tutorial.ProjectManagement.Application.DependencyInjection;
using Tutorial.ProjectManagement.Core.AppSetting;
using Tutorial.ProjectManagement.Core.DependencyInjection;
using Tutorial.ProjectManagement.Presistance.Context;

namespace Tutorial.ProjectManagement.WebApi.DependencyInjection
{
    public static class WebApiDependencyInjection
    {
        public static void AddWebApilayerDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            AddPreLayers(services, configuration);
            AddDataBase(services);
            AddMapper(services);
        }
        private static void AddPreLayers(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreLayer(configuration);
            services.AddApplicationDependencyInjection(configuration);
        }
        public static void AddMapper(IServiceCollection services)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(WebApiDependencyInjection).Assembly)).CreateMapper();
            services.AddSingleton(m => mapper);

        }
        private static void AddDataBase(IServiceCollection services)
        {
            services.AddDbContext<DatabaseService>(opt => opt.UseSqlServer(SettingsDependancyInjection.SqlSettings.ConnectionString!));
            services.AddScoped<IDataBaseService, DatabaseService>();
        }

    }
}
