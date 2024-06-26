using Newtonsoft.Json;

namespace ProjectBackend.Models;

public class WordAssetData
{
    [JsonProperty("word_id")]
    public long ID { get; set; }

    [JsonProperty("text")]
    public string? Text { get; set; }

    [JsonProperty("level_age")]
    public int LevelAge {get; set;}

    [JsonProperty("topic")]
    public long TopicDataID {get; set;}
    [JsonIgnore]
    public virtual TopicData? TopicData {get; set;}

    [JsonProperty("path_asset")]
    public string? PathAsset { get; set; }

    [JsonProperty("sentence_type")]
    public long SentenceType { get; set; }

    [JsonProperty("image")]
    public List<ImageData>? Images { get; set; }

    [JsonProperty("video")]
    public List<VideoData>? Videos { get; set; }

    [JsonProperty("audio")]
    public List<AudioData>? Audios { get; set; }

    [JsonProperty("animations")]
    public List<AnimationData>? Animations { get; set; }

    [JsonProperty("model_3d")]
    public List<Model3DData>? Model3Ds { get; set; }

    [JsonProperty("filter_word")]
    public List<WordAssetData>? FilterWords { get; set; }

    [JsonProperty("games")]
    public List<GameLessonData>? Games { get; set; }

    [JsonProperty("stories")]
    public List<StoryData>? Stories {get; set;}

    [JsonProperty("version")]
    public long Version {get; set;}
}