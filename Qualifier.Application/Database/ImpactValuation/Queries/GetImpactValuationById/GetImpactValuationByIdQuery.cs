using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ImpactValuation.Queries.GetImpactValuationById
{
    public class GetImpactValuationByIdQuery : IGetImpactValuationByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetImpactValuationByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int impactValuationId)
        {
            try
            {
                var entity = await (from item in _databaseService.ImpactValuation
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.impactValuationId == impactValuationId)
                                    select new ImpactValuationEntity()
                                    {
                                        impactValuationId = item.impactValuationId,
                                        abbreviation = item.abbreviation,
                                        name = item.name,
                                        minimumValue = item.minimumValue,
                                        maximumValue = item.maximumValue,
                                        defaultValue = item.defaultValue,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetImpactValuationByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

