using Microsoft.EntityFrameworkCore;

using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Application.Queries;

namespace MySpot.Infrastructure.DAL.Queries.Handlers
{
    internal sealed class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
    {
        private readonly MySpotDbContext _context;

        public GetUsersHandler(MySpotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
            => await _context.Users
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
    }
}
