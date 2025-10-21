using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Queries.GetActivesInventoryInDefaultRisksByDefaultRiskId
{
    public class GetActivesInventoryInDefaultRisksByDefaultRiskIdQuery : IGetActivesInventoryInDefaultRisksByDefaultRiskIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetActivesInventoryInDefaultRisksByDefaultRiskIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int defaultRiskId)
        {
            try
            {
                var entities = await (from activesInventoryInDefaultRisk in _databaseService.ActivesInventoryInDefaultRisk
                                      join activesInventory in _databaseService.ActivesInventory on activesInventoryInDefaultRisk.activesInventory equals activesInventory
                                      where ((activesInventoryInDefaultRisk.isDeleted == null || activesInventoryInDefaultRisk.isDeleted == false) && activesInventoryInDefaultRisk.defaultRiskId == defaultRiskId)
                                      && (activesInventoryInDefaultRisk.activesInventory.name.ToUpper().Contains(search.ToUpper()))
                                      select new ActivesInventoryInDefaultRiskEntity
                                      {
                                          activesInventoryInDefaultRiskId = activesInventoryInDefaultRisk.activesInventoryInDefaultRiskId,
                                          defaultRiskId = activesInventoryInDefaultRisk.defaultRiskId,
                                          activesInventoryId = activesInventoryInDefaultRisk.activesInventoryId,
                                          isActive = activesInventoryInDefaultRisk.isActive,
                                          companyId = activesInventoryInDefaultRisk.companyId,
                                          activesInventory = new ActivesInventoryEntity
                                          {
                                              name = activesInventory.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetActivesInventoryInDefaultRisksByDefaultRiskIdDto> baseResponseDto = new BaseResponseDto<GetActivesInventoryInDefaultRisksByDefaultRiskIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetActivesInventoryInDefaultRisksByDefaultRiskIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, defaultRiskId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int defaultRiskId)
        {
            var total = await (from activesInventoryInDefaultRisk in _databaseService.ActivesInventoryInDefaultRisk
                               join activesInventory in _databaseService.ActivesInventory on activesInventoryInDefaultRisk.activesInventory equals activesInventory
                               where ((activesInventoryInDefaultRisk.isDeleted == null || activesInventoryInDefaultRisk.isDeleted == false) && activesInventoryInDefaultRisk.defaultRiskId == defaultRiskId)
                               && (activesInventoryInDefaultRisk.activesInventory.name.ToUpper().Contains(search.ToUpper()))
                               select new ActivesInventoryInDefaultRiskEntity
                               {
                                   activesInventoryInDefaultRiskId = activesInventoryInDefaultRisk.activesInventoryInDefaultRiskId,
                               }).CountAsync();
            return total;
        }

    }
}

