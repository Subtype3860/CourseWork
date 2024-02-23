namespace BlogDAL.Models;

public class Commentaries
{
    public string? Id { get; set; }
    public string? Comment { get; set; }
    public string? PostId { get; set; }
    public Post? Post { get; set; }
    public DateTime DateComment { get; set; }

    public Commentaries()
    {
        Id = Guid.NewGuid().ToString();
        DateComment = DateTime.Now;
    }
    
}