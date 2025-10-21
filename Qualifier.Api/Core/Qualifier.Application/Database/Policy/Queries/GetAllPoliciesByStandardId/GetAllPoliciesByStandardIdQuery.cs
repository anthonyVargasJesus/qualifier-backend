using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Policy.Queries.GetAllPoliciesByStandardId
{
    internal class GetAllPoliciesByStandardIdQuery : IGetAllPoliciesByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllPoliciesByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int standardId)
        {
            try
            {
                var entities = await (from policy in _databaseService.Policy
                                      where ((policy.isDeleted == null || policy.isDeleted == false) && policy.standardId == standardId)
                                      select new PolicyEntity
                                      {
                                          policyId = policy.policyId,
                                          isCurrent = policy.isCurrent,
                                          date = policy.date,
                                          name = policy.name,
                                          description = policy.description,
                                          standardId = policy.standardId,
                                          companyId = policy.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllPoliciesByStandardIdDto> baseResponseDto = new BaseResponseDto<GetAllPoliciesByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllPoliciesByStandardIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

