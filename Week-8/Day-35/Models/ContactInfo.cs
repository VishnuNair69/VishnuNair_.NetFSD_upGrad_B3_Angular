using System.ComponentModel.DataAnnotations;

namespace webapp25.Models
{
    public class ContactInfo
    {
        public int ContactId { get; set; }
        [Required]
        [StringLength(10,MinimumLength =5)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        public long MobileNo { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
         public int CompanyId { get; set; }
        public int? DepartmentId { get; set; }

        public string? CompanyName { get; set; }
        public string? DepartmentName { get; set; }
    }
}