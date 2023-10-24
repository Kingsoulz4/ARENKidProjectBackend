
using System.ComponentModel.DataAnnotations;

namespace ProjectBackend.Models;

public class TopicData 
{
    public required string Id {get; set;}

    [Required]
    public required string Name {get; set;}

    public string? ThumbPath {get; set;}

    public List<WordAssetData>? WordAssetDatas {get; set;}
}