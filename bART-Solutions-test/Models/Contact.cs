using System.ComponentModel.DataAnnotations;

namespace bART_Solutions_test.Models
{
    public record Contact
    {
        public int Id { get; init; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        public ICollection<Account>? Accounts { get; set; } = new List<Account>();
    }
}
