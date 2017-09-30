﻿using Infrastructure.Data.Common;
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
        public User User { get; set; }

        [Required]
        public StorageType StorageType { get; set; }

        [Required]
        [MaxLength(100)]
        public string ContentType { get; set; }

        [Required]
        [MaxLength(20)]
        public string Extension { get; set; }
    }
}
