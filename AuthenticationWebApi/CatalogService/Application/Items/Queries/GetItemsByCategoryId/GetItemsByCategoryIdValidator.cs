using FluentValidation;

namespace CatalogService.Application.Items.Queries.GetItemsByCategoryId
{
    public class GetItemsByCategoryIdValidator : AbstractValidator<GetItemsByCategoryIdQuery>
    {
        public GetItemsByCategoryIdValidator()
        {
            RuleFor(x => x.CategoryIds)
                .NotEmpty()
                .WithMessage("CategoryIds are required.");
        }
    }
}