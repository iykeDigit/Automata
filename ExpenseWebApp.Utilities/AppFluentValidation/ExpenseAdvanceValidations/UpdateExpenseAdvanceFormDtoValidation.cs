using ExpenseWebApp.Dtos.ExpenseAdvanceDtos;
using ExpenseWebApp.Utilities.ResourceFiles;
using FluentValidation;

namespace ExpenseWebApp.Utilities.AppFluentValidation.ExpenseAdvanceValidations
{
    public class UpdateExpenseAdvanceFormDtoValidation : AbstractValidator<UpdateExpenseAdvanceDto>
    {
        public UpdateExpenseAdvanceFormDtoValidation()
        {
            RuleFor(x => x.ExpenseStatusDescription).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            RuleFor(x => x.ApprovedBy).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            RuleFor(x => x.AdvanceFormNo).NotEmpty().WithMessage(ResourceFile.FieldNotEmpty);
            RuleFor(x => x.ExpenseStatusDescription).Matches($"{FormStatus.Approved}|{FormStatus.Rejected}|{FormStatus.FurtherInfoRequired}")
                                         .WithMessage(ResourceFile.InvalidStatus);
        }
    }
}
