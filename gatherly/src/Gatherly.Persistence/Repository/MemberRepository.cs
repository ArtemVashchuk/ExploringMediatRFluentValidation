using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Repository;

internal sealed class MemberRepository(ApplicationDbContext dbContext) : IMemberRepository
{
    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var members = await dbContext
            .Set<Member>()
            .FirstOrDefaultAsync(m => m.Id == id,
                cancellationToken: cancellationToken);
        
        return members;
    }

    public async Task Add(Member member)
    {
         await dbContext.Set<Member>().AddAsync(member);
    }
}