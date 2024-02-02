using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Humanizer;

namespace Expressio.Models;

public class Expression
{
    public long Id { get; set; }
    public required string Content { get; set; }
    public List<string>? Definitions { get; set; }
    public long LanguageId { get; set; }
    public Language Language { get; set; }
  
    public IEnumerable<string> Words()
    {
        if(Content == null)
        {
            return Array.Empty<string>();
        }
    
        return Content.Split(" ").Skip(1).Reverse().Skip(1);
    }

    public string SplitFirst(string word)
    {
        if(Content == null)
            return "";

        return Content.Split($" {word} ", 2).First() + " " + word;
    }

    public string SplitLast(string word) 
    {
        if(Content == null)
            return "";

        return Content.Split($" {word} ", 2).Last();
    }
}