﻿namespace ModernEstateDemo.Models
{
    public class PropertyFeature
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int PropertyId { get; set; }
        public int FeatureId { get; set; }
        public Property Property { get; set; }
        public Feature Feature { get; set; }
    }
}
