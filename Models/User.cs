using System.ComponentModel.DataAnnotations;

namespace FinPlanBackend.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        // [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        public double Cash { get; set; }

        public double Debt { get; set; }

        public string Username { get; set; }
    }
}

