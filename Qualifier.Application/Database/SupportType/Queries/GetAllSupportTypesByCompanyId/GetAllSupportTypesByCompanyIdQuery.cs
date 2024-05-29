using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.SupportType.Queries.GetAllSupportTypesByCompanyId
{
    internal class GetAllSupportTypesByCompanyIdQuery : IGetAllSupportTypesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllSupportTypesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from supportType in _databaseService.SupportType
                                      where ((supportType.isDeleted == null || supportType.isDeleted == false) && supportType.companyId == companyId)
                                      select new SupportTypeEntity
                                      {
                                          supportTypeId = supportType.supportTypeId,
                                          name = supportType.name,
                                          companyId = supportType.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllSupportTypesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllSupportTypesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllSupportTypesByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

