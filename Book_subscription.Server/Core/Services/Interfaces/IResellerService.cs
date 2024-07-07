using Book_subscription.Server.API.DTOs.ResellerDTOs;
using Book_subscription.Server.Core.Entities;

namespace Book_subscription.Server.Core.Services.Interfaces
{
    public interface IResellerService
    {

        Task<ResellerDTO> RegisterResellerAsync(ResellerDTO resellerDTO);
        Task<Reseller> GetResellerByApiKeyAsync(string apiKey);


    }
}
