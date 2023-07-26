namespace FlitnexApi.Entities;

public class EGenre : EBase
{
    public string Name { get; set; }
    public List<EMovie> Movies { get; set; }

}