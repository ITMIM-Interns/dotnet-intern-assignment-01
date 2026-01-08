using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MiniApi.BackGroundJobs;
using MiniApi.Datas;
using MiniApi.FValidations;
using MiniApi.Services;
using MiniApi.Services.Interfaces;

namespace MiniApi.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServicesRegistration(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("MiniApiDb"));
            services.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<OrderCreateDtoValidator>();
            });
            services.AddHostedService<OrderStatusJob>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
