
using Newtonsoft.Json;

namespace ProjectBackend.Models;

public class GameLessonData
{
    [JsonProperty("Id")]
    public long Id {get; set;}

    [JsonProperty("game_id")]
    public long GameDataID {get; set;}

    [JsonIgnore]
    public virtual GameData? GameData {get; set;}

    [JsonIgnore]
    public long WordAssetDataID {get; set;}

    [JsonIgnore]
    public virtual WordAssetData? WordAssetData {get; set;}

    // 1,2,5
    [JsonProperty("word_teaching")]
    public string? WordTeaching {get; set;}

    // 1,2,4
    [JsonProperty("word_disturbing")]
    public string? WordDisturbing {get; set;}

    [JsonProperty("game_config_json")]
    public string? GameConfigJson {get; set;}

}