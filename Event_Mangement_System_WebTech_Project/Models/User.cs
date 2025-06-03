using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Mangement_System_WebTech_Project.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }

        [Required]
        [MaxLength(100)]
        public string userName { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public byte[] passwordHash { get; set; }

        [ForeignKey("Role")]
        public int userRoleId { get; set; }

        // Nav relationship
        public Role Role { get; set; }

        public ICollection<Event> OrganizedEvents { get; set; } = new List<Event>();
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }

}

