using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Enums;

namespace Gatherly.Application.Gatherings.Commands.CreateGathering;

public sealed record CreateGatheringCommand(
    Guid MemberId,
    GatheringType Type,
    DateTime ScheduledAtUtc,
    string Name,
    string? Location,
    int? MaximumNumberOfAttendees,
    int? InvitationsValidBeforeInHours) : ICommand;