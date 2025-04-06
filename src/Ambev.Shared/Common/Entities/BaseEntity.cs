﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ambev.Shared.Common.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        [JsonPropertyOrder(-1)]
        [Column(Order = 0)]
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? Deleted { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}