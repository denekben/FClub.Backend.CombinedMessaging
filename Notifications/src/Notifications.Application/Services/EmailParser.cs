using FClub.Backend.Common.ValueObjects;
using Notifications.Application.IntegrationUseCases.Branches.Handlers;
using Notifications.Application.IntegrationUseCases.Tariffs.Handlers;
using Notifications.Domain.Entities;
using System.Text.RegularExpressions;

namespace Notifications.Application.Services
{
    public static class EmailParser
    {
        public static string Parse(string text, BranchCreated branch)
        {
            var messageText = Regex.Replace(
                input: text,
                pattern: @"(?<!\\)\{branch.Name\(?<!\\)\}",
                replacement: branch.Name?.ToString() ?? string.Empty
            );

            var address = Address.Create(branch.Country, branch.City, branch.Street, branch.HouseNumber).ToString();
            messageText = Regex.Replace(
                input: messageText,
                pattern: @"(?<!\\)\{branch.Address\(?<!\\)\}",
                replacement: address
            );

            string list = string.Join("", branch.ServiceNames.Select(s => $"<li>{s}</li>"));
            messageText = Regex.Replace(
                input: messageText,
                pattern: @"(?<!\\)\{branch.ServicesList\(?<!\\)\}",
                replacement: list
            );

            return messageText;
        }

        public static string Parse(string text, TariffCreated tariff)
        {
            var messageText = Regex.Replace(
                input: text,
                pattern: @"(?<!\\)\{tariff.Name\(?<!\\)\}",
                replacement: tariff.Name?.ToString() ?? string.Empty
            );

            string list = string.Join("", tariff.ServiceNames.Select(s => $"<li>{s}</li>"));
            messageText = Regex.Replace(
                input: messageText,
                pattern: @"(?<!\\)\{tariff.ServicesList\(?<!\\)\}",
                replacement: list
            );

            messageText = Regex.Replace(
                input: messageText,
                pattern: @"(?<!\\)\{tariff.Price\(?<!\\)\}",
                replacement: (tariff.PriceForNMonths.FirstOrDefault(t => t.Key == 1 && t.Value != default).Value.ToString()
                                ?? tariff.PriceForNMonths.FirstOrDefault(t => t.Value != default).Value.ToString()) ?? string.Empty
            );

            messageText = Regex.Replace(
                input: messageText,
                pattern: @"(?<!\\)\{tariff.AllowMultiBranches\(?<!\\)\}",
                replacement: tariff.AllowMultiBranches ? "Тариф действителен во всех филиалах сети!" : string.Empty
            );

            return messageText;
        }

        public static string Parse(string text, Client client)
        {
            var messageText = Regex.Replace(
                input: text,
                pattern: @"(?<!\\)\{client.Name\(?<!\\)\}",
                replacement: client.FullName?.FirstName.ToString() ?? string.Empty
            );

            return messageText;
        }
    }
}