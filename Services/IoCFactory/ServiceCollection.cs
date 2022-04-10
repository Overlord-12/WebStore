using Microsoft.Extensions.DependencyInjection;
using WebStore.Interface.Interfaces;
using WebStore.Services;
using WebStore.Services.InMemory;
using WebStore.Services.InSQL;

namespace IoCFactory
{
    public static class ServiceCollection
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IEmployeesData, InMemoryEmployeesData>();
            services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<ICartService, InCookiesCartService>();
            services.AddScoped<IOrderService, SqlOrderService>();

        }
    }
}
