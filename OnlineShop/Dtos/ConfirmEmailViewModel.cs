using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Dtos
{
    public class ConfirmEmailViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Token { get; set; }
    }
}