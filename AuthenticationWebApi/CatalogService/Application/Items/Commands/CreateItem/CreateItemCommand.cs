using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Items.Commands.CreateItem
{
    public class CreateItemCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }

        public int CategoryId { get; set; }
    }

    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            if (!_context.Categories.Any(x => x.Id == request.CategoryId))
            {
                throw new NotFoundException(nameof(Category), request.CategoryId);
            }

            var entity = new Item
            {
                Name = request.Name,
                Description = request.Description,
                Amount = request.Amount,
                Image = request.Image,
                Price = request.Price,
                CategoryId = request.CategoryId
            };

            _context.Items.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
