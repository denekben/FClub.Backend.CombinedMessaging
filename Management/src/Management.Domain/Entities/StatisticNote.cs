using FClub.Backend.Common.Exceptions;

namespace Management.Domain.Entities
{
    public sealed class StatisticNote
    {
        public Guid Id { get; init; }
        public DateTime CreatedDate { get; init; }
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }
        public double MembershipCost { get; set; }
        public int MembershipQuantity { get; set; }

        private StatisticNote() { }

        private StatisticNote(Guid branchId, double membershipCost, int membershipQuantity)
        {
            Id = Guid.NewGuid();
            BranchId = branchId;
            var now = DateTime.Now;
            CreatedDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            MembershipCost = membershipCost;
            MembershipQuantity = membershipQuantity;
        }

        public static StatisticNote Create(Guid branchId, double membershipCost, OperationType type = OperationType.Insertion, int membershipQuantity = 1)
        {
            if (branchId == Guid.Empty)
                throw new DomainException($"Invalid argument for StatisticNote[branchId]. Entered value: {branchId}");
            if (membershipCost < 0 && type == OperationType.Insertion)
                throw new DomainException($"Invalid argument for StatisticNote[membershipCost]. Entered value: {membershipCost}");

            return new(branchId, membershipCost, membershipQuantity);
        }

        public void UpdateDetails(Guid branchId, double membershipCost, OperationType type = OperationType.Insertion, int membershipQuantity = 1)
        {
            if (branchId == Guid.Empty)
                throw new DomainException($"Invalid argument for StatisticNote[branchId]. Entered value: {branchId}");
            if (membershipCost < 0 && type == OperationType.Insertion)
                throw new DomainException($"Invalid argument for StatisticNote[membershipCost]. Entered value: {membershipCost}");
            if (membershipQuantity < 1)
                throw new DomainException($"Invalid argument for StatisticNote[membershipQuantity]. Entered value: {membershipQuantity}");

            MembershipCost += membershipCost;
            MembershipQuantity += membershipQuantity;
        }


        public enum OperationType
        {
            Insertion,
            Deletion
        }
    }
}
