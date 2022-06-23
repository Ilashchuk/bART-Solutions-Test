using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace bART_Solutions_test.Models
{
    public record Account
    {
        public int Id { get; init; }
        [Required]
        public string? Name { get; init; }
        [Required]
        public int ContactId { get; init; }
        public Contact? Contact { get; init; }
        public ICollection<Incident> Incidents { get; init; } = new List<Incident>();
    }
}
