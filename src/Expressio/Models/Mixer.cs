
using Humanizer;

namespace Expressio.Models;

public class Mixer
{
    private readonly ExpressionContext _context;

    private Dictionary<string, List<Expression>> _dic = new();

    public Mixer(ExpressionContext context)
    {
        _context = context;
        PopulateDictionnary();
    }

    public MixedExpression Generate(int? seed = null)
    {
        int usedSeed = seed ?? Environment.TickCount;
        Random rand = new Random(usedSeed);
        var element = _dic.ElementAt(rand.Next(0, _dic.Count));
        var expressions = element.Value.OrderBy(e => rand.Next()).Take(2).ToList();
        return new MixedExpression(
            expressions.First().SplitFirst(element.Key) + expressions.Last().SplitLast(element.Key),
            [expressions.First(), expressions.Last()],
            usedSeed
        );
    }

    private void PopulateDictionnary()
    {
        foreach(Expression e in _context.Expressions.ToList())
        {
            foreach(string word in e.Words())
            {
                if(_dic.ContainsKey(word))
                     _dic[word].Add(e);
                else
                    _dic[word] = new List<Expression>(){ e };
                   
            }
        }
        // Remove all the Words that are on only one expression
        _dic = _dic.Where(kv => kv.Value.Count() > 1).ToDictionary(kv => kv.Key, kv => kv.Value);
    }
}