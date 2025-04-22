using FClub.Backend.Common.Exceptions;

namespace Management.Domain.Entities
{
    public sealed class Membership
    {
        public Guid Id { get; init; }
        public double TotalCost { get; set; }
        public int MonthQuantity { get; set; }
        public Guid TariffId { get; set; }
        public Tariff Tariff { get; set; }
        public DateTime ExpiresDate { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private Membership(Guid tariffId, int monthQuantity, Guid clientId, Guid branchId)
        {
            Id = Guid.NewGuid();
            TariffId = tariffId;
            MonthQuantity = monthQuantity;
            var now = DateTime.UtcNow;
            now = new DateTime(now.Year, now.Month, now.Day);
            ExpiresDate = now.AddMonths(monthQuantity);
            ClientId = clientId;
            BranchId = branchId;
            CreatedDate = DateTime.UtcNow;
        }

        private Membership(Guid id, Guid tariffId, int monthQuantity, Guid clientId, Guid branchId)
        {
            Id = id;
            TariffId = tariffId;
            MonthQuantity = monthQuantity;
            var now = DateTime.UtcNow;
            now = new DateTime(now.Year, now.Month, now.Day);
            ExpiresDate = now.AddMonths(monthQuantity);
            ClientId = clientId;
            BranchId = branchId;
            CreatedDate = DateTime.UtcNow;
        }

        public static Membership Create(Guid tariffId, int monthQuantity, Guid clientId, Guid branchId)
        {
            if (tariffId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[tariffId]. Entered value {tariffId}");
            if (clientId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[clientId]. Entered value {clientId}");
            if (branchId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[branchId]. Entered value {branchId}");
            if (monthQuantity <= 0)
                throw new DomainException($"Invalid value for Membership[monthQuantity]. Entered value {monthQuantity}");

            return new(tariffId, monthQuantity, clientId, branchId);
        }

        public static Membership Create(Guid id, Guid tariffId, int monthQuantity, Guid clientId, Guid branchId)
        {
            if (tariffId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[tariffId]. Entered value {tariffId}");
            if (clientId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[clientId]. Entered value {clientId}");
            if (branchId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[branchId]. Entered value {branchId}");
            if (monthQuantity <= 0)
                throw new DomainException($"Invalid value for Membership[monthQuantity]. Entered value {monthQuantity}");

            return new(id, tariffId, monthQuantity, clientId, branchId);
        }


        public void UpdateDetails(Guid tariffId, int monthQuantity, Guid clientId, Guid branchId)
        {
            if (tariffId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[tariffId]. Entered value {tariffId}");
            if (clientId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[clientId]. Entered value {clientId}");
            if (branchId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[branchId]. Entered value {branchId}");
            if (monthQuantity <= 0)
                throw new DomainException($"Invalid value for Membership[monthQuantity]. Entered value {monthQuantity}");

            TariffId = tariffId;
            ExpiresDate = ExpiresDate.AddMonths(monthQuantity - MonthQuantity);
            MonthQuantity = monthQuantity;
            ClientId = clientId;
            BranchId = branchId;
        }

        public void SetCost()
        {
            if (Tariff == null)
                throw new BadRequestException("Cannot find tariff to calculate price for membership");
            if (Client == null)
                throw new BadRequestException("Cannot find client to calculate price for membership");

            var valueForNMonths = Tariff.PriceForNMonths.FirstOrDefault(x => x.Key == MonthQuantity).Value;
            if (valueForNMonths == default)
                throw new DomainException($"Cannot find tariff price for {MonthQuantity} months");

            var discountForSocialGroup = Tariff.DiscountForSocialGroup?.FirstOrDefault(x => x.Key == Client.SocialGroupId).Value;
            discountForSocialGroup = discountForSocialGroup ?? 0;

            TotalCost = Math.Ceiling((double)valueForNMonths * (1 - (double)discountForSocialGroup / 100));
        }
    }
}