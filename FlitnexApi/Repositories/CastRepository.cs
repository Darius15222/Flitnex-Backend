using FlitnexApi.Dtos;
using FlitnexApi.Dtos.MoviePaginationDto;
using FlitnexApi.Entities;
using FlitnexApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlitnexApi.Repositories;

public class CastRepository
{
    
    public ECast? GetCastById(int movieId, int id)
    {
        using var context = new Context();
        {
            var movie = context.Movie.Include(m => m.Cast)
                .SingleOrDefault(m => m.Id == movieId);

            var cast = movie.Cast.FirstOrDefault(x => x.Id == id);

            return cast;
        }
    }
    
    public DPaginationList<ECast> GetAllCasts(int movieId, DPagination pagination)
    {
        using var context = new Context();
        {
            var totalCount = context.Cast.Count();
            var itemsToSkip = (pagination.Page - 1) * pagination.PerPage;

            var movie = context.Movie.Include(m => m.Cast)
                .SingleOrDefault(m => m.Id == movieId);

            var casts = movie.Cast
                .Skip(itemsToSkip)
                .Take(pagination.PerPage)
                .ToList();
            
            var castPagination = new DPaginationList<ECast>
            {
                Items = casts,
                Pagination = new DPagination
                {
                    PerPage = pagination.PerPage,
                    Page = pagination.Page,
                    TotalCount = totalCount
                }
            };
    
            return castPagination;
        }
    }
    
    public DPaginationList<ECast> GetCastsByTerm(string term, int movieId, DPagination pagination)
    {
        using var context = new Context();
        {
            var itemsToSkip = (pagination.Page - 1) * pagination.PerPage;

            var movie = context.Movie.Include(m => m.Cast)
                .SingleOrDefault(m => m.Id == movieId);
            
            var castsQuery = movie.Cast
                .Where(c => c.Name == term);
            
            var totalCount = castsQuery.Count();
            
            var casts = castsQuery
                .Skip(itemsToSkip)
                .Take(pagination.PerPage)
                .ToList();

            var genrePagination = new DPaginationList<ECast>
            {
                Items = casts,
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
    
    public ECast AddCast(int movieId, ECast cast)
    {
        using var context = new Context();
        {
            cast.MovieId = movieId;
            context.Entry(cast).State = EntityState.Unchanged;

            context.Cast.Add(cast);
            context.SaveChanges();

        }
        return context.Cast.FirstOrDefault(x => x.MovieId == movieId);
    }
    
    public ECast UpdateCast(int movieId, ECast updatedCast)
    {
        
        // var movie = context.Movie.Include(m => m.Cast)
        //     .SingleOrDefault(m => m.Id == movieId);
        //
        // var toUpdate = movie.Cast.FirstOrDefault(g => g.Id == updatedCast.Id);
        
        
        using var context = new Context();
        {
            var toUpdate = context.Cast
                .FirstOrDefault(p => p.Id == updatedCast.Id && p.MovieId == movieId);
            
            context.Entry(toUpdate).CurrentValues.SetValues(updatedCast);
            
            toUpdate.Movie = new EMovie()
            {
                Id = movieId,
            };
            
            context.Entry(toUpdate.Movie).State = EntityState.Unchanged;
            
            // toUpdate.Name = updatedCast.Name;
            // toUpdate.Role = updatedCast.Role;
            //
            // toUpdate.MovieId = movieId;
            
            context.SaveChanges();
            //context.Casts.last => ultima entitate adaugata in tabelul de updatedCast
            return updatedCast;
        }
    }
    
    public ECast DeleteCast(int movieId, int id)
    {
        using var context = new Context();
        {
            var deleteCast = GetCastById(movieId, id);

            context.Cast.Remove(deleteCast);

            context.SaveChanges();

            return deleteCast;
        }
    }
}