using System.Net.Http.Headers;
using System.Transactions;
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
        private readonly TransactionScope _transactionScope;
        protected readonly AppDbContext Db;
        protected readonly HttpClient Client;

        protected BaseEndpointsTests(CustomWebApplicationFactory factory)
        {   
            factory.Server.PreserveExecutionContext = true;
            
            _scope = factory.Services.CreateScope();
            _transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
           
            Db = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        }

        public void Dispose()
        {
            _scope.Dispose();
            _transactionScope.Dispose();
        }
    }
}