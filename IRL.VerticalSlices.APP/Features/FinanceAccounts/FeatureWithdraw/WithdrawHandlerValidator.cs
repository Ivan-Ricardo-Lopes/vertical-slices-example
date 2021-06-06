using FluentValidation;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureWithdraw
{
    public class WithdrawHandlerValidator : AbstractValidator<WithdrawCommand>
    {
        public WithdrawHandlerValidator()
        {
            RuleFor(a => a.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");

            RuleFor(a => a.CustomerCode)
                .NotEmpty();

            RuleFor(a => a.Description)
                .MaximumLength(100)
                .NotEmpty();
        }
    }
}