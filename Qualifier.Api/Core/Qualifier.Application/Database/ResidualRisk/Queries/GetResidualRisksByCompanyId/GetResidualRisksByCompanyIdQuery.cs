using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ResidualRisk.Queries.GetResidualRisksByCompanyId
{
    public class GetResidualRisksByCompanyIdQuery : IGetResidualRisksByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetResidualRisksByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from residualRisk in _databaseService.ResidualRisk
                                      where ((residualRisk.isDeleted == null || residualRisk.isDeleted == false) && residualRisk.companyId == companyId)
                                      && (residualRisk.name.ToUpper().Contains(search.ToUpper()))
                                      select new ResidualRiskEntity
                                      {
                                          residualRiskId = residualRisk.residualRiskId,
                                          name = residualRisk.name,
                                          description = residualRisk.description,
                                          abbreviation = residualRisk.abbreviation,
                                          factor = residualRisk.factor,
                                          minimum = residualRisk.minimum,
                                          maximum = residualRisk.maximum,
                                          color = residualRisk.color,
                                          companyId = residualRisk.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetResidualRisksByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetResidualRisksByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetResidualRisksByCompanyIdDto>>(entities);
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
            var total = await (from residualRisk in _databaseService.ResidualRisk
                               where ((residualRisk.isDeleted == null || residualRisk.isDeleted == false) && residualRisk.companyId == companyId)
                               && (residualRisk.name.ToUpper().Contains(search.ToUpper()))
                               select new ResidualRiskEntity
                               {
                                   residualRiskId = residualRisk.residualRiskId,
                               }).CountAsync();
            return total;
        }

    }
}

