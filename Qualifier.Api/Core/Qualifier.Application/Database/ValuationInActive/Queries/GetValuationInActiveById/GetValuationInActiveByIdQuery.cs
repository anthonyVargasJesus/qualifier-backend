using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ValuationInActive.Queries.GetValuationInActiveById
{
    public class GetValuationInActiveByIdQuery : IGetValuationInActiveByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetValuationInActiveByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int valuationInActiveId)
        {
            try
            {
                var entity = await (from item in _databaseService.ValuationInActive
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.valuationInActiveId == valuationInActiveId)
                                    select new ValuationInActiveEntity()
                                    {
                                        valuationInActiveId = item.valuationInActiveId,
                                        activesInventoryId = item.activesInventoryId,
                                        impactValuationId = item.impactValuationId,
                                        value = item.value,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetValuationInActiveByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

