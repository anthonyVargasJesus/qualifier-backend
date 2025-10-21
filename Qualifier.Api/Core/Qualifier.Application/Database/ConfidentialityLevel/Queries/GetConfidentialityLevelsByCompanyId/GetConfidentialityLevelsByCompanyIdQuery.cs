using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ConfidentialityLevel.Queries.GetConfidentialityLevelsByCompanyId
{
    public class GetConfidentialityLevelsByCompanyIdQuery : IGetConfidentialityLevelsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetConfidentialityLevelsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from confidentialityLevel in _databaseService.ConfidentialityLevel
                                      where ((confidentialityLevel.isDeleted == null || confidentialityLevel.isDeleted == false) && confidentialityLevel.companyId == companyId)
                                      && (confidentialityLevel.name.ToUpper().Contains(search.ToUpper()))
                                      select new ConfidentialityLevelEntity
                                      {
                                          confidentialityLevelId = confidentialityLevel.confidentialityLevelId,
                                          name = confidentialityLevel.name,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetConfidentialityLevelsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetConfidentialityLevelsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetConfidentialityLevelsByCompanyIdDto>>(entities);
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
            var total = await (from confidentialityLevel in _databaseService.ConfidentialityLevel
                               where ((confidentialityLevel.isDeleted == null || confidentialityLevel.isDeleted == false) && confidentialityLevel.companyId == companyId)
                               && (confidentialityLevel.name.ToUpper().Contains(search.ToUpper()))
                               select new ConfidentialityLevelEntity
                               {
                                   confidentialityLevelId = confidentialityLevel.confidentialityLevelId,
                               }).CountAsync();
            return total;
        }

    }
}

