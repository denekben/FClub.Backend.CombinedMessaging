﻿using AccessControll.Domain.Entities;
using FClub.Backend.Common.Exceptions;

namespace AccessControl.Domain.Entities
{
    public sealed class Membership
    {
        public Guid Id { get; init; }
        public Guid TariffId { get; set; }
        public Tariff Tariff { get; set; }
        public DateTime ExpiresDate { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private Membership(Guid id, Guid tariffId, DateTime expiresDate, Guid clientId, Guid branchId)
        {
            Id = id;
            TariffId = tariffId;
            ExpiresDate = expiresDate;
            ClientId = clientId;
            BranchId = branchId;
            CreatedDate = DateTime.UtcNow;
        }

        public static Membership Create(Guid id, Guid tariffId, DateTime expiresDate, Guid clientId, Guid branchId)
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

            return new(id, tariffId, expiresDate, clientId, branchId);
        }
    }
}
