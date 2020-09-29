using MovieProjectCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProjectCore.Interface
{
    public interface IMovieRepository : IBaseRepository<MoviesEntity>
    {
    }
}
