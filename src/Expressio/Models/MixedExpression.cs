namespace Expressio.Models;

public class MixedExpression
{
    public string Content { get; }
    public IEnumerable<Expression> Expressions { get; }
    public int Seed { get; }

    public MixedExpression(string content, IEnumerable<Expression> expressions, int seed) 
    {
        Content = content;
        Expressions = expressions;
        Seed = seed;
    }
}