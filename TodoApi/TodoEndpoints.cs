using Microsoft.AspNetCore.Http.HttpResults;
using TodoApi.Data;

namespace TodoApi;

public static class TodoEndpoints
{
    public static void RegisterTodoEndpoints(this WebApplication app)
    {
        var todos = app.MapGroup("/api/todos").WithTags("Todos");
        todos.MapGet("/", GetTodos);
    }
    
    private static Results<Ok<TodoDto[]>, NotFound> GetTodos(AppDbContext db)
    {
        var todos = db.Todos.Select(t => new TodoDto(t.Id, t.Title));
        return TypedResults.Ok(todos.ToArray());
    }
}