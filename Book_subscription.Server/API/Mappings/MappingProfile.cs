using AutoMapper;
using Book_subscription.Server.API.DTOs.Authentication;
using Book_subscription.Server.API.DTOs.ResellerDTOs;
using Book_subscription.Server.API.DTOs.SubscriptionDTOs;
using Book_subscription.Server.Core.Entities;


namespace Book_subscription.Server.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Mapping from Subscription entity to SubscriptionDTO
            CreateMap<Subscription, SubscriptionDTO>();

            // Bidirectional mapping between Reseller and ResellerDTO
            CreateMap<Reseller,ResellerDTO>().ReverseMap();

            CreateMap<RegisterUserDTO, User>();
            CreateMap<User, UserResponseDTO>()
                .ForMember(dest => dest.Token, opt => opt.Ignore()); // Token will be set manually after mapping
        }
    }
}
