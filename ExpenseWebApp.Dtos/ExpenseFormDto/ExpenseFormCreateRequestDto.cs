using System.ComponentModel.DataAnnotations;

namespace ExpenseWebApp.Dtos.ExpenseFormDto
{
    public class ExpenseFormCreateRequestDto
    {
        public int UserId { get; set; }
        [Required]
        public string Description { get; set; }
        public string Token { get; set; }
    }
}
