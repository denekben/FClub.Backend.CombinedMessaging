namespace Management.Domain.DTOs
{
    public sealed record DiscountForSocialGroupDto(
        SocialGroupDto SocialGroupDto,
        int DicsountValue
    );
}
