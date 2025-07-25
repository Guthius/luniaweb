namespace Lunia.V4.Helpers;

internal static class DependencyInjection
{
    public static void AddRequestParsing(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<RequestParser>();
        services.AddScoped<Request>(s => s.GetRequiredService<RequestParser>().Request);
    }
}