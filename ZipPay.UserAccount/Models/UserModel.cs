using System.ComponentModel.DataAnnotations;

namespace ZipPay.UserAccountService.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@""]+(\.[^<>()[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Email address is not valid")]
        public string EmailAddress { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Monthly salary must be a positive number")]
        public double MonthlySalary { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Monthly expenses must be a positive number")]
        public double MonthlyExpenses { get; set; }

    }
}
