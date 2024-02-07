using System.Collections;
using Expressio.Models;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Moq;

public class Utils
{
    public static ExpressioContext CreateFakeContext()
    {
        DbContextOptions<ExpressioContext> options = new DbContextOptionsBuilder<ExpressioContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ExpressioContext(options);
        context.Database.EnsureCreated();
        return context;
    }
}
