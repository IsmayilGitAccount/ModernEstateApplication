﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ModernEstate.Domain.Entities;

namespace ModernEstate.MVC.Areas.Admin.ViewModels.Services
{
    public class UpdateAdminServiceVM
    {
        public IFormFile? Photo { get; set; }

        [Required(ErrorMessage = "Please enter title!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter description!")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter agency name!")]
        public int? AgencyId { get; set; }
        public List<Agency> Agencies { get; set; }
    }
}
