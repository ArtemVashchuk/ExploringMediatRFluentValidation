using Gatherly.Domain.Shared;
using MediatR;

namespace Gatherly.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;