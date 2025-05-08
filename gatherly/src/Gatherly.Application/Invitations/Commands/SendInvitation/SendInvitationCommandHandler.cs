using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using MediatR;

namespace Gatherly.Application.Invitations.Commands.SendInvitation;

internal sealed class SendInvitationCommandHandler(
    IMemberRepository memberRepository,
    IGatheringRepository gatheringRepository,
    IInvitationRepository invitationRepository,
    IUnitOfWork unitOfWork,
    IEmailService emailService)
    : IRequestHandler<SendInvitationCommand>
{
    public async Task Handle(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        var member = await memberRepository
            .GetByIdAsync(request.MemberId, cancellationToken);

        var gathering = await gatheringRepository
            .GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);

        if (member is null || gathering is null)
        {
            return;
        }

        Result<Invitation> invitationResult = gathering.SendInvitation(member);

        if (invitationResult.IsFailure)
        {
            // Log error
            return;
        }

        invitationRepository.Add(invitationResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Send email
        await emailService.SendInvitationSentEmailAsync(
            member,
            gathering,
            cancellationToken);
    }
}