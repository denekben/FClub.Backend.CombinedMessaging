﻿using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Client client);
        Task<Client?> GetAsync(Guid id);
        Task<Client?> GetAsync(Guid id, ClientIncludes includes);
    }

    [Flags]
    public enum ClientIncludes
    {
        SocialGroup = 1
    }
}
