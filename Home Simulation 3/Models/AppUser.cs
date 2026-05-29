using Microsoft.AspNetCore.Identity;

namespace Home_Simulation_3.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
