using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Evaluation.Queries.GetAllEvaluationsByCompanyId
{
    internal class GetAllEvaluationsByCompanyIdQuery : IGetAllEvaluationsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllEvaluationsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from evaluation in _databaseService.Evaluation
                                      where ((evaluation.isDeleted == null || evaluation.isDeleted == false) 
                                      && evaluation.companyId == companyId && evaluation.isGapAnalysis)
                                      select new EvaluationEntity
                                      {
                                          evaluationId = evaluation.evaluationId,
                                          description = evaluation.description,
                                      }).ToListAsync();

                BaseResponseDto<GetAllEvaluationsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllEvaluationsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllEvaluationsByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

