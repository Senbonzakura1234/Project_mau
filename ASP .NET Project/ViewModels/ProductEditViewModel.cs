using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ASP.NET_Project.Models;

namespace ASP.NET_Project.ViewModels
{
    public class ProductEditViewModel
    {
        public ProductEditViewModel()
        {
            GetListCategory();
            GetListBrand();
        }

        public ProductEditViewModel(int id, string name, string description,
            double price, int inStoke, int cateId, int brandId, string picture)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            InStoke = inStoke;
            Picture = picture;

            CateId = cateId;
            BrandId = brandId;

            GetListCategory();
            GetListBrand();
        }
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "The Name is required")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "The Description is required")]
        public string Description { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "The Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "The Price cannot be less than 0")]
        public double Price { get; set; }

        [DisplayName("InStoke")]
        [Required(ErrorMessage = "The InStoke is required")]
        [Range(0, int.MaxValue, ErrorMessage = "The InStoke must be greater than or equal 0")]
        public int InStoke { get; set; }

        [DisplayName("Picture")]
        [Required(ErrorMessage = "The Picture is required")]
        public string Picture { get; set; }

        [DisplayName("Category")]
        public int CateId { get; set; }

        [DisplayName("Brand")]
        public int BrandId { get; set; }

        public IEnumerable<Category> CategoriesList { get; set; }
        public IEnumerable<Brand> BrandsList { get; set; }

        public void GetListCategory()
        {
            using (var db = new MyDbContext())
            {
                CategoriesList = db.Categories.ToList().AsEnumerable();
            }

        }
        public void GetListBrand()
        {
            using (var db = new MyDbContext())
            {
                BrandsList = db.Brands.ToList().AsEnumerable();
            }
        }
    }
}