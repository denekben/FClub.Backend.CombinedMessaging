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

        private Membership(Guid tariffId, DateTime expiresDate, Guid clientId, Guid branchId)
        {
            Id = Guid.NewGuid();
            TariffId = tariffId;
            ExpiresDate = expiresDate;
            ClientId = clientId;
            BranchId = branchId;
            CreatedDate = DateTime.UtcNow;
        }

        public static Membership Create(Guid tariffId, DateTime expiresDate, Guid clientId, Guid branchId)
        {
            if (tariffId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[tariffId]. Entered value {tariffId}");
            if (clientId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[clientId]. Entered value {clientId}");
            if (branchId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[branchId]. Entered value {branchId}");
            if (expiresDate <= DateTime.UtcNow)
                throw new DomainException($"Invalid value for Membership[expiresDate]. Entered value {expiresDate}");

            return new(tariffId, expiresDate, clientId, branchId);
        }

        public void UpdateDetails(Guid id, Guid tariffId, DateTime expiresDate, Guid clientId, Guid branchId)
        {
            if (id == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[id]. Entered value {id}");
            if (tariffId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[tariffId]. Entered value {tariffId}");
            if (clientId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[clientId]. Entered value {clientId}");
            if (branchId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[branchId]. Entered value {branchId}");
            if (expiresDate <= DateTime.UtcNow)
                throw new DomainException($"Invalid value for Membership[expiresDate]. Entered value {expiresDate}");

            TariffId = tariffId;
            ExpiresDate = expiresDate;
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

            var discountForSocialGroup = Tariff.DiscountForSocialGroup?.FirstOrDefault(x => x.Key == ClientId).Value;
            discountForSocialGroup = discountForSocialGroup ?? 0;

            TotalCost = Math.Ceiling((double)valueForNMonths * (1 - (double)discountForSocialGroup / 100));
        }
    }
}