using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Data;

namespace TodoApi.Tests.Integration;

public class BaseEndpointTests
{
    [CollectionDefinition("DatabaseCollection")]
    public class DatabaseCollection : ICollectionFixture<CustomWebApplicationFactory>;

    public class BaseEndpointsTests : IDisposable
    {
        private readonly IServiceScope _scope;
        protected readonly AppDbContext Db;
        protected readonly HttpClient Client;

        protected BaseEndpointsTests(CustomWebApplicationFactory factory)
        {
            _scope = factory.Services.CreateScope();
            Db = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
            Db.Database.BeginTransaction();
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        }

        public void Dispose()
        {
            Db.Database.RollbackTransaction();
            _scope.Dispose();
        }
    }
}