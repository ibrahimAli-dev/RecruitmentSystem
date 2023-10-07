using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WazefaOn.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [DisplayName("Description")]
        public string CategoryDescription { get; set; }

        [Required]
        [DisplayName("Title")]
        public string JobTitle { get; set; }
        [Required]
        [DisplayName("Content")]
        public string JobContent { get; set; }

        [Required]
        public virtual ICollection<Job> Jobs { get; set; }
    }
}