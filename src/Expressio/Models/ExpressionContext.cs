using Microsoft.EntityFrameworkCore;

namespace Expressio.Models;

public class ExpressionContext : DbContext
{
    public ExpressionContext(DbContextOptions<ExpressionContext> options)
        : base(options)
    {
    }

    public ExpressionContext()
        : base()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Expression>().HasData(
            new FileLoader("Resources/expressions_fr.json").Load()
        );
    }

    public DbSet<Expression> Expressions { get; set; }
}