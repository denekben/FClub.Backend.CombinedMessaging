using Logging.Domain.Entities;

namespace Logging.Infrastructure.Data
{
    public static class Seed
    {
        public static List<AppUser> AppUsers = [];

        static Seed()
        {
            AppUsers.AddRange([
                AppUser.Create
                (
                    Guid.Parse("58be07ff-8668-4d38-9c76-c0f3b805fe57"),
                    false
                ),
                AppUser.Create
                (
                    Guid.Parse("40416adb-dfe7-4533-ae73-80c7dd6f2e6e"),
                    false
                ),
                AppUser.Create
                (
                    Guid.Parse("6d9ffd62-5bd7-451e-a1f2-548ea313effb"),
                    false
                )
            ]);
        }
    }
}
