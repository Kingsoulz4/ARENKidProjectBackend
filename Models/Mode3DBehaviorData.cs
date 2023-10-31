
using Newtonsoft.Json;

namespace ProjectBackend.Models;

public class Mode3DBehaviorData 
{
    public long Id { get; set; }

    [JsonIgnore]
    public long Model3DDataID {get; set;}
    [JsonIgnore]
    public virtual Model3DData? Model3DData {get; set;}

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("thumb")]
    public long Thumb { get; set; }

    [JsonProperty("audio")]
    public long Audio { get; set; }

    [JsonProperty("word_id")]
    public long WordAssetID {get; set;}

    [JsonProperty("action_type")]
    public long ActionType { get; set; }

}