using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Evaluation.Queries.GetEvaluationById
{
    public class GetEvaluationByIdQuery : IGetEvaluationByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetEvaluationByIdQuery(IDatabaseService databaseService, IMapper mapper)
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
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.evaluationId == evaluationId)
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
                                        standard = new StandardEntity
                                        {
                                            standardId = standard.standardId,
                                            name = standard.name,
                                        },
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetEvaluationByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

