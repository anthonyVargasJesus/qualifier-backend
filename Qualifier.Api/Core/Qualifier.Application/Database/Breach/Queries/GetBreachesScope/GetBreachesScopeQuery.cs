using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.Breach.Queries.GetBreachesScope
{
    // Versión sin joins de GetBreachsByEvaluationIdQuery — los 6 campos que
    // devuelve ya están directo en la fila de MAE_BREACH, no hace falta unir
    // severity/status/control/requirement/responsible para tenerlos.
    public class GetBreachesScopeQuery : IGetBreachesScopeQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetBreachesScopeQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int evaluationId)
        {
            try
            {
                var breaches = await (from breach in _databaseService.Breach
                                       where (breach.isDeleted == null || breach.isDeleted == false)
                                             && breach.evaluationId == evaluationId
                                       select new GetBreachesScopeDto
                                       {
                                           breachId = breach.breachId,
                                           type = breach.type,
                                           requirementId = breach.requirementId,
                                           controlId = breach.controlId,
                                           numerationToShow = breach.numerationToShow,
                                           title = breach.title,
                                       }).ToListAsync();

                var response = new BaseResponseDto<GetBreachesScopeDto>();
                response.data = breaches;
                return response;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
