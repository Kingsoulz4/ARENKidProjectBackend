using System.ComponentModel.DataAnnotations;

namespace ProjectBackend.Models;

public class WordAssets
{
    public int Id { get; set; }

    [Required]
    public string? Text { get; set; }

    //[Required]
    public string? LinkDownLoad { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? DateCreated { get; set; }

}