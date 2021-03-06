﻿using Infrastructure.Data.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Administration.Entities
{
    public class Memo : BaseTenantEntity
    {
        [Required]
        public int BaseEntityId { get; set; }

        [Required]
        [MaxLength(100)]
        public string BaseEntityName { get; set; }

        [Required]
        public int EntityId { get; set; }

        [Required]
        [MaxLength(100)]
        public string EntityName { get; set; }

        public string Property { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTime? ChangedOn { get; set; }

        public int ChangedById { get; set; }

        [NotMapped]
        public string ChangedByName { get; set; }
    }
}
