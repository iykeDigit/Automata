using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Data.UnitOfWork.Abstractions;
using ExpenseWebApp.Utilities;
using ExpenseWebApp.Utilities.ResourceFiles;
using System.IO;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Implementation
{
    public class ExpenseFormDetailsService : IExpenseFormDetails
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExpenseFormDetailsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       
        public async Task<Response<string>> DeleteExpenseDetail(string expenseDetailId) 
        {
            var expenseDetail = await _unitOfWork.ExpenseFormDetails.GetExpenseFormDetailsById(expenseDetailId);

           
            if (expenseDetail != null && expenseDetail.ExpenseForm.ExpenseStatus.Description.ToLower() == FormStatus.NewRequest.ToLower())
            {
                _unitOfWork.ExpenseFormDetails.Delete(expenseDetail);
                await _unitOfWork.SaveAsync();
                DeleteAttachment(expenseDetail.Attachments);
                return Response<string>.Success(ResourceFile.Success, $"Form detail with Id: {expenseDetailId} was deleted successfully");
            }
            return Response<string>.Fail(ResourceFile.ExpenseDetailNotFound);
        }

        public static void DeleteAttachment(string filePath)
        {
            if (File.Exists(filePath))  File.Delete(filePath);
        }
    }
}