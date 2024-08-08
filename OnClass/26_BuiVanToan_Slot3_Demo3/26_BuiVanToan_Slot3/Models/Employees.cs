using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _26_BuiVanToan_Slot3.Models
{
    public class Employees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId
        {
            get;
            set;
        }
        public string EmployeeFirstName
        {
            get;
            set;
        }
        public string EmployeeLastName
        {
            get;
            set;
        }
        public decimal Salary
        {
            get;



            set;
        }
        public string Designation
        {
            get;
            set;

        }
    }
}
