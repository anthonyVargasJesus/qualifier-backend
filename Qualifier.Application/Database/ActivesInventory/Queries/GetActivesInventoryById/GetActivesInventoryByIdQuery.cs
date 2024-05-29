using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActivesInventory.Queries.GetActivesInventoryById
{
    public class GetActivesInventoryByIdQuery : IGetActivesInventoryByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetActivesInventoryByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int activesInventoryId)
        {
            try
            {
                var entity = await (from item in _databaseService.ActivesInventory
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.activesInventoryId == activesInventoryId)
                                    select new ActivesInventoryEntity()
                                    {
                                        activesInventoryId = item.activesInventoryId,
                                        number = item.number,
                                        macroprocessId = item.macroprocessId,
                                        subprocessId = item.subprocessId,
                                        procedure = item.procedure,
                                        activeTypeId = item.activeTypeId,
                                        name = item.name,
                                        description = item.description,
                                        ownerId = item.ownerId,
                                        custodianId = item.custodianId,
                                        usageClassificationId = item.usageClassificationId,
                                        supportTypeId = item.supportTypeId,
                                        locationId = item.locationId,
                                        valuation = item.valuation,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetActivesInventoryByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

