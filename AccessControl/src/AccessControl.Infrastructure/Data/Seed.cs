using AccessControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Data
{
    public class Seed
    {
        public static List<AppUser> AppUsers { get; } = [];
        public static List<Client> Clients { get; } = [];

        public void SeedData(ModelBuilder modelBuilder)
        {
            InitAppUsers();
            InitClients();

            modelBuilder.Entity<AppUser>().HasData(AppUsers);

            modelBuilder.Entity<Client>().HasData(Clients.Select(c =>
                new
                {
                    c.Id,
                    c.Phone,
                    c.Email,
                    c.AllowEntry,
                    c.IsStaff,
                    c.MembershipId,
                    c.CreatedDate,
                    c.UpdatedDate
                }
            ));

            modelBuilder.Entity<Client>().OwnsOne(c => c.FullName).HasData(Clients.Select(c =>
                new
                {
                    ClientId = c.Id,
                    c.FullName.FirstName,
                    c.FullName.SecondName,
                    c.FullName.Patronymic,
                }
            ));
        }

        private void InitAppUsers()
        {
            AppUsers.AddRange([
                AppUser.Create
                (
                    Guid.Parse("58be07ff-8668-4d38-9c76-c0f3b805fe57"),
                    false
                ),
                AppUser.Create
                (
                    Guid.Parse("a8085988-e681-4f9d-85f8-e99e2fa4aeec"),
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

        private void InitClients()
        {
            Clients.AddRange([
                Client.Create(
                    Guid.Parse("58be07ff-8668-4d38-9c76-c0f3b805fe57"),
                    "Евгения", "Иолович", "Алексеевна",
                    "+78005553535", "iolovich@yandex.ru",
                    true, true, null
                ),
                Client.Create(
                    Guid.Parse("a8085988-e681-4f9d-85f8-e99e2fa4aeec"),
                    "Курбанаев", "Денис", "Алексеевич",
                    "+79991001010", "denekben@yandex.ru",
                    true, true, null
                ),
                Client.Create(
                    Guid.Parse("40416adb-dfe7-4533-ae73-80c7dd6f2e6e"),
                    "Иванов", "Иван", "Иванович",
                    "+78005553535", "ivanov@yandex.ru",
                    true, true, null
                ),
                Client.Create(
                    Guid.Parse("6d9ffd62-5bd7-451e-a1f2-548ea313effb"),
                    "Иванова", "Иванка", "Ибрагимовна",
                    "+79991001010", "ivanova@yandex.ru",
                    true, true, null
                ),
            ]);
        }
    }
}