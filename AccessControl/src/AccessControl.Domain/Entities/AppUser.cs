using FClub.Backend.Common.Exceptions;

namespace AccessControl.Domain.Entities
{
    public sealed class AppUser
    {
        public Guid Id { get; init; }
        public bool IsBlocked { get; set; }

        private AppUser() { }

        private AppUser(Guid id, bool isBlocked)
        {
            Id = id;
            IsBlocked = isBlocked;
        }

        public static AppUser Create(Guid id, bool isBlocked)
        {
            if (id == Guid.Empty)
                throw new DomainException($"Invalid value for AppUser[id]. Entered value {id}");
            return new(id, isBlocked);
        }
    }
}
