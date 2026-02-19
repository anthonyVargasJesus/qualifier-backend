using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActivesInventory.Queries.GetActivesInventoriesByCompanyId
{
    public class GetActivesInventoriesByCompanyIdQuery : IGetActivesInventoriesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetActivesInventoriesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from activesInventory in _databaseService.ActivesInventory
                                      join activeType in _databaseService.ActiveType on activesInventory.activeType equals activeType
                                      join custodian in _databaseService.Custodian on activesInventory.custodian equals custodian
                                      join location in _databaseService.Location on activesInventory.location equals location
                                      join macroprocess in _databaseService.Macroprocess on activesInventory.macroprocess equals macroprocess
                                      join owner in _databaseService.Owner on activesInventory.owner equals owner
                                      join subprocess in _databaseService.Subprocess on activesInventory.subprocess equals subprocess
                                      join supportType in _databaseService.SupportType on activesInventory.supportType equals supportType
                                      join usageClassification in _databaseService.UsageClassification on activesInventory.usageClassification equals usageClassification
                                      where ((activesInventory.isDeleted == null || activesInventory.isDeleted == false) && activesInventory.companyId == companyId)
                                      && (activesInventory.name.ToUpper().Contains(search.ToUpper()))
                                      select new ActivesInventoryEntity
                                      {
                                          activesInventoryId = activesInventory.activesInventoryId,
                                          number = activesInventory.number,
                                          name = activesInventory.name,
                                          description = activesInventory.description,
                                          valuation = activesInventory.valuation,
                                          activeType = new ActiveTypeEntity
                                          {
                                              name = activeType.name,
                                          },
                                          custodian = new CustodianEntity
                                          {
                                              name = custodian.name,
                                          },
                                          location = new LocationEntity
                                          {
                                              abbreviation = location.abbreviation,
                                              name = location.name,
                                          },
                                          macroprocess = new MacroprocessEntity
                                          {
                                              code = macroprocess.code,
                                              name = macroprocess.name,
                                          },
                                          owner = new OwnerEntity
                                          {
                                              code = owner.code,
                                              name = owner.name,
                                          },
                                          subprocess = new SubprocessEntity
                                          {
                                              code = subprocess.code,
                                              name = subprocess.name,
                                          },
                                          supportType = new SupportTypeEntity
                                          {
                                              name = supportType.name,
                                          },
                                          usageClassification = new UsageClassificationEntity
                                          {
                                              name = usageClassification.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetActivesInventoriesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetActivesInventoriesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetActivesInventoriesByCompanyIdDto>>(entities);
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
            var total = await (from activesInventory in _databaseService.ActivesInventory
                               join activeType in _databaseService.ActiveType on activesInventory.activeType equals activeType
                               join custodian in _databaseService.Custodian on activesInventory.custodian equals custodian
                               join location in _databaseService.Location on activesInventory.location equals location
                               join macroprocess in _databaseService.Macroprocess on activesInventory.macroprocess equals macroprocess
                               join owner in _databaseService.Owner on activesInventory.owner equals owner
                               join subprocess in _databaseService.Subprocess on activesInventory.subprocess equals subprocess
                               join supportType in _databaseService.SupportType on activesInventory.supportType equals supportType
                               join usageClassification in _databaseService.UsageClassification on activesInventory.usageClassification equals usageClassification
                               where ((activesInventory.isDeleted == null || activesInventory.isDeleted == false) && activesInventory.companyId == companyId)
                               && (activesInventory.name.ToUpper().Contains(search.ToUpper()))
                               select new ActivesInventoryEntity
                               {
                                   activesInventoryId = activesInventory.activesInventoryId,
                               }).CountAsync();
            return total;
        }

    }
}

