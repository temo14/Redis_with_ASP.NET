using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Redis_ASP.NET_Way.Db;
using Redis_ASP.NET_Way.Models;

namespace Redis_ASP.NET_Way
{
    public class InitService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IDistributedCache _cache;

        public InitService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var salesDb = scope.ServiceProvider.GetRequiredService<SalesContext>();

            var cache = scope.ServiceProvider.GetRequiredService<IDistributedCache>();
            // remove old data
            var cachePipe = new List<Task>
            {
                cache.RefreshAsync("top:sales", cancellationToken),
                cache.RefreshAsync("top:name", cancellationToken),
                cache.RefreshAsync("totalSales", cancellationToken)
            };

            cachePipe.AddRange(salesDb.Employees.Select(employee =>
                                                  cache.RemoveAsync($"employee:{employee.EmployeeId}:avg", cancellationToken)));

            await Task.WhenAll(cachePipe);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
