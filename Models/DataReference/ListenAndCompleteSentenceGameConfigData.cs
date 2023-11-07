using Newtonsoft.Json;

public partial class ListenAndCompleteSentenceGameData
{
    [JsonProperty("game_id")]
    public long GameId { get; set; }

    [JsonProperty("word_reference")]
    public List<long> WordReference { get; set; }

    [JsonProperty("turn")]
    public List<ListenAndCompleteSentenceGameTurnData> Turn { get; set; }
}

public partial class ListenAndCompleteSentenceGameTurnData
{
    [JsonProperty("word_id")]
    public long WordId { get; set; }

    [JsonProperty("sentence_with_gaps")]
    public string SentenceWithGaps { get; set; }

    [JsonProperty("list_answer")]
    public List<long> ListAnswer { get; set; }
}