﻿using Book_subscription.Server.Core.Entities;

namespace Book_subscription.Server.Core.Services.Interfaces
{
    public interface IResellerService
    {

        Task<Reseller> RegisterResellerAsync(string name);
        Task<Reseller> GetResellerByApiKeyAsync(string apiKey);


    }
}
