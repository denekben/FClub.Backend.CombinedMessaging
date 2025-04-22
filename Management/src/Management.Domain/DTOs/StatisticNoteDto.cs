namespace Management.Domain.DTOs
{
    public sealed record StatisticNoteDto(
        DateTime CreatedDate,
        double MembershipCost,
        int MembershipQuantity
    );
}