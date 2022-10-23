namespace OnlineShop.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class TypeDto
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Name { get; set; }
    }
}