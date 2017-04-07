using System.ComponentModel.DataAnnotations;

namespace EuroJobsCrm.Models.AccountViewModels
{
    public class RegisterContragentViewModel : RegisterViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}