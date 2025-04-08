using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.ValueObjects;
using System.Text.RegularExpressions;

namespace Notifications.Domain.Entities
{
    public sealed class Client
    {
        private readonly static Regex _phonePattern = new(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", RegexOptions.IgnoreCase);
        private readonly static Regex _emailPattern = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.IgnoreCase);

        public Guid Id { get; init; }
        public FullName FullName { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public bool AllowNotifications { get; set; }
        public DateTime LastEntry { get; set; }
        public DateTime LastNotification { get; set; }

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private Client() { }

        private Client(Guid id, FullName fullName, string? phone, string email, bool allowNotifications, DateTime lastEntry, DateTime lastNotification)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
            Email = email;
            AllowNotifications = allowNotifications;
            LastEntry = lastEntry;
            CreatedDate = DateTime.UtcNow;
            LastNotification = lastNotification;
        }

        public static Client Create(Guid id, string firstName, string secondName, string? patronymic,
            string? phone, string email, bool allowNotifications, DateTime lastEntry, DateTime lastNotification)
        {
            if (id == Guid.Empty)
                throw new DomainException($"Invalid value for Client[id]. Entered value {id}");
            if (phone != null && !_phonePattern.IsMatch(phone))
                throw new DomainException($"Invalid value for Client[phone]. Entered value {phone}");
            if (string.IsNullOrWhiteSpace(email) || !_emailPattern.IsMatch(email))
                throw new DomainException($"Invalid value for Client[email]. Entered value {email}");

            var fullName = FullName.Create(firstName, secondName, patronymic);

            return new(id, fullName, phone, email, allowNotifications, lastEntry, lastNotification);
        }

        public void UpdateDetails(string firstName, string secondName, string? patronymic,
            string? phone, string email, bool allowNotifications, DateTime lastEntry, DateTime lastNotification)
        {
            if (phone != null && !_phonePattern.IsMatch(phone))
                throw new DomainException($"Invalid value for Client[phone]. Entered value {phone}");
            if (string.IsNullOrWhiteSpace(email) || !_emailPattern.IsMatch(email))
                throw new DomainException($"Invalid value for Client[email]. Entered value {email}");

            var fullName = FullName.Create(firstName, secondName, patronymic);

            FullName = fullName;
            Phone = phone;
            Email = email;
            AllowNotifications = allowNotifications;
            LastEntry = lastEntry;
            LastNotification = lastNotification;
        }
    }
}
