

using AutoMapper;
using InventoryManagementSystem.Core.DTOs.UserDto;
using InventoryManagementSystem.Core.Entities.User;

namespace InventoryManagementSystem.Core.Mappers.UserMappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddingUserRequest, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<UserInformationResponse, User>()
                .ForMember(entity => entity.Username, opt => opt.MapFrom(dto => dto.Username))
                .ForMember(entity => entity.CreatedAt, opt => opt.MapFrom(dto => dto.CreatedAt))
                .ForMember(entity => entity.LoginSuccessfully, opt => opt.MapFrom(dto => dto.LoginSuccessfully))
                .ForMember(entity => entity.MobileNumber, opt => opt.MapFrom(dto => dto.MobileNumber))
                .ForMember(entity => entity.Email, opt => opt.MapFrom(dto => dto.Email))
                .ReverseMap();

            CreateMap<UserInformationResponse, UserRoles>()
                .ForMember(entity => entity.RoleName, opt => opt.MapFrom(dto => dto.RoleName))
                .ReverseMap();

            CreateMap<UserInformationResponse, UserStatus>()
                .ForMember(entity => entity.StatusDescripton, opt => opt.MapFrom(dto => dto.StatusDescripton))
                .ReverseMap();
        }
    }
}