using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.UsageClassification.Queries.GetAllUsageClassificationsByCompanyId
{
    internal class GetAllUsageClassificationsByCompanyIdQuery : IGetAllUsageClassificationsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllUsageClassificationsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from usageClassification in _databaseService.UsageClassification
                                      where ((usageClassification.isDeleted == null || usageClassification.isDeleted == false) && usageClassification.companyId == companyId)
                                      select new UsageClassificationEntity
                                      {
                                          usageClassificationId = usageClassification.usageClassificationId,
                                          name = usageClassification.name,
                                          companyId = usageClassification.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllUsageClassificationsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllUsageClassificationsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllUsageClassificationsByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

