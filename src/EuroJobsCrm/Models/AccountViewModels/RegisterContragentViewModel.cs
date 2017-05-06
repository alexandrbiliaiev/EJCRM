using System.ComponentModel.DataAnnotations;

namespace EuroJobsCrm.Models.AccountViewModels
{
    public class RegisterContragentViewModel : RegisterViewModel
    {
        public string Name { get; set; }
               
        public string CompanyName { get; set; }
                
        public bool PersonalDataUse { get; set; }
    }
}