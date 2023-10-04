using System.ComponentModel.DataAnnotations;

namespace ProjectBackend.Models;

public class WordAssets
{
    public long Id { get; set; }

    [Required]
    public string? Text { get; set; }

    public string? LinkDownLoad { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? DateCreated { get; set; }

}