using FluentValidation;

namespace CatalogService.Application.Categories.Queries.GetCategoriesByIds
{
    public class GetCategoriesByIdsValidator : AbstractValidator<GetCategoriesByIdsQuery>
    {
        public GetCategoriesByIdsValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty()
                .WithMessage("Ids are required.");
        }
    }
}
