using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyState.Registrar;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddEasyState(this IServiceCollection services)
    {
        ServiceLocator.ServiceProvider = services.BuildServiceProvider();
        return services;
    }
}
