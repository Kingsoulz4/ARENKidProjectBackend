using Newtonsoft.Json;

namespace ProjectBackend.Models;

public class SyncAudioData
{

    public long Id { get; set; }

    public long AudioDataDataID {get; set;}
    public virtual AudioData? AudioData {get; set;}

    [JsonProperty("e")]
    public long End { get; set; }

    [JsonProperty("s")]
    public long Start { get; set; }

    [JsonProperty("te")]
    public long Te { get; set; }

    [JsonProperty("ts")]
    public long Ts { get; set; }

    [JsonProperty("w")]
    public string? Word { get; set; }
}