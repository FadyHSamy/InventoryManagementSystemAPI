

using AutoMapper;
using InventoryManagementSystem.Core.DTOs.User;
using InventoryManagementSystem.Core.Entities.User;

namespace InventoryManagementSystem.Core.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddingUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Hashing will be handled separately
        }
    }
}