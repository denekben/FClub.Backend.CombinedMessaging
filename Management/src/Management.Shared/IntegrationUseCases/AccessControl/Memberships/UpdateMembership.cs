namespace Management.Shared.IntegrationUseCases.AccessControl.Memberships
{
    public sealed record UpdateMembership(
        Guid Id,
        Guid TariffId,
        DateTime ExpiresDate,
        Guid ClientId,
        Guid BranchId
    );
}