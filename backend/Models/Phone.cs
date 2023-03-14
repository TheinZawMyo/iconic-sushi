using System.ComponentModel.DataAnnotations;
namespace backend.Models
{
    public class Phone
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Model field is required.")]
        [MaxLength(40)]
        public string? Model { get; set; }

        [Required(ErrorMessage = "Company field is required")]
        [Range(1, 6)]
        public int CompanyId { get; set; }

        public long Quantity { get; set; } = 0;
        
        [Required(ErrorMessage = "Price field is required")]
        public long? PricePerItem { get; set; }

        public string? Specs { get; set; }
        // public bool Stock { get; set; } = true;
        public DateTime DateOfImport { get; set; } = DateTime.Now;
    }
}