using FluentValidation;

namespace CatalogService.Application.Items.Queries.GetItems
{
    public class GetItemsQueryValidator : AbstractValidator<GetItemsQuery>
    {
        public GetItemsQueryValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.");
        }
    }
}
