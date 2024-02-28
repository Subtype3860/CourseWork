namespace BlogBLL.ViewModels.Tag;

public class EditTagViewModel
{
    public List<BlogDAL.Models.Tag>? Tags { get; set; }
    public string? PostId { get; set; }
}