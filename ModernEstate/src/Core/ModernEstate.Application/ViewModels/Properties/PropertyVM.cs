using ModernEstate.Domain.Entities;

namespace ModernEstate.Application.ViewModels.Properties
{
    public class PropertyVM
    {
        //public ICollection<GetPropertyVM> Property { get; set; }
        // public ICollection<Category> Category { get; set; }
        // public ICollection<Status> Status { get; set; }
        // public ICollection<Types> Types { get; set; }
        // public ICollection<Slide> Slides { get; set; }

            public ICollection<GetPropertyVM> Property { get; set; } = new List<GetPropertyVM>();
            public ICollection<Category> Category { get; set; } = new List<Category>();
            public ICollection<Status> Status { get; set; } = new List<Status>();
            public ICollection<Types> Types { get; set; } = new List<Types>();
            public ICollection<Slide> Slides { get; set; } = new List<Slide>();
        }
    }

