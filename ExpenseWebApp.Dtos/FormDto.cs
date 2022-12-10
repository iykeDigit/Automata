using Microsoft.AspNetCore.Http;

namespace ExpenseWebApp.Dtos
{
    public class FormDto
    {
        public IFormFile File { get; set; }
    }
}
