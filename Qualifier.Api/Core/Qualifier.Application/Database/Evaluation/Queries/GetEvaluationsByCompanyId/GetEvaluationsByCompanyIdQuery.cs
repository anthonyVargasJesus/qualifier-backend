using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Evaluation.Queries.GetEvaluationsByCompanyId
{
    public class GetEvaluationsByCompanyIdQuery : IGetEvaluationsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetEvaluationsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from evaluation in _databaseService.Evaluation
                                      join evaluationState in _databaseService.EvaluationState on evaluation.evaluationState equals evaluationState
                                      where ((evaluation.isDeleted == null || evaluation.isDeleted == false) && evaluation.companyId == companyId)
                                      && (evaluation.description.ToUpper().Contains(search.ToUpper()))
                                      select new EvaluationEntity
                                      {
                                          evaluationId = evaluation.evaluationId,
                                          startDate = evaluation.startDate,
                                          endDate = evaluation.endDate,
                                          description = evaluation.description,
                                          standardId = evaluation.standardId,
                                          isCurrent = evaluation.isCurrent,
                                          evaluationState = new EvaluationStateEntity
                                          {
                                              evaluationStateId = evaluationState.evaluationStateId,
                                              name = evaluationState.name,
                                              color = evaluationState.color,
                                          },
                                      })
                                      .OrderByDescending(x => x.startDate)
                                        .Skip(skip).Take(pageSize)
                                        .ToListAsync();

                BaseResponseDto<GetEvaluationsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetEvaluationsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetEvaluationsByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception ex)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from evaluation in _databaseService.Evaluation
                               join standard in _databaseService.Standard on evaluation.standard equals standard
                               where ((evaluation.isDeleted == null || evaluation.isDeleted == false) && evaluation.companyId == companyId)
                               && (evaluation.description.ToUpper().Contains(search.ToUpper()))
                               select new EvaluationEntity
                               {
                                   evaluationId = evaluation.evaluationId,
                               }).CountAsync();
            return total;
        }

    }
}

