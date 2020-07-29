using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<MovieCategoryApplication>();
            services.AddScoped<MovieApplication>();
            services.AddScoped<SessionApplication>();
            services.AddScoped<RoomApplication>();
            services.AddScoped<FoodApplication>();
            services.AddScoped<FoodCategoryApplication>();
            services.AddScoped<OrderApplication>();
            return services;
        }
    }
}
