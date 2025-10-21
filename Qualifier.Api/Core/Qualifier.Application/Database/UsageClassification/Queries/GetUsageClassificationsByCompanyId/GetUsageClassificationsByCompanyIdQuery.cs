using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.UsageClassification.Queries.GetUsageClassificationsByCompanyId
{
    public class GetUsageClassificationsByCompanyIdQuery : IGetUsageClassificationsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetUsageClassificationsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from usageClassification in _databaseService.UsageClassification
                                      where ((usageClassification.isDeleted == null || usageClassification.isDeleted == false) && usageClassification.companyId == companyId)
                                      && (usageClassification.name.ToUpper().Contains(search.ToUpper()))
                                      select new UsageClassificationEntity
                                      {
                                          usageClassificationId = usageClassification.usageClassificationId,
                                          name = usageClassification.name,
                                          companyId = usageClassification.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetUsageClassificationsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetUsageClassificationsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetUsageClassificationsByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from usageClassification in _databaseService.UsageClassification
                               where ((usageClassification.isDeleted == null || usageClassification.isDeleted == false) && usageClassification.companyId == companyId)
                               && (usageClassification.name.ToUpper().Contains(search.ToUpper()))
                               select new UsageClassificationEntity
                               {
                                   usageClassificationId = usageClassification.usageClassificationId,
                               }).CountAsync();
            return total;
        }

    }
}

