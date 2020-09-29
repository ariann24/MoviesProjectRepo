using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProjectCore.Entities;
using MovieProjectCore.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieProjectCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MoviesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try 
            {
                IEnumerable<MoviesEntity> movieList = await _unitOfWork.Movies.GetAllAsync(x => x.IsDeleted == false);
                return Ok(movieList);
            }
            catch (Exception ex)
            {
                return StatusCode(501, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MoviesEntity moviesEntity)
        {
            try
            {
                if (moviesEntity == null)
                {
                    return StatusCode(400, new { message = "You need to input Movie title, Movie description" });
                }

                if ((moviesEntity.MovieTitle == null || moviesEntity.MovieTitle == string.Empty) ||
                    (moviesEntity.MovieDescription == null || moviesEntity.MovieDescription == string.Empty))
                {
                    return StatusCode(400, new { message = "You need to input Movie title, Movie description" });
                }

                MoviesEntity movieList = await _unitOfWork.Movies.GetAsync(x => x.MovieTitle == moviesEntity.MovieTitle && x.IsDeleted == false);

                if (movieList != null)
                {
                    return StatusCode(409, new { message = "Movie title already exist and not deleted!" });
                }

                await _unitOfWork.Movies.AddAsync(moviesEntity);
                _unitOfWork.CompleteUOW();

                return Ok(new { message = "Data sucessfully saved!" });
            }
            catch (Exception ex)
            {
                return StatusCode(501, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MoviesEntity moviesEntity)
        {
            try
            {
                MoviesEntity movieList = await _unitOfWork.Movies.GetAsync(x => x.MovieId == id);

                if (movieList == null)
                {
                    return StatusCode(501, new { message = "Movie Id did not exist" });
                }

                movieList.MovieTitle = moviesEntity.MovieTitle;
                movieList.MovieDescription = moviesEntity.MovieDescription;
                movieList.IsRented = moviesEntity.IsRented;
                movieList.RentalDate = DateTime.Now;
                _unitOfWork.Movies.Update(movieList);
                _unitOfWork.CompleteUOW();

                return Ok(new { message = "Data sucessfully updated!" });
            }
            catch (Exception ex)
            {
                return StatusCode(501, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                MoviesEntity movieList = await _unitOfWork.Movies.GetAsync(x => x.MovieId == id);

                if (movieList == null)
                {
                    return StatusCode(501, new { message = "Movie Id did not exist" });
                }

                movieList.IsDeleted = true;
                _unitOfWork.Movies.Update(movieList);
                _unitOfWork.CompleteUOW();

                return Ok(new { message = "Data sucessfully deleted!" });
            }
            catch (Exception ex)
            {
                return StatusCode(501, new { message = ex.Message });
            }
        }
    }
}
