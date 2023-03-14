using System.ComponentModel.DataAnnotations;
namespace backend.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name field is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Address field is required.")]
        public string? Address { get; set; }
        public DateTime RegisteredDate { get; set; } = DateTime.Now;
    }
}