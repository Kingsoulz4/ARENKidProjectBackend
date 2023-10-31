using Newtonsoft.Json;

namespace ProjectBackend.Models;

public class GameData
{
    [JsonProperty("id")]
    public long ID { get; set; }
    [JsonProperty("name")]
    public string? Name { get; set; }
    [JsonProperty("type")]
    public string? Type { get; set; }
    [JsonProperty("thumb")]
    public long Thumb { get; set; }

}