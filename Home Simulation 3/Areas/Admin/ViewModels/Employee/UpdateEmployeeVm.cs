using System.ComponentModel.DataAnnotations;

namespace Home_Simulation_3.Areas.Admin.ViewModels.Employee
{
    public class UpdateEmployeeVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, ErrorMessage = "Name must be max 30 characters")]
        [MinLength(2, ErrorMessage = "Name must be min 2 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "FullAddress is required")]
        [StringLength(50, ErrorMessage = "FullAddress must be max 50 characters")]
        [MinLength(2, ErrorMessage = "FullAddress must be min 2 characters")]
        public string FullAddress { get; set; }
        [Required(ErrorMessage = "PositionId is required")]
        public int PositionId { get; set; }
        public string? ImageURL { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
