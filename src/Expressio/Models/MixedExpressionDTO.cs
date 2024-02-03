namespace Expressio.Models;

public class MixedExpressionDTO
{
    public string? Content { get; set; }
    public IEnumerable<ExpressionDTO>? Expressions { get; set; }
    public int Seed { get; set; }
}