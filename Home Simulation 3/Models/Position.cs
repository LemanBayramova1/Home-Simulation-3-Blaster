using Home_Simulation_3.Models.Base;

namespace Home_Simulation_3.Models
{
    public class Position:BaseEntity
    {
       public string Name { get; set; }
       public List<Employee> Employees { get; set; }
    }
}
