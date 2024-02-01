using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;

namespace Expressio.Models;

public class FileLoader
{
    public string Path { get; }

    public FileLoader(string path)
    {
        Path = path;
    }

    public IEnumerable<Expression> Load()
    {
        using (StreamReader r = new StreamReader(Path))
        {
            string json = r.ReadToEnd();
            var results =  JsonConvert.DeserializeObject<List<Expression>>(json);
            return results == null ? new List<Expression>() : results;
        }
    }
}