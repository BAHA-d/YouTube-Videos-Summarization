using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoSummarization.Models
{
    public partial class User
    {
        public User()
        {
            Exams = new HashSet<Exam>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string Phone { get; set; } = null!;

        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one digit")]
        public string Password { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;

        public int RoleId { get; set; }

        public virtual Role? Role { get; set; } = null!;
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
