namespace FlitnexApi.Entities;

public class ECast : EBase
{
    public string Role { get; set; }
    public string Name { get; set; }
    public EMovie Movie { get; set; }
    public int MovieId { get; set; }
}