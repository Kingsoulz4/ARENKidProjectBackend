using Newtonsoft.Json;

namespace ProjectBackend.Models;

public class AudioData
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("file_path")]
    public string FilePath { get; set; }

    [JsonProperty("duration")]
    public long Duration { get; set; }

    [JsonProperty("audio_type")]
    public long AudioType { get; set; }

    [JsonProperty("sync_data")]
    public List<SyncAudioData> SyncData { get; set; }


}
