using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Documentation.Queries.GetDocumentationsByCompanyId
{
    public class GetDocumentationsByCompanyIdQuery : IGetDocumentationsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetDocumentationsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from documentation in _databaseService.Documentation
                                      join documentType in _databaseService.DocumentType on documentation.documentType equals documentType
                                      join standard in _databaseService.Standard on documentation.standard equals standard
                                      where ((documentation.isDeleted == null || documentation.isDeleted == false) && documentation.companyId == companyId)
                                      && (documentation.name.ToUpper().Contains(search.ToUpper()))
                                      select new DocumentationEntity
                                      {
                                          documentationId = documentation.documentationId,
                                          name = documentation.name,
                                          description = documentation.description,
                                          standardId = documentation.standardId,
                                          documentType = new DocumentTypeEntity
                                          {
                                              name = documentType.name,
                                          },

                                          standard = new StandardEntity
                                          {
                                              name = standard.name,
                                          },
                                      })
                                      .OrderBy(e => e.name)
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetDocumentationsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetDocumentationsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetDocumentationsByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from documentation in _databaseService.Documentation
                               join documentType in _databaseService.DocumentType on documentation.documentType equals documentType
                               join standard in _databaseService.Standard on documentation.standard equals standard
                               where ((documentation.isDeleted == null || documentation.isDeleted == false) && documentation.companyId == companyId)
                               && (documentation.name.ToUpper().Contains(search.ToUpper()))
                               select new DocumentationEntity
                               {
                                   documentationId = documentation.documentationId,
                               }).CountAsync();
            return total;
        }

    }
}

