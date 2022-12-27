
using Domain.UseCases.Order;
using WebAppOrder.Domain.UseCases.Order;

namespace WebAppOrder.Routes
{
    public static class EndPoints
    {
        public static void AddEndPoints(this WebApplication app, IServiceProvider serviceProvider)
        {
            app.UseRouting();

            app.MapPost("order/MakeOrder", (OrderRequest request, HttpRequest httpRequest) =>
                (serviceProvider.GetService(typeof(IUseCaseOrder)) as IUseCaseOrder).MakeOrder(httpRequest, request));

            app.MapGet("order/ConsumeQueue", () => 
                (serviceProvider.GetService(typeof(IUseCaseOrder)) as IUseCaseOrder).ConsumeOrderQueue());

        }

    }
}
