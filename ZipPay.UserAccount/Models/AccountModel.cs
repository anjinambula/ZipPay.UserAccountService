using System;

namespace ZipPay.UserAccountService.Models
{
    public class AccountModel
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public double CurrentBalance { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int UserId { get; set; }

    }
}
