using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseWebApp.Dtos
{
    public class PagingDto
    {
        [Required(ErrorMessage = "The page size is required")]
        public int PageSize { get; set; }
        [Required(ErrorMessage = "The page number is required")]
        public int PageNumber { get; set; }
    }
}
