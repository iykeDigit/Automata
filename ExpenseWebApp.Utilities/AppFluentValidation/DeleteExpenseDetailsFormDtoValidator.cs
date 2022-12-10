using ExpenseWebApp.Dtos.ExpenseFromDto;
using ExpenseWebApp.Utilities.ResourceFiles;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Utilities.AppFluentValidation
{
    public class DeleteExpenseDetailsFormDtoValidator : AbstractValidator<DeleteExpenseDetailsFormDto>
    {
        public DeleteExpenseDetailsFormDtoValidator()
        {
            RuleFor(x => x.userId).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            RuleFor(x => x.companyId).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            RuleFor(x => x.expenseFormDetailsId).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            RuleFor(x => x.expenseFormNumber).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
        }
    }
}
