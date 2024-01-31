using Humanizer;

namespace Expressio.Models;

public class Expression
{
    public long Id { get; set; }
    public string? Content { get; set; }
    public string[]? Definitions { get; set; }
  
    public IEnumerable<string> Words()
    {
        if(Content == null)
        {
            return Array.Empty<string>();
        }
    
        return Content.Split(" ").Skip(1);
    }
}