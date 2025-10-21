using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlGroup.Queries.GetAllControlGroupsByCompanyId
{
    internal class GetAllControlGroupsByCompanyIdQuery : IGetAllControlGroupsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllControlGroupsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from controlGroup in _databaseService.ControlGroup
                                      where ((controlGroup.isDeleted == null || controlGroup.isDeleted == false) && controlGroup.companyId == companyId)
                                      select new ControlGroupEntity
                                      {
                                          controlGroupId = controlGroup.controlGroupId,
                                          name = controlGroup.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllControlGroupsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllControlGroupsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllControlGroupsByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

