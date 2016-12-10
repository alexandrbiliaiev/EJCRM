namespace EuroJobsCrm.Dto
{
    public class UserDto : DataTransferObjectBase
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}