using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActivesInventory.Queries.GetAllActivesInventoriesByCompanyId
{
    internal class GetAllActivesInventoriesByCompanyIdQuery : IGetAllActivesInventoriesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllActivesInventoriesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from activesInventory in _databaseService.ActivesInventory
                                      where ((activesInventory.isDeleted == null || activesInventory.isDeleted == false) && activesInventory.companyId == companyId)
                                      select new ActivesInventoryEntity
                                      {
                                          activesInventoryId = activesInventory.activesInventoryId,
                                          number = activesInventory.number,
                                          name = activesInventory.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllActivesInventoriesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllActivesInventoriesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllActivesInventoriesByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }

        }

    }
}

