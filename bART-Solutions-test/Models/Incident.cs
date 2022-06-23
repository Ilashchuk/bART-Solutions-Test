using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bART_Solutions_test.Models
{
    public record Incident
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Name { get; init; }
        public string? Description { get; init; }
        [Required]
        public int AccountId { get; set; }
        public Account? Account { get; set; }
    }
}
