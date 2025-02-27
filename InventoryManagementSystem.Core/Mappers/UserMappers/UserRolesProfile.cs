﻿using AutoMapper;
using InventoryManagementSystem.Core.DTOs.UserDto;
using InventoryManagementSystem.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Mappers.UserMappers
{
    public class UserRolesProfile:Profile
    {
        public UserRolesProfile()
        {
            CreateMap<UserRoleDescriptionResponse, UserRoles>()
                .ReverseMap();
        }
    }
}
