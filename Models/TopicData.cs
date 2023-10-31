
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ProjectBackend.Models;

public class TopicData 
{
    [JsonProperty("id")]
    public required string Id {get; set;}

    [JsonProperty("name")]
    [Required]
    public required string Name {get; set;}

    [JsonProperty("thumb")]
    public string? Thumb {get; set;}

    [JsonIgnore]
    public List<WordAssetData>? WordAssetDatas {get; set;}
}