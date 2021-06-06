using FluentValidation;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureDeposit
{
    public class DepositHandlerValidator : AbstractValidator<DepositCommand>
    {
        public DepositHandlerValidator()
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