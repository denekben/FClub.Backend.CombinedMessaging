using AccessControl.Domain.Entities;

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
                log.CreatedDate,
                log.UpdatedDate
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
    }
}