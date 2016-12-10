namespace EuroJobsCrm.Dto
{
    public class DataTransferObjectBase
    {
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; }
    }
}