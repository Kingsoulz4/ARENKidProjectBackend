namespace ProjectBackend.Models;

public class AnimationData
{
    public long ID {get; set;}
    public string? Name {get; set;}

    public List<WordAssetData>? WordAssetDatas {get; set;}
}