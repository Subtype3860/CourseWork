namespace BlogDAL.Models;

public class Remark
{
    public string Id { get; set; }
    public string? Comment { get; set; }
    public string? PostId { get; set; }
    public Post? Post { get; set; }

    public Remark()
    {
        Id = Guid.NewGuid().ToString();
    }
    
}