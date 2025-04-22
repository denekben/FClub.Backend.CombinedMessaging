using AccessControl.Domain.Enums;
using FClub.Backend.Common.Exceptions;

namespace AccessControl.Domain.Entities
{
    public sealed class EntryLog
    {
        public Guid Id { get; init; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public Guid TurnstileId { get; set; }
        public Turnstile Turnstile { get; set; }
        public string ClientFullName { get; set; }
        public string? BranchName { get; set; }
        public string? ServiceName { get; set; }
        public EntryType EntryType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        private EntryLog() { }

        private EntryLog(Guid clientId, Guid turnstileId, string clientFullName, string? branchName, string? serviceName, EntryType entryType)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            TurnstileId = turnstileId;
            ClientFullName = clientFullName;
            BranchName = branchName;
            ServiceName = serviceName;
            EntryType = entryType;
            CreatedDate = DateTime.UtcNow;
        }

        public static EntryLog Create(Guid clientId, Guid turnstileId, string clientFullName, string? branchName, string? serviceName, EntryType entryType)
        {
            if (turnstileId == Guid.Empty)
                throw new DomainException($"Invalid argument for EntryLog[turnstileId]. Entered value: {turnstileId}");
            if (clientId == Guid.Empty)
                throw new DomainException($"Invalid argument for EntryLog[clientId]. Entered value: {clientId}");

            return new(clientId, turnstileId, clientFullName, branchName, serviceName, entryType);
        }
    }
}
