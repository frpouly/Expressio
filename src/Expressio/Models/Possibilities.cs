using Humanizer;

namespace Expressio.Models;

public class Possibilities
{
    public string Word { get; }
    private List<Expression> Expressions = new List<Expression>();
    public int NumberOfPossibilities { 
        get 
        {
            return Expressions.Count * (Expressions.Count - 1);
        }
    }

    public Possibilities(string word, Expression expression)
    {
        Word = word;
        Expressions.Add(expression);
    }

    public void AddExpression(Expression e) 
    {
        Expressions.Add(e);
    }

    public void RemoveDuplicates()
    {
        Expressions = Expressions.Distinct().ToList();
    }

    public MixedExpression Generate(Random rand, int seed)
    {
        var expressions = Expressions.OrderBy(e => rand.Next()).Take(2).ToList();
        return new MixedExpression(
            $"{expressions.First().SplitFirst(Word)} {expressions.Last().SplitLast(Word)}",
            [expressions.First(), expressions.Last()],
            seed
        );
    }

}