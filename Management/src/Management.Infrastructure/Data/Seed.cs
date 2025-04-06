using Management.Domain.Entities;

namespace Management.Infrastructure.Data
{
    public static class Seed
    {
        public static List<Role> Roles { get; } = [];

        static Seed()
        {
            Roles.AddRange([Role.Admin, Role.Manager]);
        }
    }
}
