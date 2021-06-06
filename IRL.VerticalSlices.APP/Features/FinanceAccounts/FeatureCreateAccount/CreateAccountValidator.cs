using FluentValidation;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureCreateAccount
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountValidator()
        {
            RuleFor(a => a.CustomerCode)
                .MaximumLength(100)
                .NotEmpty();
        }
    }
}