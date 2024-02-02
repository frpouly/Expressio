using Expressio.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Expressio.Models;

public class Language
{
    public long Id { get; set; }
    public required string Code { get; set; }
    public virtual ICollection<Expression> Expressions { get; set; }

}