using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ConfidentialityLevel.Queries.GetAllConfidentialityLevelsByCompanyId
{
    internal class GetAllConfidentialityLevelsByCompanyIdQuery : IGetAllConfidentialityLevelsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllConfidentialityLevelsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from confidentialityLevel in _databaseService.ConfidentialityLevel
                                      where ((confidentialityLevel.isDeleted == null || confidentialityLevel.isDeleted == false) && confidentialityLevel.companyId == companyId)
                                      select new ConfidentialityLevelEntity
                                      {
                                          confidentialityLevelId = confidentialityLevel.confidentialityLevelId,
                                          name = confidentialityLevel.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllConfidentialityLevelsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllConfidentialityLevelsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllConfidentialityLevelsByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

