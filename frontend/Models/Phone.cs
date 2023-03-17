using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class Phone
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Model name is required.")]
        [StringLength(40, ErrorMessage = "Model name is too long")]
        public string? Model { get; set; }

        public int CompanyId { get; set; } = 1;
        
        public long Quantity { get; set; } = 0;

        public decimal PricePerItem { get; set; } = 1.00m;

        public string? Specs { get; set; }  
        // public bool Stock { get; set; } = true;
        public DateTime DateOfImport { get; set; } = DateTime.Now;
    }
}