namespace Domain.SharedKernel.BaseUseCase
{
    public class BaseUseCase
    {
        protected IServiceProvider _serviceProvider;

        protected BaseUseCase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
