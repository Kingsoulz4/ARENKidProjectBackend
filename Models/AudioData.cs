using Newtonsoft.Json;

namespace ProjectBackend.Models;

public class AudioData
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string? Name {get; set;}

    [JsonProperty("file_path")]
    public string? FilePath { get; set; }

    [JsonProperty("duration")]
    public long Duration { get; set; }

    [JsonProperty("audio_type")]
    public long AudioType { get; set; }

    [JsonProperty("sync_data")]
    public List<SyncAudioData>? SyncData { get; set; }

    [JsonIgnore]
    public List<WordAssetData>? WordAssetDatas {get; set;}


}
