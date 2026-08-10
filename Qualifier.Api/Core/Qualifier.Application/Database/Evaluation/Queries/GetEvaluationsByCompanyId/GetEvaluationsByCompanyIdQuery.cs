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
        // standardId: opcional — sin él, lista evaluaciones de TODAS las normas de la
        // compañía mezcladas (comportamiento histórico); con él, solo las de esa norma.
        // Necesario desde que hay más de una norma cargada (antes de NTP-ISO/IEC 42001,
        // como en la práctica solo existía ISO 27001, nunca hacía falta distinguir).
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId, int? standardId = null)
        {
            try
            {
                var entities = await (from evaluation in _databaseService.Evaluation
                                      join evaluationState in _databaseService.EvaluationState on evaluation.evaluationState equals evaluationState
                                      join standard in _databaseService.Standard on evaluation.standard equals standard
                                      where ((evaluation.isDeleted == null || evaluation.isDeleted == false) && evaluation.companyId == companyId)
                                      && (evaluation.description.ToUpper().Contains(search.ToUpper()))
                                      && (standardId == null || evaluation.standardId == standardId)
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
                                          standard = new StandardEntity
                                          {
                                              standardId = standard.standardId,
                                              name = standard.name,
                                          },
                                      })
                                      .OrderByDescending(x => x.startDate)
                                        .Skip(skip).Take(pageSize)
                                        .ToListAsync();

                BaseResponseDto<GetEvaluationsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetEvaluationsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetEvaluationsByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId, standardId), pageSize);
                return baseResponseDto;
            }
            catch (Exception ex)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId, int? standardId = null)
        {
            var total = await (from evaluation in _databaseService.Evaluation
                               join standard in _databaseService.Standard on evaluation.standard equals standard
                               where ((evaluation.isDeleted == null || evaluation.isDeleted == false) && evaluation.companyId == companyId)
                               && (evaluation.description.ToUpper().Contains(search.ToUpper()))
                               && (standardId == null || evaluation.standardId == standardId)
                               select new EvaluationEntity
                               {
                                   evaluationId = evaluation.evaluationId,
                               }).CountAsync();
            return total;
        }

    }
}

