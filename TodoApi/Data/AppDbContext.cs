namespace TodoApi.Data;

using Microsoft.EntityFrameworkCore;
using System;

public class AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor contextAccessor) : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }

    private static string GetTitle()
    {
        return "";
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Console.WriteLine($"HtppContext is not null {contextAccessor.HttpContext is not null}");        
        if (contextAccessor.HttpContext is not null)
        {
            modelBuilder.Entity<Todo>().HasQueryFilter(t => t.Title == GetTitle());
        }
        base.OnModelCreating(modelBuilder);
    }
}

public class Todo
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string Title { get; set; }
}