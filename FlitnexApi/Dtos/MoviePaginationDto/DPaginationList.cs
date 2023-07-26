namespace FlitnexApi.Dtos.MoviePaginationDto;

public class DPaginationList<T>
{
    public List<T>? Items { get; set; } = new();
    public DPagination Pagination { get; set; } = new();
}