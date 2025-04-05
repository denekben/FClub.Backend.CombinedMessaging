using FClub.Backend.Common.Exceptions;

namespace AccessControl.Domain.Entities
{
    public sealed class StatisticNote
    {
        public Guid Id { get; init; }
        public DateTime CreatedDate { get; init; }
        public uint EntrtiesQuantity { get; set; }

        private StatisticNote() { }

        private StatisticNote(uint entrtiesQuantity)
        {
            Id = Guid.NewGuid();
            var today = DateTime.Today;
            CreatedDate = new DateTime(today.Year, today.Month, today.Day, today.Hour, 0, 0);
            EntrtiesQuantity = entrtiesQuantity;
        }

        public static StatisticNote Create(uint entrtiesQuantity = 1)
        {
            if (entrtiesQuantity <= 0)
                throw new DomainException($"Invalid argument for StatisticNote[entrtiesQuantity]. Entered value: {entrtiesQuantity}");

            return new(entrtiesQuantity);
        }

        public void UpdateDetails(uint entrtiesQuantity = 1)
        {
            if (entrtiesQuantity <= 0)
                throw new DomainException($"Invalid argument for StatisticNote[entrtiesQuantity]. Entered value: {entrtiesQuantity}");

            EntrtiesQuantity += entrtiesQuantity;
        }
    }
}
