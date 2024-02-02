
using System.Collections.Immutable;
using Humanizer;

namespace Expressio.Models;

public class Mixer
{
    private readonly ExpressionContext _context;

    private Dictionary<string, Possibilities> _dic = new();
    private List<string> _weights = new();

    public Mixer(ExpressionContext context)
    {
        _context = context;
        PopulateDictionnary();
    }

    public MixedExpression Generate(int? seed = null)
    {
        int usedSeed = seed ?? Environment.TickCount;
        Random rand = new Random(usedSeed);
        var word = _weights.ElementAt(rand.Next(0, _dic.Count));
        return _dic[word].Generate(rand, usedSeed);
    }

    private void PopulateDictionnary()
    {
        foreach(Expression e in _context.Expressions.ToList())
        {
            foreach(string word in e.Words())
            {
                if(_dic.TryGetValue(word, out Possibilities? value))
                    value.AddExpression(e);
                else
                    _dic[word] = new Possibilities(word, e);
                   
            }
        }
        foreach(var kv in _dic)
            kv.Value.RemoveDuplicates();
        // Remove all the Words that are on only one expression
        _dic = _dic.Where(kv => kv.Value.NumberOfPossibilities > 1)
                   .ToDictionary(kv => kv.Key, kv => kv.Value);
        foreach(var kv in _dic)
            _weights.AddRange(Enumerable.Repeat(kv.Key, kv.Value.NumberOfPossibilities));
    }
}