using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementsByDocumentationId
{
    public class GetSupportForRequirementsByDocumentationIdQuery : IGetSupportForRequirementsByDocumentationIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetSupportForRequirementsByDocumentationIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int documentationId)
        {
            try
            {
                var entities = await (from supportForRequirement in _databaseService.SupportForRequirement
                                      join requirement in _databaseService.Requirement on supportForRequirement.requirement equals requirement
                                      where ((supportForRequirement.isDeleted == null || supportForRequirement.isDeleted == false) && supportForRequirement.documentationId == documentationId)
                                      && (requirement.name.ToUpper().Contains(search.ToUpper()))
                                      select new SupportForRequirementEntity
                                      {
                                          supportForRequirementId = supportForRequirement.supportForRequirementId,
                                          documentationId = supportForRequirement.documentationId,
                                          requirementId = supportForRequirement.requirementId,
                                          requirement = new RequirementEntity
                                          {
                                              numeration = requirement.numeration,
                                              name = requirement.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetSupportForRequirementsByDocumentationIdDto> baseResponseDto = new BaseResponseDto<GetSupportForRequirementsByDocumentationIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetSupportForRequirementsByDocumentationIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, documentationId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int documentationId)
        {
            var total = await (from supportForRequirement in _databaseService.SupportForRequirement
                               join requirement in _databaseService.Requirement on supportForRequirement.requirement equals requirement
                               where ((supportForRequirement.isDeleted == null || supportForRequirement.isDeleted == false) && supportForRequirement.documentationId == documentationId)
                               && (requirement.name.ToUpper().Contains(search.ToUpper()))
                               select new SupportForRequirementEntity
                               {
                                   supportForRequirementId = supportForRequirement.supportForRequirementId,
                               }).CountAsync();
            return total;
        }

    }
}

