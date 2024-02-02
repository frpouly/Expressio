public class ExpressionDTO
{
    public long Id { get; set; }
    public required string Content { get; set; }
    public List<string>? Definitions { get; set; }
}