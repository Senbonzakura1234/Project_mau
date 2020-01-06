using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP.NET_Project.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public DateTime Birthday { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime UpdatedAt { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DeletedAt { get; set; }

        public UserStatusEnum UserStatus { get; set; }

        public enum UserStatusEnum
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

        public User()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            UserStatus = UserStatusEnum.Active;
        }
    }
}