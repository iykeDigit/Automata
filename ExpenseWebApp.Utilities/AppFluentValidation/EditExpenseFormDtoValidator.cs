using ExpenseWebApp.Dtos;
using ExpenseWebApp.Dtos.ExpenseFormDetailsDtos;
using ExpenseWebApp.Utilities.ResourceFiles;
using FluentValidation;

namespace ExpenseWebApp.Utilities.AppFluentValidation
{
    public class EditExpenseFormDtoValidator : AbstractValidator<ExpenseFormDetailDto>
    {
        public EditExpenseFormDtoValidator()
        {
//            RuleFor(x => x.EmployeeName).NotNull().NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            RuleFor(x => x.ExpenseCategoryName).NotNull().NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            RuleFor(x => x.ExpenseAmount).GreaterThan(0).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            RuleFor(x => x.ExpenseDate).NotNull().NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            RuleFor(x => x.ExpenseNote).NotNull().NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);

        }
    }
}
