namespace ApiPerformanceCompare.DataModel.Entities;

public class Post
{
    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int BlogId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public Blog? Blog { get; set; }
}
