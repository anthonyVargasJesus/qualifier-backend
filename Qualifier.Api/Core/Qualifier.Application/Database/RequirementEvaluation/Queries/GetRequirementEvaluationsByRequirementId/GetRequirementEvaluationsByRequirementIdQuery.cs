using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationsByRequirementId
{
    public class GetRequirementEvaluationsByRequirementIdQuery : IGetRequirementEvaluationsByRequirementIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRequirementEvaluationsByRequirementIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, int requirementId)
        {
            try
            {





                var entities = await (from requirementEvaluation in _databaseService.RequirementEvaluation
                                      //join requirement in _databaseService.Requirement on requirementEvaluation.requirement equals requirement
                                      join maturityLevel in _databaseService.MaturityLevel on requirementEvaluation.maturityLevel equals maturityLevel
                                      join responsible in _databaseService.Responsible on requirementEvaluation.responsible equals responsible
                                      where ((requirementEvaluation.isDeleted == null || requirementEvaluation.isDeleted == false) && requirementEvaluation.requirementId == requirementId)
                                      //&& (requirementEvaluation.requirement.name.ToUpper().Contains(search.ToUpper()))
                                      select new RequirementEvaluationEntity
                                      {
                                          requirementEvaluationId = requirementEvaluation.requirementEvaluationId,
                                          maturityLevelId = requirementEvaluation.maturityLevelId,
                                          value = requirementEvaluation.value,
                                          responsibleId = requirementEvaluation.responsibleId,
                                          justification = requirementEvaluation.justification,
                                          improvementActions = requirementEvaluation.improvementActions,
                                           maturityLevel = new MaturityLevelEntity
                                          {
                                              abbreviation = maturityLevel.abbreviation,
                                              color = maturityLevel.color,
                                          },
                                          responsible = new ResponsibleEntity
                                          {
                                              name = responsible.name,
                                          },

                                      }).Skip(skip).Take(pageSize)
                                        .ToListAsync();


                BaseResponseDto<GetRequirementEvaluationsByRequirementIdDto> baseResponseDto = new BaseResponseDto<GetRequirementEvaluationsByRequirementIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetRequirementEvaluationsByRequirementIdDto>>(entities);

                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(requirementId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(int requirementId)
        {
            var total = await (from requirementEvaluation in _databaseService.RequirementEvaluation
                               //join requirement in _databaseService.Requirement on requirementEvaluation.requirement equals requirement
                               join maturityLevel in _databaseService.MaturityLevel on requirementEvaluation.maturityLevel equals maturityLevel
                               join responsible in _databaseService.Responsible on requirementEvaluation.responsible equals responsible
                               where ((requirementEvaluation.isDeleted == null || requirementEvaluation.isDeleted == false) && requirementEvaluation.requirementId == requirementId)
                               select new RequirementEvaluationEntity
                               {
                                   requirementEvaluationId = requirementEvaluation.requirementEvaluationId,
                               }).CountAsync();
            return total;
        }

    }
}

