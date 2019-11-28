using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZipPay.UserAccountService.Entities
{
    public class Account
    {
        public Account()
        {
            CreatedDate = DateTime.UtcNow;         
        }

        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public double CurrentBalance { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual User User { get; set; }
    }
}
