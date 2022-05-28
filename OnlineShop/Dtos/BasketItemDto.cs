using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(0.1, 9999.99, ErrorMessage = "Price must be between 0.10 and 9999.99")]
        public decimal Price { get; set; }

        [Required]
        [Range(1,10 , ErrorMessage = "Quantity must be in range 1-10")]
        public int Quantity { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Type { get; set; }

    }
}