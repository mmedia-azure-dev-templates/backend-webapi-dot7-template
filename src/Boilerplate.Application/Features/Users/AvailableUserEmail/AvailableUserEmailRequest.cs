using MediatR;

namespace Boilerplate.Application.Features.Users.AvailableUserEmail;
public record AvailableUserEmailRequest(string EmailAddress) : IRequest<AvailableUserEmailResponse>;