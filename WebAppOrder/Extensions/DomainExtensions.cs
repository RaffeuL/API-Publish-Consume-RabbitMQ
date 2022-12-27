using WebAppOrder.Domain.UseCases.Order;

namespace WebAppOrder.Extensions
{
    public static class DomainExtensions
    {

        public static IServiceCollection AddDomainConfigs(this IServiceCollection services)
        {
            services.AddScoped<IUseCaseOrder, UseCaseOrder>();

            return services;
        }
    }
}
