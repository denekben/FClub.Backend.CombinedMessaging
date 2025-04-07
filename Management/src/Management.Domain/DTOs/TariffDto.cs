namespace Management.Domain.DTOs
{
    public sealed record TariffDto(
        Guid Id,
        string Name,
        Dictionary<int, int> PriceForNMonths,
        Dictionary<Guid, int>? DiscountForSocialGroup,
        bool AllowMultiBranches,
        DateTime CreatedDate,
        DateTime? UpdatedDate
    );
}
