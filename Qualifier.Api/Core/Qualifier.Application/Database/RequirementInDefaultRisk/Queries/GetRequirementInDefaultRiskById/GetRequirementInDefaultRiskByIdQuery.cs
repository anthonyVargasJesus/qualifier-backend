using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RequirementInDefaultRisk.Queries.GetRequirementInDefaultRiskById
{
    public class GetRequirementInDefaultRiskByIdQuery : IGetRequirementInDefaultRiskByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRequirementInDefaultRiskByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int requirementInDefaultRiskId)
        {
            try
            {
                var entity = await (from item in _databaseService.RequirementInDefaultRisk
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.requirementInDefaultRiskId == requirementInDefaultRiskId)
                                    select new RequirementInDefaultRiskEntity()
                                    {
                                        requirementInDefaultRiskId = item.requirementInDefaultRiskId,
                                        defaultRiskId = item.defaultRiskId,
                                        requirementId = item.requirementId,
                                        isActive = item.isActive,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetRequirementInDefaultRiskByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

