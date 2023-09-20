using Newtonsoft.Json;
using System.Collections.Generic;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);




public partial class MKWordData
{
    [JsonProperty("word_id")]
    public long WordId { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("name_display")]
    public string NameDisplay { get; set; }

    [JsonProperty("path_word")]
    public string PathWord { get; set; }

    [JsonProperty("sentence_type")]
    public long SentenceType { get; set; }

    [JsonProperty("image")]
    public List<ImageMKData> Image { get; set; }

    [JsonProperty("video")]
    public List<VideoMKData> Video { get; set; }

    [JsonProperty("audio")]
    public List<AudioMKData> Audio { get; set; }

    [JsonProperty("audio_effect")]
    public List<AudioMKData> AudioEffect { get; set; }

    [JsonProperty("gaf")]
    public object[] Gaf { get; set; }

    [JsonProperty("spine")]
    public object[] Spine { get; set; }

    [JsonProperty("animation")]
    public object[] Animation { get; set; }

    [JsonProperty("color")]
    public ColorMKData Color { get; set; }

    [JsonProperty("filter_word")]
    public object[] FilterWord { get; set; }

    [JsonProperty("phonic")]
    public Phonic[] Phonic { get; set; }

    [JsonProperty("list_not_game")]
    public object[] ListNotGame { get; set; }
}

public partial class AudioMKData
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("link")]
    public string Link { get; set; }

    [JsonProperty("file_path")]
    public string FilePath { get; set; }

    [JsonProperty("duration")]
    public long Duration { get; set; }

    [JsonProperty("voices_id")]
    public long VoicesId { get; set; }

    [JsonProperty("sync_data")]
    public object[] SyncData { get; set; }
}

public partial class ColorMKData
{
    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("highlight")]
    public string Highlight { get; set; }
}

public partial class ImageMKData
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("link")]
    public string Link { get; set; }

    [JsonProperty("file_path")]
    public string FilePath { get; set; }

    [JsonProperty("images_categories_id")]
    public long ImagesCategoriesId { get; set; }

    [JsonProperty("file_type")]
    public string FileType { get; set; }
}

public partial class Phonic
{
    [JsonProperty("audio")]
    public long[] Audio { get; set; }
}

public partial class VideoMKData
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("link")]
    public string Link { get; set; }

    [JsonProperty("file_path")]
    public string FilePath { get; set; }

    [JsonProperty("duration")]
    public long Duration { get; set; }

    [JsonProperty("white_background")]
    public long WhiteBackground { get; set; }

    [JsonProperty("video_categories_id")]
    public long VideoCategoriesId { get; set; }
}





