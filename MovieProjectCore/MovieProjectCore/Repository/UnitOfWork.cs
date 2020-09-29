using MovieProjectCore.Entities;
using MovieProjectCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProjectCore.Repository
{
    public sealed class UnitOfWork : IUnitOfWork
    {

        private readonly MovieDBContext _context;

        public UnitOfWork(MovieDBContext context)
        {
            _context = context;

            Movies = new MovieRepository(_context);
            Users = new UserRepository(_context);
        }

        public IMovieRepository Movies { get; private set; }

        public IUserRepository Users { get; private set; }

        public int CompleteUOW()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
