using Microsoft.EntityFrameworkCore;

using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Application.Queries;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Queries.Handlers
{
    internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDto>
    {
        private readonly MySpotDbContext _context;

        public GetUserHandler(MySpotDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> HandleAsync(GetUser query)
        {
            var userId = new UserId(query.UserId);
            var user = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == userId);

            return user?.AsDto();
        }
    }
}
