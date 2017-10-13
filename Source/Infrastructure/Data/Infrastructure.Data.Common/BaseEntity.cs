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
                // entity framework sets temporary negative number for the key
                if (Id.GetType() == typeof(int))
                {
                    return Convert.ToInt32(Id) > 0;
                }

                return Id != null && !object.Equals(Id, default(T));
            }
        }
    }
}
