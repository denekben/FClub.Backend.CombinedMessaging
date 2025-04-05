using FClub.Backend.Common.Exceptions;

namespace Management.Domain.Entities
{
    public sealed class StatisticNote
    {
        public Guid Id { get; init; }
        public DateTime CreatedDate { get; init; }
        public double MembershipCost { get; set; }
        public uint MembershipQuantity { get; set; }

        private StatisticNote() { }

        private StatisticNote(double membershipCost, uint membershipQuantity)
        {
            Id = Guid.NewGuid();
            var today = DateTime.Today;
            CreatedDate = new DateTime(today.Year, today.Month, today.Day, today.Hour, 0, 0);
            MembershipCost = membershipCost;
            MembershipQuantity = membershipQuantity;
        }

        public static StatisticNote Create(double membershipCost, OperationType type = OperationType.Insertion, uint membershipQuantity = 1)
        {
            if (membershipCost < 0 && type == OperationType.Insertion)
                throw new DomainException($"Invalid argument for StatisticNote[membershipCost]. Entered value: {membershipCost}");
            if (membershipQuantity < 1)
                throw new DomainException($"Invalid argument for StatisticNote[membershipQuantity]. Entered value: {membershipQuantity}");

            return new(membershipCost, membershipQuantity);
        }

        public void UpdateDetails(double membershipCost, OperationType type = OperationType.Insertion, uint membershipQuantity = 1)
        {
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
