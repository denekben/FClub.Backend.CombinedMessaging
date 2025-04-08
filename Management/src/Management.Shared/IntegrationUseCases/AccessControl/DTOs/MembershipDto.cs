namespace Management.Shared.IntegrationUseCases.AccessControl.DTOs
{
    public sealed record MembershipDto(
        Guid Id,
        Guid TariffId,
        DateTime ExpiresDate,
        Guid ClientId,
        Guid BranchId
    );
}
