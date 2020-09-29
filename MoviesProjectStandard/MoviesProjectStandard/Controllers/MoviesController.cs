using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MoviesProjectStandard.Controllers
{
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiController
    {
        private readonly MoviesDBEntities movieEntity;
        
        public MoviesController()
        {
            movieEntity = new MoviesDBEntities();
        }

        [HttpGet]
        [JwtAuthentication]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                // result = TokenValidator.TokenValidator.GetPrincipal(token);
                var movieList = await movieEntity.Movies.Where(x => x.IsDeleted == false).ToListAsync();
                return Ok(movieList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [JwtAuthentication]
        public async Task<IHttpActionResult> Post([FromBody] Movie moviesEntity)
        {
            try
            {
                if (moviesEntity == null)
                {
                    throw new Exception("You need to input Movie title, Movie description");
                }

                if ((moviesEntity.MovieTitle == null || moviesEntity.MovieTitle == string.Empty) ||
                    (moviesEntity.MovieDescription == null || moviesEntity.MovieDescription == string.Empty))
                {
                    throw new Exception("You need to input Movie title, Movie description");
                }

                var movie = await movieEntity.Movies.Where(x => x.MovieTitle == moviesEntity.MovieTitle && x.IsDeleted == false).FirstOrDefaultAsync();

                if (movie != null)
                {
                    throw new Exception("Movie title already exist and not deleted!");
                }

                movieEntity.Movies.Add(moviesEntity);
                movieEntity.SaveChanges();

                return Ok(new { message = "Data sucessfully saved!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [JwtAuthentication]
        public async Task<IHttpActionResult> Put(int id, [FromBody] Movie moviesEntity)
        {
            try
            {
                var movie = await movieEntity.Movies.Where(x => x.MovieId == id).FirstOrDefaultAsync();

                if (movie == null)
                {
                    throw new Exception("Movie Id did not exist");
                }

                movie.MovieTitle = moviesEntity.MovieTitle;
                movie.MovieDescription = moviesEntity.MovieDescription;
                movie.IsRented = moviesEntity.IsRented;
                movie.RentalDate = DateTime.Now;
                movieEntity.SaveChanges();
                return Ok(new { message = "Data sucessfully updated!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [JwtAuthentication]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var movie = await movieEntity.Movies.Where(x => x.MovieId == id).FirstOrDefaultAsync();

                if (movie == null)
                {
                    throw new Exception("Movie Id did not exist");
                }

                movie.IsDeleted = true;
                movieEntity.SaveChanges();
                return Ok(new { message = "Data sucessfully deleted!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
