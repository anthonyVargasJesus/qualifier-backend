using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActiveType.Queries.GetAllActiveTypesByCompanyId
{
    internal class GetAllActiveTypesByCompanyIdQuery : IGetAllActiveTypesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllActiveTypesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from activeType in _databaseService.ActiveType
                                      where ((activeType.isDeleted == null || activeType.isDeleted == false) && activeType.companyId == companyId)
                                      select new ActiveTypeEntity
                                      {
                                          activeTypeId = activeType.activeTypeId,
                                          name = activeType.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllActiveTypesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllActiveTypesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllActiveTypesByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

