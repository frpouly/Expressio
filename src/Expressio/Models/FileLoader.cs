using Newtonsoft.Json;

namespace Expressio.Models;

public class FileLoader
{
    public string Path { get; }

    public FileLoader(string path)
    {
        Path = path;
    }

    public List<Expression> Load()
    {
        using (StreamReader r = new StreamReader(Path))
        {
            string json = r.ReadToEnd();
            var results =  JsonConvert.DeserializeObject<List<Expression>>(json);
            return results == null ? new List<Expression>() : results;
        }
    }
}