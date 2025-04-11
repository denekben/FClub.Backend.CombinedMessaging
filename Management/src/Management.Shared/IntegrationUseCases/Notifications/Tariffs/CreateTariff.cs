namespace Management.Shared.IntegrationUseCases.Notifications.Tariffs
{
    public sealed record CreateTariff(
        string Name,
        Dictionary<int, int> PriceForNMonths,
        Dictionary<Guid, int>? DiscountForSocialGroup,
        bool AllowMultiBranches,
        List<string> serviceNames
    );
}
