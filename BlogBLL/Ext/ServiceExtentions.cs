using BlogDAL.Repositories;
using BlogDAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace BlogBLL.Ext;

public static class ServiceExtentions
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection service)
    {
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        return service;
    }

    public static IServiceCollection AddCustomRepository<T, TRepository>(this IServiceCollection service)
        where T : class where TRepository : class, IRepository<T>
    {
        service.AddScoped<IRepository<T>, TRepository>();
        return service;
    }
}