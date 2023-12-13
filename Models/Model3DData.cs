
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace ProjectBackend.Models ;

public class Model3DData
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("link_download")]
    public string? LinkDownload { get; set; }

    [JsonProperty("location")]
    public string? Location {get; set;}

    [JsonProperty("name_asset")]
    public string? NameAsset {get; set;}

    [JsonProperty("file_type")]
    public string? FileType { get; set; }

    [JsonProperty("scale_factor")]
    public float ScaleFactor {get; set;}

    [JsonProperty("behavior")]
    public List<Mode3DBehaviorData>? Behavior { get; set; }

    [JsonProperty("word_description")]
    public long WordDescription {get; set;}

    [JsonIgnore]
    public ICollection<WordAssetData>? WordAssetDatas {get; set;}


    
}