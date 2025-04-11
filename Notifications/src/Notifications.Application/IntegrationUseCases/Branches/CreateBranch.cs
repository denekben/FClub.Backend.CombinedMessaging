using MediatR;

namespace Notifications.Application.IntegrationUseCases.Branches
{
    public sealed record CreateBranch(
        string? Name,
        string? Country,
        string? City,
        string? Street,
        string? HouseNumber,
        List<string> ServiceNames
    ) : IRequest;
}
