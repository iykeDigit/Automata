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
    public class DeleteExpenseFormsByStatusDtoValidator : AbstractValidator<DeleteExpenseFormsByStatusDto>
    {
        public DeleteExpenseFormsByStatusDtoValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            RuleFor(x => x.Status).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
        }
    }
}
