using System.ComponentModel.DataAnnotations;

namespace ExpenseWebApp.Dtos.ExpenseAdvanceDtos
{
    public class UpdateExpenseAdvanceDto
    {
        public string AdvanceFormNo { get; set; }
        public string ApprovedBy { get; set; }
        public string ApproverNote { get; set; }
        public string ExpenseStatusDescription { get; set; }
    }
}