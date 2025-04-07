namespace Management.Domain.DTOs
{
    public sealed record StatisticNoteDto(
        Guid Id,
        DateTime CreatedDate,
        double MembershipCost,
        uint MembershipQuantity
    );
}