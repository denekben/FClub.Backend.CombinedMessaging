namespace Management.Shared.IntegrationUseCases.Notifications.Branches
{
    public sealed record CreateBranch(
        string? Name,
        string? Country,
        string? City,
        string? Street,
        string? HouseNumber,
        List<string> serviceNames
    );
}
