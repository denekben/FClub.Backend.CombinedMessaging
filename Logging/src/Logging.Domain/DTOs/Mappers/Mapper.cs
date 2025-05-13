using Logging.Domain.Entities;

namespace Logging.Domain.DTOs.Mappers
{
    public static class Mapper
    {
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
