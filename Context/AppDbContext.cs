using Microsoft.EntityFrameworkCore;
using pagination.Models;

namespace pagination.Context;

public class AppDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public string DbPath { get; set; }

    public AppDbContext()
    {
        DbPath = Path.Combine(Environment.CurrentDirectory, "test.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}