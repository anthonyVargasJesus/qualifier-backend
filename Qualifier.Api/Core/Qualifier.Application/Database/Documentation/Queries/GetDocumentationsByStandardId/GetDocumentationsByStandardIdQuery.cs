using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Documentation.Queries.GetDocumentationsByStandardId
{
    public class GetDocumentationsByStandardIdQuery : IGetDocumentationsByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetDocumentationsByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int standardId)
        {
            try
            {
                var entities = await (from documentation in _databaseService.Documentation
                                      where ((documentation.isDeleted == null || documentation.isDeleted == false) && documentation.standardId == standardId)
                                      && (documentation.name.ToUpper().Contains(search.ToUpper()))
                                      select new DocumentationEntity
                                      {
                                          documentationId = documentation.documentationId,
                                          name = documentation.name,
                                          description = documentation.description,
                                          template = documentation.template,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetDocumentationsByStandardIdDto> baseResponseDto = new BaseResponseDto<GetDocumentationsByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetDocumentationsByStandardIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, standardId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int standardId)
        {
            var total = await (from documentation in _databaseService.Documentation
                               where ((documentation.isDeleted == null || documentation.isDeleted == false) && documentation.standardId == standardId)
                               && (documentation.name.ToUpper().Contains(search.ToUpper()))
                               select new DocumentationEntity
                               {
                                   documentationId = documentation.documentationId,
                               }).CountAsync();
            return total;
        }

    }
}

