using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CatalogService.Application.Categories.Queries.GetCategories;
using CatalogService.Application.Common.Interfaces;
using MediatR;

namespace CatalogService.Application.Categories.Queries.GetCategoriesByIds
{
    public class GetCategoriesByIdsQuery : IRequest<IDictionary<int, CategoryDto>>
    {
        public List<int> Ids { get; set; }
    }

    public class GetCategoriesByIdsQueryHandler : IRequestHandler<GetCategoriesByIdsQuery, IDictionary<int, CategoryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesByIdsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IDictionary<int, CategoryDto>> Handle(GetCategoriesByIdsQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_context.Categories
                .Where(x => request.Ids.Contains(x.Id))
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToDictionary(x => x.Id));
        }
    }
}
