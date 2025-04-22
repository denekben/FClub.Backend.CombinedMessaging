using FClub.Backend.Common.ValueObjects;
using Management.Domain.Entities.Pivots;

namespace Management.Domain.Entities
{
    public sealed class Branch
    {
        public Guid Id { get; init; }
        public string? Name { get; set; }
        public uint MaxOccupancy { get; set; }
        public Address Address { get; set; }
        public List<ServiceBranch> ServiceBranches { get; set; } = [];
        public List<Membership> Memberships { get; set; } = [];
        public List<StatisticNote> StatisticNotes { get; set; } = [];

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private Branch() { }

        private Branch(string? name, uint maxOccupancy, Address address)
        {
            Id = Guid.NewGuid();
            Name = name;
            MaxOccupancy = maxOccupancy;
            Address = address;
            CreatedDate = DateTime.UtcNow;
        }

        private Branch(Guid id, string? name, uint maxOccupancy, Address address)
        {
            Id = id;
            Name = name;
            MaxOccupancy = maxOccupancy;
            Address = address;
            CreatedDate = DateTime.UtcNow;
        }

        public static Branch Create(string? name, uint maxOccupancy, string? country, string? city, string? street, string? houseNumber)
        {
            var address = Address.Create(country, city, street, houseNumber);

            return new(name, maxOccupancy, address);
        }

        public static Branch Create(Guid id, string? name, uint maxOccupancy, string? country, string? city, string? street, string? houseNumber)
        {
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
    }
}
