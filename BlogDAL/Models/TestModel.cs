using System.ComponentModel.DataAnnotations;

namespace BlogDAL.Models;

public class TestModel
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
}