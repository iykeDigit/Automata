using ExpenseWebApp.Dtos.ExpenseAdvanceDtos;
using ExpenseWebApp.Utilities.ResourceFiles;
using FluentValidation;
using System;

namespace ExpenseWebApp.Utilities.AppFluentValidation.ExpenseAdvanceValidations
{
    public class CreateExpenseAdvanceDtoValidation : AbstractValidator<CreateExpenseAdvanceDto>
    {
        public CreateExpenseAdvanceDtoValidation()
        {
            RuleFor(x => x.AdvanceNote).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            //RuleFor(x => x.AdvanceAmount).GreaterThan(1).WithMessage(ResourceFile.InvalidData);
            RuleFor(x => x.AdvanceDate).LessThan(DateTime.Now).WithMessage(ResourceFile.InvalidDate);
            RuleFor(x => x.AdvanceDescription).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
        }
    }
}
