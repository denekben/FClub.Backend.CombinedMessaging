using AccessControll.Domain.Entities;
using FClub.Backend.Common.Exceptions;

namespace AccessControl.Domain.Entities
{
    public sealed class StatisticNote
    {
        public Guid Id { get; init; }
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }
        public DateTime CreatedDate { get; init; }
        public int EntriesQuantity { get; set; }

        private StatisticNote() { }

        private StatisticNote(Guid branchId, int entriesQuantity)
        {
            Id = Guid.NewGuid();
            var now = DateTime.Now;
            CreatedDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            BranchId = branchId;
            EntriesQuantity = entriesQuantity;
        }

        public static StatisticNote Create(Guid branchId, int entriesQuantity = 1)
        {
            if (branchId == Guid.Empty)
                throw new DomainException($"Invalid argument for StatisticNote[branchId]. Entered value: {branchId}");
            if (entriesQuantity <= 0)
                throw new DomainException($"Invalid argument for StatisticNote[entriesQuantity]. Entered value: {entriesQuantity}");

            return new(branchId, entriesQuantity);
        }
    }
}
