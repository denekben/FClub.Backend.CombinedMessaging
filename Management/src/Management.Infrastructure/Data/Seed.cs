using FClub.Backend.Common.Services;
using Management.Application.Services;
using Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Data
{
    public class Seed
    {
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;

        public Role AdminRole { get; private set; }
        public Role ManagerRole { get; private set; }
        public List<Role> Roles { get; private set; } = new();
        public List<AppUser> Admins { get; private set; } = new();
        public List<AppUser> Managers { get; private set; } = new();

        public Seed(ITokenService tokenService, IPasswordService passwordService)
        {
            _tokenService = tokenService;
            _passwordService = passwordService;
        }

        public void SeedData(ModelBuilder modelBuilder)
        {
            InitializeRoles();
            InitializeUsers();

            modelBuilder.Entity<Role>().HasData(Roles);

            modelBuilder.Entity<AppUser>().HasData(Admins.Select(a => new
            {
                a.Id,
                a.Phone,
                a.Email,
                a.PasswordHash,
                a.IsBlocked,
                a.AllowEntry,
                a.RefreshToken,
                a.RefreshTokenExpires,
                a.RoleId,
                a.CreatedDate,
                a.UpdatedDate
            }).ToList());

            modelBuilder.Entity<AppUser>().OwnsOne(u => u.FullName).HasData(Admins.Select(a => new
            {
                AppUserId = a.Id,
                a.FullName.FirstName,
                a.FullName.SecondName,
                a.FullName.Patronymic,
            }).ToList());

            modelBuilder.Entity<AppUser>().HasData(Managers.Select(m => new
            {
                m.Id,
                m.Phone,
                m.Email,
                m.PasswordHash,
                m.IsBlocked,
                m.AllowEntry,
                m.RefreshToken,
                m.RefreshTokenExpires,
                m.RoleId,
                m.CreatedDate,
                m.UpdatedDate
            }).ToList());

            modelBuilder.Entity<AppUser>().OwnsOne(u => u.FullName).HasData(Managers.Select(m => new
            {
                AppUserId = m.Id,
                m.FullName.FirstName,
                m.FullName.SecondName,
                m.FullName.Patronymic,
            }).ToList());
        }

        private void InitializeRoles()
        {
            AdminRole = Role.Admin;
            ManagerRole = Role.Manager;
            Roles.AddRange([AdminRole, ManagerRole]);
        }

        private void InitializeUsers()
        {
            var admin1 = AppUser.Create(
                Guid.Parse("58be07ff-8668-4d38-9c76-c0f3b805fe57"),
                "Евгения", "Иолович", "Алексеевна",
                "+78005553535", "iolovich@yandex.ru",
                _passwordService.HashPassword("Iolovich123!"),
                false, true,
                _tokenService.GenerateRefreshToken(),
                _tokenService.GenerateRefreshTokenExpiresDate(),
                AdminRole.Id
            );

            var admin2 = AppUser.Create(
                Guid.Parse("a8085988-e681-4f9d-85f8-e99e2fa4aeec"),
                "Курбанаев", "Денис", "Алексеевич",
                "+79991001010", "denekben@yandex.ru",
                _passwordService.HashPassword("Denekben123!"),
                false, true,
                _tokenService.GenerateRefreshToken(),
                _tokenService.GenerateRefreshTokenExpiresDate(),
                AdminRole.Id
            );

            var manager1 = AppUser.Create(
                Guid.Parse("40416adb-dfe7-4533-ae73-80c7dd6f2e6e"),
                "Иванов", "Иван", "Иванович",
                "+78005553535", "ivanov@yandex.ru",
                _passwordService.HashPassword("Ivanov123!"),
                false, true,
                _tokenService.GenerateRefreshToken(),
                _tokenService.GenerateRefreshTokenExpiresDate(),
                ManagerRole.Id
            );

            var manager2 = AppUser.Create(
                Guid.Parse("6d9ffd62-5bd7-451e-a1f2-548ea313effb"),
                "Иванова", "Иванка", "Ибрагимовна",
                "+79991001010", "ivanova@yandex.ru",
                _passwordService.HashPassword("Ivanova123!"),
                false, true,
                _tokenService.GenerateRefreshToken(),
                _tokenService.GenerateRefreshTokenExpiresDate(),
                ManagerRole.Id
            );

            Admins.AddRange([admin1, admin2]);
            Managers.AddRange([manager1, manager2]);
        }
    }
}
