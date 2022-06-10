using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CatalogService.Application.Categories.Queries.GetCategories;
using CatalogService.Application.Common.Interfaces;
using MediatR;

namespace CatalogService.Application.Items.Queries.GetItemsByCategoryId
{
    public class GetItemsByCategoryIdQuery : IRequest<ILookup<int, ItemDto>>
    {
        public List<int> CategoryIds { get; set; }
    }

    public class GetItemsByCategoryIdQueryHandler : IRequestHandler<GetItemsByCategoryIdQuery, ILookup<int, ItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemsByCategoryIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ILookup<int, ItemDto>> Handle(GetItemsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_context.Items
                .Where(x => request.CategoryIds.Contains(x.CategoryId))
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Name)
                .ToLookup(x => x.CategoryId));
        }
    }
}
