using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlInDefaultRisk.Queries.GetControlInDefaultRiskById
{
    public class GetControlInDefaultRiskByIdQuery : IGetControlInDefaultRiskByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetControlInDefaultRiskByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int controlInDefaultRiskId)
        {
            try
            {
                var entity = await (from item in _databaseService.ControlInDefaultRisk
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.controlInDefaultRiskId == controlInDefaultRiskId)
                                    select new ControlInDefaultRiskEntity()
                                    {
                                        controlInDefaultRiskId = item.controlInDefaultRiskId,
                                        defaultRiskId = item.defaultRiskId,
                                        controlId = item.controlId,
                                        isActive = item.isActive,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetControlInDefaultRiskByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

