using Newtonsoft.Json;

public partial class StoryConfigData
{
    [JsonProperty("id")]
    public long Id {get; set;}

    [JsonProperty("name")]
    public string? Name {get; set;}

    [JsonProperty("category")]
    public string? Category { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("character")]
    public long[]? Character { get; set; }

    [JsonProperty("word_reference")]
    public long[]? WordReference { get; set; }

    [JsonProperty("story_main_content")]
    public StoryDataMainContent[]? StoryMainContent { get; set; }
}

public partial class StoryDataMainContent
{
    [JsonProperty("character")]
    public long Character { get; set; }

    [JsonProperty("behavior")]
    public string? Behavior { get; set; }

    [JsonProperty("chat")]
    public string? Chat { get; set; }

    [JsonProperty("word_id")]
    public long WordId { get; set; }

    [JsonProperty("gap_answer_correct")]
    public string? GapAnswerCorrect { get; set; }

    [JsonProperty("gap_answer_disturb")]
    public string[]? GapAnswerDisturb { get; set; }
}