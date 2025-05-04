namespace Management.Domain.DTOs
{
    public sealed record TariffWithGroupsDto(
        Guid Id,
        string Name,
        Dictionary<int, int> PriceForNMonths,
        List<DiscountForSocialGroupDto>? DiscountForSocialGroup,
        bool AllowMultiBranches,
        List<ServiceDto> Services,
        DateTime CreatedDate,
        DateTime? UpdatedDate
    );
}
