namespace FlitnexApi.Entities;

public class EMovie : EBase
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public List<EGenre> Genres { get; set; }
    public List<ECast>? Cast { get; set; }
    public string? VideoSourceUrl { get; set; }
    public string? ImageUrl { get; set; }
    
}