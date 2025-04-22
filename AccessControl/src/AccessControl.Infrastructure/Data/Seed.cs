using AccessControl.Domain.Entities;
using AccessControl.Domain.Entities.Pivots;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Data
{
    public class Seed
    {
        public static List<AppUser> AppUsers { get; private set; } = [];
        public static List<Service> Services { get; private set; } = [];
        public static List<Branch> Branches { get; private set; } = [];
        public static List<Tariff> Tariffs { get; private set; } = [];
        public static List<ServiceBranch> ServiceBranches { get; private set; } = [];
        public static List<ServiceTariff> ServiceTariffs { get; private set; } = [];
        public static List<Client> Clients { get; private set; } = [];
        public static List<Membership> Memberships { get; private set; } = [];

        public void SeedData(ModelBuilder modelBuilder)
        {
            SeedAppUsers(modelBuilder);
            SeedServices(modelBuilder);
            SeedBranches(modelBuilder);
            SeedTariffs(modelBuilder);
            SeedServiceBranches(modelBuilder);
            SeedServiceTariffs(modelBuilder);
            SeedClients(modelBuilder);
            SeedMemberships(modelBuilder);
        }

        private void SeedAppUsers(ModelBuilder modelBuilder)
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
            modelBuilder.Entity<AppUser>().HasData(AppUsers);
        }

        private void SeedServices(ModelBuilder modelBuilder)
        {
            Services.AddRange([
                Service.Create(
                    Guid.Parse("f42cee3e-3e27-45e9-9a17-bb991a69f2ef"),
                    "Тренажерный зал"
                ),
                Service.Create(
                    Guid.Parse("81c16014-83ce-4570-bd16-51e9ef4187b5"),
                    "Хамам"
                ),
                Service.Create(
                    Guid.Parse("8d313217-d403-4368-8744-d44013db63ad"),
                    "Бассейн"
                ),
                Service.Create(
                    Guid.Parse("23ad6d27-f2ea-4f57-a2e3-6a36e33fad69"),
                    "Спа зона"
                ),
                Service.Create(
                    Guid.Parse("0c3ed5b6-9ea4-428b-8931-086a41951451"),
                    "Кроссфит зона"
                ),
                Service.Create(
                    Guid.Parse("20b848e4-13fd-43d3-9452-763b6435e7c2"),
                    "Боксерская зона"
                ),
                Service.Create(
                    Guid.Parse("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a"),
                    "Фитнесс бар"
                )
            ]);
            modelBuilder.Entity<Service>().HasData(Services);
        }

        private void SeedBranches(ModelBuilder modelBuilder)
        {
            var branch1 = Branch.Create(
                Guid.Parse("097f561c-5ace-4e10-b5c6-53d1e3de4f03"),
                "Филиал на Юго-Западной",
                200,
                "Россия", "Москва", "ул. Покрышкина", "2к1"
            );

            var branch2 = Branch.Create(
                Guid.Parse("34a71229-c73a-44a3-ba97-a8f528a4b056"),
                "Филиал на Воронцовской",
                150,
                "Россия", "Москва", "ул. Профсоюзная", "59"
            );

            var branch3 = Branch.Create(
                Guid.Parse("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b"),
                "Филиал на Тверской",
                150,
                "Россия", "Москва", "ул. Тверская", "17"
            );

            var branch4 = Branch.Create(
                Guid.Parse("0d4c30ca-4ad3-4881-971c-0855de9c63ed"),
                "Филиал на Адмиралтейской",
                300,
                "Россия", "Санкт-Петербург", "Невский пр.", "11/2"
            );

            var branch5 = Branch.Create(
                Guid.Parse("95a1368b-142b-455b-933c-3ac4f936de69"),
                "Филиал на Плотинке",
                100,
                "Россия", "Екатеринбург", "пр. Ленина", "32"
            );

            Branches.AddRange([branch1, branch2, branch3, branch4, branch5]);

            modelBuilder.Entity<Branch>().HasData(Branches.Select(b => new
            {
                b.Id,
                b.Name,
                b.MaxOccupancy,
                b.CurrentClientQuantity,
                b.CreatedDate,
                b.UpdatedDate
            }).ToList());

            modelBuilder.Entity<Branch>().OwnsOne(b => b.Address).HasData(Branches.Select(b => new
            {
                BranchId = b.Id,
                b.Address.Country,
                b.Address.City,
                b.Address.Street,
                b.Address.HouseNumber
            }).ToList());
        }

        private void SeedTariffs(ModelBuilder modelBuilder)
        {
            var tariff1 = Tariff.Create(
                Guid.Parse("c99c8b78-e28d-4696-87be-70e0e02716ba"),
                "Ligth",
                false
            );

            var tariff2 = Tariff.Create(
                Guid.Parse("880c1cc1-e67a-4fc2-aa02-4066cb54f794"),
                "Standart",
                true
            );

            var tariff3 = Tariff.Create(
                Guid.Parse("5973248c-c6b4-4858-8f96-3888db6340bd"),
                "Standart",
                true
            );

            Tariffs.AddRange([tariff1, tariff2, tariff3]);

            modelBuilder.Entity<Tariff>().HasData(Tariffs);
        }

        private void SeedServiceBranches(ModelBuilder modelBuilder)
        {
            List<ServiceBranch> sb1 = [
                ServiceBranch.Create(
                    Guid.Parse("a555d64e-15ec-44bd-8f66-b1b0dfcbb194"),
                    Guid.Parse("f42cee3e-3e27-45e9-9a17-bb991a69f2ef"),
                    Guid.Parse("097f561c-5ace-4e10-b5c6-53d1e3de4f03")),
                ServiceBranch.Create(
                    Guid.Parse("3f05477d-e461-45f5-815b-c3ee4f6cead6"),
                    Guid.Parse("0c3ed5b6-9ea4-428b-8931-086a41951451"),
                    Guid.Parse("097f561c-5ace-4e10-b5c6-53d1e3de4f03")),
                ServiceBranch.Create(
                    Guid.Parse("254f5a76-f7c6-499a-8188-070ac113a34c"),
                    Guid.Parse("8d313217-d403-4368-8744-d44013db63ad"),
                    Guid.Parse("097f561c-5ace-4e10-b5c6-53d1e3de4f03")),
                ServiceBranch.Create(
                    Guid.Parse("fb095e82-6b14-4b72-9cf8-f5de556918b3"),
                    Guid.Parse("23ad6d27-f2ea-4f57-a2e3-6a36e33fad69"),
                    Guid.Parse("097f561c-5ace-4e10-b5c6-53d1e3de4f03")),
                ServiceBranch.Create(
                    Guid.Parse("9c0c14e8-bfd5-4be0-8a7c-41e12d28783e"),
                    Guid.Parse("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a"),
                    Guid.Parse("097f561c-5ace-4e10-b5c6-53d1e3de4f03")),
            ];

            List<ServiceBranch> sb2 = [
                ServiceBranch.Create(
                    Guid.Parse("04152a62-d357-4357-96e8-6de53cd2d9a3"),
                    Guid.Parse("f42cee3e-3e27-45e9-9a17-bb991a69f2ef"),
                    Guid.Parse("34a71229-c73a-44a3-ba97-a8f528a4b056")),
                ServiceBranch.Create(
                    Guid.Parse("badadc9c-45bd-4312-9220-c6f29d5aa095"),
                    Guid.Parse("0c3ed5b6-9ea4-428b-8931-086a41951451"),
                    Guid.Parse("34a71229-c73a-44a3-ba97-a8f528a4b056")),
                ServiceBranch.Create(
                    Guid.Parse("a9f39f1b-a1fb-440a-a4e6-4c9b8a79f000"),
                    Guid.Parse("23ad6d27-f2ea-4f57-a2e3-6a36e33fad69"),
                    Guid.Parse("34a71229-c73a-44a3-ba97-a8f528a4b056")),
                ServiceBranch.Create(
                    Guid.Parse("e73aca6e-6527-4fa2-98e6-6ef686c0401d"),
                    Guid.Parse("20b848e4-13fd-43d3-9452-763b6435e7c2"),
                    Guid.Parse("34a71229-c73a-44a3-ba97-a8f528a4b056")),
                ServiceBranch.Create(
                    Guid.Parse("454491e2-36b2-4cdf-a958-1784c43cab92"),
                    Guid.Parse("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a"),
                    Guid.Parse("34a71229-c73a-44a3-ba97-a8f528a4b056")),
                ServiceBranch.Create(
                    Guid.Parse("ef48ac8b-8598-4b22-8714-394df231abb5"),
                    Guid.Parse("81c16014-83ce-4570-bd16-51e9ef4187b5"),
                    Guid.Parse("34a71229-c73a-44a3-ba97-a8f528a4b056"))
            ];

            List<ServiceBranch> sb3 = [
                ServiceBranch.Create(
                    Guid.Parse("841e3ac8-8327-42fb-873a-13ccd523020a"),
                    Guid.Parse("f42cee3e-3e27-45e9-9a17-bb991a69f2ef"),
                    Guid.Parse("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b")),
                ServiceBranch.Create(
                    Guid.Parse("28d5d133-0ca0-44f3-b1f0-caa72af55b41"),
                    Guid.Parse("8d313217-d403-4368-8744-d44013db63ad"),
                    Guid.Parse("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b")),
                ServiceBranch.Create(
                    Guid.Parse("dfeb0375-b1fb-4caa-a78d-1b65fdf5edcc"),
                    Guid.Parse("20b848e4-13fd-43d3-9452-763b6435e7c2"),
                    Guid.Parse("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b")),
                ServiceBranch.Create(
                    Guid.Parse("6ecf3d3b-ca83-470d-825e-d996f263adca"),
                    Guid.Parse("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a"),
                    Guid.Parse("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b"))
            ];

            List<ServiceBranch> sb4 = [
                ServiceBranch.Create(
                    Guid.Parse("92996e26-1b77-45ba-999a-1cf98cffa845"),
                    Guid.Parse("f42cee3e-3e27-45e9-9a17-bb991a69f2ef"),
                    Guid.Parse("0d4c30ca-4ad3-4881-971c-0855de9c63ed")),
                ServiceBranch.Create(
                    Guid.Parse("8755e66e-0268-4dad-a7e8-5c9704cf0f4d"),
                    Guid.Parse("0c3ed5b6-9ea4-428b-8931-086a41951451"),
                    Guid.Parse("0d4c30ca-4ad3-4881-971c-0855de9c63ed")),
                ServiceBranch.Create(
                    Guid.Parse("616efb28-d857-4062-8a58-e352795346f1"),
                    Guid.Parse("8d313217-d403-4368-8744-d44013db63ad"),
                    Guid.Parse("0d4c30ca-4ad3-4881-971c-0855de9c63ed")),
                ServiceBranch.Create(
                    Guid.Parse("cd24c8e0-8c95-4f49-b8e7-f93ee8bf2565"),
                    Guid.Parse("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a"),
                    Guid.Parse("0d4c30ca-4ad3-4881-971c-0855de9c63ed")),
                ServiceBranch.Create(
                    Guid.Parse("35bd287b-67a6-4dab-8bd2-e7d1293abad7"),
                    Guid.Parse("20b848e4-13fd-43d3-9452-763b6435e7c2"),
                    Guid.Parse("0d4c30ca-4ad3-4881-971c-0855de9c63ed")),
                ServiceBranch.Create(
                    Guid.Parse("6f998816-ac33-41a3-ac5e-41c783a09603"),
                    Guid.Parse("23ad6d27-f2ea-4f57-a2e3-6a36e33fad69"),
                    Guid.Parse("0d4c30ca-4ad3-4881-971c-0855de9c63ed")),
                ServiceBranch.Create(
                    Guid.Parse("3839fa0e-d877-497a-be9e-50ae909f6a34"),
                    Guid.Parse("81c16014-83ce-4570-bd16-51e9ef4187b5"),
                    Guid.Parse("0d4c30ca-4ad3-4881-971c-0855de9c63ed"))
            ];

            List<ServiceBranch> sb5 = [
                ServiceBranch.Create(
                    Guid.Parse("ab474a82-920d-45ed-a1aa-34d7309927e5"),
                    Guid.Parse("f42cee3e-3e27-45e9-9a17-bb991a69f2ef"),
                    Guid.Parse("95a1368b-142b-455b-933c-3ac4f936de69")),
                ServiceBranch.Create(
                    Guid.Parse("48fba502-23da-47d1-8411-84eef6c116bb"),
                    Guid.Parse("0c3ed5b6-9ea4-428b-8931-086a41951451"),
                    Guid.Parse("95a1368b-142b-455b-933c-3ac4f936de69")),
                ServiceBranch.Create(
                    Guid.Parse("18f63434-0ca1-4ea1-a9b8-638eaf93da0d"),
                    Guid.Parse("23ad6d27-f2ea-4f57-a2e3-6a36e33fad69"),
                    Guid.Parse("95a1368b-142b-455b-933c-3ac4f936de69"))
            ];

            ServiceBranches.AddRange(sb1);
            ServiceBranches.AddRange(sb2);
            ServiceBranches.AddRange(sb3);
            ServiceBranches.AddRange(sb4);
            ServiceBranches.AddRange(sb5);

            modelBuilder.Entity<ServiceBranch>().HasData(ServiceBranches);
        }

        private void SeedServiceTariffs(ModelBuilder modelBuilder)
        {
            List<ServiceTariff> st1 = [
                ServiceTariff.Create(
                    Guid.Parse("6d8999af-6bed-4ff2-89c7-5f827da036ee"),
                    Guid.Parse("f42cee3e-3e27-45e9-9a17-bb991a69f2ef"),
                    Guid.Parse("c99c8b78-e28d-4696-87be-70e0e02716ba")),
                ServiceTariff.Create(
                    Guid.Parse("c1c7c03c-8380-44a3-882b-9954884056e4"),
                    Guid.Parse("0c3ed5b6-9ea4-428b-8931-086a41951451"),
                    Guid.Parse("c99c8b78-e28d-4696-87be-70e0e02716ba")),
                ServiceTariff.Create(
                    Guid.Parse("0e167d4b-45b8-44bf-9f9d-86dcc6c37c70"),
                    Guid.Parse("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a"),
                    Guid.Parse("c99c8b78-e28d-4696-87be-70e0e02716ba")),
                ServiceTariff.Create(
                    Guid.Parse("e45a7d13-6f35-4836-bc68-602e3997e09d"),
                    Guid.Parse("20b848e4-13fd-43d3-9452-763b6435e7c2"),
                    Guid.Parse("c99c8b78-e28d-4696-87be-70e0e02716ba"))
            ];

            List<ServiceTariff> st2 = [
                ServiceTariff.Create(
                    Guid.Parse("00c25001-11d7-426e-8213-1478be2b9e34"),
                    Guid.Parse("f42cee3e-3e27-45e9-9a17-bb991a69f2ef"),
                    Guid.Parse("880c1cc1-e67a-4fc2-aa02-4066cb54f794")),
                ServiceTariff.Create(
                    Guid.Parse("4b337ef0-663a-4046-bd6f-12ef6a28bb78"),
                    Guid.Parse("0c3ed5b6-9ea4-428b-8931-086a41951451"),
                    Guid.Parse("880c1cc1-e67a-4fc2-aa02-4066cb54f794")),
                ServiceTariff.Create(
                    Guid.Parse("52fa1b9c-a615-466b-8710-626a3fa3e00e"),
                    Guid.Parse("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a"),
                    Guid.Parse("880c1cc1-e67a-4fc2-aa02-4066cb54f794")),
                ServiceTariff.Create(
                    Guid.Parse("83bc5ea4-60f4-4332-bca3-b28734526de6"),
                    Guid.Parse("20b848e4-13fd-43d3-9452-763b6435e7c2"),
                    Guid.Parse("880c1cc1-e67a-4fc2-aa02-4066cb54f794")),
                ServiceTariff.Create(
                    Guid.Parse("e7bcd2e8-f60f-4363-9bc0-d962305bbb2c"),
                    Guid.Parse("8d313217-d403-4368-8744-d44013db63ad"),
                    Guid.Parse("880c1cc1-e67a-4fc2-aa02-4066cb54f794"))
            ];

            List<ServiceTariff> st3 = [
                ServiceTariff.Create(
                    Guid.Parse("67b598ae-2ca1-4d85-b9e6-eb1c9c5c4250"),
                    Guid.Parse("f42cee3e-3e27-45e9-9a17-bb991a69f2ef"),
                    Guid.Parse("5973248c-c6b4-4858-8f96-3888db6340bd")),
                ServiceTariff.Create(
                    Guid.Parse("b4d02fa4-890b-4f2d-a2a2-6aa627d6afee"),
                    Guid.Parse("0c3ed5b6-9ea4-428b-8931-086a41951451"),
                    Guid.Parse("5973248c-c6b4-4858-8f96-3888db6340bd")),
                ServiceTariff.Create(
                    Guid.Parse("41a41f2d-7bef-414b-801b-66a7d7be33d6"),
                    Guid.Parse("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a"),
                    Guid.Parse("5973248c-c6b4-4858-8f96-3888db6340bd")),
                ServiceTariff.Create(
                    Guid.Parse("486f0065-bc54-4b53-925b-7e0a1a2dd522"),
                    Guid.Parse("20b848e4-13fd-43d3-9452-763b6435e7c2"),
                    Guid.Parse("5973248c-c6b4-4858-8f96-3888db6340bd")),
                ServiceTariff.Create(
                    Guid.Parse("d18e55eb-92af-4f83-b251-0751eef2be35"),
                    Guid.Parse("8d313217-d403-4368-8744-d44013db63ad"),
                    Guid.Parse("5973248c-c6b4-4858-8f96-3888db6340bd")),
                ServiceTariff.Create(
                    Guid.Parse("e8d54a03-88cb-4a4e-a94e-77cab40b38c7"),
                    Guid.Parse("81c16014-83ce-4570-bd16-51e9ef4187b5"),
                    Guid.Parse("5973248c-c6b4-4858-8f96-3888db6340bd")),
                ServiceTariff.Create(
                    Guid.Parse("f86e08f0-8fcd-4010-b2c7-71884200616a"),
                    Guid.Parse("23ad6d27-f2ea-4f57-a2e3-6a36e33fad69"),
                    Guid.Parse("5973248c-c6b4-4858-8f96-3888db6340bd"))
            ];

            ServiceTariffs.AddRange(st1);
            ServiceTariffs.AddRange(st2);
            ServiceTariffs.AddRange(st3);

            modelBuilder.Entity<ServiceTariff>().HasData(ServiceTariffs);
        }

        private void SeedClients(ModelBuilder modelBuilder)
        {
            Clients.AddRange([
                Client.Create(
                    Guid.Parse("58be07ff-8668-4d38-9c76-c0f3b805fe57"),
                    "Евгения", "Иолович", "Алексеевна",
                    "+78005553535", "iolovich@yandex.ru",
                    true, true
                ),
                Client.Create(
                    Guid.Parse("40416adb-dfe7-4533-ae73-80c7dd6f2e6e"),
                    "Иванов", "Иван", "Иванович",
                    "+78005553535", "ivanov@yandex.ru",
                    true, true
                ),
                Client.Create(
                    Guid.Parse("6d9ffd62-5bd7-451e-a1f2-548ea313effb"),
                    "Петров", "Петр", "Петрович",
                    "+79991001010", "petrov@yandex.ru",
                    true, true
                ),
                Client.Create(
                    Guid.Parse("1db4505a-02f3-49a5-9837-aec1b0ecca44"),
                    "Иван", "Иванов", "Иванович",
                    "+79991234567", "ivanov@example.com",
                    true, false
                ),
                Client.Create(
                    Guid.Parse("287bc96f-469a-4acb-9f83-ca0932c787e2"),
                    "Петр", "Петров", "Петрович",
                    "+79992345678", "petrov@example.com",
                    true, false
                ),
                Client.Create(
                    Guid.Parse("754d703a-f1ea-425a-b3eb-b98829627774"),
                    "Анна", "Сидорова", "Сергеевна",
                    "+79993456789", "sidorova@example.com",
                    true, false
                ),
                Client.Create(
                    Guid.Parse("d789e2e0-13d7-4fdb-9b38-2df0675525fc"),
                    "Мария", "Кузнецова", "Алексеевна",
                    "+79994567890", "kuznetsova@example.com",
                    true, false
                ),
                Client.Create(
                    Guid.Parse("3294e0e3-6409-431b-8ed2-db3819ebc635"),
                    "Алексей", "Смирнов", "Дмитриевич",
                    "+79995678901", "smirnov@example.com",
                    true, false
                ),
                Client.Create(
                    Guid.Parse("ed8a6578-96f3-4891-a816-ef0559b27ed3"),
                    "Елена", "Попова", "Викторовна",
                    "+79996789012", "popova@example.com",
                    true, false
                ),
                Client.Create(
                    Guid.Parse("a783ccef-eaf0-415d-b72a-6dffeeb247f5"),
                    "Дмитрий", "Васильев", "Олегович",
                    "+79997890123", "vasilev@example.com",
                    true, false
                ),
                Client.Create(
                    Guid.Parse("d1cbac4f-29bb-46ad-a6dd-b987523de71a"),
                    "Ольга", "Новикова", "Игоревна",
                    "+79998901234", "novikova@example.com",
                    true, false
                )
            ]);

            modelBuilder.Entity<Client>().HasData(Clients.Select(c =>
                new
                {
                    c.Id,
                    c.Phone,
                    c.Email,
                    c.AllowEntry,
                    c.IsStaff,
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

        private void SeedMemberships(ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;
            now = new DateTime(now.Year, now.Month, now.Day);

            var m1 = Membership.Create(
                Guid.Parse("82347d00-1363-4f40-99de-50b4096d44c8"),
                Guid.Parse("c99c8b78-e28d-4696-87be-70e0e02716ba"),
                now.AddMonths(1),
                Guid.Parse("1db4505a-02f3-49a5-9837-aec1b0ecca44"),
                Guid.Parse("097f561c-5ace-4e10-b5c6-53d1e3de4f03")
            );
            var m2 = Membership.Create(
                Guid.Parse("f34285ab-2bfa-47a2-bda8-5ad707e24c8b"),
                Guid.Parse("880c1cc1-e67a-4fc2-aa02-4066cb54f794"),
                now.AddMonths(1),
                Guid.Parse("287bc96f-469a-4acb-9f83-ca0932c787e2"),
                Guid.Parse("34a71229-c73a-44a3-ba97-a8f528a4b056")
            );
            var m3 = Membership.Create(
                Guid.Parse("3a1d21d6-3bc4-4f26-ba7e-1ef9bb3b5286"),
                Guid.Parse("5973248c-c6b4-4858-8f96-3888db6340bd"),
                now.AddMonths(1),
                Guid.Parse("754d703a-f1ea-425a-b3eb-b98829627774"),
                Guid.Parse("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b")
            );
            var m4 = Membership.Create(
                Guid.Parse("b661a029-3d11-43c2-a652-f233cdc7bc3e"),
                Guid.Parse("c99c8b78-e28d-4696-87be-70e0e02716ba"),
                now.AddMonths(6),
                Guid.Parse("d789e2e0-13d7-4fdb-9b38-2df0675525fc"),
                Guid.Parse("0d4c30ca-4ad3-4881-971c-0855de9c63ed")
            );
            var m5 = Membership.Create(
                Guid.Parse("25825c4c-e04f-40c6-a00d-4a9dfbdbb91d"),
                Guid.Parse("880c1cc1-e67a-4fc2-aa02-4066cb54f794"),
                now.AddMonths(6),
                Guid.Parse("3294e0e3-6409-431b-8ed2-db3819ebc635"),
                Guid.Parse("097f561c-5ace-4e10-b5c6-53d1e3de4f03")
            );
            var m6 = Membership.Create(
                Guid.Parse("b9e3f831-eb10-414b-93c1-b0888d970c9f"),
                Guid.Parse("5973248c-c6b4-4858-8f96-3888db6340bd"),
                now.AddMonths(6),
                Guid.Parse("ed8a6578-96f3-4891-a816-ef0559b27ed3"),
                Guid.Parse("34a71229-c73a-44a3-ba97-a8f528a4b056")
            );
            var m7 = Membership.Create(
                Guid.Parse("7898f7a6-6f24-47e8-bf6d-7766e1638878"),
                Guid.Parse("880c1cc1-e67a-4fc2-aa02-4066cb54f794"),
                now.AddMonths(12),
                Guid.Parse("a783ccef-eaf0-415d-b72a-6dffeeb247f5"),
                Guid.Parse("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b")
            );
            var m8 = Membership.Create(
                Guid.Parse("53a621b5-fedc-4fc9-9232-6a62858d8e59"),
                Guid.Parse("5973248c-c6b4-4858-8f96-3888db6340bd"),
                now.AddMonths(12),
                Guid.Parse("d1cbac4f-29bb-46ad-a6dd-b987523de71a"),
                Guid.Parse("0d4c30ca-4ad3-4881-971c-0855de9c63ed")
            );

            Memberships.AddRange([m1, m2, m3, m4, m5, m6, m7, m8]);

            modelBuilder.Entity<Membership>().HasData(Memberships);
        }
    }
}