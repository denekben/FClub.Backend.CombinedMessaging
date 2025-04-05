using FClub.Backend.Common.Exceptions;

namespace AccessControl.Domain.Entities
{
    public sealed class UserLog
    {
        private const string _serviceName = "AccessControl";

        public Guid Id { get; init; }
        public Guid AppUserId { get; set; }
        public string ServiceName { get; init; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        private UserLog() { }

        private UserLog(Guid appUserId, string text)
        {
            Id = Guid.NewGuid();
            AppUserId = appUserId;
            ServiceName = _serviceName;
            Text = text;
            CreatedDate = DateTime.UtcNow;
        }

        public static UserLog Create(Guid appUserId, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new DomainException($"Invalid argument for UserLog[text]. Entered value: {text}");

            return new(appUserId, text);
        }
    }
}
