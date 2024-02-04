
using System.Collections.Immutable;
using Humanizer;

namespace Expressio.Models;

public class Mixer
{
    private readonly List<Expression> _expressions;

    private Dictionary<string, Possibilities> _dic = new();
    private List<string> _weights = new();

    public Mixer(List<Expression> expressions)
    {
        _expressions = expressions;
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
        foreach(Expression e in _expressions)
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

    private class Possibilities
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
}