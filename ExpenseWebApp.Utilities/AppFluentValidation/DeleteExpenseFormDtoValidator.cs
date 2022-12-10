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
    public class DeleteExpenseFormDtoValidator : AbstractValidator<DeleteExpenseFormDto>
    {
        public DeleteExpenseFormDtoValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            RuleFor(x => x.FormNumber).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
        }
    }
}
