// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NET_Project.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [DisplayName("Fullname")]
        [Required(ErrorMessage = "The Fullname is required")]
        public string Fullname { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "The Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DisplayName("Phone")]
        [Required(ErrorMessage = "The Phone Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Confirm Password doesn't match")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Age")]
        [Required(ErrorMessage = "Age is required")]
        [Range(5, int.MaxValue, ErrorMessage = "Age must be higher than 4")]
        public int Age { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "Gender is required")]
        [Range(0, 3, ErrorMessage = "Error Gender field")]
        public GenderEnum Gender { get; set; }
        public enum GenderEnum
        {
            [Display(Name = "Male")]
            Male = 0,
            [Display(Name = "Female")]
            Female = 1,
            [Display(Name = "Undefined")]
            Undefined = 2,
            [Display(Name = "Private")]
            Private = 3
        }
        [DisplayName("Status")]
        [Required(ErrorMessage = "Status is required")]
        [Range(0, 4, ErrorMessage = "Error Status field")]
        public StatusEnum Status { get; set; }
        public enum StatusEnum
        {
            [Display(Name = "Inactive")]
            Inactive = 0,
            [Display(Name = "Active")]
            Active = 1,
            [Display(Name = "Locked")]
            Locked = 2,
            [Display(Name = "Banned")]
            Banned = 3,
            [Display(Name = "Disabled")]
            Disabled = 4
        }


        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime UpdatedAt { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Customer()
        {
            Status = StatusEnum.Inactive;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}