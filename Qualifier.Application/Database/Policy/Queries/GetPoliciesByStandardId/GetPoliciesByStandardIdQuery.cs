using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Policy.Queries.GetPoliciesByStandardId
{
    public class GetPoliciesByStandardIdQuery : IGetPoliciesByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetPoliciesByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int standardId)
        {
            try
            {
                var entities = await (from policy in _databaseService.Policy
                                      where ((policy.isDeleted == null || policy.isDeleted == false) && policy.standardId == standardId)
                                      && (policy.name.ToUpper().Contains(search.ToUpper()))
                                      select new PolicyEntity
                                      {
                                          policyId = policy.policyId,
                                          isCurrent = policy.isCurrent,
                                          date = policy.date,
                                          name = policy.name,
                                          description = policy.description,
                                          standardId = policy.standardId,
                                          companyId = policy.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetPoliciesByStandardIdDto> baseResponseDto = new BaseResponseDto<GetPoliciesByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetPoliciesByStandardIdDto>>(entities);
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
            var total = await (from policy in _databaseService.Policy
                               where ((policy.isDeleted == null || policy.isDeleted == false) && policy.standardId == standardId)
                               && (policy.name.ToUpper().Contains(search.ToUpper()))
                               select new PolicyEntity
                               {
                                   policyId = policy.policyId,
                               }).CountAsync();
            return total;
        }

    }
}

