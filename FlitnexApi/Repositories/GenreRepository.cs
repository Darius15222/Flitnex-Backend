using FlitnexApi.Dtos;
using FlitnexApi.Dtos.MoviePaginationDto;
using FlitnexApi.Entities;
using FlitnexApi.Persistence;

namespace FlitnexApi.Repositories;

public class GenreRepository
{
    public EGenre? GetGenreById(int id)
    {
        using var context = new Context();
        {
            return context.Genre.SingleOrDefault(g => g.Id == id);
        }
    }
    
    public DPaginationList<EGenre> GetAllGenres(DPagination pagination)
    {
        using var context = new Context();
        {
            var totalCount = context.Genre.Count();
            var itemsToSkip = (pagination.Page - 1) * pagination.PerPage;

            var genres = context.Genre
                .Skip(itemsToSkip)
                .Take(pagination.PerPage)
                .ToList();
            
            var genrePagination = new DPaginationList<EGenre>
            {
                Items = genres,
                Pagination = new DPagination
                {
                    PerPage = pagination.PerPage,
                    Page = pagination.Page,
                    TotalCount = totalCount
                }
            };

            return genrePagination;
        }
    }
    
    public DPaginationList<EGenre> GetGenresByTerm(string term, DPagination pagination)
    {
        using var context = new Context();
        {
            var itemsToSkip = (pagination.Page - 1) * pagination.PerPage;

            var genresQuery = context.Genre
                .Where(g => g.Name == term);
            
            var totalCount = genresQuery.Count();
            
            var genres = genresQuery
                .Skip(itemsToSkip)
                .Take(pagination.PerPage)
                .ToList();

            var genrePagination = new DPaginationList<EGenre>
            {
                Items = genres,
                Pagination = new DPagination
                {
                    PerPage = pagination.PerPage,
                    Page = pagination.Page,
                    TotalCount = totalCount
                }
            };

            return genrePagination;
        }
    }

    public EGenre AddGenre(EGenre genre)
    {
        using var context = new Context();
        {
            context.Genre.Add(genre);
            context.SaveChanges();
        }
        return genre;
    }

    public EGenre UpdateGenre(EGenre updatedGenre)
    {
        using var context = new Context();
        {
            var toUpdate = context.Genre.FirstOrDefault(g => g.Id == updatedGenre.Id);
            
            context.Entry(toUpdate).CurrentValues.SetValues(updatedGenre);
            context.Update(toUpdate);
            context.SaveChanges();
            //context.genres.last => ultima entitate adaugata in tabelul de updatedGenre
            return updatedGenre;
        }
    }
    
    public EGenre DeleteGenre(int id)
    {
        using var context = new Context();
        {
            var genre = context.Genre.Find(id);
            context.Genre.Remove(genre);
            
            context.SaveChanges();

            return genre;
        }
    }
}