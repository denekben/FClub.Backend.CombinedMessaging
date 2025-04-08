namespace Management.Domain.DTOs
{
    public sealed record TariffDto(
        Guid Id,
        string Name,
        Dictionary<int, int> PriceForNMonths,
        Dictionary<Guid, int>? DiscountForSocialGroup,
        bool AllowMultiBranches,
        List<ServiceDto> Services,
        DateTime CreatedDate,
        DateTime? UpdatedDate
    );
}
