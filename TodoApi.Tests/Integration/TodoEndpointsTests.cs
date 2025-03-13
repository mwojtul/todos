using System.Net.Http.Json;
using Shouldly;
using TodoApi.Data;
using Xunit.Abstractions;

namespace TodoApi.Tests.Integration;

[Collection("DatabaseCollection")]
public class TodoEndpointsTests(CustomWebApplicationFactory factory, ITestOutputHelper testOutputHelper) : BaseEndpointTests.BaseEndpointsTests(factory)
{
    [Fact]
    public async Task GetTodos_ReturnsAllTodos()
    {
        // Create a todo
        Db.Todos.Add(new Todo { Title = "Todo 1" });
        await Db.SaveChangesAsync();
        
        // Get all todos
        var response = await Client.GetAsync("/api/todos");
        var todos = await response.Content.ReadFromJsonAsync<List<TodoDto>>();
        
        todos.ShouldNotBeNull();
        // This will fail as IHttpContextAccessor returns null for HttpContext 
        todos.Count.ShouldBe(0);
    }
}