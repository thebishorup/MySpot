using Microsoft.EntityFrameworkCore;

using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repositories
{
    internal sealed class PostgresUserRepository : IUserRepository
    {
        private readonly MySpotDbContext _context;

        public PostgresUserRepository(MySpotDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
            => await _context.AddAsync(user);

        public Task<User> GetByEmailAsync(string email)
            => _context.Users.SingleOrDefaultAsync(x => x.Email == email);

        public Task<User> GetByIdAsync(UserId id)
            => _context.Users.SingleOrDefaultAsync(x => x.Id == id);

        public Task<User> GetByUserNameAsync(string userName)
            => _context.Users.SingleOrDefaultAsync(x => x.UserName == userName);
    }
}
