using System.ComponentModel.DataAnnotations;

namespace Home_Simulation_3.Areas.Admin.ViewModels.Position
{
    public class UpdatePositionVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Name must be max 20 characters")]
        [MinLength(2, ErrorMessage = "Name must be min 2 characters")]
        public string Name { get; set; }
    }
}
