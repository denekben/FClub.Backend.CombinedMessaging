﻿using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface ISocialGroupRepository
    {
        Task<SocialGroup?> GetAsync(Guid id);
        Task AddAsync(SocialGroup socialGroup);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(SocialGroup socialGroup);
    }
}
