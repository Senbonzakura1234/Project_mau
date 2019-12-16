using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ASP.NET_Project.ViewModels
{
    public class CategoryViewModel
    {
        public  CategoryViewModel()
        {
        }
        public  CategoryViewModel(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}