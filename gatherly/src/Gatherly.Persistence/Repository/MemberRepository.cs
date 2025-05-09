using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Repository;

internal sealed class MemberRepository(ApplicationDbContext dbContext) : IMemberRepository
{
    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await dbContext
            .Set<Member>()
            .FirstOrDefaultAsync(m => m.Id == id,
                cancellationToken: cancellationToken);

    public async Task AddAsync(Member member) => await dbContext.Set<Member>().AddAsync(member);

    public async Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Member>()
            .AnyAsync(m => m.Email.Value.ToLower() == email.ToLower(), cancellationToken);
    }
}