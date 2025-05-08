using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Gatherings.Commands.CreateGathering;

internal sealed class CreateGatheringCommandHandler(
    IMemberRepository memberRepository,
    IGatheringRepository gatheringRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateGatheringCommand>
{
    public async Task<Result> Handle(CreateGatheringCommand request, CancellationToken cancellationToken)
    {
        var member = await memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member is null)
        {
            return Result.Failure(new Error("Member.NotFound", "The member was not found."));
        }

        var gathering = Gathering.Create(
            Guid.NewGuid(),
            member,
            request.Type,
            request.ScheduledAtUtc,
            request.Name,
            request.Location,
            request.MaximumNumberOfAttendees,
            request.InvitationsValidBeforeInHours);

        gatheringRepository.Add(gathering);

        await unitOfWork.SaveChangesAsync(cancellationToken);

       return Result.Success();
    }
}