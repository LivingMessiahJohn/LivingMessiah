using Microsoft.Extensions.DependencyInjection;

namespace RCL.Features.Calendar.Data;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddCalendar(this IServiceCollection services)
  {
    services
    .AddSingleton<IService, Service>();
    return services;
  }
}
