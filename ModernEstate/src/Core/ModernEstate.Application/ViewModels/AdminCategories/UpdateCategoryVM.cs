﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ModernEstate.MVC.Areas.Admin.ViewModels.Categories
{
    public class UpdateAdminCategoryVM
    {
        [Required(ErrorMessage = "Please enter category name!")]
        public string CategoryName { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
