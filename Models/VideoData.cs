
using Newtonsoft.Json;

namespace ProjectBackend.Models;

public partial class VideoData
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string? Name {get; set;}

    [JsonProperty("link")]
    public string? Link { get; set; }

    [JsonProperty("file_path")]
    public string? FilePath { get; set; }

    [JsonProperty("duration")]
    public int Duration { get; set; }

    [JsonProperty("video_type")]
    public int VideoType { get; set; }

    [JsonIgnore]
    public List<WordAssetData>? WordAssetDatas{get; set;}
}