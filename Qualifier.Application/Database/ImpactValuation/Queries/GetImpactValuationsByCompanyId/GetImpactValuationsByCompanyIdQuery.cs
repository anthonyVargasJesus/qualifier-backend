using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ImpactValuation.Queries.GetImpactValuationsByCompanyId
{
    public class GetImpactValuationsByCompanyIdQuery : IGetImpactValuationsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetImpactValuationsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from impactValuation in _databaseService.ImpactValuation
                                      where ((impactValuation.isDeleted == null || impactValuation.isDeleted == false) && impactValuation.companyId == companyId)
                                      && (impactValuation.name.ToUpper().Contains(search.ToUpper()))
                                      select new ImpactValuationEntity
                                      {
                                          impactValuationId = impactValuation.impactValuationId,
                                          abbreviation = impactValuation.abbreviation,
                                          name = impactValuation.name,
                                          minimumValue = impactValuation.minimumValue,
                                          maximumValue = impactValuation.maximumValue,
                                          defaultValue = impactValuation.defaultValue,
                                          companyId = impactValuation.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetImpactValuationsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetImpactValuationsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetImpactValuationsByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from impactValuation in _databaseService.ImpactValuation
                               where ((impactValuation.isDeleted == null || impactValuation.isDeleted == false) && impactValuation.companyId == companyId)
                               && (impactValuation.name.ToUpper().Contains(search.ToUpper()))
                               select new ImpactValuationEntity
                               {
                                   impactValuationId = impactValuation.impactValuationId,
                               }).CountAsync();
            return total;
        }

    }
}

