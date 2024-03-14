using System.ComponentModel.DataAnnotations;

namespace BlogBLL.ViewModels.Tag;

public class EditTagViewModel
{
    public string? Id { get; set; }
    [Display(Name = "Наименование тега")]
    public string? Stick { get; set; }
}