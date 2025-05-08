using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Invitations.Commands.AcceptInvitation;

internal sealed class AcceptInvitationCommandHandler(
    IGatheringRepository gatheringRepository,
    IAttendeeRepository attendeeRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AcceptInvitationCommand>
{
    public async Task<Result> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var gathering = await gatheringRepository
            .GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);

        if (gathering is null)
        {
            return Result.Failure(
                DomainErrors.Gathering.NotFound(request.GatheringId));
        }

        var invitation = gathering.Invitations
            .First(i => i.Id == request.InvitationId);

        if (invitation.Status != InvitationStatus.Pending)
        {
            return Result.Failure(
                DomainErrors.Invitation.AlreadyAccepted(invitation.Id));
        }

        var acceptInvitationResult = gathering.AcceptInvitation(invitation);

        if (acceptInvitationResult.IsSuccess)
        {
            attendeeRepository.Add(acceptInvitationResult.Value);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}