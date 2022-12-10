using ExpenseWebApp.Dtos.ExpenseFormDto;
using ExpenseWebApp.Utilities.ResourceFiles;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Utilities.AppFluentValidation.ExpenseFormValidations
{
    public class ExpenseFormCreateRequestDtoValidator : AbstractValidator<ExpenseFormCreateRequestDto>
    {
        public ExpenseFormCreateRequestDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(ResourceFile.FieldNotEmpty)
                .NotNull().WithMessage(ResourceFile.NotNull);

            RuleFor(x => x.UserId)
                .NotNull().WithMessage(ResourceFile.NotNull);               

        }
    }
}
