using Microsoft.EntityFrameworkCore;

namespace Expressio.Models;

public class ExpressioContext : DbContext
{
    public ExpressioContext(DbContextOptions<ExpressioContext> options)
        : base(options)
    {
    }

    public ExpressioContext()
        : base()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>()
            .HasMany(e => e.Expressions)
            .WithOne(e => e.Language)
            .HasForeignKey("LanguageId")
            .IsRequired();
        modelBuilder.Entity<Language>().HasData(
            new Language(){ Id = 1, Code = "fr" }
        );
        modelBuilder.Entity<Expression>().HasData(
            new FileLoader("Resources/expressions_fr.json").Load().Skip(3000)
        );
    }

    public virtual DbSet<Language> Languages { get; set; }
    public virtual DbSet<Expression> Expressions { get; set; }
}