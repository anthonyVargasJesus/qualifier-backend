using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Risk.Queries.GetRisksByCompanyId
{
    public class GetRisksByCompanyIdQuery : IGetRisksByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRisksByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from risk in _databaseService.Risk
                                      join control in _databaseService.Control on risk.control equals control
                                      join controlType in _databaseService.ControlType on risk.controlType equals controlType
                                      join menace in _databaseService.Menace on risk.menace equals menace
                                      join responsible in _databaseService.Responsible on risk.responsible equals responsible
                                      join riskLevel in _databaseService.RiskLevel on risk.riskLevel equals riskLevel
                                      join vulnerability in _databaseService.Vulnerability on risk.vulnerability equals vulnerability
                                      where ((risk.isDeleted == null || risk.isDeleted == false) && risk.companyId == companyId)
                                      && (risk.activesInventoryName.ToUpper().Contains(search.ToUpper()))
                                      select new RiskEntity
                                      {
                                          riskId = risk.riskId,
                                          activesInventoryId = risk.activesInventoryId,
                                          activesInventoryNumber = risk.activesInventoryNumber,
                                          macroProcess = risk.macroProcess,
                                          subProcess = risk.subProcess,
                                          activesInventoryName = risk.activesInventoryName,
                                          activesInventoryValuation = risk.activesInventoryValuation,
                                          menaceId = risk.menaceId,
                                          vulnerabilityId = risk.vulnerabilityId,
                                          menaceLevel = risk.menaceLevel,
                                          vulnerabilityLevel = risk.vulnerabilityLevel,
                                          controlId = risk.controlId,
                                          riskAssessmentValue = risk.riskAssessmentValue,
                                          riskLevelId = risk.riskLevelId,
                                          treatmentMethod = risk.treatmentMethod,
                                          controlTypeId = risk.controlTypeId,
                                          control = new ControlEntity
                                          {
                                              name = control.name,
                                          },
                                          controlType = new ControlTypeEntity
                                          {
                                              name = controlType.name,
                                          },
                                          menace = new MenaceEntity
                                          {
                                              name = menace.name,
                                          },
                                          responsible = new ResponsibleEntity
                                          {
                                              name = responsible.name,
                                          },
                                          riskLevel = new RiskLevelEntity
                                          {
                                              name = riskLevel.name,
                                          },
                                          vulnerability = new VulnerabilityEntity
                                          {
                                              name = vulnerability.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetRisksByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetRisksByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetRisksByCompanyIdDto>>(entities);
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
            var total = await (from risk in _databaseService.Risk
                               join control in _databaseService.Control on risk.control equals control
                               join controlType in _databaseService.ControlType on risk.controlType equals controlType
                               join menace in _databaseService.Menace on risk.menace equals menace
                               join responsible in _databaseService.Responsible on risk.responsible equals responsible
                               join riskLevel in _databaseService.RiskLevel on risk.riskLevel equals riskLevel
                               join vulnerability in _databaseService.Vulnerability on risk.vulnerability equals vulnerability
                               where ((risk.isDeleted == null || risk.isDeleted == false) && risk.companyId == companyId)
                               && (risk.activesInventoryName.ToUpper().Contains(search.ToUpper()))
                               select new RiskEntity
                               {
                                   riskId = risk.riskId,
                               }).CountAsync();
            return total;
        }

    }
}

