using Gatherly.Domain.Repositories;

namespace Gatherly.Persistence;

internal sealed class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await dbContext.SaveChangesAsync(cancellationToken);
}