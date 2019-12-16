// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NET_Project.Models
{
    public class Category
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "The Name is required")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "The Description is required")]
        public string Description { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime UpdatedAt { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public CategoryStatusEnum CategoryStatus { get; set; }

        public enum CategoryStatusEnum
        {
            [Display(Name = "Deleted")]
            Deleted = 0,
            [Display(Name = "Show")]
            Show = 1
        }

        public Category()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            CategoryStatus = CategoryStatusEnum.Show;
        }
    }
}