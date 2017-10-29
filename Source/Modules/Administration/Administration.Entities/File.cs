using Infrastructure.Data.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Administration.Entities
{
    public class File : BaseTenantEntity<int>
    {
        [Required]
        [MaxLength(100)]
        public Guid Guid { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string Path { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public StorageTypeEnum StorageType { get; set; }

        [Required]
        [MaxLength(100)]
        public string ContentType { get; set; }

        [Required]
        [MaxLength(20)]
        public string Extension { get; set; }
    }
}
