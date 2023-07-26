namespace FlitnexApi.Dtos.MovieDto;

public class DMovieUpdateParams
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string Title { get; }
    public List<int> GenreIds { get; } = new();
    public string? VideoSourceUrl { get; set; }
    public string? ImageUrl { get; set; }
}