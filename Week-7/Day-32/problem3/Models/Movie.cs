using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Movie
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Genre { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

   [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    [Required]
    public string Rating { get; set; }
}
