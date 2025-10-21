using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Queries.GetAllActivesInventoryInDefaultRisksByDefaultRiskId
{
internal class GetAllActivesInventoryInDefaultRisksByDefaultRiskIdQuery : IGetAllActivesInventoryInDefaultRisksByDefaultRiskIdQuery
{
private readonly IDatabaseService _databaseService;
private readonly IMapper _mapper;
public GetAllActivesInventoryInDefaultRisksByDefaultRiskIdQuery(IDatabaseService databaseService, IMapper mapper)
{
_databaseService = databaseService;
_mapper = mapper;
}

public async Task<Object> Execute(int defaultRiskId)
{
try
{
var entities = await (from activesInventoryInDefaultRisk in _databaseService.ActivesInventoryInDefaultRisk
join activesInventory in _databaseService.ActivesInventory on activesInventoryInDefaultRisk.activesInventory equals activesInventory
where ((activesInventoryInDefaultRisk.isDeleted == null || activesInventoryInDefaultRisk.isDeleted == false) && activesInventoryInDefaultRisk.defaultRiskId == defaultRiskId)
select new ActivesInventoryInDefaultRiskEntity
{
activesInventoryInDefaultRiskId = activesInventoryInDefaultRisk.activesInventoryInDefaultRiskId,
defaultRiskId = activesInventoryInDefaultRisk.defaultRiskId,
activesInventoryId = activesInventoryInDefaultRisk.activesInventoryId,
isActive = activesInventoryInDefaultRisk.isActive,
companyId = activesInventoryInDefaultRisk.companyId,
}).ToListAsync();

BaseResponseDto<GetAllActivesInventoryInDefaultRisksByDefaultRiskIdDto> baseResponseDto = new BaseResponseDto<GetAllActivesInventoryInDefaultRisksByDefaultRiskIdDto>();
baseResponseDto.data = _mapper.Map<List<GetAllActivesInventoryInDefaultRisksByDefaultRiskIdDto>>(entities);
return baseResponseDto;
}
catch (Exception)
{
return BaseApplication.getExceptionErrorResponse();
}
}

}
}

