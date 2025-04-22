namespace Management.Shared.IntegrationUseCases.AccessControl.Memberships
{
    public sealed record CreateMembership(
        Guid Id,
        Guid? TariffId,
        DateTime ExpiresDate,
        Guid ClientId,
        Guid BranchId
    );
}