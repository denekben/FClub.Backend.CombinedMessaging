using AccessControl.Domain.Entities;
using AccessControl.Domain.Entities.Pivots;
using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.ValueObjects;

namespace AccessControl.Domain.Entities
{
    public sealed class Branch
    {
        public Guid Id { get; init; }
        public string? Name { get; set; }
        public uint MaxOccupancy { get; set; }
        public uint CurrentClientQuantity { get; set; }
        public Address Address { get; set; }
        public List<Membership> Memberships { get; set; } = [];
        public List<ServiceBranch> ServiceBranches { get; set; } = [];
        public List<Turnstile> Turnstiles { get; set; } = [];
        public List<StatisticNote> StatisticNotes { get; set; } = [];

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private Branch() { }

        private Branch(Guid id, string? name, uint maxOccupancy, Address address)
        {
            Id = id;
            Name = name;
            MaxOccupancy = maxOccupancy;
            CurrentClientQuantity = 0;
            Address = address;
            CreatedDate = DateTime.UtcNow;
        }

        public static Branch Create(Guid id, string? name, uint maxOccupancy, string? country, string? city, string? street, string? houseNumber)
        {
            if (id == Guid.Empty)
                throw new DomainException($"Invalid value for Branch[id]. Entered value {id}");
            var address = Address.Create(country, city, street, houseNumber);

            return new(id, name, maxOccupancy, address);
        }

        public void UpdateDetails(string? name, uint maxOccupancy, string? country, string? city, string? street, string? houseNumber)
        {
            var address = Address.Create(country, city, street, houseNumber);

            Name = name;
            Address = address;
            MaxOccupancy = maxOccupancy;
        }

        public void Enter()
        {
            CurrentClientQuantity += 1;
        }

        public void Exit()
        {
            CurrentClientQuantity -= 1;
        }
    }
}
