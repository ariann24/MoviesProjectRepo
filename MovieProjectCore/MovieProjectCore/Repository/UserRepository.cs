using MovieProjectCore.Entities;
using MovieProjectCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProjectCore.Repository
{
    internal sealed class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        internal UserRepository(MovieDBContext context)
           : base(context)
        {
        }

        private MovieDBContext MovieDBContext
        {
            get { return _context as MovieDBContext; }
        }
    }
}
