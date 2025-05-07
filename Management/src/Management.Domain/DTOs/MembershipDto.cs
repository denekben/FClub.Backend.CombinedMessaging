namespace Management.Domain.DTOs
{
    public sealed record MembershipDto(
        Guid Id,
        double TotalCost,
        int MonthQuantity,
        Guid BranchId,
        TariffDto Tariff,
        DateTime ExpiresDate,
        DateTime CreatedDate,
        DateTime? UpdatedDate
    );
}