using FlitnexApi.Dtos;
using FlitnexApi.Dtos.MoviePaginationDto;
using FlitnexApi.Entities;
using FlitnexApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlitnexApi.Repositories;

public class MovieRepository
{
    // private const string PathToMovie = @"C:\Users\Darius\Desktop\Practica\Flitnex\Backend\FlitnexApi\JsonsMovie.json";
    // private const string PathToMoviePagination = @"C:\Users\Darius\Desktop\Practica\Flitnex\Backend\FlitnexApi\Jsons\MoviePagination.json";
    // private const string PathToMovies = @"C:\Users\Darius\Desktop\Practica\Flitnex\Backend\FlitnexApi\Jsons\Movies.json";
    //
    // public DMovie? GetMovieById(int id)
    // {
    //     var jsonContent = File.ReadAllText(PathToMovies);
    //
    //     var movies = JsonConvert.DeserializeObject<List<DMovie>>(jsonContent);
    //     
    //     var movie = movies?.FirstOrDefault(m => m.Id == id);
    //
    //     return movie;
    // }
    //
    //
    // public DPaginationList<DMovie> GetAllMovies(string term, DPagination pagination)
    // {
    //     var jsonContent = File.ReadAllText(PathToMovies);
    //     var movies = JsonConvert.DeserializeObject<List<DMovie>>(jsonContent);
    //
    //     var filteredMovies = movies?.Where(m => m.Title.Contains(term)).ToList();
    //
    //     var itemsToSkip = (pagination.Page - 1) * pagination.PerPage;
    //
    //     var pagedMovies = filteredMovies?.Skip(itemsToSkip).Take(pagination.PerPage).ToList();
    //
    //     var moviePagination = new DPaginationList<DMovie>
    //     {
    //         Items = pagedMovies,
    //         Pagination = new DPagination
    //         {
    //             PerPage = pagination.PerPage,
    //             Page = pagination.Page,
    //             // TotalCount = filteredMovies?.Count ?? 0
    //         }
    //     };
    //
    //     return moviePagination;
    // }

    public EMovie? GetMovieById(int id)
    {
        using var context = new Context();
        {
            return context.Movie.Include(x => x.Genres)
                .Include(c => c.Cast)
                .SingleOrDefault(g => g.Id == id);
        }
    }
    
    public DPaginationList<EMovie> GetAllMovies(DPagination pagination)
    {
        using var context = new Context();
        {
            var totalCount = context.Movie.Count();
            var itemsToSkip = (pagination.Page - 1) * pagination.PerPage;

            var movies = context.Movie
                .Skip(itemsToSkip)
                .Take(pagination.PerPage)
                .ToList();

            var moviePagination = new DPaginationList<EMovie>
            {
                Items = movies,
                Pagination = new DPagination
                {
                    PerPage = pagination.PerPage,
                    Page = pagination.Page,
                    TotalCount = totalCount
                }
            };

            return moviePagination;
        }
    }
    
    public DPaginationList<EMovie> GetMoviesByTerm(string term, DPagination pagination)
    {
        using var context = new Context();
        {
            var itemsToSkip = (pagination.Page - 1) * pagination.PerPage;

            var moviesQuery = context.Movie
                .Where(m => m.Title == term);
            
            var totalCount = moviesQuery.Count();
            
            var movies = moviesQuery
                .Skip(itemsToSkip)
                .Take(pagination.PerPage)
                .ToList();

            var genrePagination = new DPaginationList<EMovie>
            {
                Items = movies,
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

    public EMovie AddMovie(EMovie movie)
    {
        using var context = new Context();
        {
            context.Movie.Add(movie);
            movie.Genres.ForEach(g => context.Entry(g).State = EntityState.Unchanged);
            context.SaveChanges();
        }
        return GetMovieById(movie.Id);
    }

    public EMovie UpdateMovie(EMovie updatedMovie)
    {
        // var toUpdate = context1.Movie.Include(g => g.Genres)
        //     .FirstOrDefault(g => g.Id == updatedMovie.Id);
        var toUpdate = GetMovieById(updatedMovie.Id);
        
        using (var context1 = new Context())
        {
            context1.Attach(toUpdate);
            context1.Entry(toUpdate).CurrentValues.SetValues(updatedMovie);
            toUpdate.Genres.Clear();

            context1.SaveChanges();
        }


        using (var context2 = new Context())
        {
            context2.Attach(toUpdate);
            updatedMovie.Genres.ForEach(g =>
            {
                toUpdate.Genres.Add(g);
                context2.Entry(g).State = EntityState.Unchanged;
            });

            context2.SaveChanges();
        }

        return GetMovieById(updatedMovie.Id);
    }

    public EMovie DeleteMovie(int id)
    {
        using var context = new Context();
        {
            var movie = GetMovieById(id);
            
            movie.Genres.Clear();
            movie.Cast.Clear();
            
            context.Movie.Remove(movie);

            context.SaveChanges();

            return movie;
        }
    }
}