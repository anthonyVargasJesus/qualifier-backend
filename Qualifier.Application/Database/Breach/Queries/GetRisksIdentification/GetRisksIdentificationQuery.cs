﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Breach.Queries.GetRisksIdentification
{
    internal class GetRisksIdentificationQuery : IGetRisksIdentificationQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRisksIdentificationQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var currentEvaluation = await (from item in _databaseService.Evaluation
                                               where ((item.isDeleted == null || item.isDeleted == false) && item.isCurrent)
                                               select new EvaluationEntity()
                                               {
                                                   evaluationId = item.evaluationId,
                                                   startDate = item.startDate,
                                                   endDate = item.endDate,
                                                   description = item.description,
                                               }).FirstOrDefaultAsync();

                int evaluationId = 0;
                if (currentEvaluation != null)
                    evaluationId = currentEvaluation.evaluationId;

                var breachs = await (from breach in _databaseService.Breach
                                     join breachSeverity in _databaseService.BreachSeverity on breach.breachSeverity equals breachSeverity
                                     join bt2 in _databaseService.Control on breach.controlId equals bt2.controlId into _bt2
                                     from control in _bt2.DefaultIfEmpty()
                                     join bt in _databaseService.Requirement on breach.requirementId equals bt.requirementId into _bt
                                     from requirement in _bt.DefaultIfEmpty()
                                     where ((breach.isDeleted == null || breach.isDeleted == false) && breach.evaluationId == evaluationId)
                                     && (breach.title.ToUpper().Contains(search.ToUpper()))
                                     select new BreachEntity
                                     {
                                         breachId = breach.breachId,
                                         type = breach.type,
                                         requirementId = breach.requirementId,
                                         controlId = breach.controlId,
                                         numerationToShow = breach.numerationToShow,
                                         title = breach.title,
                                         breachSeverityId = breach.breachSeverityId,
                                         breachStatusId = breach.breachStatusId,
                                         responsibleId = breach.responsibleId,
                                         breachSeverity = new BreachSeverityEntity
                                         {
                                             name = breachSeverity.name,
                                             color = breachSeverity.color,
                                         },
                                         control = (control != null) ? new ControlEntity
                                         {
                                             number = control.number,
                                             name = control.name,
                                         } : null,
                                         requirement = (requirement != null) ? new RequirementEntity
                                         {
                                             numeration = requirement.numeration,
                                             name = requirement.name,
                                         } : null,
                                     }).Skip(skip).Take(pageSize)
                                     .ToListAsync();

                var allDefaultRisks = await (from requirementInDefaultRisk in _databaseService.RequirementInDefaultRisk
                                          join defaultRisk in _databaseService.DefaultRisk on requirementInDefaultRisk.defaultRisk equals defaultRisk
                                          join menace in _databaseService.Menace on defaultRisk.menace equals menace
                                          join vulnerability in _databaseService.Vulnerability on defaultRisk.vulnerability equals vulnerability
                                          where ((requirementInDefaultRisk.isDeleted == null || requirementInDefaultRisk.isDeleted == false)
                                      && requirementInDefaultRisk.companyId == companyId && requirementInDefaultRisk.isActive == true)
                                          select new RequirementInDefaultRiskEntity
                                          {
                                              defaultRiskId = requirementInDefaultRisk.defaultRiskId,
                                              requirementId = requirementInDefaultRisk.requirementId,
                                              defaultRisk = new DefaultRiskEntity
                                              {
                                                  defaultRiskId = defaultRisk.defaultRiskId,
                                                  name = defaultRisk.name,
                                              },
                                          }).ToListAsync();

                foreach (BreachEntity item in breachs)
                {
                    if (item.requirement != null)
                    {
                        item.title = "Requisito " + item.numerationToShow + " – " + item.title;
                        var defaultRisks = new List<DefaultRiskEntity>();
                        foreach(var risk in allDefaultRisks.Where(e => e.requirementId == item.requirementId))
                        {
                            var defaultRisk = new DefaultRiskEntity
                            {
                                defaultRiskId = risk.defaultRiskId,
                                name = risk.defaultRisk.name,
                            };
                            defaultRisks.Add(defaultRisk);
                        }
                        item.defaultRisks = defaultRisks;
                    }
                      

                    if (item.control != null)
                        item.title = "Control " + item.numerationToShow + " – " + item.title;
                }

                var currentEvaluationDto = _mapper.Map<GetRisksIdentificationCurrentEvaluationDto>(currentEvaluation);
                var breachsDto = _mapper.Map<List<GetRisksIdentificationDto>>(breachs);

                var response = new GetRisksIdentificationResponseDto();
                response.currentEvaluation = currentEvaluationDto;
                response.breachs = breachsDto;
                response.pagination = Pagination.GetPagination(await getTotal(search, evaluationId), pageSize);

                return response;

            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int evaluationId)
        {
            var total = await (from breach in _databaseService.Breach
                               join breachSeverity in _databaseService.BreachSeverity on breach.breachSeverity equals breachSeverity
                               join breachStatus in _databaseService.BreachStatus on breach.breachStatus equals breachStatus
                               join bt2 in _databaseService.Control on breach.controlId equals bt2.controlId into _bt2
                               from control in _bt2.DefaultIfEmpty()
                               join bt in _databaseService.Requirement on breach.requirementId equals bt.requirementId into _bt
                               from requirement in _bt.DefaultIfEmpty()
                               join responsible in _databaseService.Responsible on breach.responsible equals responsible
                               where ((breach.isDeleted == null || breach.isDeleted == false) && breach.evaluationId == evaluationId)
                               && (breach.title.ToUpper().Contains(search.ToUpper()))
                               select new BreachEntity
                               {
                                   breachId = breach.breachId,
                               }).CountAsync();
            return total;
        }

    }
}
