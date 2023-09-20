

using Newtonsoft.Json;

namespace ProjectBackend.Models ;

public partial class ImageData
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string?  Name {get; set;}

    [JsonProperty("link")]
    public string? Link { get; set; }

    [JsonProperty("file_path")]
    public string? FilePath { get; set; }

    [JsonProperty("image_type")]
    public long ImageType { get; set; }
}