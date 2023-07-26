namespace FlitnexApi.Dtos.MovieDto;

public class DMovie
{ 
    public int Id { get; set; }
    public string? Description { get; set; }
    public string Title { get; set; }
    public List<DGenre> Genres { get; set; }
    public List<DCast>? Cast { get; set; }
    public string? VideoSourceUrl { get; set; }
    public string? ImageUrl { get; set; }
}