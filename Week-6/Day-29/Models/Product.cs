
using System.ComponentModel.DataAnnotations;

public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength (15,MinimumLength =5)]
         public string Name { get; set; }
         [StringLength (15,MinimumLength =5)]
        public string Category { get; set; }
        [Required]
        public double Price { get; set; }
    }