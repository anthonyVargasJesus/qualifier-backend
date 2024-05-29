using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActiveType.Queries.GetActiveTypesByCompanyId
{
    public class GetActiveTypesByCompanyIdQuery : IGetActiveTypesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetActiveTypesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from activeType in _databaseService.ActiveType
                                      where ((activeType.isDeleted == null || activeType.isDeleted == false) && activeType.companyId == companyId)
                                      && (activeType.name.ToUpper().Contains(search.ToUpper()))
                                      select new ActiveTypeEntity
                                      {
                                          activeTypeId = activeType.activeTypeId,
                                          name = activeType.name,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetActiveTypesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetActiveTypesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetActiveTypesByCompanyIdDto>>(entities);
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
            var total = await (from activeType in _databaseService.ActiveType
                               where ((activeType.isDeleted == null || activeType.isDeleted == false) && activeType.companyId == companyId)
                               && (activeType.name.ToUpper().Contains(search.ToUpper()))
                               select new ActiveTypeEntity
                               {
                                   activeTypeId = activeType.activeTypeId,
                               }).CountAsync();
            return total;
        }

    }
}

