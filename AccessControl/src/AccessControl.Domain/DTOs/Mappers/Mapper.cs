using AccessControl.Domain.Entities;
using AccessControll.Domain.Entities;
using FClub.Backend.Common.ValueObjects.DTOs.Mappers;

namespace AccessControl.Domain.DTOs.Mappers
{
    public static class Mapper
    {
        public static TurnstileDto AsDto(this Turnstile turnstile)
        {
            return new(
                turnstile.Id,
                turnstile.Name,
                turnstile.IsMain,
                turnstile.BranchId,
                turnstile.ServiceId,
                turnstile.CreatedDate,
                turnstile.UpdatedDate
            );
        }

        public static EntryLogDto AsDto(this EntryLog log)
        {
            return new(
                log.Id,
                log.ClientId,
                log.ClientFullName,
                log.TurnstileId,
                log.BranchName,
                log.ServiceName,
                log.EntryType.ToString(),
                log.CreatedDate,
                log.UpdatedDate
            );
        }

        public static StatisticNoteDto AsDto(this StatisticNote stat)
        {
            return new(
                stat.CreatedDate,
                stat.EntriesQuantity
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

        public static BranchDto AsDto(this Branch branch)
        {
            return new(
                branch.Id,
                branch.Name,
                branch.MaxOccupancy,
                branch.CurrentClientQuantity,
                branch.Address.AsDto()
            );
        }
    }
}