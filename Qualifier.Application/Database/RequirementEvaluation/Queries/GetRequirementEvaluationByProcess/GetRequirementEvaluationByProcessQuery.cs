using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Domain.Entities;


namespace Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationByProcess
{
    public class GetRequirementEvaluationByProcessQuery : IGetRequirementEvaluationByProcessQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetRequirementEvaluationByProcessQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int standardId, int evaluationId)
        {
            try
            {
                var requirements = await (from requirement in _databaseService.Requirement
                                          where ((requirement.isDeleted == null || requirement.isDeleted == false) 
                                          && requirement.standardId == standardId
                                          && requirement.isEvaluable)
                                          select new RequirementEntity
                                          {
                                              requirementId = requirement.requirementId,
                                              numeration = requirement.numeration,
                                              name = requirement.name,
                                              description = requirement.description,
                                              level = requirement.level,
                                              parentId = requirement.parentId,
                                              letter = (requirement.letter == null) ? "" : requirement.letter,
                                          }).ToListAsync();

                var standardEntity = new StandardEntity();
                standardEntity.setRequirementsWithChildren(requirements);

                var evaluations = await (from requirementEvaluation in _databaseService.RequirementEvaluation
                                         join requirement in _databaseService.Requirement on requirementEvaluation.requirement equals requirement
                                         join maturityLevel in _databaseService.MaturityLevel on requirementEvaluation.maturityLevel equals maturityLevel
                                         join responsible in _databaseService.Responsible on requirementEvaluation.responsible equals responsible
                                         where ((requirementEvaluation.isDeleted == null || requirementEvaluation.isDeleted == false) && requirementEvaluation.evaluationId == evaluationId)
                                         select new RequirementEvaluationEntity
                                         {
                                             requirementEvaluationId = requirementEvaluation.requirementEvaluationId,
                                             maturityLevelId = requirementEvaluation.maturityLevelId,
                                             value = requirementEvaluation.value,
                                             requirementId = requirementEvaluation.requirementId,
                                             responsibleId = requirementEvaluation.responsibleId,
                                             justification = requirementEvaluation.justification == null ? "" : requirementEvaluation.justification,
                                             improvementActions = requirementEvaluation.improvementActions == null ? "" : requirementEvaluation.improvementActions,
                                             requirement = new RequirementEntity
                                             {
                                                 requirementId = requirement.requirementId,
                                                 name = requirement.name,
                                             },
                                             maturityLevel = new MaturityLevelEntity
                                             {
                                                 abbreviation = maturityLevel.abbreviation,
                                                 color = maturityLevel.color,
                                                 value = maturityLevel.value,
                                             },
                                             responsible = new ResponsibleEntity
                                             {
                                                 name = responsible.name,
                                             },
                                             auditorStatus = requirementEvaluation.auditorStatus,
                                         }).ToListAsync();

                var allDocumentation = await (from documentation in _databaseService.Documentation
                                              where ((documentation.isDeleted == null || documentation.isDeleted == false)
                                              && documentation.standardId == standardId)
                                              select new DocumentationEntity
                                              {
                                                  documentationId = documentation.documentationId,
                                                  name = documentation.name,
                                              }).ToListAsync();

                var referenceDocumentations = await (from referenceDocumentation in _databaseService.ReferenceDocumentation
                                                     where ((referenceDocumentation.isDeleted == null || referenceDocumentation.isDeleted == false)
                                                     && referenceDocumentation.evaluationId == evaluationId)
                                                     select new ReferenceDocumentationEntity
                                                     {
                                                         documentationId = referenceDocumentation.documentationId,
                                                         requirementEvaluationId = referenceDocumentation.requirementEvaluationId,
                                                         name = referenceDocumentation.name,
                                                     }).ToListAsync();

                foreach (var item in evaluations)
                {
                    item.referenceDocumentations = referenceDocumentations.Where(e => e.requirementEvaluationId == item.requirementEvaluationId).ToList();
                    setEvaluationState(item);
                    setAuditorStatus(item);
                }

                standardEntity.setEvaluationsToRequirements(evaluations);

                BaseResponseDto<GetRequirementEvaluationsByProcessRequirementDto> baseResponseDto = new BaseResponseDto<GetRequirementEvaluationsByProcessRequirementDto>();
                List<GetRequirementEvaluationsByProcessRequirementDto> data = _mapper.Map<List<GetRequirementEvaluationsByProcessRequirementDto>>(standardEntity.requirements);

                baseResponseDto.data = data;
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }


        private void setEvaluationState(RequirementEvaluationEntity item)
        {
            const decimal OPTIMIZED_VALUE = 5.00m;
            const decimal PREDICTABLE_VALUE = 4.00m;
            const decimal ESTABLISHED_VALUE = 3.00m;
            const decimal MANAGED_VALUE = 2.00m;
            const decimal INITIAL_VALUE = 1.00m;
            const decimal NOT_IMPLEMENTED_VALUE = 1.00m;
            const decimal NOT_APPLICABLE_VALUE = 0.00m;

            var state = "Pendiente";

            if (item.maturityLevel != null)
            {
                if (item.maturityLevel.value == MANAGED_VALUE || item.maturityLevel.value == ESTABLISHED_VALUE
           || item.maturityLevel.value == PREDICTABLE_VALUE || item.maturityLevel.value == OPTIMIZED_VALUE)
                {
                    state = "Cumplido";
                }
                else if (item.maturityLevel.value == NOT_IMPLEMENTED_VALUE || item.maturityLevel.value == INITIAL_VALUE
           || item.maturityLevel.value == NOT_APPLICABLE_VALUE)
                {
                    state = "No cumplido";

                }

                    item.percentage = (item.value / OPTIMIZED_VALUE) * 100;

            }

            item.state = state;

        }

        private void setAuditorStatus(RequirementEvaluationEntity item)
        {
            const int PENDING_AUDITOR_STATUS_VALUE = 1;
            const int ACCEPTED_AUDITOR_STATUS_VALUE = 2;
            const int REJECTED_AUDITOR_STATUS_VALUE = 3;

            if (item.auditorStatus == PENDING_AUDITOR_STATUS_VALUE)
                item.auditorStatusText = "En revisión";
            else if (item.auditorStatus == ACCEPTED_AUDITOR_STATUS_VALUE)
                item.auditorStatusText = "Validado";
            else if (item.auditorStatus == REJECTED_AUDITOR_STATUS_VALUE)
                item.auditorStatusText = "Rechazado";
            else
                item.auditorStatusText = "Estado desconocido";
        }


    }
}
