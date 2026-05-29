using System.ComponentModel.DataAnnotations;

namespace Home_Simulation_3.ViewModels.Account
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
