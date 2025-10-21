using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationsByControlEvaluationId
{
    public class GetReferenceDocumentationsByControlEvaluationIdQuery : IGetReferenceDocumentationsByControlEvaluationIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetReferenceDocumentationsByControlEvaluationIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int controlEvaluationId)
        {
            try
            {
                var entities = await (from referenceDocumentation in _databaseService.ReferenceDocumentation
                                      join documentation in _databaseService.Documentation on referenceDocumentation.documentation equals documentation
                                      where ((referenceDocumentation.isDeleted == null || referenceDocumentation.isDeleted == false) && referenceDocumentation.controlEvaluationId == controlEvaluationId)
                                      && (referenceDocumentation.name.ToUpper().Contains(search.ToUpper()))
                                      select new ReferenceDocumentationEntity
                                      {
                                          referenceDocumentationId = referenceDocumentation.referenceDocumentationId,
                                          name = referenceDocumentation.name,
                                          documentationId = referenceDocumentation.documentationId,
                                          url = (referenceDocumentation.url == null) ? "" : referenceDocumentation.url,
                                          documentation = new DocumentationEntity
                                          {
                                              name = documentation.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetReferenceDocumentationsByControlEvaluationIdDto> baseResponseDto = new BaseResponseDto<GetReferenceDocumentationsByControlEvaluationIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetReferenceDocumentationsByControlEvaluationIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, controlEvaluationId), pageSize);
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int controlEvaluationId)
        {
            var total = await (from referenceDocumentation in _databaseService.ReferenceDocumentation
                               join documentation in _databaseService.Documentation on referenceDocumentation.documentation equals documentation
                               where ((referenceDocumentation.isDeleted == null || referenceDocumentation.isDeleted == false) && referenceDocumentation.controlEvaluationId == controlEvaluationId)
                               && (referenceDocumentation.name.ToUpper().Contains(search.ToUpper()))
                               select new ReferenceDocumentationEntity
                               {
                                   referenceDocumentationId = referenceDocumentation.referenceDocumentationId,
                               }).CountAsync();
            return total;
        }

    }
}

