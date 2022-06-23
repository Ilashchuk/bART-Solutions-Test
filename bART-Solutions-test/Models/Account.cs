using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace bART_Solutions_test.Models
{
    public record Account
    {
        public int Id { get; init; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int ContactId { get; set; }
        public Contact? Contact { get; set; }
        public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
    }
}
