using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Common
{
    public abstract class BaseEntity<T>
    {
        [Required]
        public T Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Required]
        public int Version { get; set; }

        [NotMapped]
        public bool IsSaved
        {
            get
            {
                return Id != null && !object.Equals(Id, default(T));
            }
        }
    }
}
