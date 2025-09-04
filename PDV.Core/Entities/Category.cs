using System.ComponentModel.DataAnnotations;

namespace PDV.Core.Entities
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Product> Products { get; set; } = new List<Product>(); // 1 : N
    }
}
