using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationsByRequirementEvaluationId
{
    public class GetReferenceDocumentationsByRequirementEvaluationIdQuery : IGetReferenceDocumentationsByRequirementEvaluationIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetReferenceDocumentationsByRequirementEvaluationIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int requirementEvaluationId)
        {
            try
            {
                var entities = await (from referenceDocumentation in _databaseService.ReferenceDocumentation
                                      join documentation in _databaseService.Documentation on referenceDocumentation.documentation equals documentation into documentationJoin
                                      from documentation in documentationJoin.DefaultIfEmpty()
                                      join user in _databaseService.User on referenceDocumentation.creationUserId equals user.userId into userJoin
                                      from user in userJoin.DefaultIfEmpty()
                                      where ((referenceDocumentation.isDeleted == null || referenceDocumentation.isDeleted == false) && referenceDocumentation.requirementEvaluationId == requirementEvaluationId)
                                      && (referenceDocumentation.name.ToUpper().Contains(search.ToUpper()))
                                      select new ReferenceDocumentationEntity
                                      {
                                          referenceDocumentationId = referenceDocumentation.referenceDocumentationId,
                                          name = referenceDocumentation.name,
                                          url = (referenceDocumentation.url == null) ? "" : referenceDocumentation.url,
                                          description = referenceDocumentation.description == null ? "" : referenceDocumentation.description,
                                          evidenceType = referenceDocumentation.evidenceType,
                                          fileSizeBytes = referenceDocumentation.fileSizeBytes,
                                          creationDate = referenceDocumentation.creationDate,
                                          creationUserEmail = user == null ? null : user.email,
                                          isObsolete = referenceDocumentation.isObsolete,
                                          documentation = documentation == null ? null : new DocumentationEntity
                                          {
                                              name = documentation.name,
                                          },
                                      })
                                      .OrderBy(x => x.name)
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetReferenceDocumentationsByRequirementEvaluationIdDto> baseResponseDto = new BaseResponseDto<GetReferenceDocumentationsByRequirementEvaluationIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetReferenceDocumentationsByRequirementEvaluationIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, requirementEvaluationId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int requirementEvaluationId)
        {
            var total = await (from referenceDocumentation in _databaseService.ReferenceDocumentation
                               where ((referenceDocumentation.isDeleted == null || referenceDocumentation.isDeleted == false) && referenceDocumentation.requirementEvaluationId == requirementEvaluationId)
                               && (referenceDocumentation.name.ToUpper().Contains(search.ToUpper()))
                               select new ReferenceDocumentationEntity
                               {
                                   referenceDocumentationId = referenceDocumentation.referenceDocumentationId,
                               }).CountAsync();
            return total;
        }

    }
}

