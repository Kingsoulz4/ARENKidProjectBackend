using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ProjectBackend.Models;
public class StoryData
{
    [JsonProperty("id")]
    public long Id {get;  set;}

    [JsonProperty("name")]
    [Required]
    public string? Name {get; set;}

    [JsonProperty("thumb")]
    public long Thumb {get; set;}

    [JsonProperty("category")]
    public string? Category {get; set;}

    [JsonProperty("type")]
    public string? Type {get; set;}

    [JsonProperty("story_data_config_content")]
    public string? StoryDataConfigContent {get; set;}

    [JsonIgnore]
    public List<WordAssetData>? WordAssetDatas {get; set;} 
}