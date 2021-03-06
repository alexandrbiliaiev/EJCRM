﻿using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class UserDto : DataTransferObjectBase
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int? CtgId { get; set; }
        public string Name { get; set; }

        public string UserRole { get; set; }


        public UserDto()
        {
            
        }

        public UserDto(ApplicationUser user)
        {
            UserName = user.UserName;
            Email = user.Email;
            Id = user.Id;
        }

        public UserDto(AspNetUsers user)
        {

            UserName = user?.UserName;
            Email = user?.Email;
            Id = user?.Id;
        }

    }
}