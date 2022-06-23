using System.ComponentModel.DataAnnotations;

namespace bART_Solutions_test.Models
{
    public record Contact
    {
        public int Id { get; init; }
        [Required]
        public string? FirstName { get; init; }
        [Required]
        public string? LastName { get; init; }
        [Required]
        public string? Email { get; init; }
        public ICollection<Account> Accounts { get; init; } = new List<Account>();
    }
}
