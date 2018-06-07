using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Common
{
    public abstract class BaseEntity
    {
        [Required]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Required]
        public int Version { get; set; }

        [NotMapped]
        public bool IsSaved => Id != default(int) && Id > 0;

        public override string ToString() => IsSaved ? $"{GetType().Name} - {Id}" : GetType().Name;
    }
}
