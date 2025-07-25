using Microsoft.Extensions.DependencyInjection;

namespace Lunia.V4.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options => options
            .RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));
    }
}