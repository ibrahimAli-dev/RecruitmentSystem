using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WazefaOn.Models
{
    public class Job
    {
        public int Id { get; set; }
        [DisplayName("Job Title")]
        public string JobTitle { get; set; }
        [DisplayName("Job jobContent ")]

        public string jobContent { get; set; }
        public int MaxApplicants { get; set; }
        public DateTime ExpiryDate { get; set; }
        [DisplayName("Is Active")]
        public bool isActive { get; set; } = true;
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }

    public class ApplyForJob
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime ApplyDate { get; set; }
        public int JobId { get; set; }
        public virtual Job job { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser user { get; set; }
    }

    public class RolesViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class JobsViewModel {
        public string JobTitle { get; set; }
        public IEnumerable<ApplyForJob> Items { get; set; }
    }

    public class ContactModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }

    }
}