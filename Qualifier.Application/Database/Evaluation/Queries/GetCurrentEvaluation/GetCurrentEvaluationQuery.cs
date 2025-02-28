using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.Evaluation.Queries.GetEvaluationById;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation
{
    public class GetCurrentEvaluationQuery : IGetCurrentEvaluationQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetCurrentEvaluationQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int evaluationId)
        {
            try
            {
                var entity = await (from item in _databaseService.Evaluation
                                    join standard in _databaseService.Standard on item.standard equals standard
                                    join evaluationState in _databaseService.EvaluationState on item.evaluationState equals evaluationState
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.isCurrent)
                                    select new EvaluationEntity()
                                    {
                                        evaluationId = item.evaluationId,
                                        startDate = item.startDate,
                                        endDate = item.endDate,
                                        description = item.description,
                                        referenceEvaluationId = item.referenceEvaluationId,
                                        isGapAnalysis = item.isGapAnalysis,
                                        standardId = standard.standardId,
                                        isCurrent = item.isCurrent,
                                        evaluationState = new EvaluationStateEntity
                                        {
                                            name = evaluationState.name,
                                            color = evaluationState.color,
                                        },
                                        standard = new StandardEntity
                                        {
                                            name = standard.name,
                                        },
                                    }).FirstOrDefaultAsync();

                int standardId = 0;
                if (entity != null)
                    standardId = entity.standardId;

                var currentScope = await (from item in _databaseService.Scope
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.isCurrent && item.standardId == standardId)
                                    select new ScopeEntity()
                                    {
                                        scopeId = item.scopeId,
                                        name = item.name,
                                    }).FirstOrDefaultAsync();

                var currentPolicy = await (from item in _databaseService.Policy
                                          where ((item.isDeleted == null || item.isDeleted == false) && item.isCurrent && item.standardId == standardId)
                                          select new PolicyEntity()
                                          {
                                              policyId = item.policyId,
                                              name = item.name,
                                          }).FirstOrDefaultAsync();

                var entityDto = _mapper.Map<GetCurrentEvaluationDto>(entity);
                entityDto.currentScope = _mapper.Map<GetCurrentScopeDto>(currentScope);
                entityDto.currentPolicy = _mapper.Map<GetCurrentPolicyDto>(currentPolicy);

                return entityDto;

            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
