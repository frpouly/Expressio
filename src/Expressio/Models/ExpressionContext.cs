using Microsoft.EntityFrameworkCore;

namespace Expressio.Models;

public class ExpressionContext : DbContext
{
    public ExpressionContext(DbContextOptions<ExpressionContext> options)
        : base(options)
    {
    }

    public DbSet<Expression> Expressions { get; set; }
}