namespace WebAppOrder.Extensions
{
    public static class APIExtensions
    {
        public static void RegisterAPI(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        
        public static void RegisterAPI(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
        }
    }
}
