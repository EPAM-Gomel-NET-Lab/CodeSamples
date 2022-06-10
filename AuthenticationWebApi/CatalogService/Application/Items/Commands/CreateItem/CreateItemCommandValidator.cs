using FluentValidation;

namespace CatalogService.Application.Items.Commands.CreateItem
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Amount)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
