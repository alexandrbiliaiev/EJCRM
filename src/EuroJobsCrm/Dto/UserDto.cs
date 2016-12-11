using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class UserDto : DataTransferObjectBase
    {
        public UserDto()
        {
            
        }
        

        public UserDto(AspNetUsers responsibleUser)
        {
            if (responsibleUser == null)
            {
                return;
            }
            Id = responsibleUser.Id;
            Email = responsibleUser.Email;
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}