using System.ComponentModel.DataAnnotations;

namespace ZipPay.UserAccountService.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]        
        public string UserName { get; set; }

        [Required]        
        public string EmailAddress { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Monthly salary must be a positive number")]
        public double MonthlySalary { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Monthly expenses must be a positive number")]
        public double MonthlyExpenses { get; set; }        
    }
}
