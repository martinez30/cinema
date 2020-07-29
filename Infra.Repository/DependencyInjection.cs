using Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infra.Repository
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddDbContext<CineContext>(x =>
            {
                x.UseSqlServer(Configuration.ConnectionString, options =>
                 {
                     options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                 });
            }, ServiceLifetime.Scoped
            );
            services.AddScoped<ICineContext, CineContext>();
            return services;
        }
    }
}
