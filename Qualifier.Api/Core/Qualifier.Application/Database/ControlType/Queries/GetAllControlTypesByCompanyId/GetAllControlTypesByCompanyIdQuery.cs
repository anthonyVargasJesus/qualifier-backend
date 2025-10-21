using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlType.Queries.GetAllControlTypesByCompanyId
{
    internal class GetAllControlTypesByCompanyIdQuery : IGetAllControlTypesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllControlTypesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from controlType in _databaseService.ControlType
                                      where ((controlType.isDeleted == null || controlType.isDeleted == false) && controlType.companyId == companyId)
                                      select new ControlTypeEntity
                                      {
                                          controlTypeId = controlType.controlTypeId,
                                          name = controlType.name,
                                          companyId = controlType.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllControlTypesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllControlTypesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllControlTypesByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

