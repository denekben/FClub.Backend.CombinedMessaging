namespace AccessControl.Domain.DTOs
{
    public sealed record StatisticNoteDto(
        DateTime CreatedDate,
        int EntriesQuantity
    );
}
