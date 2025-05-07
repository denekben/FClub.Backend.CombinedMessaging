using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.ValueObjects.DTOs.Mappers;
using Management.Domain.Entities;

namespace Management.Domain.DTOs.Mappers
{
    public static class Mapper
    {
        public static BranchDto AsDto(this Branch branch, List<Service>? services)
        {
            if (branch.Address == null)
                throw new MappingException("Cannot map BranchDto: Address is null");

            var serviceDtos = services?.Select(s => s.AsDto()).ToList();

            return new(
                branch.Id,
                branch.Name,
                branch.MaxOccupancy,
                branch.Address.AsDto(),
                serviceDtos ?? [],
                branch.CreatedDate,
                branch.UpdatedDate);
        }

        public static ServiceDto AsDto(this Service service)
        {
            return new(
                service.Id,
                service.Name,
                service.CreatedDate,
                service.UpdatedDate);
        }

        public static ClientDto AsDto(this Client client, Membership? membership, SocialGroup? socialGroup)
        {
            if (client.FullName == null)
                throw new MappingException("Cannot map ClientDto: FullName is null");

            MembershipDto? membershipDto = null;
            SocialGroupDto? socialGroupDto = null;

            if (membership != null)
                membershipDto = membership.AsDto();
            if (socialGroup != null)
                socialGroupDto = socialGroup.AsDto();

            return new(
                client.Id,
                client.FullName.AsDto(),
                client.Phone,
                client.Email,
                client.IsStaff,
                client.AllowEntry,
                client.AllowNotifications,
                membershipDto,
                socialGroupDto,
                client.CreatedDate,
                client.UpdatedDate
            );
        }

        public static MembershipDto AsDto(this Membership membership)
        {
            if (membership.Tariff == null)
                throw new MappingException("Cannot map MembershipDto: Tariff is null");

            return new(
                membership.Id,
                membership.TotalCost,
                membership.MonthQuantity,
                membership.BranchId,
                membership.Tariff.AsDto(),
                membership.ExpiresDate,
                membership.CreatedDate,
                membership.UpdatedDate
            );
        }

        public static MembershipDto AsDto(this Membership membership, List<Service> services)
        {
            if (membership.Tariff == null)
                throw new MappingException("Cannot map MembershipDto: Tariff is null");

            return new(
                membership.Id,
                membership.TotalCost,
                membership.MonthQuantity,
                membership.BranchId,
                membership.Tariff.AsDto(services),
                membership.ExpiresDate,
                membership.CreatedDate,
                membership.UpdatedDate
            );
        }

        public static TariffDto AsDto(this Tariff tariff)
        {
            return new(
                tariff.Id,
                tariff.Name,
                tariff.PriceForNMonths,
                tariff.DiscountForSocialGroup,
                tariff.AllowMultiBranches,
                [],
                tariff.CreatedDate,
                tariff.UpdatedDate
            );
        }

        public static TariffDto AsDto(this Tariff tariff, List<Service>? services)
        {
            var serviceDtos = services?.Select(s => s.AsDto()).ToList();

            return new(
                tariff.Id,
                tariff.Name,
                tariff.PriceForNMonths,
                tariff.DiscountForSocialGroup,
                tariff.AllowMultiBranches,
                serviceDtos ?? [],
                tariff.CreatedDate,
                tariff.UpdatedDate
            );
        }

        public static TariffWithGroupsDto AsDto(this Tariff tariff, List<Service>? services, Dictionary<SocialGroup, int>? discounts)
        {
            var serviceDtos = services?.Select(s => s.AsDto()).ToList();
            List<DiscountForSocialGroupDto>? discountsDto = [];
            foreach (var discount in (discounts ?? []))
            {
                discountsDto.Add(new(discount.Key.AsDto(), discount.Value));
            }


            return new(
                tariff.Id,
                tariff.Name,
                tariff.PriceForNMonths,
                discountsDto,
                tariff.AllowMultiBranches,
                serviceDtos ?? [],
                tariff.CreatedDate,
                tariff.UpdatedDate
            );
        }

        public static SocialGroupDto AsDto(this SocialGroup socialGroup)
        {
            return new(
                socialGroup.Id,
                socialGroup.Name,
                socialGroup.CreatedDate,
                socialGroup.UpdatedDate
            );
        }

        public static RoleDto AsDto(this Role role)
        {
            return new(
                role.Id,
                role.Name,
                role.CreatedDate,
                role.UpdatedDate
            );
        }

        public static StatisticNoteDto AsDto(this StatisticNote note)
        {
            return new(
                note.CreatedDate,
                note.MembershipCost,
                note.MembershipQuantity
            );
        }

        public static UserLogDto AsDto(this UserLog log)
        {
            return new(
                log.Id,
                log.AppUserId,
                log.ServiceName,
                log.Text,
                log.CreatedDate,
                log.UpdatedDate
            );
        }

        public static UserDto AsDto(this AppUser user)
        {
            if (user.Role == null)
                throw new MappingException("Cannot map UserDto: Role is null");
            if (user.FullName == null)
                throw new MappingException("Cannot map UserDto: FullName is null");

            return new(
                user.Id,
                user.FullName.AsDto(),
                user.Phone,
                user.Email,
                user.IsBlocked,
                user.AllowEntry,
                user.Role.AsDto(),
                user.CreatedDate,
                user.UpdatedDate
            );
        }
    }
}