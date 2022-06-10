using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CatalogService.Application.Categories.Queries.GetCategories;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Mappings;
using CatalogService.Application.Common.Models;
using MediatR;

namespace CatalogService.Application.Items.Queries.GetItems
{
    public class GetItemsQuery : IRequest<PaginatedList<ItemDto>>
    {
        public int CategoryId { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, PaginatedList<ItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Items
                .Where(x => x.CategoryId == request.CategoryId)
                .OrderBy(x => x.Name)
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
