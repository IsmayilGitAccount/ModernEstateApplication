﻿namespace ModernEstate.MVC.Areas.Admin.ViewModels.Agencies
{
    public class GetAdminAgencyVM
    {
        public int Id { get; set; }
        public string AgencyName { get; set; }
        public double TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
