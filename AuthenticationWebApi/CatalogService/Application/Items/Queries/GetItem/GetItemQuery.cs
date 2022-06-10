using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CatalogService.Application.Categories.Queries.GetCategories;
using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Items.Queries.GetItems
{
    public class GetItemQuery : IRequest<ItemDto>
    {
        public int Id { get; set; }
    }

    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ItemDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ItemDto> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Items.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Item), request.Id);
            }

            return _mapper.Map<ItemDto>(entity);
        }
    }
}
