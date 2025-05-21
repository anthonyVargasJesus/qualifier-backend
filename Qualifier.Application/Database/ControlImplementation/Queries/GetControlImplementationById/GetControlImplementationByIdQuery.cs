using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlImplementation.Queries.GetControlImplementationById
{
    public class GetControlImplementationByIdQuery : IGetControlImplementationByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetControlImplementationByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int controlImplementationId)
        {
            try
            {
                var entity = await (from item in _databaseService.ControlImplementation
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.controlImplementationId == controlImplementationId)
                                    select new ControlImplementationEntity()
                                    {
                                        controlImplementationId = item.controlImplementationId,
                                        riskId = item.riskId,
                                        activities = item.activities,
                                        startDate = item.startDate,
                                        verificationDate = item.verificationDate,
                                        responsibleId = item.responsibleId,
                                        observation = item.observation,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetControlImplementationByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

