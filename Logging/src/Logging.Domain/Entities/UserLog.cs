using FClub.Backend.Common.Exceptions;

namespace Logging.Domain.Entities
{
    public sealed class UserLog
    {
        private static readonly List<string> _allowedServiceNames = ["Management", "AccessControl", "Notifications"];

        public Guid Id { get; init; }
        public Guid AppUserId { get; set; }
        public string ServiceName { get; init; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        private UserLog() { }

        private UserLog(Guid id, Guid appUserId, string serviceName, string text, DateTime createdDate)
        {
            Id = id;
            AppUserId = appUserId;
            ServiceName = serviceName;
            Text = text;
            CreatedDate = createdDate;
        }

        public static UserLog Create(Guid id, Guid appUserId, string serviceName, string text, DateTime createdDate)
        {
            if (id == Guid.Empty)
                throw new DomainException($"Invalid argument for UserLog[id]. Entered value: {id}");
            if (appUserId == Guid.Empty)
                throw new DomainException($"Invalid argument for UserLog[appUserId]. Entered value: {appUserId}");
            if (!_allowedServiceNames.Any(sn => sn == serviceName))
                throw new DomainException($"Invalid argument for UserLog[serviceName]. Entered value: {serviceName}");
            if (string.IsNullOrWhiteSpace(text))
                throw new DomainException($"Invalid argument for UserLog[text]. Entered value: {text}");

            return new(id, appUserId, serviceName, text, createdDate);
        }
    }
}
