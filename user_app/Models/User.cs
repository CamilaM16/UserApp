using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace user_app.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string LogOnName { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string PasswordHash { get; set; }

        [Required]
        public bool IsEnabled { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [Required]
        public DateTime PasswordChangeDate { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nchar")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nchar")]
        public string LastName { get; set; }
        public object Name { get; set; }
    }
}