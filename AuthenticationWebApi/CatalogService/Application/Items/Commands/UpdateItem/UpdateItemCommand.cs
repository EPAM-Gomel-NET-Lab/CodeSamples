using System.Threading;
using System.Threading.Tasks;
using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CatalogService.Application.Items.Commands.UpdateItem
{
    public class UpdateItemCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }
    }

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateItemCommandHandler> _logger;

        public UpdateItemCommandHandler(IApplicationDbContext context,
            ILogger<UpdateItemCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Items.FindAsync(request.Id);

            if (entity == null)
            {
                _logger.LogInformation($"The item with id {request.Id} was not found.");

                throw new NotFoundException(nameof(Item), request.Id);
            }

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.Amount = request.Amount;
            entity.Image = request.Image;
            entity.Price = request.Price;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Raising ItemChanged event for the item with id {request.Id}.");

            _logger.LogInformation($"ItemChanged event for the item with id {request.Id} was raised.");

            return Unit.Value;
        }
    }
}
