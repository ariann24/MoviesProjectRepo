using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProjectCore.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository Movies { get; }

        IUserRepository Users { get;}

        int CompleteUOW();
    }
}
