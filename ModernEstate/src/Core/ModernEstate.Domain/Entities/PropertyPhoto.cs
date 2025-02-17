﻿using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class PropertyPhoto : BaseEntity
    {
        public string Photo { get; set; }
        public bool? IsPrimary { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
