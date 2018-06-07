using Infrastructure.Data.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Administration.Entities
{
    public class User : BaseTenantEntity
    {
        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(256)]
        public string Password { get; set; }

        [Required]
        public bool EmailConfirmed { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        public DateTime LastLogIn { get; set; }

        public DateTime LastActivity { get; set; }

        [Required]
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public virtual ModulePrivilege ModulePrivilege { get; set; }

        [NotMapped]
        public string Name => $"{FirstName} {LastName}";
    }
}
