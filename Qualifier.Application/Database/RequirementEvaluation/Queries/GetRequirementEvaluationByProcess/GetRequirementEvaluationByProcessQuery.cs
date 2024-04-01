using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.Requirement.Queries.GetRequirementsByStandardId;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
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
                                          where ((requirement.isDeleted == null || requirement.isDeleted == false) && requirement.standardId == standardId)
                                          select new RequirementEntity
                                          {
                                              requirementId = requirement.requirementId,
                                              numeration = requirement.numeration,
                                              name = requirement.name,
                                              description = requirement.description,
                                              level = requirement.level,
                                              parentId = requirement.parentId,
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
                                             justification = requirementEvaluation.justification,
                                             improvementActions = requirementEvaluation.improvementActions,
                                             requirement = new RequirementEntity
                                             {
                                                 requirementId = requirement.requirementId,
                                                 name = requirement.name,
                                             },
                                             maturityLevel = new MaturityLevelEntity
                                             {
                                                 abbreviation = maturityLevel.abbreviation,
                                                 color = maturityLevel.color,
                                             },
                                             responsible = new ResponsibleEntity
                                             {
                                                 name = responsible.name,
                                             },

                                         }).ToListAsync();

                standardEntity.setEvaluationsToRequirements(evaluations);

                BaseResponseDto<GetRequirementEvaluationsByProcessRequirementDto> baseResponseDto = new BaseResponseDto<GetRequirementEvaluationsByProcessRequirementDto>();
                List<GetRequirementEvaluationsByProcessRequirementDto> data = _mapper.Map<List<GetRequirementEvaluationsByProcessRequirementDto>>(standardEntity.requirements);

                baseResponseDto.data = data;
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}
