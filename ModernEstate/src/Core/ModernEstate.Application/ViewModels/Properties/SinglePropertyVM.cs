﻿using ModernEstate.Domain.Entities;

namespace ModernEstate.Application.ViewModels.Properties
{
    public class SinglePropertyVM
    {
        public Property Property { get; set; }
        public ICollection<PropertyPhoto> PropertyPhotos { get; set; }
        public ICollection<Property> RecentlyProperties { get; set; }
    }
}
