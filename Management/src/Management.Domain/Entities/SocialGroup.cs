using FClub.Backend.Common.Exceptions;

namespace Management.Domain.Entities
{
    public sealed class SocialGroup
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public List<Client> Clients { get; set; } = [];

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private SocialGroup() { }

        private SocialGroup(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatedDate = DateTime.UtcNow;
        }

        private SocialGroup(Guid id, string name)
        {
            Id = id;
            Name = name;
            CreatedDate = DateTime.UtcNow;
        }

        public static SocialGroup Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException($"Invalid argument for Service[name]. Entered value: {name}");

            return new(name);
        }

        public static SocialGroup Create(Guid id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException($"Invalid argument for Service[name]. Entered value: {name}");

            return new(id, name);
        }

        public void UpdateDetails(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException($"Invalid argument for Service[name]. Entered value: {name}");

            Name = name;
        }
    }
}
