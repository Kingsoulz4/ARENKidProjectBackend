namespace ProjectBackend.Models;

public class GameData
{
    public long ID {get; set;}
    public string? Name {get; set;}

    public List<long>? WordTeaching {get; set;}

    public List<long>? WordDisturbing {get; set;}
}