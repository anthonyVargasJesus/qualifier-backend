using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Menu.Queries.GetAllMenusByCompanyId
{
    internal class GetAllMenusByCompanyIdQuery : IGetAllMenusByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllMenusByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from menu in _databaseService.Menu
                                      where ((menu.isDeleted == null || menu.isDeleted == false) && menu.companyId == companyId)
                                      select new MenuEntity
                                      {
                                          menuId = menu.menuId,
                                          name = menu.name,
                                          image = menu.image,
                                          companyId = menu.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllMenusByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllMenusByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllMenusByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

