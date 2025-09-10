using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Queries.GetActivesInventoryInDefaultRiskById
{
public class GetActivesInventoryInDefaultRiskByIdQuery : IGetActivesInventoryInDefaultRiskByIdQuery
{
private readonly IDatabaseService _databaseService;
private readonly IMapper _mapper;

public GetActivesInventoryInDefaultRiskByIdQuery(IDatabaseService databaseService, IMapper mapper)
{
_databaseService = databaseService;
_mapper = mapper;
}
public async Task<Object> Execute(int activesInventoryInDefaultRiskId)
{
try
{
var entity = await (from item in _databaseService.ActivesInventoryInDefaultRisk
where ((item.isDeleted == null || item.isDeleted == false) && item.activesInventoryInDefaultRiskId == activesInventoryInDefaultRiskId)
select new ActivesInventoryInDefaultRiskEntity()
{
activesInventoryInDefaultRiskId = item.activesInventoryInDefaultRiskId,
defaultRiskId = item.defaultRiskId,
activesInventoryId = item.activesInventoryId,
isActive = item.isActive,
companyId = item.companyId,
}).FirstOrDefaultAsync();

 return _mapper.Map<GetActivesInventoryInDefaultRiskByIdDto>(entity);
}
catch (Exception)
{
return BaseApplication.getExceptionErrorResponse();
}
}

}
}

